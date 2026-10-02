namespace Laibrary_Payment_App.Models
{
    public class SeatMaster
    {
        public int SeatId { get; set; }
        public string SeatNumber { get; set; } = string.Empty;
        public string? Area { get; set; }
        public string Status { get; set; } = "Available"; // Available, Occupied, Reserved, Maintenance
        public bool IsActive { get; set; } = true;
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime? ModifiedDate { get; set; }

        // Navigation property
        public ICollection<StudentMembership> StudentMemberships { get; set; } = new List<StudentMembership>();
    }
}
