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
    public class CreateInvoiceItemDto
    {
        public string Description { get; set; } = string.Empty;
        public int Quantity { get; set; } = 1;
        public decimal Rate { get; set; }
        public decimal Amount => Math.Round(Quantity * Rate, 2, MidpointRounding.AwayFromZero);
    }

    public class CreateInvoiceRequestDto
    {
        public int StudentId { get; set; }
        public int? MembershipId { get; set; }
        public DateTime InvoiceDate { get; set; } = DateTime.Today;
        public DateTime DueDate { get; set; } = DateTime.Today.AddDays(7);
        public decimal DiscountAmount { get; set; }
        public List<CreateInvoiceItemDto> Items { get; set; } = new List<CreateInvoiceItemDto>();
        public string CreatedBy { get; set; } = "System";
    }

    public class InvoiceCalculationResultDto
    {
        public decimal SubTotal { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal AdditionalCharges { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal CurrentCharges { get; set; }
        public decimal PreviousOutstanding { get; set; }
        public decimal GrandTotal { get; set; }
        public decimal PaidAmount { get; set; }
        public decimal BalanceAmount { get; set; }
    }

    public class InvoiceListDto
    {
        public int InvoiceId { get; set; }
        public string InvoiceNo { get; set; } = string.Empty;
        public DateTime InvoiceDate { get; set; }
        public DateTime DueDate { get; set; }
        public int StudentId { get; set; }
        public string StudentCode { get; set; } = string.Empty;
        public string StudentName { get; set; } = string.Empty;
        public string MobileNo { get; set; } = string.Empty;
        public int? MembershipId { get; set; }
        public string MembershipPlanName { get; set; } = "None / General";
        public string? SeatNumber { get; set; }
        public decimal SubTotal { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal AdditionalCharges { get; set; }
        public decimal CurrentCharges { get; set; }
        public decimal PreviousOutstanding { get; set; }
        public decimal GrandTotal { get; set; }
        public decimal PaidAmount { get; set; }
        public decimal BalanceAmount { get; set; }
        public string Status { get; set; } = string.Empty;
        public string CreatedBy { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; }
    }

    public class InvoiceFilterDto
    {
        public string? SearchText { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public string? Status { get; set; }
        public int? StudentId { get; set; }
    }

    public interface IBillingService
    {
        Task<string> GenerateInvoiceNumberAsync(DateTime invoiceDate, CancellationToken cancellationToken = default);
        Task<decimal> GetPreviousOutstandingAsync(int studentId, CancellationToken cancellationToken = default);
        Task<StudentMembership?> GetActiveMembershipForStudentAsync(int studentId, CancellationToken cancellationToken = default);
        Task<InvoiceCalculationResultDto> CalculateInvoicePreviewAsync(int studentId, int? membershipId, decimal discountAmount, List<CreateInvoiceItemDto> items, CancellationToken cancellationToken = default);
        Task<(bool Success, string Message, Invoice? Invoice)> GenerateInvoiceAsync(CreateInvoiceRequestDto request, CancellationToken cancellationToken = default);
        Task<List<InvoiceListDto>> GetInvoicesAsync(InvoiceFilterDto filter, CancellationToken cancellationToken = default);
        Task<Invoice?> GetInvoiceByIdWithDetailsAsync(int invoiceId, CancellationToken cancellationToken = default);
    }

    public class BillingService : IBillingService
    {
        private readonly LibraryDbContext _context;
        private static readonly SemaphoreSlim _invoiceLock = new SemaphoreSlim(1, 1);

        public BillingService(LibraryDbContext context)
        {
            _context = context;
        }

        public async Task<string> GenerateInvoiceNumberAsync(DateTime invoiceDate, CancellationToken cancellationToken = default)
        {
            var year = invoiceDate.Year;
            var prefix = $"INV-{year:D4}-";

            // Find maximum existing sequence for this prefix
            var existingInvoices = await _context.Invoices
                .AsNoTracking()
                .Where(i => i.InvoiceNo.StartsWith(prefix))
                .Select(i => i.InvoiceNo)
                .ToListAsync(cancellationToken);

            int maxSeq = 0;
            foreach (var invNo in existingInvoices)
            {
                var parts = invNo.Split('-');
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

        public async Task<decimal> GetPreviousOutstandingAsync(int studentId, CancellationToken cancellationToken = default)
        {
            // Previous outstanding is calculated from all previous valid non-cancelled invoices for this student.
            // Balance = GrandTotal - PaidAmount (in Phase 4 BalanceAmount == GrandTotal)
            var outstanding = await _context.Invoices
                .AsNoTracking()
                .Where(i => i.StudentId == studentId && i.Status != "Cancelled")
                .SumAsync(i => (decimal?)i.BalanceAmount, cancellationToken) ?? 0.00m;

            return Math.Round(outstanding, 2, MidpointRounding.AwayFromZero);
        }

        public async Task<StudentMembership?> GetActiveMembershipForStudentAsync(int studentId, CancellationToken cancellationToken = default)
        {
            var today = DateTime.Today;
            return await _context.StudentMemberships
                .Include(m => m.Plan)
                .Include(m => m.Seat)
                .AsNoTracking()
                .Where(m => m.StudentId == studentId && m.Status == "Active" && m.EndDate.Date >= today)
                .OrderByDescending(m => m.StartDate)
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<InvoiceCalculationResultDto> CalculateInvoicePreviewAsync(
            int studentId,
            int? membershipId,
            decimal discountAmount,
            List<CreateInvoiceItemDto> items,
            CancellationToken cancellationToken = default)
        {
            decimal subTotal = 0.00m;
            decimal additionalCharges = 0.00m;

            foreach (var item in items)
            {
                var itemAmt = Math.Round(item.Quantity * item.Rate, 2, MidpointRounding.AwayFromZero);
                subTotal += itemAmt;

                // If this is an additional charge (not the reading space membership line), track it
                if (!item.Description.StartsWith("Reading Space Membership", StringComparison.OrdinalIgnoreCase) &&
                    !item.Description.StartsWith("Membership", StringComparison.OrdinalIgnoreCase))
                {
                    additionalCharges += itemAmt;
                }
            }

            subTotal = Math.Round(subTotal, 2, MidpointRounding.AwayFromZero);
            discountAmount = Math.Max(0.00m, Math.Round(discountAmount, 2, MidpointRounding.AwayFromZero));

            if (discountAmount > subTotal)
            {
                discountAmount = subTotal;
            }

            decimal taxAmount = 0.00m;
            decimal currentCharges = Math.Round(subTotal - discountAmount + taxAmount, 2, MidpointRounding.AwayFromZero);
            decimal previousOutstanding = await GetPreviousOutstandingAsync(studentId, cancellationToken);
            decimal grandTotal = Math.Round(currentCharges + previousOutstanding, 2, MidpointRounding.AwayFromZero);
            decimal paidAmount = 0.00m; // Phase 4: No payment collection
            decimal balanceAmount = grandTotal;

            return new InvoiceCalculationResultDto
            {
                SubTotal = subTotal,
                DiscountAmount = discountAmount,
                AdditionalCharges = additionalCharges,
                TaxAmount = taxAmount,
                CurrentCharges = currentCharges,
                PreviousOutstanding = previousOutstanding,
                GrandTotal = grandTotal,
                PaidAmount = paidAmount,
                BalanceAmount = balanceAmount
            };
        }

        public async Task<(bool Success, string Message, Invoice? Invoice)> GenerateInvoiceAsync(
            CreateInvoiceRequestDto request,
            CancellationToken cancellationToken = default)
        {
            if (request == null)
            {
                return (false, "Invalid billing request.", null);
            }

            if (request.StudentId <= 0)
            {
                return (false, "Please select a valid student.", null);
            }

            if (request.Items == null || request.Items.Count == 0)
            {
                return (false, "Invoice must contain at least one line item.", null);
            }

            if (request.DiscountAmount < 0)
            {
                return (false, "Discount amount cannot be negative.", null);
            }

            await _invoiceLock.WaitAsync(cancellationToken);
            try
            {
                // 1. Validate student
                var student = await _context.Students.FindAsync(new object[] { request.StudentId }, cancellationToken);
                if (student == null)
                {
                    return (false, "Selected student was not found.", null);
                }

                if (!student.IsActive)
                {
                    return (false, "Cannot bill an inactive student.", null);
                }

                // 2. Validate membership if provided
                StudentMembership? membership = null;
                if (request.MembershipId.HasValue && request.MembershipId.Value > 0)
                {
                    membership = await _context.StudentMemberships
                        .Include(m => m.Plan)
                        .Include(m => m.Seat)
                        .FirstOrDefaultAsync(m => m.MembershipId == request.MembershipId.Value && m.StudentId == request.StudentId, cancellationToken);

                    if (membership == null)
                    {
                        return (false, "The selected membership was not found for this student.", null);
                    }

                    // Duplicate Bill Protection: check if an unpaid active invoice already exists for this exact membership
                    bool invoiceAlreadyExists = await _context.Invoices.AnyAsync(
                        i => i.MembershipId == membership.MembershipId && i.Status != "Cancelled",
                        cancellationToken);

                    if (invoiceAlreadyExists)
                    {
                        return (false, $"An active invoice already exists for Membership #{membership.MembershipId} ({membership.Plan.PlanName}). Duplicate billing is prevented.", null);
                    }
                }

                // 3. Calculate charges
                decimal subTotal = 0.00m;
                decimal additionalCharges = 0.00m;
                var details = new List<InvoiceDetail>();

                foreach (var item in request.Items)
                {
                    if (string.IsNullOrWhiteSpace(item.Description))
                    {
                        return (false, "All invoice items must have a description.", null);
                    }

                    if (item.Quantity <= 0)
                    {
                        return (false, $"Invalid quantity '{item.Quantity}' for item '{item.Description}'.", null);
                    }

                    if (item.Rate < 0)
                    {
                        return (false, $"Item rate cannot be negative for '{item.Description}'.", null);
                    }

                    var lineAmount = Math.Round(item.Quantity * item.Rate, 2, MidpointRounding.AwayFromZero);
                    subTotal += lineAmount;

                    if (!item.Description.StartsWith("Reading Space Membership", StringComparison.OrdinalIgnoreCase) &&
                        !item.Description.StartsWith("Membership", StringComparison.OrdinalIgnoreCase))
                    {
                        additionalCharges += lineAmount;
                    }

                    details.Add(new InvoiceDetail
                    {
                        Description = item.Description.Trim(),
                        Quantity = item.Quantity,
                        Rate = Math.Round(item.Rate, 2, MidpointRounding.AwayFromZero),
                        Amount = lineAmount
                    });
                }

                subTotal = Math.Round(subTotal, 2, MidpointRounding.AwayFromZero);
                decimal discount = Math.Round(request.DiscountAmount, 2, MidpointRounding.AwayFromZero);

                if (discount > subTotal)
                {
                    return (false, $"Discount (?{discount:N2}) cannot exceed the subtotal (?{subTotal:N2}).", null);
                }

                decimal taxAmount = 0.00m;
                decimal currentCharges = Math.Round(subTotal - discount + taxAmount, 2, MidpointRounding.AwayFromZero);

                // 4. Calculate previous outstanding (from prior valid invoices)
                decimal previousOutstanding = await GetPreviousOutstandingAsync(request.StudentId, cancellationToken);
                decimal grandTotal = Math.Round(currentCharges + previousOutstanding, 2, MidpointRounding.AwayFromZero);
                decimal paidAmount = 0.00m;
                decimal balanceAmount = grandTotal;

                // 5. Transactional Invoice Creation
                using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);
                try
                {
                    // Generate unique invoice number inside transaction
                    string invoiceNo = await GenerateInvoiceNumberAsync(request.InvoiceDate, cancellationToken);

                    var invoice = new Invoice
                    {
                        InvoiceNo = invoiceNo,
                        StudentId = request.StudentId,
                        MembershipId = request.MembershipId,
                        InvoiceDate = request.InvoiceDate,
                        DueDate = request.DueDate,
                        SubTotal = subTotal,
                        DiscountAmount = discount,
                        AdditionalCharges = additionalCharges,
                        TaxAmount = taxAmount,
                        CurrentCharges = currentCharges,
                        PreviousOutstanding = previousOutstanding,
                        GrandTotal = grandTotal,
                        PaidAmount = paidAmount,
                        BalanceAmount = balanceAmount,
                        Status = "Unpaid",
                        CreatedBy = string.IsNullOrWhiteSpace(request.CreatedBy) ? "System" : request.CreatedBy.Trim(),
                        CreatedDate = DateTime.UtcNow
                    };

                    _context.Invoices.Add(invoice);
                    await _context.SaveChangesAsync(cancellationToken);

                    // Add details
                    foreach (var d in details)
                    {
                        d.InvoiceId = invoice.InvoiceId;
                        _context.InvoiceDetails.Add(d);
                    }

                    await _context.SaveChangesAsync(cancellationToken);

                    await transaction.CommitAsync(cancellationToken);

                    // Re-load with navigation properties
                    var completeInvoice = await GetInvoiceByIdWithDetailsAsync(invoice.InvoiceId, cancellationToken);
                    return (true, $"Invoice {invoiceNo} generated successfully.", completeInvoice);
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync(cancellationToken);
                    return (false, $"Failed to generate invoice: {ex.Message}", null);
                }
            }
            finally
            {
                _invoiceLock.Release();
            }
        }

        public async Task<List<InvoiceListDto>> GetInvoicesAsync(InvoiceFilterDto filter, CancellationToken cancellationToken = default)
        {
            var query = _context.Invoices
                .Include(i => i.Student)
                .Include(i => i.Membership)
                    .ThenInclude(m => m!.Plan)
                .Include(i => i.Membership)
                    .ThenInclude(m => m!.Seat)
                .AsNoTracking()
                .AsQueryable();

            if (filter.StudentId.HasValue && filter.StudentId.Value > 0)
            {
                query = query.Where(i => i.StudentId == filter.StudentId.Value);
            }

            if (!string.IsNullOrWhiteSpace(filter.SearchText))
            {
                var st = filter.SearchText.Trim();
                query = query.Where(i =>
                    i.InvoiceNo.Contains(st) ||
                    i.Student.FullName.Contains(st) ||
                    i.Student.StudentCode.Contains(st) ||
                    i.Student.MobileNo.Contains(st));
            }

            if (filter.FromDate.HasValue)
            {
                var from = filter.FromDate.Value.Date;
                query = query.Where(i => i.InvoiceDate >= from);
            }

            if (filter.ToDate.HasValue)
            {
                var to = filter.ToDate.Value.Date.AddDays(1).AddTicks(-1);
                query = query.Where(i => i.InvoiceDate <= to);
            }

            if (!string.IsNullOrWhiteSpace(filter.Status) && filter.Status != "All")
            {
                query = query.Where(i => i.Status == filter.Status);
            }

            var invoices = await query
                .OrderByDescending(i => i.InvoiceDate)
                .ThenByDescending(i => i.InvoiceId)
                .ToListAsync(cancellationToken);

            return invoices.Select(i => new InvoiceListDto
            {
                InvoiceId = i.InvoiceId,
                InvoiceNo = i.InvoiceNo,
                InvoiceDate = i.InvoiceDate,
                DueDate = i.DueDate,
                StudentId = i.StudentId,
                StudentCode = i.Student?.StudentCode ?? string.Empty,
                StudentName = i.Student?.FullName ?? string.Empty,
                MobileNo = i.Student?.MobileNo ?? string.Empty,
                MembershipId = i.MembershipId,
                MembershipPlanName = i.Membership?.Plan?.PlanName ?? "General / Other",
                SeatNumber = i.Membership?.Seat?.SeatNumber,
                SubTotal = i.SubTotal,
                DiscountAmount = i.DiscountAmount,
                AdditionalCharges = i.AdditionalCharges,
                CurrentCharges = i.CurrentCharges,
                PreviousOutstanding = i.PreviousOutstanding,
                GrandTotal = i.GrandTotal,
                PaidAmount = i.PaidAmount,
                BalanceAmount = i.BalanceAmount,
                Status = i.Status,
                CreatedBy = i.CreatedBy,
                CreatedDate = i.CreatedDate
            }).ToList();
        }

        public async Task<Invoice?> GetInvoiceByIdWithDetailsAsync(int invoiceId, CancellationToken cancellationToken = default)
        {
            return await _context.Invoices
                .Include(i => i.Student)
                .Include(i => i.Membership)
                    .ThenInclude(m => m!.Plan)
                .Include(i => i.Membership)
                    .ThenInclude(m => m!.Seat)
                .Include(i => i.InvoiceDetails)
                .AsNoTracking()
                .FirstOrDefaultAsync(i => i.InvoiceId == invoiceId, cancellationToken);
        }
    }
}
