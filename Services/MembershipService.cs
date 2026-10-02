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
    public interface IMembershipService
    {
        Task<StudentMembership?> GetActiveMembershipAsync(int studentId, CancellationToken cancellationToken = default);
        Task<List<StudentMembership>> GetMembershipHistoryAsync(int studentId, CancellationToken cancellationToken = default);
        Task<StudentMembership?> GetMembershipByIdAsync(int membershipId, CancellationToken cancellationToken = default);
        Task<List<StudentMembership>> GetAllMembershipsAsync(
            int? studentId = null,
            int? planId = null,
            int? seatId = null,
            string? status = null,
            DateTime? startDate = null,
            DateTime? endDate = null,
            string? searchTerm = null,
            CancellationToken cancellationToken = default);

        Task<(bool IsSuccess, string Message, StudentMembership? Membership)> CreateMembershipAsync(
            StudentMembership membership,
            CancellationToken cancellationToken = default);

        Task<(bool IsSuccess, string Message, StudentMembership? Membership)> RenewMembershipAsync(
            int existingMembershipId,
            int newPlanId,
            int? newSeatId,
            DateTime? explicitStartDate = null,
            decimal discountAmount = 0m,
            string? remarks = null,
            CancellationToken cancellationToken = default);

        Task<(bool IsSuccess, string Message)> CancelMembershipAsync(
            int membershipId,
            string? cancellationReason = null,
            CancellationToken cancellationToken = default);

        Task<int> ExpireMembershipsAsync(CancellationToken cancellationToken = default);
        Task<List<SeatMaster>> GetAvailableSeatsAsync(
            DateTime startDate,
            DateTime endDate,
            int? currentMembershipId = null,
            CancellationToken cancellationToken = default);

        Task<(bool IsValid, string Message)> ValidateMembershipAsync(
            StudentMembership membership,
            bool isNew = true,
            CancellationToken cancellationToken = default);
    }

    public class MembershipService : IMembershipService
    {
        private readonly LibraryDbContext _dbContext;

        public MembershipService(LibraryDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<StudentMembership?> GetActiveMembershipAsync(int studentId, CancellationToken cancellationToken = default)
        {
            await ExpireMembershipsAsync(cancellationToken);

            var today = DateTime.UtcNow.Date;
            return await _dbContext.StudentMemberships
                .Include(m => m.Student)
                .Include(m => m.Plan)
                .Include(m => m.Seat)
                .AsNoTracking()
                .Where(m => m.StudentId == studentId &&
                            m.Status == "Active" &&
                            m.EndDate.Date >= today)
                .OrderByDescending(m => m.EndDate)
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<List<StudentMembership>> GetMembershipHistoryAsync(int studentId, CancellationToken cancellationToken = default)
        {
            await ExpireMembershipsAsync(cancellationToken);

            return await _dbContext.StudentMemberships
                .Include(m => m.Student)
                .Include(m => m.Plan)
                .Include(m => m.Seat)
                .AsNoTracking()
                .Where(m => m.StudentId == studentId)
                .OrderByDescending(m => m.StartDate)
                .ThenByDescending(m => m.MembershipId)
                .ToListAsync(cancellationToken);
        }

        public async Task<StudentMembership?> GetMembershipByIdAsync(int membershipId, CancellationToken cancellationToken = default)
        {
            return await _dbContext.StudentMemberships
                .Include(m => m.Student)
                .Include(m => m.Plan)
                .Include(m => m.Seat)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.MembershipId == membershipId, cancellationToken);
        }

        public async Task<List<StudentMembership>> GetAllMembershipsAsync(
            int? studentId = null,
            int? planId = null,
            int? seatId = null,
            string? status = null,
            DateTime? startDate = null,
            DateTime? endDate = null,
            string? searchTerm = null,
            CancellationToken cancellationToken = default)
        {
            await ExpireMembershipsAsync(cancellationToken);

            var query = _dbContext.StudentMemberships
                .Include(m => m.Student)
                .Include(m => m.Plan)
                .Include(m => m.Seat)
                .AsNoTracking()
                .AsQueryable();

            if (studentId.HasValue)
            {
                query = query.Where(m => m.StudentId == studentId.Value);
            }

            if (planId.HasValue)
            {
                query = query.Where(m => m.PlanId == planId.Value);
            }

            if (seatId.HasValue)
            {
                query = query.Where(m => m.SeatId == seatId.Value);
            }

            if (!string.IsNullOrWhiteSpace(status) && status != "All" && status != "All Statuses")
            {
                query = query.Where(m => m.Status == status);
            }

            if (startDate.HasValue)
            {
                var sDate = startDate.Value.Date;
                query = query.Where(m => m.StartDate.Date >= sDate);
            }

            if (endDate.HasValue)
            {
                var eDate = endDate.Value.Date;
                query = query.Where(m => m.EndDate.Date <= eDate);
            }

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                var term = searchTerm.Trim().ToLower();
                query = query.Where(m =>
                    m.Student.FullName.ToLower().Contains(term) ||
                    m.Student.StudentCode.ToLower().Contains(term) ||
                    m.Student.MobileNo.Contains(term) ||
                    (m.Seat != null && m.Seat.SeatNumber.ToLower().Contains(term)) ||
                    m.Plan.PlanName.ToLower().Contains(term));
            }

            return await query
                .OrderByDescending(m => m.CreatedDate)
                .ThenByDescending(m => m.MembershipId)
                .ToListAsync(cancellationToken);
        }

        public async Task<List<SeatMaster>> GetAvailableSeatsAsync(
            DateTime startDate,
            DateTime endDate,
            int? currentMembershipId = null,
            CancellationToken cancellationToken = default)
        {
            await ExpireMembershipsAsync(cancellationToken);

            var start = startDate.Date;
            var end = endDate.Date;

            var occupiedSeatIds = await _dbContext.StudentMemberships
                .AsNoTracking()
                .Where(m => m.SeatId.HasValue &&
                            m.Status == "Active" &&
                            (!currentMembershipId.HasValue || m.MembershipId != currentMembershipId.Value) &&
                            m.StartDate.Date <= end &&
                            m.EndDate.Date >= start)
                .Select(m => m.SeatId!.Value)
                .Distinct()
                .ToListAsync(cancellationToken);

            return await _dbContext.Seats
                .AsNoTracking()
                .Where(s => s.IsActive &&
                            s.Status != "Maintenance" &&
                            s.Status != "Inactive" &&
                            !occupiedSeatIds.Contains(s.SeatId))
                .OrderBy(s => s.SeatNumber)
                .ToListAsync(cancellationToken);
        }

        public async Task<(bool IsValid, string Message)> ValidateMembershipAsync(
            StudentMembership membership,
            bool isNew = true,
            CancellationToken cancellationToken = default)
        {
            if (membership == null)
            {
                return (false, "Membership details are required.");
            }

            // 1. Student must exist and be active
            var student = await _dbContext.Students
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.StudentId == membership.StudentId, cancellationToken);

            if (student == null)
            {
                return (false, "Selected student was not found.");
            }

            if (!student.IsActive)
            {
                return (false, $"Student '{student.FullName}' is inactive. Only active students can be assigned memberships.");
            }

            // 2. Plan must exist and be active
            var plan = await _dbContext.MembershipPlans
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.PlanId == membership.PlanId, cancellationToken);

            if (plan == null)
            {
                return (false, "Selected membership plan was not found.");
            }

            if (!plan.IsActive)
            {
                return (false, $"Membership plan '{plan.PlanName}' is inactive.");
            }

            // 3. Start Date and End Date validation
            if (membership.StartDate == default)
            {
                return (false, "Start date is required.");
            }

            if (membership.EndDate.Date < membership.StartDate.Date)
            {
                return (false, "End date cannot be earlier than start date.");
            }

            // 4. Overlapping active membership for this student
            var start = membership.StartDate.Date;
            var end = membership.EndDate.Date;

            bool hasOverlappingStudentMembership = await _dbContext.StudentMemberships
                .AsNoTracking()
                .AnyAsync(m => m.StudentId == membership.StudentId &&
                               m.Status == "Active" &&
                               (isNew || m.MembershipId != membership.MembershipId) &&
                               m.StartDate.Date <= end &&
                               m.EndDate.Date >= start,
                          cancellationToken);

            if (hasOverlappingStudentMembership)
            {
                return (false, $"Student '{student.FullName}' already has an active membership during the requested period ({start:dd-MMM-yyyy} to {end:dd-MMM-yyyy}).");
            }

            // 5. Seat validation (if seat assigned)
            if (membership.SeatId.HasValue)
            {
                var seat = await _dbContext.Seats
                    .AsNoTracking()
                    .FirstOrDefaultAsync(s => s.SeatId == membership.SeatId.Value, cancellationToken);

                if (seat == null)
                {
                    return (false, "Selected seat was not found.");
                }

                if (!seat.IsActive)
                {
                    return (false, $"Seat '{seat.SeatNumber}' is inactive and cannot be assigned.");
                }

                if (seat.Status == "Maintenance")
                {
                    return (false, $"Seat '{seat.SeatNumber}' is under maintenance and cannot be assigned.");
                }

                if (seat.Status == "Inactive")
                {
                    return (false, $"Seat '{seat.SeatNumber}' is marked inactive and cannot be assigned.");
                }

                // Check if seat is occupied by another student during this period
                var seatConflict = await _dbContext.StudentMemberships
                    .Include(m => m.Student)
                    .AsNoTracking()
                    .FirstOrDefaultAsync(m => m.SeatId == membership.SeatId.Value &&
                                              m.Status == "Active" &&
                                              (isNew || m.MembershipId != membership.MembershipId) &&
                                              m.StartDate.Date <= end &&
                                              m.EndDate.Date >= start,
                                         cancellationToken);

                if (seatConflict != null)
                {
                    return (false, $"Seat '{seat.SeatNumber}' is already occupied by student '{seatConflict.Student?.FullName}' from {seatConflict.StartDate:dd-MMM-yyyy} to {seatConflict.EndDate:dd-MMM-yyyy}.");
                }
            }

            // 6. Discount validations
            if (membership.DiscountAmount < 0m)
            {
                return (false, "Discount amount cannot be negative.");
            }

            if (membership.ActualFee < 0m)
            {
                return (false, "Membership fee cannot be negative.");
            }

            if (membership.DiscountAmount > membership.ActualFee)
            {
                return (false, $"Discount amount (₹{membership.DiscountAmount:N2}) cannot exceed the plan fee (₹{membership.ActualFee:N2}).");
            }

            return (true, string.Empty);
        }

        public async Task<(bool IsSuccess, string Message, StudentMembership? Membership)> CreateMembershipAsync(
            StudentMembership membership,
            CancellationToken cancellationToken = default)
        {
            await ExpireMembershipsAsync(cancellationToken);

            var validation = await ValidateMembershipAsync(membership, isNew: true, cancellationToken);
            if (!validation.IsValid)
            {
                return (false, validation.Message, null);
            }

            // Fetch Plan to ensure ActualFee is set correctly if not provided
            var plan = await _dbContext.MembershipPlans.FindAsync(new object[] { membership.PlanId }, cancellationToken);
            if (plan != null)
            {
                membership.ActualFee = plan.Fee;
            }

            membership.FinalFee = membership.ActualFee - membership.DiscountAmount;
            membership.Status = "Active";
            membership.CreatedDate = DateTime.UtcNow;
            membership.ModifiedDate = null;

            using var transaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken);
            try
            {
                _dbContext.StudentMemberships.Add(membership);
                await _dbContext.SaveChangesAsync(cancellationToken);

                // Update Seat Status to Occupied if assigned and within current or future period
                if (membership.SeatId.HasValue)
                {
                    var seat = await _dbContext.Seats.FindAsync(new object[] { membership.SeatId.Value }, cancellationToken);
                    if (seat != null)
                    {
                        var today = DateTime.UtcNow.Date;
                        if (membership.StartDate.Date <= today && membership.EndDate.Date >= today)
                        {
                            seat.Status = "Occupied";
                        }
                        seat.ModifiedDate = DateTime.UtcNow;
                        await _dbContext.SaveChangesAsync(cancellationToken);
                    }
                }

                await transaction.CommitAsync(cancellationToken);
                return (true, "Student membership created successfully.", membership);
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync(cancellationToken);
                return (false, $"Failed to create membership: {ex.Message}", null);
            }
        }

        public async Task<(bool IsSuccess, string Message, StudentMembership? Membership)> RenewMembershipAsync(
            int existingMembershipId,
            int newPlanId,
            int? newSeatId,
            DateTime? explicitStartDate = null,
            decimal discountAmount = 0m,
            string? remarks = null,
            CancellationToken cancellationToken = default)
        {
            await ExpireMembershipsAsync(cancellationToken);

            var existing = await _dbContext.StudentMemberships
                .Include(m => m.Student)
                .Include(m => m.Plan)
                .FirstOrDefaultAsync(m => m.MembershipId == existingMembershipId, cancellationToken);

            if (existing == null)
            {
                return (false, "Existing membership not found.", null);
            }

            var plan = await _dbContext.MembershipPlans.FindAsync(new object[] { newPlanId }, cancellationToken);
            if (plan == null || !plan.IsActive)
            {
                return (false, "Selected renewal plan is invalid or inactive.", null);
            }

            // Determine Start Date:
            // "The renewal period begins from: Current EndDate + 1 day unless the user explicitly chooses a future start date."
            DateTime startDate;
            if (explicitStartDate.HasValue && explicitStartDate.Value.Date > existing.EndDate.Date)
            {
                startDate = explicitStartDate.Value.Date;
            }
            else
            {
                startDate = existing.EndDate.Date.AddDays(1);
            }

            DateTime endDate = MembershipDateCalculator.CalculateEndDate(startDate, plan.DurationValue, plan.DurationUnit);

            var newMembership = new StudentMembership
            {
                StudentId = existing.StudentId,
                PlanId = newPlanId,
                SeatId = newSeatId,
                StartDate = startDate,
                EndDate = endDate,
                ActualFee = plan.Fee,
                DiscountAmount = discountAmount,
                FinalFee = plan.Fee - discountAmount,
                Status = "Active",
                Remarks = string.IsNullOrWhiteSpace(remarks)
                    ? $"Renewed from Membership #{existing.MembershipId}"
                    : remarks.Trim(),
                CreatedDate = DateTime.UtcNow
            };

            var validation = await ValidateMembershipAsync(newMembership, isNew: true, cancellationToken);
            if (!validation.IsValid)
            {
                return (false, validation.Message, null);
            }

            using var transaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken);
            try
            {
                _dbContext.StudentMemberships.Add(newMembership);
                await _dbContext.SaveChangesAsync(cancellationToken);

                if (newMembership.SeatId.HasValue)
                {
                    var seat = await _dbContext.Seats.FindAsync(new object[] { newMembership.SeatId.Value }, cancellationToken);
                    if (seat != null)
                    {
                        var today = DateTime.UtcNow.Date;
                        if (newMembership.StartDate.Date <= today && newMembership.EndDate.Date >= today)
                        {
                            seat.Status = "Occupied";
                        }
                        seat.ModifiedDate = DateTime.UtcNow;
                        await _dbContext.SaveChangesAsync(cancellationToken);
                    }
                }

                await transaction.CommitAsync(cancellationToken);
                return (true, $"Membership renewed successfully from {startDate:dd-MMM-yyyy} to {endDate:dd-MMM-yyyy}.", newMembership);
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync(cancellationToken);
                return (false, $"Failed to renew membership: {ex.Message}", null);
            }
        }

        public async Task<(bool IsSuccess, string Message)> CancelMembershipAsync(
            int membershipId,
            string? cancellationReason = null,
            CancellationToken cancellationToken = default)
        {
            var membership = await _dbContext.StudentMemberships.FindAsync(new object[] { membershipId }, cancellationToken);
            if (membership == null)
            {
                return (false, "Membership not found.");
            }

            if (membership.Status == "Cancelled")
            {
                return (false, "Membership is already cancelled.");
            }

            using var transaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken);
            try
            {
                membership.Status = "Cancelled";
                membership.ModifiedDate = DateTime.UtcNow;
                if (!string.IsNullOrWhiteSpace(cancellationReason))
                {
                    membership.Remarks = string.IsNullOrWhiteSpace(membership.Remarks)
                        ? $"Cancellation Reason: {cancellationReason.Trim()}"
                        : $"{membership.Remarks} | Cancellation Reason: {cancellationReason.Trim()}";
                }

                await _dbContext.SaveChangesAsync(cancellationToken);

                // Release seat if assigned and no other active membership occupies it
                if (membership.SeatId.HasValue)
                {
                    var today = DateTime.UtcNow.Date;
                    bool seatStillOccupied = await _dbContext.StudentMemberships.AnyAsync(
                        m => m.MembershipId != membershipId &&
                             m.SeatId == membership.SeatId.Value &&
                             m.Status == "Active" &&
                             m.StartDate.Date <= today &&
                             m.EndDate.Date >= today,
                        cancellationToken);

                    if (!seatStillOccupied)
                    {
                        var seat = await _dbContext.Seats.FindAsync(new object[] { membership.SeatId.Value }, cancellationToken);
                        if (seat != null && seat.Status == "Occupied" && seat.IsActive)
                        {
                            seat.Status = "Available";
                            seat.ModifiedDate = DateTime.UtcNow;
                            await _dbContext.SaveChangesAsync(cancellationToken);
                        }
                    }
                }

                await transaction.CommitAsync(cancellationToken);
                return (true, "Membership cancelled successfully and seat made available.");
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync(cancellationToken);
                return (false, $"Failed to cancel membership: {ex.Message}");
            }
        }

        public async Task<int> ExpireMembershipsAsync(CancellationToken cancellationToken = default)
        {
            var today = DateTime.UtcNow.Date;
            var expiredMemberships = await _dbContext.StudentMemberships
                .Where(m => m.Status == "Active" && m.EndDate.Date < today)
                .ToListAsync(cancellationToken);

            if (expiredMemberships.Count == 0)
            {
                return 0;
            }

            var affectedSeatIds = expiredMemberships
                .Where(m => m.SeatId.HasValue)
                .Select(m => m.SeatId!.Value)
                .Distinct()
                .ToList();

            foreach (var m in expiredMemberships)
            {
                m.Status = "Expired";
                m.ModifiedDate = DateTime.UtcNow;
            }

            // Free seats that are no longer occupied by any active membership
            foreach (var seatId in affectedSeatIds)
            {
                bool stillOccupied = await _dbContext.StudentMemberships.AnyAsync(
                    m => m.SeatId == seatId &&
                         m.Status == "Active" &&
                         m.StartDate.Date <= today &&
                         m.EndDate.Date >= today &&
                         !expiredMemberships.Select(e => e.MembershipId).Contains(m.MembershipId),
                    cancellationToken);

                if (!stillOccupied)
                {
                    var seat = await _dbContext.Seats.FindAsync(new object[] { seatId }, cancellationToken);
                    if (seat != null && seat.Status == "Occupied" && seat.IsActive)
                    {
                        seat.Status = "Available";
                        seat.ModifiedDate = DateTime.UtcNow;
                    }
                }
            }

            await _dbContext.SaveChangesAsync(cancellationToken);
            return expiredMemberships.Count;
        }
    }
}
