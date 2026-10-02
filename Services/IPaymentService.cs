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
    public class OutstandingInvoiceDto
    {
        public int InvoiceId { get; set; }
        public string InvoiceNo { get; set; } = string.Empty;
        public DateTime InvoiceDate { get; set; }
        public DateTime DueDate { get; set; }

        public int StudentId { get; set; }
        public string StudentCode { get; set; } = string.Empty;
        public string StudentName { get; set; } = string.Empty;
        public string MobileNo { get; set; } = string.Empty;

        public int? MembershipId { get; set; }
        public string PlanName { get; set; } = "General / Non-membership";
        public string? SeatNumber { get; set; }

        public decimal GrandTotal { get; set; }
        public decimal PaidAmount { get; set; }
        public decimal BalanceAmount { get; set; }
        public string Status { get; set; } = string.Empty;
    }

    public class InvoiceBalanceDto
    {
        public int InvoiceId { get; set; }
        public string InvoiceNo { get; set; } = string.Empty;
        public decimal GrandTotal { get; set; }
        public decimal TotalPaid { get; set; }
        public decimal OutstandingBalance { get; set; }
        public string Status { get; set; } = string.Empty;
        public bool IsCancelled => string.Equals(Status, "Cancelled", StringComparison.OrdinalIgnoreCase);
    }

    public class CreatePaymentRequestDto
    {
        public int InvoiceId { get; set; }
        public int StudentId { get; set; }
        public DateTime PaymentDate { get; set; } = DateTime.Today;
        public decimal Amount { get; set; }
        public string PaymentMode { get; set; } = PaymentModes.Cash;
        public string? ReferenceNo { get; set; }
        public string? Remarks { get; set; }
        public string ReceivedBy { get; set; } = "System";
    }

    public class PaymentListDto
    {
        public int PaymentId { get; set; }
        public string PaymentNo { get; set; } = string.Empty;
        public DateTime PaymentDate { get; set; }

        public int InvoiceId { get; set; }
        public string InvoiceNo { get; set; } = string.Empty;

        public int StudentId { get; set; }
        public string StudentCode { get; set; } = string.Empty;
        public string StudentName { get; set; } = string.Empty;
        public string MobileNo { get; set; } = string.Empty;

        public decimal Amount { get; set; }
        public string PaymentMode { get; set; } = string.Empty;
        public string? ReferenceNo { get; set; }
        public string? Remarks { get; set; }
        public string ReceivedBy { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty; // Active, Cancelled

        public decimal InvoiceGrandTotal { get; set; }
        public decimal InvoiceBalance { get; set; }
    }

    public class StudentPaymentLedgerItemDto
    {
        public int PaymentId { get; set; }
        public string PaymentNo { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public string InvoiceNo { get; set; } = string.Empty;
        public decimal InvoiceTotal { get; set; }
        public decimal PaymentAmount { get; set; }
        public decimal RunningBalance { get; set; }
        public string PaymentMode { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public string? ReferenceNo { get; set; }
    }

    public class PaymentFilterDto
    {
        public string? SearchText { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public int? StudentId { get; set; }
        public int? InvoiceId { get; set; }
        public string? PaymentMode { get; set; }
        public string? Status { get; set; }
    }

    public interface IPaymentService
    {
        Task<string> GeneratePaymentNumberAsync(DateTime paymentDate, CancellationToken cancellationToken = default);
        Task<List<OutstandingInvoiceDto>> GetOutstandingInvoicesAsync(int? studentId = null, CancellationToken cancellationToken = default);
        Task<InvoiceBalanceDto> GetInvoiceBalanceAsync(int invoiceId, CancellationToken cancellationToken = default);
        Task<(bool Success, string Message, Payment? Payment)> CreatePaymentAsync(CreatePaymentRequestDto request, CancellationToken cancellationToken = default);
        Task<Payment?> GetPaymentByIdAsync(int paymentId, CancellationToken cancellationToken = default);
        Task<List<PaymentListDto>> GetPaymentHistoryAsync(PaymentFilterDto filter, CancellationToken cancellationToken = default);
        Task<List<StudentPaymentLedgerItemDto>> GetStudentPaymentHistoryAsync(int studentId, CancellationToken cancellationToken = default);
        Task<List<PaymentListDto>> GetInvoicePaymentsAsync(int invoiceId, CancellationToken cancellationToken = default);
        Task<(bool Success, string Message)> CancelPaymentAsync(int paymentId, string cancelledBy, string? reason = null, CancellationToken cancellationToken = default);
    }
}
