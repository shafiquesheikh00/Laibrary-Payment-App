using System;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Laibrary_Payment_App.Data;
using Laibrary_Payment_App.Reports;
using Laibrary_Payment_App.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Laibrary_Payment_App.Forms.Reports
{
    public partial class InvoiceRegisterReportForm : Form
    {
        private readonly IReportService _reportService;

        public InvoiceRegisterReportForm()
        {
            InitializeComponent();
            _reportService = ResolveReportService();
            SetupControls();
        }

        private static IReportService ResolveReportService()
        {
            if (Program.ServiceProvider != null)
            {
                var s = Program.ServiceProvider.GetService<IReportService>();
                if (s != null) return s;
            }
            return new ReportService(new LibraryDbContext());
        }

        private void SetupControls()
        {
            dtpFrom.Value = DateTime.Today.AddDays(-30);
            dtpTo.Value = DateTime.Today;

            cmbStatus.Items.Add("All");
            cmbStatus.Items.Add("Unpaid");
            cmbStatus.Items.Add("PartiallyPaid");
            cmbStatus.Items.Add("Paid");
            cmbStatus.Items.Add("Cancelled");
            cmbStatus.SelectedIndex = 0;

            SetupGrid();
        }

        private void SetupGrid()
        {
            dgvData.Columns.Clear();
            dgvData.AutoGenerateColumns = false;

            dgvData.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "InvoiceNo", HeaderText = "Invoice No", Width = 125 });
            dgvData.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Date", HeaderText = "Date", Width = 95, DefaultCellStyle = new DataGridViewCellStyle { Format = "dd-MMM-yyyy" } });
            dgvData.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "StudentCode", HeaderText = "Student Code", Width = 100 });
            dgvData.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "StudentName", HeaderText = "Student Name", Width = 150 });
            dgvData.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Membership", HeaderText = "Membership", Width = 110 });
            dgvData.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "CurrentCharges",
                HeaderText = "Current (₹)",
                Width = 105,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "N2", Alignment = DataGridViewContentAlignment.MiddleRight }
            });
            dgvData.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "PreviousOutstanding",
                HeaderText = "Prev Out (₹)",
                Width = 105,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "N2", Alignment = DataGridViewContentAlignment.MiddleRight }
            });
            dgvData.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "GrandTotal",
                HeaderText = "Grand Total (₹)",
                Width = 115,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "N2", Alignment = DataGridViewContentAlignment.MiddleRight, Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold) }
            });
            dgvData.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Paid",
                HeaderText = "Paid (₹)",
                Width = 100,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "N2", Alignment = DataGridViewContentAlignment.MiddleRight }
            });
            dgvData.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Balance",
                HeaderText = "Balance (₹)",
                Width = 105,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "N2", Alignment = DataGridViewContentAlignment.MiddleRight }
            });
            dgvData.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Status", HeaderText = "Status", Width = 95 });
        }

        protected override async void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            await LoadReportAsync();
        }

        private async Task LoadReportAsync()
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                btnApply.Enabled = false;

                var filter = new InvoiceRegisterFilterDto
                {
                    FromDate = dtpFrom.Value.Date,
                    ToDate = dtpTo.Value.Date,
                    Status = cmbStatus.SelectedItem?.ToString()
                };

                var data = await _reportService.GetInvoiceRegisterReportAsync(filter);
                dgvData.DataSource = data;

                decimal current = data.Sum(x => x.CurrentCharges);
                decimal prevOut = data.Sum(x => x.PreviousOutstanding);
                decimal grand = data.Sum(x => x.GrandTotal);
                decimal paid = data.Sum(x => x.Paid);
                decimal balance = data.Sum(x => x.Balance);

                var inCulture = CultureInfo.CreateSpecificCulture("hi-IN");
                lblSummary.Text = $"Total Invoices: {data.Count}";
                lblTotals.Text = $"Current: {current.ToString("C2", inCulture)} | Prev Out: {prevOut.ToString("C2", inCulture)} | Grand: {grand.ToString("C2", inCulture)} | Paid: {paid.ToString("C2", inCulture)} | Bal: {balance.ToString("C2", inCulture)}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load invoice register: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnApply.Enabled = true;
                Cursor = Cursors.Default;
            }
        }

        private async void btnApply_Click(object sender, EventArgs e)
        {
            await LoadReportAsync();
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            ReportExportHelper.ExportToExcel(dgvData, "Invoice Register Report", $"{lblSummary.Text} | {lblTotals.Text}");
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            ReportExportHelper.PrintReport(dgvData, "Invoice Register Report", $"{lblSummary.Text} | {lblTotals.Text}");
        }
    }
}
