using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Laibrary_Payment_App.Data;
using Laibrary_Payment_App.Models;
using Laibrary_Payment_App.Reports;

namespace Laibrary_Payment_App.Services
{
    public interface IDashboardService
    {
        Task<DashboardSummaryDto> GetSummaryAsync(CancellationToken cancellationToken = default);
        Task<List<DashboardExpiringMembershipDto>> GetExpiringMembershipsAsync(int limit = 10, CancellationToken cancellationToken = default);
    }

    public class DashboardService : IDashboardService
    {
        private readonly LibraryDbContext _dbContext;

        public DashboardService(LibraryDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<DashboardExpiringMembershipDto>> GetExpiringMembershipsAsync(int limit = 10, CancellationToken cancellationToken = default)
        {
            var today = DateTime.UtcNow.Date;
            var list = await _dbContext.StudentMemberships
                .AsNoTracking()
                .Include(m => m.Student)
                .Include(m => m.Plan)
                .Include(m => m.Seat)
                .Where(m => m.Status == "Active" && m.EndDate.Date >= today)
                .OrderBy(m => m.EndDate)
                .Take(limit)
                .Select(m => new DashboardExpiringMembershipDto
                {
                    StudentName = m.Student != null ? m.Student.FullName : "Unknown",
                    SeatNumber = m.Seat != null ? m.Seat.SeatNumber : "Unassigned",
                    PlanName = m.Plan != null ? m.Plan.PlanName : "Standard",
                    ExpiryDate = m.EndDate,
                    DaysRemaining = (m.EndDate.Date - today).Days
                })
                .ToListAsync(cancellationToken);

            return list;
        }

        public async Task<DashboardSummaryDto> GetSummaryAsync(CancellationToken cancellationToken = default)
        {
            var summary = new DashboardSummaryDto();

            try
            {
                var today = DateTime.UtcNow.Date;
                var next7Days = today.AddDays(7);
                var startOfMonth = new DateTime(today.Year, today.Month, 1);
                var endOfMonth = startOfMonth.AddMonths(1);

                // Auto-expire past memberships and sync seats first
                var pastMemberships = await _dbContext.StudentMemberships
                    .Where(m => m.Status == "Active" && m.EndDate.Date < today)
                    .ToListAsync(cancellationToken);

                if (pastMemberships.Count > 0)
                {
                    foreach (var m in pastMemberships)
                    {
                        m.Status = "Expired";
                        m.ModifiedDate = DateTime.UtcNow;
                    }

                    var expiredSeatIds = pastMemberships
                        .Where(m => m.SeatId.HasValue)
                        .Select(m => m.SeatId!.Value)
                        .Distinct()
                        .ToList();

                    foreach (var sId in expiredSeatIds)
                    {
                        bool isStillOccupied = await _dbContext.StudentMemberships.AnyAsync(
                            m => m.SeatId == sId &&
                                 m.Status == "Active" &&
                                 m.StartDate.Date <= today &&
                                 m.EndDate.Date >= today &&
                                 !pastMemberships.Select(p => p.MembershipId).Contains(m.MembershipId),
                            cancellationToken);

                        if (!isStillOccupied)
                        {
                            var seat = await _dbContext.Seats.FindAsync(new object[] { sId }, cancellationToken);
                            if (seat != null && seat.Status == "Occupied" && seat.IsActive)
                            {
                                seat.Status = "Available";
                                seat.ModifiedDate = DateTime.UtcNow;
                            }
                        }
                    }

                    await _dbContext.SaveChangesAsync(cancellationToken);
                }

                // Student statistics
                summary.TotalStudents = await _dbContext.Students.CountAsync(cancellationToken);
                summary.ActiveStudents = await _dbContext.Students.CountAsync(s => s.IsActive, cancellationToken);
                summary.InactiveStudents = await _dbContext.Students.CountAsync(s => !s.IsActive, cancellationToken);

                // Membership statistics
                summary.ActiveMemberships = await _dbContext.StudentMemberships.CountAsync(
                    m => m.Status == "Active" && m.EndDate.Date >= today,
                    cancellationToken);

                summary.ExpiringMemberships = await _dbContext.StudentMemberships.CountAsync(
                    m => m.Status == "Active" && m.EndDate.Date >= today && m.EndDate.Date <= next7Days,
                    cancellationToken);

                summary.ExpiredMemberships = await _dbContext.StudentMemberships.CountAsync(
                    m => m.Status == "Expired" || (m.EndDate.Date < today && m.Status != "Cancelled"),
                    cancellationToken);

                // Seat statistics
                summary.TotalActiveSeats = await _dbContext.Seats.CountAsync(s => s.IsActive, cancellationToken);
                summary.AvailableSeats = await _dbContext.Seats.CountAsync(
                    s => s.IsActive && s.Status == "Available",
                    cancellationToken);

                summary.OccupiedSeats = await _dbContext.Seats.CountAsync(
                    s => s.IsActive && s.Status == "Occupied",
                    cancellationToken);

                summary.MaintenanceSeats = await _dbContext.Seats.CountAsync(
                    s => s.IsActive && s.Status == "Maintenance",
                    cancellationToken);

                // Billing statistics queried directly from SQL Server
                summary.TodayInvoicesCount = await _dbContext.Invoices.CountAsync(
                    i => i.InvoiceDate.Date == today && i.Status != "Cancelled",
                    cancellationToken);

                summary.TodayBillingAmount = await _dbContext.Invoices
                    .Where(i => i.InvoiceDate.Date == today && i.Status != "Cancelled")
                    .SumAsync(i => (decimal?)i.GrandTotal, cancellationToken) ?? 0.00m;

                // Outstanding must exclude Paid and Cancelled invoices
                summary.CurrentOutstandingAmount = await _dbContext.Invoices
                    .Where(i => i.Status != "Cancelled" && i.Status != "Paid")
                    .SumAsync(i => (decimal?)i.BalanceAmount, cancellationToken) ?? 0.00m;

                summary.TotalInvoicesCount = await _dbContext.Invoices.CountAsync(
                    i => i.Status != "Cancelled",
                    cancellationToken);

                // Payment and Collection statistics queried directly from SQL Server
                summary.TodayCollectionAmount = await _dbContext.Payments
                    .Where(p => p.PaymentDate.Date == today && p.Status == "Active")
                    .SumAsync(p => (decimal?)p.Amount, cancellationToken) ?? 0.00m;

                summary.ThisMonthCollectionAmount = await _dbContext.Payments
                    .Where(p => p.PaymentDate >= startOfMonth && p.PaymentDate < endOfMonth && p.Status == "Active")
                    .SumAsync(p => (decimal?)p.Amount, cancellationToken) ?? 0.00m;

                summary.TodayPaymentCount = await _dbContext.Payments.CountAsync(
                    p => p.PaymentDate.Date == today && p.Status == "Active",
                    cancellationToken);

                summary.UnpaidInvoicesCount = await _dbContext.Invoices.CountAsync(
                    i => i.Status == "Unpaid",
                    cancellationToken);

                summary.PartiallyPaidInvoicesCount = await _dbContext.Invoices.CountAsync(
                    i => i.Status == "PartiallyPaid",
                    cancellationToken);

                summary.PaidInvoicesCount = await _dbContext.Invoices.CountAsync(
                    i => i.Status == "Paid",
                    cancellationToken);

                // Nearest upcoming expiring memberships for dashboard widget
                summary.ExpiringMembershipsList = await GetExpiringMembershipsAsync(8, cancellationToken);
            }
            catch
            {
                // Fall back to zeros on database reading exception
            }

            return summary;
        }
    }
}
