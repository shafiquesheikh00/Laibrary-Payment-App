using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Laibrary_Payment_App.Data;
using Laibrary_Payment_App.Models;
using Laibrary_Payment_App.Reports;

namespace Laibrary_Payment_App.Services
{
    public interface IReportService
    {
        Task<List<DailyCollectionDto>> GetDailyCollectionReportAsync(DailyCollectionFilterDto filter, CancellationToken cancellationToken = default);
        Task<List<DateWiseCollectionDto>> GetDateWiseCollectionReportAsync(DateWiseCollectionFilterDto filter, CancellationToken cancellationToken = default);
        Task<List<MonthlyCollectionDto>> GetMonthlyCollectionReportAsync(MonthlyCollectionFilterDto filter, CancellationToken cancellationToken = default);
        Task<List<OutstandingDto>> GetOutstandingReportAsync(OutstandingFilterDto filter, CancellationToken cancellationToken = default);
        Task<List<StudentLedgerDto>> GetStudentLedgerReportAsync(StudentLedgerFilterDto filter, CancellationToken cancellationToken = default);
        Task<List<PaymentHistoryReportDto>> GetPaymentHistoryReportAsync(PaymentHistoryReportFilterDto filter, CancellationToken cancellationToken = default);
        Task<List<InvoiceRegisterDto>> GetInvoiceRegisterReportAsync(InvoiceRegisterFilterDto filter, CancellationToken cancellationToken = default);
        Task<List<MembershipRegisterDto>> GetMembershipRegisterReportAsync(MembershipRegisterFilterDto filter, CancellationToken cancellationToken = default);
        Task<List<MembershipExpiryDto>> GetMembershipExpiryReportAsync(MembershipExpiryFilterDto filter, CancellationToken cancellationToken = default);
        Task<SeatOccupancySummaryDto> GetSeatOccupancyReportAsync(SeatOccupancyFilterDto filter, CancellationToken cancellationToken = default);
        Task<List<StudentRegisterDto>> GetStudentRegisterReportAsync(StudentRegisterFilterDto filter, CancellationToken cancellationToken = default);
    }

    public class ReportService : IReportService
    {
        private readonly LibraryDbContext _context;

        public ReportService(LibraryDbContext context)
        {
            _context = context;
        }

        public async Task<List<DailyCollectionDto>> GetDailyCollectionReportAsync(DailyCollectionFilterDto filter, CancellationToken cancellationToken = default)
        {
            var targetDate = filter.Date.Date;

            var query = _context.Payments
                .AsNoTracking()
                .Include(p => p.Invoice)
                .Include(p => p.Student)
                .Where(p => p.PaymentDate.Date == targetDate && p.Status == "Active");

            if (!string.IsNullOrWhiteSpace(filter.PaymentMode) && filter.PaymentMode != "All")
            {
                query = query.Where(p => p.PaymentMode == filter.PaymentMode);
            }

            var results = await query
                .OrderBy(p => p.PaymentDate)
                .ThenBy(p => p.PaymentId)
                .Select(p => new DailyCollectionDto
                {
                    PaymentNo = p.PaymentNo,
                    PaymentDate = p.PaymentDate,
                    InvoiceNo = p.Invoice != null ? p.Invoice.InvoiceNo : string.Empty,
                    StudentCode = p.Student != null ? p.Student.StudentCode : string.Empty,
                    StudentName = p.Student != null ? p.Student.FullName : string.Empty,
                    PaymentMode = p.PaymentMode,
                    ReferenceNo = p.ReferenceNo ?? string.Empty,
                    Amount = p.Amount,
                    ReceivedBy = p.ReceivedBy,
                    Status = p.Status
                })
                .ToListAsync(cancellationToken);

            return results;
        }

