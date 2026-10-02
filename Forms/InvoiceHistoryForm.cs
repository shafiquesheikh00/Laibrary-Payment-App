using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Laibrary_Payment_App.Data;
using Laibrary_Payment_App.Services;

namespace Laibrary_Payment_App.Forms
{
    public partial class InvoiceHistoryForm : Form
    {
        private readonly IBillingService _billingService;
        private readonly LibraryDbContext _dbContext;
        private readonly int? _filterStudentId;

        public InvoiceHistoryForm(int? studentId = null)
        {
            InitializeComponent();
            _dbContext = new LibraryDbContext();
            _billingService = new BillingService(_dbContext);
            _filterStudentId = studentId;
        }

        private async void InvoiceHistoryForm_Load(object sender, EventArgs e)
        {
            SetupGridColumns();
            cmbStatus.SelectedIndex = 0; // "All"
            dtpFromDate.Value = DateTime.Today.AddMonths(-1);
            dtpToDate.Value = DateTime.Today;

            await LoadInvoicesAsync();
        }

        private void SetupGridColumns()
        {
            dgvInvoices.AutoGenerateColumns = false;
            dgvInvoices.Columns.Clear();

            dgvInvoices.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(InvoiceListDto.InvoiceNo),
                HeaderText = "Invoice No",
                Width = 140,
                ReadOnly = true
            });

            dgvInvoices.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(InvoiceListDto.InvoiceDate),
                HeaderText = "Date",
                Width = 95,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "dd-MMM-yyyy" },
                ReadOnly = true
            });

            dgvInvoices.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(InvoiceListDto.StudentCode),
                HeaderText = "Student Code",
                Width = 110,
                ReadOnly = true
            });

            dgvInvoices.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(InvoiceListDto.StudentName),
                HeaderText = "Student Name",
                FillWeight = 25,
                ReadOnly = true
            });

            dgvInvoices.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(InvoiceListDto.MembershipPlanName),
                HeaderText = "Plan / Basis",
                Width = 130,
                ReadOnly = true
            });

            dgvInvoices.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(InvoiceListDto.CurrentCharges),
                HeaderText = "Current Charges (?)",
                Width = 120,
                DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight, Format = "N2" },
                ReadOnly = true
            });

            dgvInvoices.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(InvoiceListDto.PreviousOutstanding),
                HeaderText = "Prev Outstanding (?)",
                Width = 130,
                DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight, Format = "N2" },
                ReadOnly = true
            });

            dgvInvoices.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(InvoiceListDto.GrandTotal),
                HeaderText = "Grand Total (?)",
                Width = 120,
                DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight, Format = "N2" },
                ReadOnly = true
            });

            dgvInvoices.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(InvoiceListDto.PaidAmount),
                HeaderText = "Paid (?)",
                Width = 90,
                DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight, Format = "N2" },
                ReadOnly = true
            });

            dgvInvoices.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(InvoiceListDto.BalanceAmount),
                HeaderText = "Balance (?)",
                Width = 110,
                DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight, Format = "N2" },
                ReadOnly = true
            });

            dgvInvoices.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(InvoiceListDto.Status),
                HeaderText = "Status",
                Width = 100,
                DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter },
                ReadOnly = true
            });
        }

        private async Task LoadInvoicesAsync()
        {
            Cursor = Cursors.WaitCursor;
            try
            {
                var filter = new InvoiceFilterDto
                {
                    SearchText = txtSearch.Text.Trim(),
                    Status = cmbStatus.SelectedItem?.ToString(),
                    StudentId = _filterStudentId,
                    FromDate = chkDateFilter.Checked ? dtpFromDate.Value.Date : null,
                    ToDate = chkDateFilter.Checked ? dtpToDate.Value.Date : null
                };

                var list = await _billingService.GetInvoicesAsync(filter);
                dgvInvoices.DataSource = list;

                // Color code rows based on Status
                foreach (DataGridViewRow row in dgvInvoices.Rows)
                {
                    if (row.DataBoundItem is InvoiceListDto item)
                    {
                        if (item.Status == "Paid")
                        {
                            row.Cells[10].Style.ForeColor = Color.FromArgb(6, 95, 70);
                            row.Cells[10].Style.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
                        }
                        else if (item.Status == "Unpaid")
                        {
                            row.Cells[10].Style.ForeColor = Color.FromArgb(185, 28, 28);
                            row.Cells[10].Style.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
                        }
                        else if (item.Status == "Cancelled")
                        {
                            row.DefaultCellStyle.ForeColor = Color.FromArgb(148, 163, 184);
                        }
                    }
                }

                decimal totalBilled = list.Where(i => i.Status != "Cancelled").Sum(i => i.GrandTotal);
                decimal totalOutstanding = list.Where(i => i.Status != "Cancelled").Sum(i => i.BalanceAmount);
                lblSummaryStats.Text = $"Showing {list.Count} Invoices   |   Total Billed: ?{totalBilled:N2}   |   Total Outstanding Balance: ?{totalOutstanding:N2}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading invoices: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                lblSummaryStats.Text = "Error loading invoices.";
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private async void btnFilter_Click(object sender, EventArgs e)
        {
            await LoadInvoicesAsync();
        }

        private async void btnReset_Click(object sender, EventArgs e)
        {
            txtSearch.Clear();
            chkDateFilter.Checked = false;
            cmbStatus.SelectedIndex = 0;
            await LoadInvoicesAsync();
        }

        private void chkDateFilter_CheckedChanged(object sender, EventArgs e)
        {
            dtpFromDate.Enabled = chkDateFilter.Checked;
            dtpToDate.Enabled = chkDateFilter.Checked;
        }

        private async void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
                await LoadInvoicesAsync();
            }
        }

        private void btnViewInvoice_Click(object sender, EventArgs e)
        {
            OpenSelectedInvoice();
        }

        private void dgvInvoices_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                OpenSelectedInvoice();
            }
        }

        private void OpenSelectedInvoice()
        {
            if (dgvInvoices.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select an invoice to view.", "Selection", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (dgvInvoices.SelectedRows[0].DataBoundItem is InvoiceListDto item)
            {
                using (var viewForm = new InvoiceViewForm(item.InvoiceId))
                {
                    viewForm.ShowDialog(this);
                }
            }
        }

        private async void btnNewInvoice_Click(object sender, EventArgs e)
        {
            using (var billingForm = new BillingForm(_filterStudentId))
            {
                billingForm.ShowDialog(this);
            }

            await LoadInvoicesAsync();
        }

        private async void btnReceivePayment_Click(object sender, EventArgs e)
        {
            if (dgvInvoices.SelectedRows.Count == 0 || !(dgvInvoices.SelectedRows[0].DataBoundItem is InvoiceListDto item))
            {
                MessageBox.Show("Please select an invoice from the list to receive payment against.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (item.Status == "Cancelled")
            {
                MessageBox.Show("Cannot receive payment against a cancelled invoice.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (item.BalanceAmount <= 0)
            {
                MessageBox.Show($"Invoice '{item.InvoiceNo}' is already fully paid (Balance: ₹0.00).", "Fully Paid", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using (var payForm = new PaymentForm(initialInvoiceId: item.InvoiceId))
            {
                payForm.ShowDialog(this);
            }

            await LoadInvoicesAsync();
        }
    }
}
