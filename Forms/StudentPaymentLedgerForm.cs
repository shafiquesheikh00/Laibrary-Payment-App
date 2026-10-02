using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore;
using Laibrary_Payment_App.Data;
using Laibrary_Payment_App.Models;
using Laibrary_Payment_App.Services;

namespace Laibrary_Payment_App.Forms
{
    public partial class StudentPaymentLedgerForm : Form
    {
        private readonly int _studentId;
        private readonly LibraryDbContext _dbContext;
        private readonly IPaymentService _paymentService;

        public StudentPaymentLedgerForm(int studentId)
        {
            InitializeComponent();
            _studentId = studentId;
            _dbContext = new LibraryDbContext();
            _paymentService = new PaymentService(_dbContext);

            this.Load += StudentPaymentLedgerForm_Load;
        }

        public StudentPaymentLedgerForm(int studentId, IPaymentService paymentService, LibraryDbContext dbContext)
        {
            InitializeComponent();
            _studentId = studentId;
            _dbContext = dbContext ?? new LibraryDbContext();
            _paymentService = paymentService ?? new PaymentService(_dbContext);

            this.Load += StudentPaymentLedgerForm_Load;
        }

        private async void StudentPaymentLedgerForm_Load(object? sender, EventArgs e)
        {
            SetupGridColumns();
            await LoadStudentInfoAndLedgerAsync();
        }

        private void SetupGridColumns()
        {
            dgvLedger.AutoGenerateColumns = false;
            dgvLedger.Columns.Clear();

            dgvLedger.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(StudentPaymentLedgerItemDto.Date),
                HeaderText = "Date",
                Width = 100,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "dd-MMM-yyyy" },
                ReadOnly = true
            });

            dgvLedger.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(StudentPaymentLedgerItemDto.InvoiceNo),
                HeaderText = "Invoice",
                Width = 135,
                ReadOnly = true
            });

            dgvLedger.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(StudentPaymentLedgerItemDto.InvoiceTotal),
                HeaderText = "Invoice Total",
                Width = 110,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "C2", Alignment = DataGridViewContentAlignment.MiddleRight },
                ReadOnly = true
            });

            dgvLedger.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(StudentPaymentLedgerItemDto.PaymentNo),
                HeaderText = "Payment No",
                Width = 140,
                ReadOnly = true
            });

            dgvLedger.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(StudentPaymentLedgerItemDto.PaymentAmount),
                HeaderText = "Payment",
                Width = 110,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "C2", Alignment = DataGridViewContentAlignment.MiddleRight, ForeColor = Color.FromArgb(22, 163, 74), Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold) },
                ReadOnly = true
            });

            dgvLedger.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(StudentPaymentLedgerItemDto.RunningBalance),
                HeaderText = "Running Balance",
                Width = 120,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "C2", Alignment = DataGridViewContentAlignment.MiddleRight, ForeColor = Color.FromArgb(185, 28, 28), Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold) },
                ReadOnly = true
            });

            dgvLedger.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(StudentPaymentLedgerItemDto.PaymentMode),
                HeaderText = "Payment Mode",
                Width = 105,
                ReadOnly = true
            });

            dgvLedger.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(StudentPaymentLedgerItemDto.ReferenceNo),
                HeaderText = "Reference No",
                Width = 120,
                ReadOnly = true
            });

            dgvLedger.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(StudentPaymentLedgerItemDto.Status),
                HeaderText = "Status",
                Width = 85,
                ReadOnly = true
            });

            dgvLedger.CellFormatting += DgvLedger_CellFormatting;
        }

        private void DgvLedger_CellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= dgvLedger.Rows.Count) return;

            var row = dgvLedger.Rows[e.RowIndex];
            if (row.DataBoundItem is StudentPaymentLedgerItemDto item)
            {
                if (item.Status == "Cancelled")
                {
                    row.DefaultCellStyle.ForeColor = Color.FromArgb(148, 163, 184);
                    if (dgvLedger.Columns[e.ColumnIndex].DataPropertyName == nameof(StudentPaymentLedgerItemDto.Status))
                    {
                        e.CellStyle.ForeColor = Color.FromArgb(220, 38, 38);
                        e.CellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
                    }
                }
                else if (dgvLedger.Columns[e.ColumnIndex].DataPropertyName == nameof(StudentPaymentLedgerItemDto.Status))
                {
                    e.CellStyle.ForeColor = Color.FromArgb(22, 163, 74);
                    e.CellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
                }
            }
        }

        private async Task LoadStudentInfoAndLedgerAsync()
        {
            try
            {
                var student = await _dbContext.Students
                    .AsNoTracking()
                    .FirstOrDefaultAsync(s => s.StudentId == _studentId);

                if (student != null)
                {
                    lblStudentName.Text = student.FullName;
                    lblStudentDetails.Text = $"Student Code: {student.StudentCode}  |  Mobile: {student.MobileNo}  |  Email: {student.Email ?? "-"}";
                }

                // Invoices summary
                var invoices = await _dbContext.Invoices
                    .AsNoTracking()
                    .Where(i => i.StudentId == _studentId && i.Status != "Cancelled")
                    .ToListAsync();

                decimal totalInvoiced = invoices.Sum(i => i.GrandTotal);

                // Payments
                var payments = await _dbContext.Payments
                    .AsNoTracking()
                    .Where(p => p.StudentId == _studentId && p.Status == "Active")
                    .ToListAsync();

                decimal totalPaid = payments.Sum(p => p.Amount);
                decimal outstanding = Math.Max(0m, totalInvoiced - totalPaid);

                lblInvoicedVal.Text = $"₹{totalInvoiced:N2}";
                lblPaidVal.Text = $"₹{totalPaid:N2}";
                lblOutstandingVal.Text = $"₹{outstanding:N2}";

                // Ledger items
                var ledgerItems = await _paymentService.GetStudentPaymentHistoryAsync(_studentId);
                dgvLedger.DataSource = ledgerItems;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load student ledger: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnReceivePayment_Click(object? sender, EventArgs e)
        {
            using var payForm = new PaymentForm(initialInvoiceId: null, initialStudentId: _studentId);
            payForm.ShowDialog(this);
            await LoadStudentInfoAndLedgerAsync();
        }

        private void btnViewReceipt_Click(object? sender, EventArgs e)
        {
            if (dgvLedger.SelectedRows.Count > 0 &&
                dgvLedger.SelectedRows[0].DataBoundItem is StudentPaymentLedgerItemDto item)
            {
                using var receiptForm = new PaymentReceiptForm(item.PaymentId);
                receiptForm.ShowDialog(this);
            }
            else
            {
                MessageBox.Show("Please select a payment record from the ledger to view receipt.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void dgvLedger_CellDoubleClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                btnViewReceipt_Click(sender, e);
            }
        }

        private void btnClose_Click(object? sender, EventArgs e)
        {
            this.Close();
        }
    }
}