        public async Task<List<DateWiseCollectionDto>> GetDateWiseCollectionReportAsync(DateWiseCollectionFilterDto filter, CancellationToken cancellationToken = default)
        {
            var from = filter.FromDate.Date;
            var to = filter.ToDate.Date;

            var payments = await _context.Payments
                .AsNoTracking()
                .Where(p => p.PaymentDate.Date >= from && p.PaymentDate.Date <= to && p.Status == "Active")
                .Select(p => new
                {
                    Date = p.PaymentDate.Date,
                    p.PaymentMode,
                    p.Amount
                })
                .ToListAsync(cancellationToken);

            var grouped = payments
                .GroupBy(p => p.Date)
                .OrderBy(g => g.Key)
                .Select(g => new DateWiseCollectionDto
                {
                    Date = g.Key,
                    PaymentCount = g.Count(),
                    CashAmount = g.Where(x => x.PaymentMode == PaymentModes.Cash).Sum(x => x.Amount),
                    UpiAmount = g.Where(x => x.PaymentMode == PaymentModes.UPI).Sum(x => x.Amount),
                    CardAmount = g.Where(x => x.PaymentMode == PaymentModes.Card).Sum(x => x.Amount),
                    BankTransferAmount = g.Where(x => x.PaymentMode == PaymentModes.BankTransfer).Sum(x => x.Amount),
                    OtherAmount = g.Where(x => x.PaymentMode == PaymentModes.Other).Sum(x => x.Amount),
                    TotalCollection = g.Sum(x => x.Amount)
                })
                .ToList();

            return grouped;
        }

        public async Task<List<MonthlyCollectionDto>> GetMonthlyCollectionReportAsync(MonthlyCollectionFilterDto filter, CancellationToken cancellationToken = default)
        {
            var query = _context.Payments
                .AsNoTracking()
                .Where(p => p.PaymentDate.Year == filter.Year && p.Status == "Active");

            if (filter.Month.HasValue && filter.Month.Value > 0)
            {
                query = query.Where(p => p.PaymentDate.Month == filter.Month.Value);
            }

            var payments = await query
                .Select(p => new
                {
                    p.PaymentDate.Year,
                    p.PaymentDate.Month,
                    p.PaymentMode,
                    p.Amount
                })
                .ToListAsync(cancellationToken);

            var grouped = payments
                .GroupBy(p => new { p.Year, p.Month })
                .OrderBy(g => g.Key.Year)
                .ThenBy(g => g.Key.Month)
                .Select(g => new MonthlyCollectionDto
                {
                    Year = g.Key.Year,
                    Month = g.Key.Month,
                    MonthName = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(g.Key.Month) + " " + g.Key.Year,
                    PaymentCount = g.Count(),
                    CashAmount = g.Where(x => x.PaymentMode == PaymentModes.Cash).Sum(x => x.Amount),
                    UpiAmount = g.Where(x => x.PaymentMode == PaymentModes.UPI).Sum(x => x.Amount),
                    CardAmount = g.Where(x => x.PaymentMode == PaymentModes.Card).Sum(x => x.Amount),
                    BankTransferAmount = g.Where(x => x.PaymentMode == PaymentModes.BankTransfer).Sum(x => x.Amount),
                    OtherAmount = g.Where(x => x.PaymentMode == PaymentModes.Other).Sum(x => x.Amount),
                    TotalCollection = g.Sum(x => x.Amount)
                })
                .ToList();

            return grouped;
        }

