using System;

namespace Laibrary_Payment_App.Models
{
    public class Payment
    {
        public int PaymentId { get; set; }
        public string PaymentNo { get; set; } = string.Empty;

        public int InvoiceId { get; set; }
        public int StudentId { get; set; }

        public DateTime PaymentDate { get; set; }
        public decimal Amount { get; set; }

        public string PaymentMode { get; set; } = PaymentModes.Cash;
        public string? ReferenceNo { get; set; }
        public string? Remarks { get; set; }

        public string ReceivedBy { get; set; } = "System";

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime? ModifiedDate { get; set; }

        public string Status { get; set; } = "Active"; // Active, Cancelled

        public string? CancelledBy { get; set; }
        public DateTime? CancelledDate { get; set; }

        // Navigation properties
        public Invoice Invoice { get; set; } = null!;
        public StudentMaster Student { get; set; } = null!;
    }
}
