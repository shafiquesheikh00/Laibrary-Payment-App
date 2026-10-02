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
    public partial class PaymentHistoryForm : Form
    {
        private readonly LibraryDbContext _dbContext;
        private readonly IPaymentService _paymentService;
        private readonly int? _filterStudentId;
        private readonly int? _filterInvoiceId;

        public PaymentHistoryForm(int? studentId = null, int? invoiceId = null)
        {
            InitializeComponent();
            _dbContext = new LibraryDbContext();
            _paymentService = new PaymentService(_dbContext);
            _filterStudentId = studentId;
            _filterInvoiceId = invoiceId;

            this.Load += PaymentHistoryForm_Load;
        }

        public PaymentHistoryForm(IPaymentService paymentService, LibraryDbContext dbContext, int? studentId = null, int? invoiceId = null)
        {
            InitializeComponent();
            _dbContext = dbContext ?? new LibraryDbContext();
            _paymentService = paymentService ?? new PaymentService(_dbContext);
            _filterStudentId = studentId;
            _filterInvoiceId = invoiceId;

            this.Load += PaymentHistoryForm_Load;
        }

        private async void PaymentHistoryForm_Load(object? sender, EventArgs e)
        {
            SetupGridColumns();
            PopulateFilterCombos();

            if (_filterStudentId.HasValue || _filterInvoiceId.HasValue)
            {
                dtpFromDate.Value = DateTime.Today.AddYears(-2);
            }
            else
            {
                dtpFromDate.Value = DateTime.Today.AddMonths(-1);
            }
            dtpToDate.Value = DateTime.Today;

            await LoadPaymentsAsync();
        }

        private void SetupGridColumns()
        {
            dgvPayments.AutoGenerateColumns = false;
            dgvPayments.Columns.Clear();

            dgvPayments.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(PaymentListDto.PaymentNo),
                HeaderText = "Payment No",
                Width = 140,
                ReadOnly = true
            });

            dgvPayments.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(PaymentListDto.PaymentDate),
                HeaderText = "Date",
                Width = 100,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "dd-MMM-yyyy" },
                ReadOnly = true
            });

            dgvPayments.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(PaymentListDto.InvoiceNo),
                HeaderText = "Invoice No",
                Width = 135,
                ReadOnly = true
            });

            dgvPayments.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "StudentDisplayName",
                HeaderText = "Student",
                Width = 190,
                ReadOnly = true
            });

            dgvPayments.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(PaymentListDto.Amount),
                HeaderText = "Amount",
                Width = 110,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "C2", Alignment = DataGridViewContentAlignment.MiddleRight, Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold) },
                ReadOnly = true
            });

            dgvPayments.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(PaymentListDto.PaymentMode),
                HeaderText = "Mode",
                Width = 95,
                ReadOnly = true
            });

            dgvPayments.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(PaymentListDto.ReferenceNo),
                HeaderText = "Reference No",
                Width = 130,
                ReadOnly = true
            });

            dgvPayments.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(PaymentListDto.ReceivedBy),
                HeaderText = "Received By",
                Width = 110,
                ReadOnly = true
            });

            dgvPayments.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(PaymentListDto.Status),
                HeaderText = "Status",
                Width = 90,
                ReadOnly = true
            });

            dgvPayments.CellFormatting += DgvPayments_CellFormatting;
        }

        private void DgvPayments_CellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= dgvPayments.Rows.Count) return;

            var row = dgvPayments.Rows[e.RowIndex];
            if (row.DataBoundItem is PaymentRowViewModel item)
            {
                if (item.Status == "Cancelled")
                {
                    row.DefaultCellStyle.ForeColor = Color.FromArgb(148, 163, 184);
                    if (dgvPayments.Columns[e.ColumnIndex].DataPropertyName == nameof(PaymentListDto.Status))
                    {
                        e.CellStyle.ForeColor = Color.FromArgb(220, 38, 38);
                        e.CellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
                    }
                }
                else
                {
                    if (dgvPayments.Columns[e.ColumnIndex].DataPropertyName == nameof(PaymentListDto.Status))
                    {
                        e.CellStyle.ForeColor = Color.FromArgb(22, 163, 74);
                        e.CellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
                    }
                    else if (dgvPayments.Columns[e.ColumnIndex].DataPropertyName == nameof(PaymentListDto.Amount))
                    {
                        e.CellStyle.ForeColor = Color.FromArgb(15, 23, 42);
                    }
                }
            }
        }

        private void PopulateFilterCombos()
        {
            cmbPaymentMode.Items.Clear();
            cmbPaymentMode.Items.Add("All");
            foreach (var mode in PaymentModes.All)
            {
                cmbPaymentMode.Items.Add(mode);
            }
            cmbPaymentMode.SelectedIndex = 0;

            cmbStatus.Items.Clear();
            cmbStatus.Items.Add("All");
            cmbStatus.Items.Add("Active");
            cmbStatus.Items.Add("Cancelled");
            cmbStatus.SelectedIndex = 0;
        }

        private async Task LoadPaymentsAsync()
        {
            try
            {
                var filter = new PaymentFilterDto
                {
                    SearchText = string.IsNullOrWhiteSpace(txtSearch.Text) ? null : txtSearch.Text.Trim(),
                    FromDate = dtpFromDate.Value.Date,
                    ToDate = dtpToDate.Value.Date.AddDays(1).AddTicks(-1),
                    StudentId = _filterStudentId,
                    InvoiceId = _filterInvoiceId,
                    PaymentMode = cmbPaymentMode.SelectedIndex > 0 ? cmbPaymentMode.SelectedItem?.ToString() : null,
                    Status = cmbStatus.SelectedIndex > 0 ? cmbStatus.SelectedItem?.ToString() : null
                };

                var payments = await _paymentService.GetPaymentHistoryAsync(filter);

                var viewModels = payments.Select(p => new PaymentRowViewModel
                {
                    PaymentId = p.PaymentId,
                    PaymentNo = p.PaymentNo,
                    PaymentDate = p.PaymentDate,
                    InvoiceId = p.InvoiceId,
                    InvoiceNo = p.InvoiceNo,
                    StudentId = p.StudentId,
                    StudentCode = p.StudentCode,
                    StudentName = p.StudentName,
                    StudentDisplayName = $"{p.StudentCode} - {p.StudentName}",
                    Amount = p.Amount,
                    PaymentMode = p.PaymentMode,
                    ReferenceNo = string.IsNullOrWhiteSpace(p.ReferenceNo) ? "-" : p.ReferenceNo,
                    ReceivedBy = p.ReceivedBy,
                    Status = p.Status
                }).ToList();

                dgvPayments.DataSource = viewModels;

                decimal activeTotal = viewModels.Where(v => v.Status == "Active").Sum(v => v.Amount);
                lblTotalCollected.Text = $"Total Active Collection: ₹{activeTotal:N2}";
                lblTotalCount.Text = $"{viewModels.Count} payment(s) found ({viewModels.Count(v => v.Status == "Active")} active, {viewModels.Count(v => v.Status == "Cancelled")} cancelled)";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load payment history: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnFilter_Click(object? sender, EventArgs e)
        {
            await LoadPaymentsAsync();
        }

        private async void btnReset_Click(object? sender, EventArgs e)
        {
            txtSearch.Clear();
            cmbPaymentMode.SelectedIndex = 0;
            cmbStatus.SelectedIndex = 0;
            dtpFromDate.Value = DateTime.Today.AddMonths(-1);
            dtpToDate.Value = DateTime.Today;
            await LoadPaymentsAsync();
        }

        private async void txtSearch_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                await LoadPaymentsAsync();
            }
        }

        private PaymentRowViewModel? GetSelectedPayment()
        {
            if (dgvPayments.SelectedRows.Count > 0 &&
                dgvPayments.SelectedRows[0].DataBoundItem is PaymentRowViewModel item)
            {
                return item;
            }
            return null;
        }

        private void btnViewReceipt_Click(object? sender, EventArgs e)
        {
            var selected = GetSelectedPayment();
            if (selected == null)
            {
                MessageBox.Show("Please select a payment record to view receipt.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using var receiptForm = new PaymentReceiptForm(selected.PaymentId);
            receiptForm.ShowDialog(this);
        }

        private void btnPrintReceipt_Click(object? sender, EventArgs e)
        {
            var selected = GetSelectedPayment();
            if (selected == null)
            {
                MessageBox.Show("Please select a payment record to print receipt.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using var receiptForm = new PaymentReceiptForm(selected.PaymentId);
            receiptForm.ShowDialog(this);
        }

        private async void btnCancelPayment_Click(object? sender, EventArgs e)
        {
            var selected = GetSelectedPayment();
            if (selected == null)
            {
                MessageBox.Show("Please select a payment to cancel / reverse.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (selected.Status == "Cancelled")
            {
                MessageBox.Show("This payment is already cancelled.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var confirm = MessageBox.Show(
                $"Are you sure you want to cancel payment '{selected.PaymentNo}' for ₹{selected.Amount:N2}?\n\n" +
                $"This will reverse the amount from Invoice '{selected.InvoiceNo}', restore its outstanding balance, and mark the payment as Cancelled.\n\n" +
                $"Payment record will NOT be deleted from the database.",
                "Confirm Payment Cancellation / Reversal",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (confirm != DialogResult.Yes) return;

            string reason = "Operator cancelled payment";
            // Simple prompt for reason
            using (var reasonDialog = new Form())
            {
                reasonDialog.Text = "Cancellation Reason";
                reasonDialog.Size = new Size(420, 180);
                reasonDialog.StartPosition = FormStartPosition.CenterParent;
                reasonDialog.FormBorderStyle = FormBorderStyle.FixedDialog;
                reasonDialog.MaximizeBox = false;
                reasonDialog.MinimizeBox = false;

                var lbl = new Label { Text = "Enter cancellation reason / remarks:", Location = new Point(20, 20), AutoSize = true };
                var txt = new TextBox { Location = new Point(20, 48), Width = 360, Text = "Customer request / correction" };
                var btnOk = new Button { Text = "Confirm Cancellation", DialogResult = DialogResult.OK, Location = new Point(190, 95), Width = 150, Height = 30, BackColor = Color.FromArgb(185, 28, 28), ForeColor = Color.White, FlatStyle = FlatStyle.Flat };
                var btnCancel = new Button { Text = "Back", DialogResult = DialogResult.Cancel, Location = new Point(90, 95), Width = 90, Height = 30 };

                reasonDialog.Controls.AddRange(new Control[] { lbl, txt, btnOk, btnCancel });
                reasonDialog.AcceptButton = btnOk;
                reasonDialog.CancelButton = btnCancel;

                if (reasonDialog.ShowDialog(this) == DialogResult.OK && !string.IsNullOrWhiteSpace(txt.Text))
                {
                    reason = txt.Text.Trim();
                }
            }

            string operatorUser = UserSession.CurrentUserName ?? "Admin";
            var result = await _paymentService.CancelPaymentAsync(selected.PaymentId, operatorUser, reason);

            if (result.Success)
            {
                MessageBox.Show(result.Message, "Payment Cancelled", MessageBoxButtons.OK, MessageBoxIcon.Information);
                await LoadPaymentsAsync();
            }
            else
            {
                MessageBox.Show(result.Message, "Cancellation Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnNewPayment_Click(object? sender, EventArgs e)
        {
            using var payForm = new PaymentForm();
            payForm.ShowDialog(this);
            await LoadPaymentsAsync();
        }

        private void dgvPayments_CellDoubleClick(object? sender, DataGridViewCellEventArgs e)
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

        private sealed class PaymentRowViewModel : PaymentListDto
        {
            public string StudentDisplayName { get; set; } = string.Empty;
        }
    }
}
