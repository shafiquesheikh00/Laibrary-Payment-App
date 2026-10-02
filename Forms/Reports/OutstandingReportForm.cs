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
    public partial class OutstandingReportForm : Form
    {
        private readonly IReportService _reportService;

        public OutstandingReportForm()
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
            cmbStatus.Items.Add("All");
            cmbStatus.Items.Add("Unpaid");
            cmbStatus.Items.Add("PartiallyPaid");
            cmbStatus.SelectedIndex = 0;

            SetupGrid();
        }

        private void SetupGrid()
        {
            dgvData.Columns.Clear();
            dgvData.AutoGenerateColumns = false;

            dgvData.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "StudentCode", HeaderText = "Student Code", Width = 110 });
            dgvData.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "StudentName", HeaderText = "Student Name", Width = 160 });
            dgvData.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Mobile", HeaderText = "Mobile", Width = 110 });
            dgvData.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "InvoiceNo", HeaderText = "Invoice No", Width = 130 });
            dgvData.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "InvoiceDate", HeaderText = "Date", Width = 100, DefaultCellStyle = new DataGridViewCellStyle { Format = "dd-MMM-yyyy" } });
            dgvData.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "GrandTotal",
                HeaderText = "Grand Total (₹)",
                Width = 115,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "N2", Alignment = DataGridViewContentAlignment.MiddleRight }
            });
            dgvData.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "PaidAmount",
                HeaderText = "Paid (₹)",
                Width = 110,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "N2", Alignment = DataGridViewContentAlignment.MiddleRight }
            });
            dgvData.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Balance",
                HeaderText = "Balance (₹)",
                Width = 120,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "N2", Alignment = DataGridViewContentAlignment.MiddleRight, Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold) }
            });
            dgvData.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Status", HeaderText = "Status", Width = 100 });
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

                var filter = new OutstandingFilterDto
                {
                    SearchText = txtSearch.Text.Trim(),
                    Status = cmbStatus.SelectedItem?.ToString()
                };

                var data = await _reportService.GetOutstandingReportAsync(filter);
                dgvData.DataSource = data;

                decimal totalBalance = data.Sum(x => x.Balance);
                decimal totalGrand = data.Sum(x => x.GrandTotal);
                decimal totalPaid = data.Sum(x => x.PaidAmount);

                var inCulture = CultureInfo.CreateSpecificCulture("hi-IN");
                lblSummary.Text = $"Invoices: {data.Count}   |   Grand: {totalGrand.ToString("C2", inCulture)}   |   Paid: {totalPaid.ToString("C2", inCulture)}";
                lblTotalBalance.Text = $"Total Outstanding Balance: {totalBalance.ToString("C2", inCulture)}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load outstanding report: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            ReportExportHelper.ExportToExcel(dgvData, "Outstanding Report", $"{lblSummary.Text} | {lblTotalBalance.Text}");
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            ReportExportHelper.PrintReport(dgvData, "Outstanding Report", $"{lblSummary.Text} | {lblTotalBalance.Text}");
        }
    }
}
