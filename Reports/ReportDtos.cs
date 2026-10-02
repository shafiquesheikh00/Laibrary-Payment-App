using System;
using System.Collections.Generic;

namespace Laibrary_Payment_App.Reports
{
    // Filter DTOs
    public class DailyCollectionFilterDto
    {
        public DateTime Date { get; set; } = DateTime.Today;
        public string? PaymentMode { get; set; }
    }

    public class DateWiseCollectionFilterDto
    {
        public DateTime FromDate { get; set; } = DateTime.Today.AddDays(-30);
        public DateTime ToDate { get; set; } = DateTime.Today;
    }

    public class MonthlyCollectionFilterDto
    {
        public int Year { get; set; } = DateTime.Today.Year;
        public int? Month { get; set; } = DateTime.Today.Month;
    }

    public class OutstandingFilterDto
    {
        public DateTime? AsOnDate { get; set; }
        public int? StudentId { get; set; }
        public string? Status { get; set; } // "All", "Unpaid", "PartiallyPaid"
        public string? SearchText { get; set; }
    }

    public class StudentLedgerFilterDto
    {
        public int StudentId { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
    }

    public class PaymentHistoryReportFilterDto
    {
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public int? StudentId { get; set; }
        public string? PaymentMode { get; set; }
        public string? Status { get; set; } // "All", "Active", "Cancelled"
    }

    public class InvoiceRegisterFilterDto
    {
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public int? StudentId { get; set; }
        public string? Status { get; set; }
    }

    public class MembershipRegisterFilterDto
    {
        public DateTime? StartDateFrom { get; set; }
        public DateTime? StartDateTo { get; set; }
        public int? PlanId { get; set; }
        public int? SeatId { get; set; }
        public string? Status { get; set; } // "All", "Active", "Expired", "Cancelled"
    }

    public class MembershipExpiryFilterDto
    {
        public int? ExpiryWithinDays { get; set; } = 7;
        public DateTime? ExpiryFrom { get; set; }
        public DateTime? ExpiryTo { get; set; }
    }

    public class SeatOccupancyFilterDto
    {
        public string? Status { get; set; } // "All", "Occupied", "Available", "Maintenance"
        public string? Area { get; set; }
    }

    public class StudentRegisterFilterDto
    {
        public string? SearchText { get; set; }
        public string? ActiveStatus { get; set; } // "All", "Active", "Inactive"
    }

    // Result DTOs
    public class DailyCollectionDto
    {
        public string PaymentNo { get; set; } = string.Empty;
        public DateTime PaymentDate { get; set; }
        public string InvoiceNo { get; set; } = string.Empty;
        public string StudentCode { get; set; } = string.Empty;
        public string StudentName { get; set; } = string.Empty;
        public string PaymentMode { get; set; } = string.Empty;
        public string ReferenceNo { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public string ReceivedBy { get; set; } = string.Empty;
        public string Status { get; set; } = "Active";
    }

    public class DateWiseCollectionDto
    {
        public DateTime Date { get; set; }
        public int PaymentCount { get; set; }
        public decimal CashAmount { get; set; }
        public decimal UpiAmount { get; set; }
        public decimal CardAmount { get; set; }
        public decimal BankTransferAmount { get; set; }
        public decimal OtherAmount { get; set; }
        public decimal TotalCollection { get; set; }
    }

    public class MonthlyCollectionDto
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public string MonthName { get; set; } = string.Empty;
        public int PaymentCount { get; set; }
        public decimal CashAmount { get; set; }
        public decimal UpiAmount { get; set; }
        public decimal CardAmount { get; set; }
        public decimal BankTransferAmount { get; set; }
        public decimal OtherAmount { get; set; }
        public decimal TotalCollection { get; set; }
    }

    public class OutstandingDto
    {
        public int StudentId { get; set; }
        public string StudentCode { get; set; } = string.Empty;
        public string StudentName { get; set; } = string.Empty;
        public string Mobile { get; set; } = string.Empty;
        public int InvoiceId { get; set; }
        public string InvoiceNo { get; set; } = string.Empty;
        public DateTime InvoiceDate { get; set; }
        public decimal GrandTotal { get; set; }
        public decimal PaidAmount { get; set; }
        public decimal Balance { get; set; }
        public string Status { get; set; } = string.Empty;
    }