        public async Task<List<OutstandingDto>> GetOutstandingReportAsync(OutstandingFilterDto filter, CancellationToken cancellationToken = default)
        {
            var query = _context.Invoices
                .AsNoTracking()
                .Include(i => i.Student)
                .Include(i => i.Payments)
                .Where(i => i.Status != "Cancelled");

            if (filter.AsOnDate.HasValue)
            {
                var asOn = filter.AsOnDate.Value.Date;
                query = query.Where(i => i.InvoiceDate.Date <= asOn);
            }

            if (filter.StudentId.HasValue && filter.StudentId.Value > 0)
            {
                query = query.Where(i => i.StudentId == filter.StudentId.Value);
            }

            if (!string.IsNullOrWhiteSpace(filter.SearchText))
            {
                var search = filter.SearchText.Trim().ToLower();
                query = query.Where(i => (i.Student != null && (i.Student.FullName.ToLower().Contains(search) || i.Student.StudentCode.ToLower().Contains(search) || i.Student.MobileNo.Contains(search))) || i.InvoiceNo.ToLower().Contains(search));
            }

            var invoices = await query
                .OrderByDescending(i => i.InvoiceDate)
                .ThenByDescending(i => i.InvoiceId)
                .ToListAsync(cancellationToken);

            var list = new List<OutstandingDto>();
            foreach (var inv in invoices)
            {
                // Source of truth: compute paid from active payments
                var validPayments = inv.Payments
                    .Where(p => p.Status == "Active" && (!filter.AsOnDate.HasValue || p.PaymentDate.Date <= filter.AsOnDate.Value.Date))
                    .Sum(p => p.Amount);

                var balance = inv.GrandTotal - validPayments;

                // Outstanding report excludes fully paid or negative balances
                if (balance <= 0)
                {
                    continue;
                }

                string computedStatus = validPayments == 0 ? "Unpaid" : "PartiallyPaid";

                if (!string.IsNullOrWhiteSpace(filter.Status) && filter.Status != "All")
                {
                    if (!string.Equals(computedStatus, filter.Status, StringComparison.OrdinalIgnoreCase))
                    {
                        continue;
                    }
                }

                list.Add(new OutstandingDto
                {
                    StudentId = inv.StudentId,
                    StudentCode = inv.Student?.StudentCode ?? string.Empty,
                    StudentName = inv.Student?.FullName ?? string.Empty,
                    Mobile = inv.Student?.MobileNo ?? string.Empty,
                    InvoiceId = inv.InvoiceId,
                    InvoiceNo = inv.InvoiceNo,
                    InvoiceDate = inv.InvoiceDate,
                    GrandTotal = inv.GrandTotal,
                    PaidAmount = validPayments,
                    Balance = balance,
                    Status = computedStatus
                });
            }

            return list;
        }

        public async Task<List<StudentLedgerDto>> GetStudentLedgerReportAsync(StudentLedgerFilterDto filter, CancellationToken cancellationToken = default)
        {
            var list = new List<StudentLedgerDto>();

            // 1. Invoices (Debit)
            var invQuery = _context.Invoices
                .AsNoTracking()
                .Include(i => i.Membership)
                .ThenInclude(m => m!.Plan)
                .Where(i => i.StudentId == filter.StudentId && i.Status != "Cancelled");

            if (filter.FromDate.HasValue)
                invQuery = invQuery.Where(i => i.InvoiceDate.Date >= filter.FromDate.Value.Date);
            if (filter.ToDate.HasValue)
                invQuery = invQuery.Where(i => i.InvoiceDate.Date <= filter.ToDate.Value.Date);

            var invoices = await invQuery.ToListAsync(cancellationToken);
            foreach (var inv in invoices)
            {
                string desc = inv.Membership?.Plan?.PlanName != null
                    ? $"Invoice for {inv.Membership.Plan.PlanName}"
                    : "Library Invoice Charges";

                list.Add(new StudentLedgerDto
                {
                    Date = inv.InvoiceDate,
                    Type = "Invoice",
                    Reference = inv.InvoiceNo,
                    Description = desc,
                    Debit = inv.GrandTotal,
                    Credit = 0m,
                    ModeOrStatus = inv.Status
                });
            }

            // 2. Payments (Credit) - Only active payments
            var payQuery = _context.Payments
                .AsNoTracking()
                .Where(p => p.StudentId == filter.StudentId && p.Status == "Active");

            if (filter.FromDate.HasValue)
                payQuery = payQuery.Where(p => p.PaymentDate.Date >= filter.FromDate.Value.Date);
            if (filter.ToDate.HasValue)
                payQuery = payQuery.Where(p => p.PaymentDate.Date <= filter.ToDate.Value.Date);

            var payments = await payQuery.ToListAsync(cancellationToken);
            foreach (var p in payments)
            {
                string desc = string.IsNullOrWhiteSpace(p.ReferenceNo)
                    ? $"Payment received via {p.PaymentMode}"
                    : $"Payment received via {p.PaymentMode} (Ref: {p.ReferenceNo})";

                list.Add(new StudentLedgerDto
                {
                    Date = p.PaymentDate,
                    Type = "Payment",
                    Reference = p.PaymentNo,
                    Description = desc,
                    Debit = 0m,
                    Credit = p.Amount,
                    ModeOrStatus = p.PaymentMode
                });
            }

            // Sort chronologically: if same date, invoices first so charge is posted before payment
            var sorted = list
                .OrderBy(x => x.Date)
                .ThenBy(x => x.Type == "Invoice" ? 0 : 1)
                .ThenBy(x => x.Reference)
                .ToList();

            decimal runningBalance = 0m;
            foreach (var item in sorted)
            {
                runningBalance += (item.Debit - item.Credit);
                item.RunningBalance = runningBalance;
            }

            return sorted;
        }

