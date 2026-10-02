namespace Laibrary_Payment_App.Models
{
    public class MembershipPlan
    {
        public int PlanId { get; set; }
        public string PlanName { get; set; } = string.Empty;
        public int DurationValue { get; set; }
        public string DurationUnit { get; set; } = string.Empty; // Day, Week, Month, Quarter, Year
        public decimal Fee { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime? ModifiedDate { get; set; }

        // Navigation property
        public ICollection<StudentMembership> StudentMemberships { get; set; } = new List<StudentMembership>();
    }
}
