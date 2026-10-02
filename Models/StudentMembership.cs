namespace Laibrary_Payment_App.Models
{
    public class StudentMembership
    {
        public int MembershipId { get; set; }
        public int StudentId { get; set; }
        public int PlanId { get; set; }
        public int? SeatId { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public decimal ActualFee { get; set; }
        public decimal DiscountAmount { get; set; } = 0.00m;
        public decimal FinalFee { get; set; }

        public string Status { get; set; } = "Active"; // Active, Expired, Cancelled, Pending
        public string? Remarks { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime? ModifiedDate { get; set; }

        // Navigation properties
        public StudentMaster Student { get; set; } = null!;
        public MembershipPlan Plan { get; set; } = null!;
        public SeatMaster? Seat { get; set; }
        public ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();
    }
}