        public async Task<List<PaymentHistoryReportDto>> GetPaymentHistoryReportAsync(PaymentHistoryReportFilterDto filter, CancellationToken cancellationToken = default)
        {
            var query = _context.Payments
                .AsNoTracking()
                .Include(p => p.Invoice)
                .Include(p => p.Student)
                .AsQueryable();

            if (filter.FromDate.HasValue)
                query = query.Where(p => p.PaymentDate.Date >= filter.FromDate.Value.Date);
            if (filter.ToDate.HasValue)
                query = query.Where(p => p.PaymentDate.Date <= filter.ToDate.Value.Date);
            if (filter.StudentId.HasValue && filter.StudentId.Value > 0)
                query = query.Where(p => p.StudentId == filter.StudentId.Value);
            if (!string.IsNullOrWhiteSpace(filter.PaymentMode) && filter.PaymentMode != "All")
                query = query.Where(p => p.PaymentMode == filter.PaymentMode);
            if (!string.IsNullOrWhiteSpace(filter.Status) && filter.Status != "All")
                query = query.Where(p => p.Status == filter.Status);

            var list = await query
                .OrderByDescending(p => p.PaymentDate)
                .ThenByDescending(p => p.PaymentId)
                .Select(p => new PaymentHistoryReportDto
                {
                    PaymentNo = p.PaymentNo,
                    Date = p.PaymentDate,
                    InvoiceNo = p.Invoice != null ? p.Invoice.InvoiceNo : string.Empty,
                    StudentCode = p.Student != null ? p.Student.StudentCode : string.Empty,
                    StudentName = p.Student != null ? p.Student.FullName : string.Empty,
                    Amount = p.Amount,
                    Mode = p.PaymentMode,
                    Reference = p.ReferenceNo ?? string.Empty,
                    ReceivedBy = p.ReceivedBy,
                    Status = p.Status
                })
                .ToListAsync(cancellationToken);

            return list;
        }

        public async Task<List<InvoiceRegisterDto>> GetInvoiceRegisterReportAsync(InvoiceRegisterFilterDto filter, CancellationToken cancellationToken = default)
        {
            var query = _context.Invoices
                .AsNoTracking()
                .Include(i => i.Student)
                .Include(i => i.Membership)
                .ThenInclude(m => m!.Plan)
                .AsQueryable();

            if (filter.FromDate.HasValue)
                query = query.Where(i => i.InvoiceDate.Date >= filter.FromDate.Value.Date);
            if (filter.ToDate.HasValue)
                query = query.Where(i => i.InvoiceDate.Date <= filter.ToDate.Value.Date);
            if (filter.StudentId.HasValue && filter.StudentId.Value > 0)
                query = query.Where(i => i.StudentId == filter.StudentId.Value);
            if (!string.IsNullOrWhiteSpace(filter.Status) && filter.Status != "All")
                query = query.Where(i => i.Status == filter.Status);

            var list = await query
                .OrderByDescending(i => i.InvoiceDate)
                .ThenByDescending(i => i.InvoiceId)
                .Select(i => new InvoiceRegisterDto
                {
                    InvoiceId = i.InvoiceId,
                    InvoiceNo = i.InvoiceNo,
                    Date = i.InvoiceDate,
                    StudentCode = i.Student != null ? i.Student.StudentCode : string.Empty,
                    StudentName = i.Student != null ? i.Student.FullName : string.Empty,
                    Membership = i.Membership != null && i.Membership.Plan != null ? i.Membership.Plan.PlanName : "General",
                    Subtotal = i.SubTotal,
                    Discount = i.DiscountAmount,
                    AdditionalCharges = i.AdditionalCharges,
                    Tax = i.TaxAmount,
                    CurrentCharges = i.CurrentCharges,
                    PreviousOutstanding = i.PreviousOutstanding,
                    GrandTotal = i.GrandTotal,
                    Paid = i.PaidAmount,
                    Balance = i.BalanceAmount,
                    Status = i.Status
                })
                .ToListAsync(cancellationToken);

            return list;
        }

