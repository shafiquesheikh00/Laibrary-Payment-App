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
    public interface IMembershipPlanService
    {
        Task<List<MembershipPlan>> GetAllPlansAsync(bool includeInactive = true, string? searchTerm = null, CancellationToken cancellationToken = default);
        Task<MembershipPlan?> GetPlanByIdAsync(int planId, CancellationToken cancellationToken = default);
        Task<(bool IsSuccess, string Message, MembershipPlan? Plan)> AddPlanAsync(MembershipPlan plan, CancellationToken cancellationToken = default);
        Task<(bool IsSuccess, string Message)> UpdatePlanAsync(MembershipPlan plan, CancellationToken cancellationToken = default);
        Task<(bool IsSuccess, string Message, bool NewStatus)> ToggleActiveStatusAsync(int planId, CancellationToken cancellationToken = default);
    }

    public class MembershipPlanService : IMembershipPlanService
    {
        private readonly LibraryDbContext _dbContext;

        public MembershipPlanService(LibraryDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<MembershipPlan>> GetAllPlansAsync(bool includeInactive = true, string? searchTerm = null, CancellationToken cancellationToken = default)
        {
            var query = _dbContext.MembershipPlans.AsNoTracking().AsQueryable();

            if (!includeInactive)
            {
                query = query.Where(p => p.IsActive);
            }

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                var term = searchTerm.Trim().ToLower();
                query = query.Where(p => p.PlanName.ToLower().Contains(term) || p.DurationUnit.ToLower().Contains(term));
            }

            return await query.OrderBy(p => p.PlanName).ToListAsync(cancellationToken);
        }

        public async Task<MembershipPlan?> GetPlanByIdAsync(int planId, CancellationToken cancellationToken = default)
        {
            return await _dbContext.MembershipPlans
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.PlanId == planId, cancellationToken);
        }

        public async Task<(bool IsSuccess, string Message, MembershipPlan? Plan)> AddPlanAsync(MembershipPlan plan, CancellationToken cancellationToken = default)
        {
            var validation = await ValidatePlanAsync(plan, isNew: true, cancellationToken);
            if (!validation.IsValid)
            {
                return (false, validation.Message, null);
            }

            plan.PlanName = plan.PlanName.Trim();
            plan.DurationUnit = plan.DurationUnit.Trim();
            plan.CreatedDate = DateTime.UtcNow;
            plan.ModifiedDate = null;

            try
            {
                _dbContext.MembershipPlans.Add(plan);
                await _dbContext.SaveChangesAsync(cancellationToken);
                return (true, $"Membership plan '{plan.PlanName}' added successfully.", plan);
            }
            catch (Exception ex)
            {
                return (false, $"Error saving membership plan: {ex.Message}", null);
            }
        }

        public async Task<(bool IsSuccess, string Message)> UpdatePlanAsync(MembershipPlan plan, CancellationToken cancellationToken = default)
        {
            var existing = await _dbContext.MembershipPlans.FindAsync(new object[] { plan.PlanId }, cancellationToken);
            if (existing == null)
            {
                return (false, "Membership plan not found.");
            }

            var validation = await ValidatePlanAsync(plan, isNew: false, cancellationToken);
            if (!validation.IsValid)
            {
                return (false, validation.Message);
            }

            existing.PlanName = plan.PlanName.Trim();
            existing.DurationValue = plan.DurationValue;
            existing.DurationUnit = plan.DurationUnit.Trim();
            existing.Fee = plan.Fee;
            existing.IsActive = plan.IsActive;
            existing.ModifiedDate = DateTime.UtcNow;

            try
            {
                await _dbContext.SaveChangesAsync(cancellationToken);
                return (true, $"Membership plan '{existing.PlanName}' updated successfully.");
            }
            catch (Exception ex)
            {
                return (false, $"Error updating membership plan: {ex.Message}");
            }
        }

        public async Task<(bool IsSuccess, string Message, bool NewStatus)> ToggleActiveStatusAsync(int planId, CancellationToken cancellationToken = default)
        {
            var plan = await _dbContext.MembershipPlans.FindAsync(new object[] { planId }, cancellationToken);
            if (plan == null)
            {
                return (false, "Membership plan not found.", false);
            }

            // If activating, verify no active duplicate plan name exists
            if (!plan.IsActive)
            {
                bool duplicateActive = await _dbContext.MembershipPlans.AnyAsync(
                    p => p.PlanId != planId && p.PlanName.ToLower() == plan.PlanName.ToLower() && p.IsActive,
                    cancellationToken);

                if (duplicateActive)
                {
                    return (false, $"An active plan with the name '{plan.PlanName}' already exists.", false);
                }
            }

            plan.IsActive = !plan.IsActive;
            plan.ModifiedDate = DateTime.UtcNow;

            try
            {
                await _dbContext.SaveChangesAsync(cancellationToken);
                var statusText = plan.IsActive ? "activated" : "deactivated";
                return (true, $"Membership plan '{plan.PlanName}' has been {statusText}.", plan.IsActive);
            }
            catch (Exception ex)
            {
                return (false, $"Error updating plan status: {ex.Message}", !plan.IsActive);
            }
        }

        private async Task<(bool IsValid, string Message)> ValidatePlanAsync(MembershipPlan plan, bool isNew, CancellationToken cancellationToken)
        {
            if (plan == null)
            {
                return (false, "Plan details are required.");
            }

            if (string.IsNullOrWhiteSpace(plan.PlanName))
            {
                return (false, "Plan name is required.");
            }

            if (plan.PlanName.Trim().Length > 100)
            {
                return (false, "Plan name cannot exceed 100 characters.");
            }

            if (plan.DurationValue <= 0)
            {
                return (false, "Duration value must be greater than 0.");
            }

            if (string.IsNullOrWhiteSpace(plan.DurationUnit))
            {
                return (false, "Duration unit is required.");
            }

            var validUnits = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
            {
                "Day", "Week", "Month", "Quarter", "Half-Yearly", "Year"
            };

            if (!validUnits.Contains(plan.DurationUnit.Trim()))
            {
                return (false, $"Duration unit '{plan.DurationUnit}' is invalid. Allowed units: Day, Week, Month, Quarter, Half-Yearly, Year.");
            }

            if (plan.Fee < 0)
            {
                return (false, "Fee cannot be negative.");
            }

            // Duplicate active plan name check
            var trimmedName = plan.PlanName.Trim().ToLower();
            bool duplicateActive = await _dbContext.MembershipPlans.AnyAsync(
                p => (isNew || p.PlanId != plan.PlanId) &&
                     p.PlanName.ToLower() == trimmedName &&
                     p.IsActive,
                cancellationToken);

            if (duplicateActive && plan.IsActive)
            {
                return (false, $"An active plan with the name '{plan.PlanName.Trim()}' already exists.");
            }

            return (true, string.Empty);
        }
    }
}