    public class StudentLedgerDto
    {
        public DateTime Date { get; set; }
        public string Type { get; set; } = string.Empty; // "Invoice" or "Payment"
        public string Reference { get; set; } = string.Empty; // InvoiceNo or PaymentNo
        public string Description { get; set; } = string.Empty;
        public decimal Debit { get; set; } // Invoices
        public decimal Credit { get; set; } // Payments
        public decimal RunningBalance { get; set; }
        public string ModeOrStatus { get; set; } = string.Empty;
    }

    public class PaymentHistoryReportDto
    {
        public string PaymentNo { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public string InvoiceNo { get; set; } = string.Empty;
        public string StudentCode { get; set; } = string.Empty;
        public string StudentName { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public string Mode { get; set; } = string.Empty;
        public string Reference { get; set; } = string.Empty;
        public string ReceivedBy { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
    }

    public class InvoiceRegisterDto
    {
        public int InvoiceId { get; set; }
        public string InvoiceNo { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public string StudentCode { get; set; } = string.Empty;
        public string StudentName { get; set; } = string.Empty;
        public string Membership { get; set; } = string.Empty;
        public decimal Subtotal { get; set; }
        public decimal Discount { get; set; }
        public decimal AdditionalCharges { get; set; }
        public decimal Tax { get; set; }
        public decimal CurrentCharges { get; set; }
        public decimal PreviousOutstanding { get; set; }
        public decimal GrandTotal { get; set; }
        public decimal Paid { get; set; }
        public decimal Balance { get; set; }
        public string Status { get; set; } = string.Empty;
    }

    public class MembershipRegisterDto
    {
        public int MembershipId { get; set; }
        public string StudentCode { get; set; } = string.Empty;
        public string StudentName { get; set; } = string.Empty;
        public string PlanName { get; set; } = string.Empty;
        public string SeatNumber { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal Fee { get; set; }
        public decimal Discount { get; set; }
        public decimal FinalFee { get; set; }
        public string Status { get; set; } = string.Empty;
    }

    public class MembershipExpiryDto
    {
        public int MembershipId { get; set; }
        public string StudentName { get; set; } = string.Empty;
        public string Mobile { get; set; } = string.Empty;
        public string PlanName { get; set; } = string.Empty;
        public string SeatNumber { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int DaysRemaining { get; set; }
        public string Status { get; set; } = string.Empty;
    }

    public class SeatOccupancyDto
    {
        public int SeatId { get; set; }
        public string SeatNumber { get; set; } = string.Empty;
        public string Area { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public string StudentName { get; set; } = string.Empty;
        public string StudentCode { get; set; } = string.Empty;
        public string MembershipPlan { get; set; } = string.Empty;
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }

    public class SeatOccupancySummaryDto
    {
        public int TotalSeats { get; set; }
        public int ActiveSeats { get; set; }
        public int OccupiedSeats { get; set; }
        public int AvailableSeats { get; set; }
        public int MaintenanceSeats { get; set; }
        public int InactiveSeats { get; set; }
        public double OccupancyPercentage { get; set; }
        public List<SeatOccupancyDto> Seats { get; set; } = new List<SeatOccupancyDto>();
    }

    public class StudentRegisterDto
    {
        public int StudentId { get; set; }
        public string StudentCode { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Mobile { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string College { get; set; } = string.Empty;
        public string Course { get; set; } = string.Empty;
        public string ActiveStatus { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; }
    }

    public class DashboardExpiringMembershipDto
    {
        public string StudentName { get; set; } = string.Empty;
        public string SeatNumber { get; set; } = string.Empty;
        public string PlanName { get; set; } = string.Empty;
        public DateTime ExpiryDate { get; set; }
        public int DaysRemaining { get; set; }
    }
}
