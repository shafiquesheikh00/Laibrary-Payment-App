using System.Collections.Generic;
using Laibrary_Payment_App.Reports;

namespace Laibrary_Payment_App.Models
{
    public class DashboardSummaryDto
    {
        // Student metrics
        public int TotalStudents { get; set; }
        public int ActiveStudents { get; set; }
        public int InactiveStudents { get; set; }

        // Membership metrics
        public int ActiveMemberships { get; set; }
        public int ExpiringMemberships { get; set; }
        public int ExpiredMemberships { get; set; }

        // Seat metrics
        public int TotalActiveSeats { get; set; }
        public int AvailableSeats { get; set; }
        public int OccupiedSeats { get; set; }
        public int MaintenanceSeats { get; set; }

        // Billing statistics
        public int TodayInvoicesCount { get; set; }
        public decimal TodayBillingAmount { get; set; }
        public decimal CurrentOutstandingAmount { get; set; }
        public int TotalInvoicesCount { get; set; }

        // Payment & Collection statistics
        public decimal TodayCollectionAmount { get; set; }
        public decimal ThisMonthCollectionAmount { get; set; }
        public int TodayPaymentCount { get; set; }
        public int UnpaidInvoicesCount { get; set; }
        public int PartiallyPaidInvoicesCount { get; set; }
        public int PaidInvoicesCount { get; set; }

        // Dashboard Expiring Memberships preview table
        public List<DashboardExpiringMembershipDto> ExpiringMembershipsList { get; set; } = new List<DashboardExpiringMembershipDto>();
    }
}
