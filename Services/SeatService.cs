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
    public interface ISeatService
    {
        Task<List<SeatMaster>> GetAllSeatsAsync(bool includeInactive = true, string? area = null, string? status = null, string? searchTerm = null, CancellationToken cancellationToken = default);
        Task<SeatMaster?> GetSeatByIdAsync(int seatId, CancellationToken cancellationToken = default);
        Task<List<SeatMaster>> GetAvailableSeatsForPeriodAsync(DateTime startDate, DateTime endDate, int? currentMembershipId = null, CancellationToken cancellationToken = default);
        Task<(bool IsSuccess, string Message, SeatMaster? Seat)> AddSeatAsync(SeatMaster seat, CancellationToken cancellationToken = default);
        Task<(bool IsSuccess, string Message)> UpdateSeatAsync(SeatMaster seat, CancellationToken cancellationToken = default);
        Task<(bool IsSuccess, string Message, bool NewStatus)> ToggleActiveStatusAsync(int seatId, CancellationToken cancellationToken = default);
        Task<(bool IsSuccess, string Message)> UpdateSeatStatusAsync(int seatId, string newStatus, CancellationToken cancellationToken = default);
        Task<List<string>> GetAllAreasAsync(CancellationToken cancellationToken = default);
        Task<int> SyncSeatStatusesWithActiveMembershipsAsync(CancellationToken cancellationToken = default);
    }

    public class SeatService : ISeatService
    {
        private readonly LibraryDbContext _dbContext;

        public static readonly string[] ValidStatuses = { "Available", "Occupied", "Maintenance", "Inactive" };

        public SeatService(LibraryDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<SeatMaster>> GetAllSeatsAsync(bool includeInactive = true, string? area = null, string? status = null, string? searchTerm = null, CancellationToken cancellationToken = default)
        {
            var query = _dbContext.Seats.AsNoTracking().AsQueryable();

            if (!includeInactive)
            {
                query = query.Where(s => s.IsActive);
            }

            if (!string.IsNullOrWhiteSpace(area) && area != "All Areas")
            {
                query = query.Where(s => s.Area == area);
            }

            if (!string.IsNullOrWhiteSpace(status) && status != "All Statuses")
            {
                query = query.Where(s => s.Status == status);
            }

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                var term = searchTerm.Trim().ToLower();
                query = query.Where(s => s.SeatNumber.ToLower().Contains(term) || (s.Area != null && s.Area.ToLower().Contains(term)));
            }

            return await query.OrderBy(s => s.SeatNumber).ToListAsync(cancellationToken);
        }

        public async Task<SeatMaster?> GetSeatByIdAsync(int seatId, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Seats
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.SeatId == seatId, cancellationToken);
        }

        /// <summary>
        /// Retrieves active seats that are NOT under maintenance, NOT inactive,
        /// and have no conflicting active student membership during [startDate, endDate].
        /// </summary>
        public async Task<List<SeatMaster>> GetAvailableSeatsForPeriodAsync(DateTime startDate, DateTime endDate, int? currentMembershipId = null, CancellationToken cancellationToken = default)
        {
            var start = startDate.Date;
            var end = endDate.Date;

            // Find all seat IDs that have active memberships overlapping with the period
            var conflictingSeatIds = await _dbContext.StudentMemberships
                .AsNoTracking()
                .Where(m => m.SeatId.HasValue &&
                            m.Status == "Active" &&
                            (!currentMembershipId.HasValue || m.MembershipId != currentMembershipId.Value) &&
                            m.StartDate.Date <= end &&
                            m.EndDate.Date >= start)
                .Select(m => m.SeatId!.Value)
                .Distinct()
                .ToListAsync(cancellationToken);

            // Active seats excluding Maintenance and Inactive statuses, and excluding conflicting seats
            return await _dbContext.Seats
                .AsNoTracking()
                .Where(s => s.IsActive &&
                            s.Status != "Maintenance" &&
                            s.Status != "Inactive" &&
                            !conflictingSeatIds.Contains(s.SeatId))
                .OrderBy(s => s.SeatNumber)
                .ToListAsync(cancellationToken);
        }

        public async Task<(bool IsSuccess, string Message, SeatMaster? Seat)> AddSeatAsync(SeatMaster seat, CancellationToken cancellationToken = default)
        {
            var validation = await ValidateSeatAsync(seat, isNew: true, cancellationToken);
            if (!validation.IsValid)
            {
                return (false, validation.Message, null);
            }

            seat.SeatNumber = seat.SeatNumber.Trim().ToUpperInvariant();
            seat.Area = string.IsNullOrWhiteSpace(seat.Area) ? null : seat.Area.Trim();
            seat.CreatedDate = DateTime.UtcNow;
            seat.ModifiedDate = null;

            try
            {
                _dbContext.Seats.Add(seat);
                await _dbContext.SaveChangesAsync(cancellationToken);
                return (true, $"Seat '{seat.SeatNumber}' added successfully.", seat);
            }
            catch (Exception ex)
            {
                return (false, $"Error adding seat: {ex.Message}", null);
            }
        }

        public async Task<(bool IsSuccess, string Message)> UpdateSeatAsync(SeatMaster seat, CancellationToken cancellationToken = default)
        {
            var existing = await _dbContext.Seats.FindAsync(new object[] { seat.SeatId }, cancellationToken);
            if (existing == null)
            {
                return (false, "Seat not found.");
            }

            var validation = await ValidateSeatAsync(seat, isNew: false, cancellationToken);
            if (!validation.IsValid)
            {
                return (false, validation.Message);
            }

            // If changing to Maintenance or Inactive, verify it's not currently occupied by an active membership
            if (seat.Status is "Maintenance" or "Inactive")
            {
                bool hasActiveMembership = await _dbContext.StudentMemberships.AnyAsync(
                    m => m.SeatId == seat.SeatId && m.Status == "Active",
                    cancellationToken);

                if (hasActiveMembership)
                {
                    return (false, $"Cannot set seat '{existing.SeatNumber}' to '{seat.Status}' while it is assigned to an active membership.");
                }
            }

            existing.SeatNumber = seat.SeatNumber.Trim().ToUpperInvariant();
            existing.Area = string.IsNullOrWhiteSpace(seat.Area) ? null : seat.Area.Trim();
            existing.Status = seat.Status;
            existing.IsActive = seat.IsActive;
            existing.ModifiedDate = DateTime.UtcNow;

            try
            {
                await _dbContext.SaveChangesAsync(cancellationToken);
                return (true, $"Seat '{existing.SeatNumber}' updated successfully.");
            }
            catch (Exception ex)
            {
                return (false, $"Error updating seat: {ex.Message}");
            }
        }

        public async Task<(bool IsSuccess, string Message, bool NewStatus)> ToggleActiveStatusAsync(int seatId, CancellationToken cancellationToken = default)
        {
            var seat = await _dbContext.Seats.FindAsync(new object[] { seatId }, cancellationToken);
            if (seat == null)
            {
                return (false, "Seat not found.", false);
            }

            if (seat.IsActive)
            {
                // Verify not assigned to an active membership before deactivating
                bool hasActiveMembership = await _dbContext.StudentMemberships.AnyAsync(
                    m => m.SeatId == seatId && m.Status == "Active",
                    cancellationToken);

                if (hasActiveMembership)
                {
                    return (false, $"Cannot deactivate seat '{seat.SeatNumber}' because it is currently assigned to an active student membership.", seat.IsActive);
                }
            }

            seat.IsActive = !seat.IsActive;
            if (!seat.IsActive && seat.Status == "Available")
            {
                seat.Status = "Inactive";
            }
            else if (seat.IsActive && seat.Status == "Inactive")
            {
                seat.Status = "Available";
            }

            seat.ModifiedDate = DateTime.UtcNow;

            try
            {
                await _dbContext.SaveChangesAsync(cancellationToken);
                var statusText = seat.IsActive ? "activated" : "deactivated";
                return (true, $"Seat '{seat.SeatNumber}' has been {statusText}.", seat.IsActive);
            }
            catch (Exception ex)
            {
                return (false, $"Error updating seat active status: {ex.Message}", !seat.IsActive);
            }
        }

        public async Task<(bool IsSuccess, string Message)> UpdateSeatStatusAsync(int seatId, string newStatus, CancellationToken cancellationToken = default)
        {
            if (!ValidStatuses.Contains(newStatus))
            {
                return (false, $"Invalid seat status '{newStatus}'.");
            }

            var seat = await _dbContext.Seats.FindAsync(new object[] { seatId }, cancellationToken);
            if (seat == null)
            {
                return (false, "Seat not found.");
            }

            if (newStatus is "Maintenance" or "Inactive")
            {
                bool hasActive = await _dbContext.StudentMemberships.AnyAsync(
                    m => m.SeatId == seatId && m.Status == "Active",
                    cancellationToken);

                if (hasActive)
                {
                    return (false, $"Cannot change seat '{seat.SeatNumber}' to '{newStatus}' because it is assigned to an active membership.");
                }
            }

            seat.Status = newStatus;
            seat.ModifiedDate = DateTime.UtcNow;

            try
            {
                await _dbContext.SaveChangesAsync(cancellationToken);
                return (true, $"Seat '{seat.SeatNumber}' status updated to '{newStatus}'.");
            }
            catch (Exception ex)
            {
                return (false, $"Error updating seat status: {ex.Message}");
            }
        }

        public async Task<List<string>> GetAllAreasAsync(CancellationToken cancellationToken = default)
        {
            return await _dbContext.Seats
                .AsNoTracking()
                .Where(s => !string.IsNullOrWhiteSpace(s.Area))
                .Select(s => s.Area!)
                .Distinct()
                .OrderBy(a => a)
                .ToListAsync(cancellationToken);
        }

        public async Task<int> SyncSeatStatusesWithActiveMembershipsAsync(CancellationToken cancellationToken = default)
        {
            var now = DateTime.UtcNow.Date;
            var occupiedSeatIds = await _dbContext.StudentMemberships
                .AsNoTracking()
                .Where(m => m.SeatId.HasValue && m.Status == "Active" && m.StartDate.Date <= now && m.EndDate.Date >= now)
                .Select(m => m.SeatId!.Value)
                .Distinct()
                .ToListAsync(cancellationToken);

            var seats = await _dbContext.Seats.ToListAsync(cancellationToken);
            int updatedCount = 0;

            foreach (var seat in seats)
            {
                if (occupiedSeatIds.Contains(seat.SeatId))
                {
                    if (seat.Status != "Occupied")
                    {
                        seat.Status = "Occupied";
                        seat.ModifiedDate = DateTime.UtcNow;
                        updatedCount++;
                    }
                }
                else
                {
                    if (seat.Status == "Occupied")
                    {
                        seat.Status = "Available";
                        seat.ModifiedDate = DateTime.UtcNow;
                        updatedCount++;
                    }
                }
            }

            if (updatedCount > 0)
            {
                await _dbContext.SaveChangesAsync(cancellationToken);
            }

            return updatedCount;
        }

        private async Task<(bool IsValid, string Message)> ValidateSeatAsync(SeatMaster seat, bool isNew, CancellationToken cancellationToken)
        {
            if (seat == null)
            {
                return (false, "Seat details are required.");
            }

            if (string.IsNullOrWhiteSpace(seat.SeatNumber))
            {
                return (false, "Seat number is required.");
            }

            if (seat.SeatNumber.Trim().Length > 20)
            {
                return (false, "Seat number cannot exceed 20 characters.");
            }

            if (!string.IsNullOrWhiteSpace(seat.Area) && seat.Area.Trim().Length > 50)
            {
                return (false, "Area cannot exceed 50 characters.");
            }

            if (!ValidStatuses.Contains(seat.Status))
            {
                return (false, $"Invalid seat status '{seat.Status}'. Allowed: {string.Join(", ", ValidStatuses)}.");
            }

            var trimmedNum = seat.SeatNumber.Trim().ToUpperInvariant();
            bool duplicate = await _dbContext.Seats.AnyAsync(
                s => (isNew || s.SeatId != seat.SeatId) && s.SeatNumber.ToUpper() == trimmedNum,
                cancellationToken);

            if (duplicate)
            {
                return (false, $"Seat number '{seat.SeatNumber.Trim()}' already exists.");
            }

            return (true, string.Empty);
        }
    }
}
