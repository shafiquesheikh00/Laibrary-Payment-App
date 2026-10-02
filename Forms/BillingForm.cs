using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Laibrary_Payment_App.Data;
using Laibrary_Payment_App.Models;
using Laibrary_Payment_App.Security;
using Laibrary_Payment_App.Services;

namespace Laibrary_Payment_App.Forms
{
    public partial class BillingForm : Form
    {
        private readonly IBillingService _billingService;
        private readonly LibraryDbContext _dbContext;
        private readonly int? _initialStudentId;

        private List<StudentMaster> _activeStudents = new List<StudentMaster>();
        private StudentMembership? _currentMembership;
        private BindingList<CreateInvoiceItemDto> _invoiceItems = new BindingList<CreateInvoiceItemDto>();
        private bool _isLoading = false;

        public BillingForm(int? initialStudentId = null)
        {
            InitializeComponent();
            _dbContext = new LibraryDbContext();
            _billingService = new BillingService(_dbContext);
            _initialStudentId = initialStudentId;
        }

        private async void BillingForm_Load(object sender, EventArgs e)
        {
            SetupGrid();
            InitChargeDefaults();
            await LoadActiveStudentsAsync();

            if (_initialStudentId.HasValue && _initialStudentId.Value > 0)
            {
                SelectStudentById(_initialStudentId.Value);
            }
        }

        private void SetupGrid()
        {
            dgvInvoiceItems.AutoGenerateColumns = false;
            dgvInvoiceItems.Columns.Clear();

            dgvInvoiceItems.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(CreateInvoiceItemDto.Description),
                HeaderText = "Description",
                FillWeight = 50,
                ReadOnly = true
            });

