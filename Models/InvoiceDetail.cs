using System;

namespace Laibrary_Payment_App.Models
{
    public class InvoiceDetail
    {
        public int InvoiceDetailId { get; set; }
        public int InvoiceId { get; set; }

        public string Description { get; set; } = string.Empty;
        public int Quantity { get; set; } = 1;
        public decimal Rate { get; set; }
        public decimal Amount { get; set; }

        // Navigation property
        public Invoice Invoice { get; set; } = null!;
    }
}
