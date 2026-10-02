using System;
using System.Collections.Generic;

namespace Laibrary_Payment_App.Models
{
    public class Invoice
    {
        public int InvoiceId { get; set; }
        public string InvoiceNo { get; set; } = string.Empty;
        public int StudentId { get; set; }
        public int? MembershipId { get; set; }

        public DateTime InvoiceDate { get; set; }
        public DateTime DueDate { get; set; }

        public decimal SubTotal { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal AdditionalCharges { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal CurrentCharges { get; set; }
        public decimal PreviousOutstanding { get; set; }
        public decimal GrandTotal { get; set; }

        public decimal PaidAmount { get; set; }
        public decimal BalanceAmount { get; set; }

        public string Status { get; set; } = "Unpaid"; // Draft, Unpaid, PartiallyPaid, Paid, Cancelled

        public string CreatedBy { get; set; } = "System";
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }

        // Navigation properties
        public StudentMaster Student { get; set; } = null!;
        public StudentMembership? Membership { get; set; }
        public ICollection<InvoiceDetail> InvoiceDetails { get; set; } = new List<InvoiceDetail>();
        public ICollection<Payment> Payments { get; set; } = new List<Payment>();
    }
}