            dgvInvoiceItems.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(CreateInvoiceItemDto.Quantity),
                HeaderText = "Qty",
                Width = 70,
                DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter },
                ReadOnly = true
            });

            dgvInvoiceItems.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(CreateInvoiceItemDto.Rate),
                HeaderText = "Rate (?)",
                Width = 120,
                DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight, Format = "N2" },
                ReadOnly = true
            });

            dgvInvoiceItems.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(CreateInvoiceItemDto.Amount),
                HeaderText = "Amount (?)",
                Width = 130,
                DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight, Format = "N2" },
                ReadOnly = true
            });

            dgvInvoiceItems.DataSource = _invoiceItems;
            _invoiceItems.ListChanged += async (s, ev) =>
            {
                if (!_isLoading)
                {
                    await RecalculateSummaryAsync();
                }
            };
        }

        private void InitChargeDefaults()
        {
            cmbChargeType.SelectedIndex = 0;
            numChargeQty.Value = 1;
            numChargeRate.Value = 100.00m;
            dtpInvoiceDate.Value = DateTime.Today;
            dtpDueDate.Value = DateTime.Today.AddDays(7);
        }

        private async Task LoadActiveStudentsAsync()
        {
            try
            {
                _activeStudents = await Task.Run(() =>
                    _dbContext.Students
                        .Where(s => s.IsActive)
                        .OrderBy(s => s.FullName)
                        .ToList());

                PopulateStudentDropdown(_activeStudents);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading active students: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PopulateStudentDropdown(List<StudentMaster> students)
        {
            cmbStudents.SelectedIndexChanged -= cmbStudents_SelectedIndexChanged;
            cmbStudents.Items.Clear();

            foreach (var s in students)
            {
                cmbStudents.Items.Add(new StudentComboItem
                {
                    StudentId = s.StudentId,
                    DisplayText = $"{s.FullName} [{s.StudentCode}] - {s.MobileNo}"
                });
            }

            cmbStudents.SelectedIndexChanged += cmbStudents_SelectedIndexChanged;

            if (cmbStudents.Items.Count > 0)
            {
                cmbStudents.SelectedIndex = 0;
            }
            else
            {
                ClearStudentAndMembership();
            }
        }

        private void SelectStudentById(int studentId)
        {
            for (int i = 0; i < cmbStudents.Items.Count; i++)
            {
                if (cmbStudents.Items[i] is StudentComboItem item && item.StudentId == studentId)
                {
                    cmbStudents.SelectedIndex = i;
                    break;
                }
            }
        }

        private void txtSearchStudent_TextChanged(object sender, EventArgs e)
        {
            var search = txtSearchStudent.Text.Trim();
            if (string.IsNullOrEmpty(search))
            {
                PopulateStudentDropdown(_activeStudents);
                return;
            }

            var filtered = _activeStudents.Where(s =>
                s.FullName.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                s.StudentCode.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                s.MobileNo.Contains(search, StringComparison.OrdinalIgnoreCase)).ToList();

            PopulateStudentDropdown(filtered);
        }

        private async void cmbStudents_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (cmbStudents.SelectedItem is StudentComboItem item)
            {
                await OnStudentSelectedAsync(item.StudentId);
            }
        }

        private async Task OnStudentSelectedAsync(int studentId)
        {
            var student = _activeStudents.FirstOrDefault(s => s.StudentId == studentId);
            if (student == null)
            {
                ClearStudentAndMembership();
                return;
            }

            lblStudentDetails.Text = $"Code: {student.StudentCode}   |   Name: {student.FullName}   |   Mobile: {student.MobileNo}   |   Status: {(student.IsActive ? "Active" : "Inactive")}";

            _isLoading = true;
            try
            {
                // Query active membership
                _currentMembership = await _billingService.GetActiveMembershipForStudentAsync(studentId);

                _invoiceItems.Clear();

                if (_currentMembership != null)
                {
                    lblNoMembershipAlert.Visible = false;
                    lblMembershipIdValue.Text = $"#{_currentMembership.MembershipId}";
                    lblPlanValue.Text = _currentMembership.Plan?.PlanName ?? "-";
                    lblSeatValue.Text = _currentMembership.Seat?.SeatNumber ?? "None";
                    lblStartDateValue.Text = _currentMembership.StartDate.ToString("dd-MMM-yyyy");
                    lblEndDateValue.Text = _currentMembership.EndDate.ToString("dd-MMM-yyyy");
                    lblActualFeeValue.Text = $"?{_currentMembership.ActualFee:N2}";
                    lblDiscountValue.Text = $"?{_currentMembership.DiscountAmount:N2}";
                    lblFinalFeeValue.Text = $"?{_currentMembership.FinalFee:N2}";

                    // Apply membership discount default
                    numDiscount.Value = Math.Min(numDiscount.Maximum, _currentMembership.DiscountAmount);

                    // Add base membership line item
                    _invoiceItems.Add(new CreateInvoiceItemDto
                    {
                        Description = $"Reading Space Membership - {_currentMembership.Plan?.PlanName ?? "Standard"}",
                        Quantity = 1,
                        Rate = _currentMembership.ActualFee
                    });

                    btnGenerateInvoice.Enabled = true;
                }
                else
                {
                    lblNoMembershipAlert.Visible = true;
                    lblMembershipIdValue.Text = "-";
                    lblPlanValue.Text = "-";
                    lblSeatValue.Text = "-";
                    lblStartDateValue.Text = "-";
                    lblEndDateValue.Text = "-";
                    lblActualFeeValue.Text = "?0.00";
                    lblDiscountValue.Text = "?0.00";
                    lblFinalFeeValue.Text = "?0.00";
                    numDiscount.Value = 0;

                    // Do not allow normal billing until the billing basis is selected or general item added
                    btnGenerateInvoice.Enabled = false;
                }
            }
            finally
            {
                _isLoading = false;
            }

            await RecalculateSummaryAsync();
        }

        private void ClearStudentAndMembership()
        {
            lblStudentDetails.Text = "Code: -   |   Name: -   |   Mobile: -   |   Status: -";
            lblNoMembershipAlert.Visible = true;
            lblMembershipIdValue.Text = "-";
            lblPlanValue.Text = "-";
            lblSeatValue.Text = "-";
            lblStartDateValue.Text = "-";
            lblEndDateValue.Text = "-";
            lblActualFeeValue.Text = "?0.00";
            lblDiscountValue.Text = "?0.00";
            lblFinalFeeValue.Text = "?0.00";
            _currentMembership = null;
            _invoiceItems.Clear();
            btnGenerateInvoice.Enabled = false;
        }

        private async Task RecalculateSummaryAsync()
        {
            int studentId = 0;
            if (cmbStudents.SelectedItem is StudentComboItem item)
            {
                studentId = item.StudentId;
            }

            if (studentId <= 0)
            {
                SetSummaryZeros();
                return;
            }

            var itemsList = _invoiceItems.ToList();
            var result = await _billingService.CalculateInvoicePreviewAsync(
                studentId,
                _currentMembership?.MembershipId,
                numDiscount.Value,
                itemsList);

            lblSubTotalValue.Text = $"?{result.SubTotal:N2}";
            lblDiscountSummaryValue.Text = $"-?{result.DiscountAmount:N2}";
            lblAdditionalChargesValue.Text = $"?{result.AdditionalCharges:N2}";
            lblTaxValue.Text = $"?{result.TaxAmount:N2}";
            lblCurrentChargesValue.Text = $"?{result.CurrentCharges:N2}";
            lblPreviousOutstandingValue.Text = $"?{result.PreviousOutstanding:N2}";
            lblGrandTotalValue.Text = $"?{result.GrandTotal:N2}";
            lblPaidValue.Text = $"?{result.PaidAmount:N2}";
            lblBalanceValue.Text = $"?{result.BalanceAmount:N2}";

            // Enable generate invoice if we have items
            btnGenerateInvoice.Enabled = itemsList.Count > 0;
        }

        private void SetSummaryZeros()
        {
            lblSubTotalValue.Text = "?0.00";
            lblDiscountSummaryValue.Text = "-?0.00";
            lblAdditionalChargesValue.Text = "?0.00";
            lblTaxValue.Text = "?0.00";
            lblCurrentChargesValue.Text = "?0.00";
            lblPreviousOutstandingValue.Text = "?0.00";
            lblGrandTotalValue.Text = "?0.00";
            lblPaidValue.Text = "?0.00";
            lblBalanceValue.Text = "?0.00";
        }

        private void cmbChargeType_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cmbChargeType.SelectedItem?.ToString())
            {
                case "Registration Fee":
                    numChargeRate.Value = 100.00m;
                    break;
                case "Locker Fee":
                    numChargeRate.Value = 200.00m;
                    break;
                case "Library Card Fee":
                    numChargeRate.Value = 50.00m;
                    break;
                case "Late Return Fine":
                    numChargeRate.Value = 50.00m;
                    break;
                default:
                    numChargeRate.Value = 0.00m;
                    break;
            }
        }

        private void btnAddCharge_Click(object sender, EventArgs e)
        {
            var description = cmbChargeType.SelectedItem?.ToString() ?? "Additional Charge";
            int qty = (int)numChargeQty.Value;
            decimal rate = numChargeRate.Value;

            if (qty <= 0)
            {
                MessageBox.Show("Quantity must be greater than zero.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (rate < 0)
            {
                MessageBox.Show("Rate cannot be negative.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Check if already present, if so, increase quantity
            var existing = _invoiceItems.FirstOrDefault(i => i.Description.Equals(description, StringComparison.OrdinalIgnoreCase));
            if (existing != null)
            {
                existing.Quantity += qty;
                _invoiceItems.ResetBindings();
            }
            else
            {
                _invoiceItems.Add(new CreateInvoiceItemDto
                {
                    Description = description,
                    Quantity = qty,
                    Rate = rate
                });
            }
        }

        private void btnRemoveItem_Click(object sender, EventArgs e)
        {
            if (dgvInvoiceItems.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select an item to remove.", "Selection", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var selected = dgvInvoiceItems.SelectedRows[0].DataBoundItem as CreateInvoiceItemDto;
            if (selected != null)
            {
                _invoiceItems.Remove(selected);
            }
        }

        private async void numDiscount_ValueChanged(object sender, EventArgs e)
        {
            if (!_isLoading)
            {
                await RecalculateSummaryAsync();
            }
        }

        private void dtpInvoiceDate_ValueChanged(object sender, EventArgs e)
        {
            dtpDueDate.Value = dtpInvoiceDate.Value.AddDays(7);
        }

        private async void btnGenerateInvoice_Click(object sender, EventArgs e)
        {
            if (cmbStudents.SelectedItem is not StudentComboItem studentItem)
            {
                MessageBox.Show("Please choose an active student.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (_invoiceItems.Count == 0)
            {
                MessageBox.Show("Cannot generate an invoice with zero items.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Disable button immediately to prevent double-click duplicates
            btnGenerateInvoice.Enabled = false;
            Cursor = Cursors.WaitCursor;

            try
            {
                var request = new CreateInvoiceRequestDto
                {
                    StudentId = studentItem.StudentId,
                    MembershipId = _currentMembership?.MembershipId,
                    InvoiceDate = dtpInvoiceDate.Value.Date,
                    DueDate = dtpDueDate.Value.Date,
                    DiscountAmount = numDiscount.Value,
                    CreatedBy = CurrentSession.CurrentUser?.FullName ?? "Admin",
                    Items = _invoiceItems.ToList()
                };

                var (success, message, createdInvoice) = await _billingService.GenerateInvoiceAsync(request);

                if (!success || createdInvoice == null)
                {
                    MessageBox.Show(message, "Invoice Generation Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    btnGenerateInvoice.Enabled = true;
                    return;
                }

                var askView = MessageBox.Show(
                    $"{message}\n\nInvoice No: {createdInvoice.InvoiceNo}\nGrand Total: ?{createdInvoice.GrandTotal:N2}\nStatus: {createdInvoice.Status}\n\nWould you like to open the Invoice Preview now?",
                    "Invoice Created Successfully",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Information);

                if (askView == DialogResult.Yes)
                {
                    using (var viewForm = new InvoiceViewForm(createdInvoice.InvoiceId))
                    {
                        viewForm.ShowDialog(this);
                    }
                }

                // Refresh student and form state
                await OnStudentSelectedAsync(studentItem.StudentId);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Unexpected error while generating invoice: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnGenerateInvoice.Enabled = true;
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private async void btnClear_Click(object sender, EventArgs e)
        {
            InitChargeDefaults();
            if (cmbStudents.SelectedItem is StudentComboItem item)
            {
                await OnStudentSelectedAsync(item.StudentId);
            }
        }

        private void btnHistory_Click(object sender, EventArgs e)
        {
            int? studentId = null;
            if (cmbStudents.SelectedItem is StudentComboItem item)
            {
                studentId = item.StudentId;
            }

            using (var historyForm = new InvoiceHistoryForm(studentId))
            {
                historyForm.ShowDialog(this);
            }
        }

        private class StudentComboItem
        {
            public int StudentId { get; set; }
            public string DisplayText { get; set; } = string.Empty;
            public override string ToString() => DisplayText;
        }
    }
}
