namespace Laibrary_Payment_App.Models
{
    public class StudentMaster
    {
        public int StudentId { get; set; }
        public string StudentCode { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string MobileNo { get; set; } = string.Empty;
        public string? Email { get; set; }
        public string? Address { get; set; }
        public string? CollegeName { get; set; }
        public string? CourseName { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime? ModifiedDate { get; set; }

        // Navigation properties
        public ICollection<StudentMembership> StudentMemberships { get; set; } = new List<StudentMembership>();
        public ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();
        public ICollection<Payment> Payments { get; set; } = new List<Payment>();
    }
}
