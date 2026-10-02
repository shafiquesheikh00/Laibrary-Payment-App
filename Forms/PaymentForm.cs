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
    public partial class PaymentForm : Form
    {
        private readonly LibraryDbContext _dbContext;
        private readonly IPaymentService _paymentService;
        private readonly int? _preselectedInvoiceId;
        private readonly int? _preselectedStudentId;

        private List<StudentMaster> _allStudents = new();
        private List<OutstandingInvoiceDto> _currentOutstandingInvoices = new();
        private OutstandingInvoiceDto? _selectedInvoice;
        private bool _isLoading = false;

        public PaymentForm(int? initialInvoiceId = null, int? initialStudentId = null)
        {
            InitializeComponent();
            _dbContext = new LibraryDbContext();
            _paymentService = new PaymentService(_dbContext);
            _preselectedInvoiceId = initialInvoiceId;
            _preselectedStudentId = initialStudentId;

            this.Load += PaymentForm_Load;
        }

        public PaymentForm(IPaymentService paymentService, LibraryDbContext dbContext, int? initialInvoiceId = null, int? initialStudentId = null)
        {
            InitializeComponent();
            _dbContext = dbContext ?? new LibraryDbContext();
            _paymentService = paymentService ?? new PaymentService(_dbContext);
            _preselectedInvoiceId = initialInvoiceId;
            _preselectedStudentId = initialStudentId;

            this.Load += PaymentForm_Load;
        }

        private async void PaymentForm_Load(object? sender, EventArgs e)
        {
            SetupGridColumns();
            PopulatePaymentModes();

            txtReceivedBy.Text = UserSession.CurrentUserName ?? "Admin";
            dtpPaymentDate.Value = DateTime.Today;

            await LoadStudentsAsync();

            if (_preselectedInvoiceId.HasValue)
            {
                await SelectPreselectedInvoiceAsync(_preselectedInvoiceId.Value);
            }
            else if (_preselectedStudentId.HasValue)
            {
                SelectStudentInCombo(_preselectedStudentId.Value);
            }
        }

        private void SetupGridColumns()
        {
            dgvOutstandingInvoices.AutoGenerateColumns = false;
            dgvOutstandingInvoices.Columns.Clear();

            dgvOutstandingInvoices.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(OutstandingInvoiceDto.InvoiceNo),
                HeaderText = "Invoice No",
                Width = 130,
                ReadOnly = true
            });

            dgvOutstandingInvoices.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(OutstandingInvoiceDto.InvoiceDate),
                HeaderText = "Date",
                Width = 95,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "dd-MMM-yyyy" },
                ReadOnly = true
            });

            dgvOutstandingInvoices.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(OutstandingInvoiceDto.PlanName),
                HeaderText = "Plan / Desc",
                Width = 140,
                ReadOnly = true
            });

            dgvOutstandingInvoices.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(OutstandingInvoiceDto.SeatNumber),
                HeaderText = "Seat",
                Width = 65,
                ReadOnly = true
            });

            dgvOutstandingInvoices.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(OutstandingInvoiceDto.GrandTotal),
                HeaderText = "Grand Total",
                Width = 100,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "C2", Alignment = DataGridViewContentAlignment.MiddleRight },
                ReadOnly = true
            });

            dgvOutstandingInvoices.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(OutstandingInvoiceDto.PaidAmount),
                HeaderText = "Paid",
                Width = 95,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "C2", Alignment = DataGridViewContentAlignment.MiddleRight },
                ReadOnly = true
            });

            dgvOutstandingInvoices.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(OutstandingInvoiceDto.BalanceAmount),
                HeaderText = "Balance",
                Width = 105,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "C2", Alignment = DataGridViewContentAlignment.MiddleRight, ForeColor = Color.FromArgb(185, 28, 28), Font = new Font("Segoe UI", 9F, FontStyle.Bold) },
                ReadOnly = true
            });

            dgvOutstandingInvoices.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(OutstandingInvoiceDto.Status),
                HeaderText = "Status",
                Width = 95,
                ReadOnly = true
            });
        }

        private void PopulatePaymentModes()
        {
            cmbPaymentMode.Items.Clear();
            foreach (var mode in PaymentModes.All)
            {
                cmbPaymentMode.Items.Add(mode);
            }
            cmbPaymentMode.SelectedItem = PaymentModes.Cash;
        }

        private async Task LoadStudentsAsync()
        {
            try
            {
                _isLoading = true;
                _allStudents = await _dbContext.Students
                    .AsNoTracking()
                    .Where(s => s.IsActive)
                    .OrderBy(s => s.FullName)
                    .ToListAsync();

                BindStudentsCombo(_allStudents);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load students: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                _isLoading = false;
            }
        }

        private void BindStudentsCombo(List<StudentMaster> students)
        {
            var displayList = new List<object>
            {
                new { StudentId = 0, DisplayText = "-- Select a Student --" }
            };

            foreach (var s in students)
            {
                displayList.Add(new
                {
                    StudentId = s.StudentId,
                    DisplayText = $"{s.StudentCode} - {s.FullName} ({s.MobileNo})"
                });
            }

            cmbStudents.DataSource = null;
            cmbStudents.DisplayMember = "DisplayText";
            cmbStudents.ValueMember = "StudentId";
            cmbStudents.DataSource = displayList;
            cmbStudents.SelectedIndex = 0;
        }

        private void SelectStudentInCombo(int studentId)
        {
            for (int i = 0; i < cmbStudents.Items.Count; i++)
            {
                dynamic item = cmbStudents.Items[i];
                if (item.StudentId == studentId)
                {
                    cmbStudents.SelectedIndex = i;
                    break;
                }
            }
        }

        private async Task SelectPreselectedInvoiceAsync(int invoiceId)
        {
            var inv = await _dbContext.Invoices
                .AsNoTracking()
                .FirstOrDefaultAsync(i => i.InvoiceId == invoiceId);

            if (inv != null)
            {
                SelectStudentInCombo(inv.StudentId);
                // Invoices for student will load via combo event; find invoice row
                await LoadInvoicesForStudentAsync(inv.StudentId);

                for (int i = 0; i < dgvOutstandingInvoices.Rows.Count; i++)
                {
                    if (dgvOutstandingInvoices.Rows[i].DataBoundItem is OutstandingInvoiceDto dto && dto.InvoiceId == invoiceId)
                    {
                        dgvOutstandingInvoices.ClearSelection();
                        dgvOutstandingInvoices.Rows[i].Selected = true;
                        dgvOutstandingInvoices.CurrentCell = dgvOutstandingInvoices.Rows[i].Cells[0];
                        UpdateInvoiceSelection(dto);
                        break;
                    }
                }
            }
        }

        private void txtSearchStudent_TextChanged(object? sender, EventArgs e)
        {
            if (_isLoading) return;

            string query = txtSearchStudent.Text.Trim().ToLowerInvariant();
            if (string.IsNullOrWhiteSpace(query))
            {
                BindStudentsCombo(_allStudents);
            }
            else
            {
                var filtered = _allStudents
                    .Where(s => s.FullName.ToLowerInvariant().Contains(query)
                             || s.StudentCode.ToLowerInvariant().Contains(query)
                             || s.MobileNo.ToLowerInvariant().Contains(query))
                    .ToList();
                BindStudentsCombo(filtered);
            }
        }

        private async void cmbStudents_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (_isLoading || cmbStudents.SelectedIndex <= 0)
            {
                ClearInvoiceGridAndSummary();
                return;
            }

            dynamic? selectedItem = cmbStudents.SelectedItem;
            if (selectedItem != null && selectedItem.StudentId > 0)
            {
                int studentId = (int)selectedItem.StudentId;
                await LoadInvoicesForStudentAsync(studentId);
            }
            else
            {
                ClearInvoiceGridAndSummary();
            }
        }

        private async Task LoadInvoicesForStudentAsync(int studentId)
        {
            try
            {
                _currentOutstandingInvoices = await _paymentService.GetOutstandingInvoicesAsync(studentId);
                dgvOutstandingInvoices.DataSource = null;
                dgvOutstandingInvoices.DataSource = _currentOutstandingInvoices;

                if (_currentOutstandingInvoices.Count > 0)
                {
                    dgvOutstandingInvoices.ClearSelection();
                    dgvOutstandingInvoices.Rows[0].Selected = true;
                    UpdateInvoiceSelection(_currentOutstandingInvoices[0]);
                }
                else
                {
                    ClearInvoiceSelection();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load outstanding invoices: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvOutstandingInvoices_SelectionChanged(object? sender, EventArgs e)
        {
            if (dgvOutstandingInvoices.SelectedRows.Count > 0 &&
                dgvOutstandingInvoices.SelectedRows[0].DataBoundItem is OutstandingInvoiceDto invoiceDto)
            {
                UpdateInvoiceSelection(invoiceDto);
            }
            else
            {
                ClearInvoiceSelection();
            }
        }

        private void UpdateInvoiceSelection(OutstandingInvoiceDto inv)
        {
            _selectedInvoice = inv;

            lblSummaryInvNoVal.Text = inv.InvoiceNo;
            lblSummaryInvDateVal.Text = inv.InvoiceDate.ToString("dd-MMM-yyyy");
            lblSummaryStudentVal.Text = $"{inv.StudentCode} - {inv.StudentName}";
            lblSummaryPlanVal.Text = inv.PlanName;
            lblSummarySeatVal.Text = string.IsNullOrWhiteSpace(inv.SeatNumber) ? "None" : inv.SeatNumber;
            lblSummaryGrandVal.Text = $"₹{inv.GrandTotal:N2}";
            lblSummaryPaidVal.Text = $"₹{inv.PaidAmount:N2}";
            lblSummaryBalanceVal.Text = $"₹{inv.BalanceAmount:N2}";

            numAmount.Maximum = 1000000m;
            numAmount.Value = inv.BalanceAmount;
        }

        private void ClearInvoiceSelection()
        {
            _selectedInvoice = null;
            lblSummaryInvNoVal.Text = "-";
            lblSummaryInvDateVal.Text = "-";
            lblSummaryStudentVal.Text = "-";
            lblSummaryPlanVal.Text = "-";
            lblSummarySeatVal.Text = "-";
            lblSummaryGrandVal.Text = "₹0.00";
            lblSummaryPaidVal.Text = "₹0.00";
            lblSummaryBalanceVal.Text = "₹0.00";
            numAmount.Value = 0;
        }

        private void ClearInvoiceGridAndSummary()
        {
            _currentOutstandingInvoices.Clear();
            dgvOutstandingInvoices.DataSource = null;
            ClearInvoiceSelection();
        }

        private void btnPayFullBalance_Click(object? sender, EventArgs e)
        {
            if (_selectedInvoice != null)
            {
                numAmount.Value = _selectedInvoice.BalanceAmount;
            }
            else
            {
                MessageBox.Show("Please select an invoice first.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void cmbPaymentMode_SelectedIndexChanged(object? sender, EventArgs e)
        {
            string mode = cmbPaymentMode.SelectedItem?.ToString() ?? PaymentModes.Cash;
            if (mode == PaymentModes.Cash)
            {
                txtReferenceNo.PlaceholderText = "Optional for cash";
            }
            else if (mode == PaymentModes.UPI)
            {
                txtReferenceNo.PlaceholderText = "UPI Reference / Transaction ID";
            }
            else if (mode == PaymentModes.Card)
            {
                txtReferenceNo.PlaceholderText = "Card Auth Code / Last 4 Digits";
            }
            else if (mode == PaymentModes.BankTransfer)
            {
                txtReferenceNo.PlaceholderText = "NEFT / RTGS / IMPS UTR Number";
            }
            else
            {
                txtReferenceNo.PlaceholderText = "Transaction reference";
            }
        }

        private async void btnSavePayment_Click(object? sender, EventArgs e)
        {
            await ProcessPaymentAsync(printReceipt: false);
        }

        private async void btnSaveAndPrint_Click(object? sender, EventArgs e)
        {
            await ProcessPaymentAsync(printReceipt: true);
        }

        private async Task ProcessPaymentAsync(bool printReceipt)
        {
            if (_selectedInvoice == null)
            {
                MessageBox.Show("Please select an outstanding invoice to receive payment against.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            decimal paymentAmount = numAmount.Value;
            if (paymentAmount <= 0)
            {
                MessageBox.Show("Payment amount must be greater than ₹0.00.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                numAmount.Focus();
                return;
            }

            // CRITICAL VALIDATION RULE 8 & 4:
            // "Payment amount cannot exceed the outstanding balance." Do NOT silently reduce the payment amount.
            if (paymentAmount > _selectedInvoice.BalanceAmount)
            {
                MessageBox.Show("Payment amount cannot exceed the outstanding balance.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                numAmount.Focus();
                return;
            }

            string mode = cmbPaymentMode.SelectedItem?.ToString() ?? PaymentModes.Cash;
            if (!PaymentModes.IsValid(mode))
            {
                MessageBox.Show("Please select a valid payment mode.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbPaymentMode.Focus();
                return;
            }

            string receivedBy = string.IsNullOrWhiteSpace(UserSession.CurrentUserName) ? "Admin" : UserSession.CurrentUserName;

            var request = new CreatePaymentRequestDto
            {
                InvoiceId = _selectedInvoice.InvoiceId,
                StudentId = _selectedInvoice.StudentId,
                PaymentDate = dtpPaymentDate.Value.Date,
                Amount = paymentAmount,
                PaymentMode = mode,
                ReferenceNo = string.IsNullOrWhiteSpace(txtReferenceNo.Text) ? null : txtReferenceNo.Text.Trim(),
                Remarks = string.IsNullOrWhiteSpace(txtRemarks.Text) ? null : txtRemarks.Text.Trim(),
                ReceivedBy = receivedBy
            };

            btnSavePayment.Enabled = false;
            btnSaveAndPrint.Enabled = false;

            try
            {
                var result = await _paymentService.CreatePaymentAsync(request);
                if (!result.Success || result.Payment == null)
                {
                    MessageBox.Show(result.Message, "Payment Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var payment = result.Payment;
                MessageBox.Show($"Payment recorded successfully!\n\nReceipt / Payment No: {payment.PaymentNo}\nAmount Received: ₹{payment.Amount:N2}\nPayment Mode: {payment.PaymentMode}", "Payment Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                if (printReceipt)
                {
                    using var receiptForm = new PaymentReceiptForm(payment.PaymentId);
                    receiptForm.ShowDialog(this);
                }

                // Refresh invoices for current student
                int studentId = _selectedInvoice.StudentId;
                txtReferenceNo.Clear();
                txtRemarks.Clear();
                await LoadInvoicesForStudentAsync(studentId);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Unexpected error while saving payment: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnSavePayment.Enabled = true;
                btnSaveAndPrint.Enabled = true;
            }
        }

        private void btnClear_Click(object? sender, EventArgs e)
        {
            txtSearchStudent.Clear();
            if (cmbStudents.Items.Count > 0)
                cmbStudents.SelectedIndex = 0;
            ClearInvoiceGridAndSummary();
            txtReferenceNo.Clear();
            txtRemarks.Clear();
            numAmount.Value = 0;
            cmbPaymentMode.SelectedItem = PaymentModes.Cash;
            dtpPaymentDate.Value = DateTime.Today;
        }

        private void btnHistory_Click(object? sender, EventArgs e)
        {
            int? studentId = null;
            if (cmbStudents.SelectedItem != null)
            {
                dynamic item = cmbStudents.SelectedItem;
                if (item.StudentId > 0)
                {
                    studentId = (int)item.StudentId;
                }
            }

            using var historyForm = new PaymentHistoryForm(studentId);
            historyForm.ShowDialog(this);
        }
    }
}
