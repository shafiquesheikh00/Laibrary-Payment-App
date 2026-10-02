using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Laibrary_Payment_App.Data;
using Laibrary_Payment_App.Models;

namespace Laibrary_Payment_App.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly LibraryDbContext _context;
        private static readonly SemaphoreSlim _paymentLock = new SemaphoreSlim(1, 1);

        public PaymentService(LibraryDbContext context)
        {
            _context = context;
        }

        public async Task<string> GeneratePaymentNumberAsync(DateTime paymentDate, CancellationToken cancellationToken = default)
        {
            var year = paymentDate.Year;
            var prefix = $"PAY-{year:D4}-";

            // Find maximum existing sequence for this prefix
            var existingPayments = await _context.Payments
                .AsNoTracking()
                .Where(p => p.PaymentNo.StartsWith(prefix))
                .Select(p => p.PaymentNo)
                .ToListAsync(cancellationToken);

            int maxSeq = 0;
            foreach (var payNo in existingPayments)
            {
                var parts = payNo.Split('-');
                if (parts.Length == 3 && int.TryParse(parts[2], out int seq))
                {
                    if (seq > maxSeq)
                    {
                        maxSeq = seq;
                    }
                }
            }

            int nextSeq = maxSeq + 1;
            return $"{prefix}{nextSeq:D6}";
        }

        public async Task<List<OutstandingInvoiceDto>> GetOutstandingInvoicesAsync(int? studentId = null, CancellationToken cancellationToken = default)
        {
            var query = _context.Invoices
                .Include(i => i.Student)
                .Include(i => i.Membership)
                    .ThenInclude(m => m!.Plan)
                .Include(i => i.Membership)
                    .ThenInclude(m => m!.Seat)
                .AsNoTracking()
                .Where(i => i.Status != "Cancelled" && i.BalanceAmount > 0);

            if (studentId.HasValue && studentId.Value > 0)
            {
                query = query.Where(i => i.StudentId == studentId.Value);
            }

            var invoices = await query
                .OrderBy(i => i.InvoiceDate)
                .ToListAsync(cancellationToken);

            return invoices.Select(i => new OutstandingInvoiceDto
            {
                InvoiceId = i.InvoiceId,
                InvoiceNo = i.InvoiceNo,
                InvoiceDate = i.InvoiceDate,
                DueDate = i.DueDate,
                StudentId = i.StudentId,
                StudentCode = i.Student.StudentCode,
                StudentName = i.Student.FullName,
                MobileNo = i.Student.MobileNo,
                MembershipId = i.MembershipId,
                PlanName = i.Membership?.Plan?.PlanName ?? "General / Non-membership",
                SeatNumber = i.Membership?.Seat?.SeatNumber,
                GrandTotal = i.GrandTotal,
                PaidAmount = i.PaidAmount,
                BalanceAmount = i.BalanceAmount,
                Status = i.Status
            }).ToList();
        }

        public async Task<InvoiceBalanceDto> GetInvoiceBalanceAsync(int invoiceId, CancellationToken cancellationToken = default)
        {
            var invoice = await _context.Invoices
                .AsNoTracking()
                .FirstOrDefaultAsync(i => i.InvoiceId == invoiceId, cancellationToken);

            if (invoice == null)
            {
                return new InvoiceBalanceDto();
            }

            // Query active payments from SQL Server as true source of truth
            var activePaid = await _context.Payments
                .AsNoTracking()
                .Where(p => p.InvoiceId == invoiceId && p.Status == "Active")
                .SumAsync(p => (decimal?)p.Amount, cancellationToken) ?? 0.00m;

            var balance = Math.Max(0.00m, invoice.GrandTotal - activePaid);

            return new InvoiceBalanceDto
            {
                InvoiceId = invoice.InvoiceId,
                InvoiceNo = invoice.InvoiceNo,
                GrandTotal = invoice.GrandTotal,
                TotalPaid = activePaid,
                OutstandingBalance = balance,
                Status = invoice.Status
            };
        }

        public async Task<(bool Success, string Message, Payment? Payment)> CreatePaymentAsync(
            CreatePaymentRequestDto request,
            CancellationToken cancellationToken = default)
        {
            if (request == null)
            {
                return (false, "Invalid payment request.", null);
            }

            if (request.InvoiceId <= 0)
            {
                return (false, "Please select an invoice.", null);
            }

            if (request.Amount <= 0)
            {
                return (false, "Payment amount must be greater than zero.", null);
            }

            if (!PaymentModes.IsValid(request.PaymentMode))
            {
                return (false, $"Invalid payment mode. Supported modes are: {string.Join(", ", PaymentModes.All)}.", null);
            }

            // Concurrency lock to prevent simultaneous race conditions
            await _paymentLock.WaitAsync(cancellationToken);
            try
            {
                using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);

                // 1. Load invoice with tracking
                var invoice = await _context.Invoices
                    .Include(i => i.Student)
                    .Include(i => i.Membership)
                        .ThenInclude(m => m!.Plan)
                    .Include(i => i.Membership)
                        .ThenInclude(m => m!.Seat)
                    .FirstOrDefaultAsync(i => i.InvoiceId == request.InvoiceId, cancellationToken);

                if (invoice == null)
                {
                    return (false, "Invoice was not found.", null);
                }

                if (string.Equals(invoice.Status, "Cancelled", StringComparison.OrdinalIgnoreCase))
                {
                    return (false, "Cannot receive payment against a Cancelled invoice.", null);
                }

                // 2. Validate student matches
                if (request.StudentId > 0 && request.StudentId != invoice.StudentId)
                {
                    return (false, "Selected student does not match the invoice student.", null);
                }

                // 3. Query current valid active payments as source of truth
                var existingPaid = await _context.Payments
                    .Where(p => p.InvoiceId == invoice.InvoiceId && p.Status == "Active")
                    .SumAsync(p => (decimal?)p.Amount, cancellationToken) ?? 0.00m;

                var currentBalance = Math.Round(invoice.GrandTotal - existingPaid, 2, MidpointRounding.AwayFromZero);

                // 4. Overpayment check
                if (request.Amount > currentBalance)
                {
                    return (false, "Payment amount cannot exceed the outstanding balance.", null);
                }

                // 5. Generate unique Payment Number
                var paymentDate = request.PaymentDate.Date == DateTime.MinValue ? DateTime.Today : request.PaymentDate;
                var paymentNo = await GeneratePaymentNumberAsync(paymentDate, cancellationToken);

                // 6. Create Payment record
                var payment = new Payment
                {
                    PaymentNo = paymentNo,
                    InvoiceId = invoice.InvoiceId,
                    StudentId = invoice.StudentId,
                    PaymentDate = paymentDate,
                    Amount = Math.Round(request.Amount, 2, MidpointRounding.AwayFromZero),
                    PaymentMode = PaymentModes.Normalize(request.PaymentMode),
                    ReferenceNo = string.IsNullOrWhiteSpace(request.ReferenceNo) ? null : request.ReferenceNo.Trim(),
                    Remarks = string.IsNullOrWhiteSpace(request.Remarks) ? null : request.Remarks.Trim(),
                    ReceivedBy = string.IsNullOrWhiteSpace(request.ReceivedBy) ? "System" : request.ReceivedBy.Trim(),
                    Status = "Active",
                    CreatedDate = DateTime.UtcNow
                };

                _context.Payments.Add(payment);

                // 7. Recalculate invoice paid amount, balance, and status
                var newTotalPaid = Math.Round(existingPaid + payment.Amount, 2, MidpointRounding.AwayFromZero);
                var newBalance = Math.Round(invoice.GrandTotal - newTotalPaid, 2, MidpointRounding.AwayFromZero);

                invoice.PaidAmount = newTotalPaid;
                invoice.BalanceAmount = newBalance;

                if (newBalance <= 0)
                {
                    invoice.Status = "Paid";
                }
                else if (newTotalPaid > 0)
                {
                    invoice.Status = "PartiallyPaid";
                }
                else
                {
                    invoice.Status = "Unpaid";
                }

                invoice.ModifiedBy = payment.ReceivedBy;
                invoice.ModifiedDate = DateTime.UtcNow;

                await _context.SaveChangesAsync(cancellationToken);
                await transaction.CommitAsync(cancellationToken);

                // Attach navigation objects for immediate receipt generation
                payment.Invoice = invoice;
                payment.Student = invoice.Student;

                return (true, $"Payment {payment.PaymentNo} of ₹{payment.Amount:N2} recorded successfully.", payment);
            }
            catch (Exception ex)
            {
                return (false, $"Failed to record payment: {ex.Message}", null);
            }
            finally
            {
                _paymentLock.Release();
            }
        }

        public async Task<Payment?> GetPaymentByIdAsync(int paymentId, CancellationToken cancellationToken = default)
        {
            return await _context.Payments
                .Include(p => p.Invoice)
                    .ThenInclude(i => i.InvoiceDetails)
                .Include(p => p.Invoice)
                    .ThenInclude(i => i.Membership)
                        .ThenInclude(m => m!.Plan)
                .Include(p => p.Invoice)
                    .ThenInclude(i => i.Membership)
                        .ThenInclude(m => m!.Seat)
                .Include(p => p.Student)
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.PaymentId == paymentId, cancellationToken);
        }

        public async Task<List<PaymentListDto>> GetPaymentHistoryAsync(PaymentFilterDto filter, CancellationToken cancellationToken = default)
        {
            var query = _context.Payments
                .Include(p => p.Invoice)
                .Include(p => p.Student)
                .AsNoTracking()
                .AsQueryable();

            if (filter != null)
            {
                if (!string.IsNullOrWhiteSpace(filter.SearchText))
                {
                    var search = filter.SearchText.Trim();
                    query = query.Where(p =>
                        p.PaymentNo.Contains(search) ||
                        p.Invoice.InvoiceNo.Contains(search) ||
                        p.Student.FullName.Contains(search) ||
                        p.Student.StudentCode.Contains(search) ||
                        (p.ReferenceNo != null && p.ReferenceNo.Contains(search)));
                }

                if (filter.FromDate.HasValue)
                {
                    var from = filter.FromDate.Value.Date;
                    query = query.Where(p => p.PaymentDate.Date >= from);
                }

                if (filter.ToDate.HasValue)
                {
                    var to = filter.ToDate.Value.Date;
                    query = query.Where(p => p.PaymentDate.Date <= to);
                }

                if (filter.StudentId.HasValue && filter.StudentId.Value > 0)
                {
                    query = query.Where(p => p.StudentId == filter.StudentId.Value);
                }

                if (filter.InvoiceId.HasValue && filter.InvoiceId.Value > 0)
                {
                    query = query.Where(p => p.InvoiceId == filter.InvoiceId.Value);
                }

                if (!string.IsNullOrWhiteSpace(filter.PaymentMode) && filter.PaymentMode != "All")
                {
                    query = query.Where(p => p.PaymentMode == filter.PaymentMode);
                }

                if (!string.IsNullOrWhiteSpace(filter.Status) && filter.Status != "All")
                {
                    query = query.Where(p => p.Status == filter.Status);
                }
            }

            var payments = await query
                .OrderByDescending(p => p.PaymentDate)
                .ThenByDescending(p => p.PaymentId)
                .ToListAsync(cancellationToken);

            return payments.Select(p => new PaymentListDto
            {
                PaymentId = p.PaymentId,
                PaymentNo = p.PaymentNo,
                PaymentDate = p.PaymentDate,
                InvoiceId = p.InvoiceId,
                InvoiceNo = p.Invoice.InvoiceNo,
                StudentId = p.StudentId,
                StudentCode = p.Student.StudentCode,
                StudentName = p.Student.FullName,
                MobileNo = p.Student.MobileNo,
                Amount = p.Amount,
                PaymentMode = p.PaymentMode,
                ReferenceNo = p.ReferenceNo,
                Remarks = p.Remarks,
                ReceivedBy = p.ReceivedBy,
                Status = p.Status,
                InvoiceGrandTotal = p.Invoice.GrandTotal,
                InvoiceBalance = p.Invoice.BalanceAmount
            }).ToList();
        }

        public async Task<List<StudentPaymentLedgerItemDto>> GetStudentPaymentHistoryAsync(int studentId, CancellationToken cancellationToken = default)
        {
            var payments = await _context.Payments
                .Include(p => p.Invoice)
                .AsNoTracking()
                .Where(p => p.StudentId == studentId)
                .OrderBy(p => p.PaymentDate)
                .ThenBy(p => p.PaymentId)
                .ToListAsync(cancellationToken);

            // Group payments by invoice to calculate accurate running balance per invoice
            var ledger = new List<StudentPaymentLedgerItemDto>();

            var invoiceGroups = payments.GroupBy(p => p.InvoiceId);
            foreach (var grp in invoiceGroups)
            {
                var invoice = grp.First().Invoice;
                decimal runningBalance = invoice.GrandTotal;

                foreach (var pay in grp)
                {
                    if (string.Equals(pay.Status, "Active", StringComparison.OrdinalIgnoreCase))
                    {
                        runningBalance = Math.Max(0.00m, runningBalance - pay.Amount);
                    }

                    ledger.Add(new StudentPaymentLedgerItemDto
                    {
                        PaymentId = pay.PaymentId,
                        PaymentNo = pay.PaymentNo,
                        Date = pay.PaymentDate,
                        InvoiceNo = invoice.InvoiceNo,
                        InvoiceTotal = invoice.GrandTotal,
                        PaymentAmount = pay.Amount,
                        RunningBalance = runningBalance,
                        PaymentMode = pay.PaymentMode,
                        Status = pay.Status,
                        ReferenceNo = pay.ReferenceNo
                    });
                }
            }

            return ledger.OrderByDescending(l => l.Date).ThenByDescending(l => l.PaymentId).ToList();
        }

        public async Task<List<PaymentListDto>> GetInvoicePaymentsAsync(int invoiceId, CancellationToken cancellationToken = default)
        {
            var payments = await _context.Payments
                .Include(p => p.Invoice)
                .Include(p => p.Student)
                .AsNoTracking()
                .Where(p => p.InvoiceId == invoiceId)
                .OrderBy(p => p.PaymentDate)
                .ThenBy(p => p.PaymentId)
                .ToListAsync(cancellationToken);

            return payments.Select(p => new PaymentListDto
            {
                PaymentId = p.PaymentId,
                PaymentNo = p.PaymentNo,
                PaymentDate = p.PaymentDate,
                InvoiceId = p.InvoiceId,
                InvoiceNo = p.Invoice.InvoiceNo,
                StudentId = p.StudentId,
                StudentCode = p.Student.StudentCode,
                StudentName = p.Student.FullName,
                MobileNo = p.Student.MobileNo,
                Amount = p.Amount,
                PaymentMode = p.PaymentMode,
                ReferenceNo = p.ReferenceNo,
                Remarks = p.Remarks,
                ReceivedBy = p.ReceivedBy,
                Status = p.Status,
                InvoiceGrandTotal = p.Invoice.GrandTotal,
                InvoiceBalance = p.Invoice.BalanceAmount
            }).ToList();
        }

        public async Task<(bool Success, string Message)> CancelPaymentAsync(
            int paymentId,
            string cancelledBy,
            string? reason = null,
            CancellationToken cancellationToken = default)
        {
            if (paymentId <= 0)
            {
                return (false, "Invalid payment ID.");
            }

            await _paymentLock.WaitAsync(cancellationToken);
            try
            {
                using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);

                var payment = await _context.Payments
                    .Include(p => p.Invoice)
                    .FirstOrDefaultAsync(p => p.PaymentId == paymentId, cancellationToken);

                if (payment == null)
                {
                    return (false, "Payment record was not found.");
                }

                if (string.Equals(payment.Status, "Cancelled", StringComparison.OrdinalIgnoreCase))
                {
                    return (false, "This payment is already cancelled.");
                }

                // 1. Soft-cancel payment record
                payment.Status = "Cancelled";
                payment.CancelledBy = string.IsNullOrWhiteSpace(cancelledBy) ? "System" : cancelledBy.Trim();
                payment.CancelledDate = DateTime.UtcNow;
                if (!string.IsNullOrWhiteSpace(reason))
                {
                    payment.Remarks = string.IsNullOrWhiteSpace(payment.Remarks)
                        ? $"[Cancelled: {reason.Trim()}]"
                        : $"{payment.Remarks} | [Cancelled: {reason.Trim()}]";
                }

                payment.ModifiedDate = DateTime.UtcNow;

                // 2. Recalculate invoice based only on remaining Active payments
                var invoice = payment.Invoice;
                var remainingActivePaid = await _context.Payments
                    .Where(p => p.InvoiceId == invoice.InvoiceId && p.Status == "Active" && p.PaymentId != payment.PaymentId)
                    .SumAsync(p => (decimal?)p.Amount, cancellationToken) ?? 0.00m;

                var newBalance = Math.Round(invoice.GrandTotal - remainingActivePaid, 2, MidpointRounding.AwayFromZero);

                invoice.PaidAmount = remainingActivePaid;
                invoice.BalanceAmount = newBalance;

                if (newBalance <= 0)
                {
                    invoice.Status = "Paid";
                }
                else if (remainingActivePaid > 0)
                {
                    invoice.Status = "PartiallyPaid";
                }
                else
                {
                    invoice.Status = "Unpaid";
                }

                invoice.ModifiedBy = payment.CancelledBy;
                invoice.ModifiedDate = DateTime.UtcNow;

                await _context.SaveChangesAsync(cancellationToken);
                await transaction.CommitAsync(cancellationToken);

                return (true, $"Payment {payment.PaymentNo} has been cancelled successfully. Invoice balance restored to ₹{newBalance:N2}.");
            }
            catch (Exception ex)
            {
                return (false, $"Failed to cancel payment: {ex.Message}");
            }
            finally
            {
                _paymentLock.Release();
            }
        }
    }
}