        public async Task<List<MembershipRegisterDto>> GetMembershipRegisterReportAsync(MembershipRegisterFilterDto filter, CancellationToken cancellationToken = default)
        {
            var query = _context.StudentMemberships
                .AsNoTracking()
                .Include(m => m.Student)
                .Include(m => m.Plan)
                .Include(m => m.Seat)
                .AsQueryable();

            if (filter.StartDateFrom.HasValue)
                query = query.Where(m => m.StartDate.Date >= filter.StartDateFrom.Value.Date);
            if (filter.StartDateTo.HasValue)
                query = query.Where(m => m.StartDate.Date <= filter.StartDateTo.Value.Date);
            if (filter.PlanId.HasValue && filter.PlanId.Value > 0)
                query = query.Where(m => m.PlanId == filter.PlanId.Value);
            if (filter.SeatId.HasValue && filter.SeatId.Value > 0)
                query = query.Where(m => m.SeatId == filter.SeatId.Value);
            if (!string.IsNullOrWhiteSpace(filter.Status) && filter.Status != "All")
                query = query.Where(m => m.Status == filter.Status);

            var list = await query
                .OrderByDescending(m => m.StartDate)
                .ThenByDescending(m => m.MembershipId)
                .Select(m => new MembershipRegisterDto
                {
                    MembershipId = m.MembershipId,
                    StudentCode = m.Student != null ? m.Student.StudentCode : string.Empty,
                    StudentName = m.Student != null ? m.Student.FullName : string.Empty,
                    PlanName = m.Plan != null ? m.Plan.PlanName : string.Empty,
                    SeatNumber = m.Seat != null ? m.Seat.SeatNumber : "Unassigned",
                    StartDate = m.StartDate,
                    EndDate = m.EndDate,
                    Fee = m.ActualFee,
                    Discount = m.DiscountAmount,
                    FinalFee = m.FinalFee,
                    Status = m.Status
                })
                .ToListAsync(cancellationToken);

            return list;
        }

        public async Task<List<MembershipExpiryDto>> GetMembershipExpiryReportAsync(MembershipExpiryFilterDto filter, CancellationToken cancellationToken = default)
        {
            var today = DateTime.UtcNow.Date;
            var query = _context.StudentMemberships
                .AsNoTracking()
                .Include(m => m.Student)
                .Include(m => m.Plan)
                .Include(m => m.Seat)
                .Where(m => m.Status == "Active");

            if (filter.ExpiryFrom.HasValue && filter.ExpiryTo.HasValue)
            {
                var from = filter.ExpiryFrom.Value.Date;
                var to = filter.ExpiryTo.Value.Date;
                query = query.Where(m => m.EndDate.Date >= from && m.EndDate.Date <= to);
            }
            else if (filter.ExpiryWithinDays.HasValue && filter.ExpiryWithinDays.Value > 0)
            {
                var maxDate = today.AddDays(filter.ExpiryWithinDays.Value);
                query = query.Where(m => m.EndDate.Date >= today && m.EndDate.Date <= maxDate);
            }
            else
            {
                query = query.Where(m => m.EndDate.Date >= today);
            }

            var memberships = await query
                .OrderBy(m => m.EndDate)
                .ToListAsync(cancellationToken);

            var list = memberships.Select(m => new MembershipExpiryDto
            {
                MembershipId = m.MembershipId,
                StudentName = m.Student?.FullName ?? "Unknown",
                Mobile = m.Student?.MobileNo ?? string.Empty,
                PlanName = m.Plan?.PlanName ?? "General",
                SeatNumber = m.Seat?.SeatNumber ?? "Unassigned",
                StartDate = m.StartDate,
                EndDate = m.EndDate,
                DaysRemaining = (m.EndDate.Date - today).Days,
                Status = m.Status
            }).ToList();

            return list;
        }

        public async Task<SeatOccupancySummaryDto> GetSeatOccupancyReportAsync(SeatOccupancyFilterDto filter, CancellationToken cancellationToken = default)
        {
            var today = DateTime.UtcNow.Date;
            var seats = await _context.Seats
                .AsNoTracking()
                .OrderBy(s => s.SeatNumber)
                .ToListAsync(cancellationToken);

            // Active memberships today to get student & plan info for occupied seats
            var activeMemberships = await _context.StudentMemberships
                .AsNoTracking()
                .Include(m => m.Student)
                .Include(m => m.Plan)
                .Where(m => m.Status == "Active" && m.StartDate.Date <= today && m.EndDate.Date >= today && m.SeatId.HasValue)
                .ToListAsync(cancellationToken);

            var memMap = activeMemberships
                .GroupBy(m => m.SeatId!.Value)
                .ToDictionary(g => g.Key, g => g.First());

            var summary = new SeatOccupancySummaryDto
            {
                TotalSeats = seats.Count,
                ActiveSeats = seats.Count(s => s.IsActive),
                OccupiedSeats = seats.Count(s => s.IsActive && s.Status == "Occupied"),
                AvailableSeats = seats.Count(s => s.IsActive && s.Status == "Available"),
                MaintenanceSeats = seats.Count(s => s.IsActive && s.Status == "Maintenance"),
                InactiveSeats = seats.Count(s => !s.IsActive)
            };

            summary.OccupancyPercentage = summary.ActiveSeats > 0
                ? Math.Round(((double)summary.OccupiedSeats / summary.ActiveSeats) * 100.0, 1)
                : 0.0;

            var items = new List<SeatOccupancyDto>();
            foreach (var s in seats)
            {
                memMap.TryGetValue(s.SeatId, out var mem);

                string seatStatus = !s.IsActive ? "Inactive" : s.Status;

                if (!string.IsNullOrWhiteSpace(filter.Status) && filter.Status != "All")
                {
                    if (!string.Equals(seatStatus, filter.Status, StringComparison.OrdinalIgnoreCase))
                    {
                        continue;
                    }
                }

                if (!string.IsNullOrWhiteSpace(filter.Area) && filter.Area != "All")
                {
                    if (!string.Equals(s.Area, filter.Area, StringComparison.OrdinalIgnoreCase))
                    {
                        continue;
                    }
                }

                items.Add(new SeatOccupancyDto
                {
                    SeatId = s.SeatId,
                    SeatNumber = s.SeatNumber,
                    Area = s.Area ?? string.Empty,
                    Status = seatStatus,
                    StudentName = mem?.Student?.FullName ?? string.Empty,
                    StudentCode = mem?.Student?.StudentCode ?? string.Empty,
                    MembershipPlan = mem?.Plan?.PlanName ?? string.Empty,
                    StartDate = mem?.StartDate,
                    EndDate = mem?.EndDate
                });
            }

            summary.Seats = items;
            return summary;
        }

        public async Task<List<StudentRegisterDto>> GetStudentRegisterReportAsync(StudentRegisterFilterDto filter, CancellationToken cancellationToken = default)
        {
            var query = _context.Students.AsNoTracking().AsQueryable();

            if (!string.IsNullOrWhiteSpace(filter.SearchText))
            {
                var s = filter.SearchText.Trim().ToLower();
                query = query.Where(st => st.FullName.ToLower().Contains(s) || st.StudentCode.ToLower().Contains(s) || st.MobileNo.Contains(s) || (st.Email != null && st.Email.ToLower().Contains(s)));
            }

            if (!string.IsNullOrWhiteSpace(filter.ActiveStatus) && filter.ActiveStatus != "All")
            {
                bool isActive = filter.ActiveStatus == "Active";
                query = query.Where(st => st.IsActive == isActive);
            }

            var list = await query
                .OrderBy(st => st.StudentCode)
                .Select(st => new StudentRegisterDto
                {
                    StudentId = st.StudentId,
                    StudentCode = st.StudentCode,
                    Name = st.FullName,
                    Mobile = st.MobileNo,
                    Email = st.Email ?? string.Empty,
                    College = st.CollegeName ?? string.Empty,
                    Course = st.CourseName ?? string.Empty,
                    ActiveStatus = st.IsActive ? "Active" : "Inactive",
                    CreatedDate = st.CreatedDate
                })
                .ToListAsync(cancellationToken);

            return list;
        }
    }
}
