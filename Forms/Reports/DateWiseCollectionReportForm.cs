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
    public partial class DateWiseCollectionReportForm : Form
    {
        private readonly IReportService _reportService;

        public DateWiseCollectionReportForm()
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

            SetupGrid();
        }

        private void SetupGrid()
        {
            dgvData.Columns.Clear();
            dgvData.AutoGenerateColumns = false;

            dgvData.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Date", HeaderText = "Date", Width = 110, DefaultCellStyle = new DataGridViewCellStyle { Format = "dd-MMM-yyyy" } });
            dgvData.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "PaymentCount", HeaderText = "Payments", Width = 90, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight } });
            dgvData.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "CashAmount", HeaderText = "Cash (₹)", Width = 110, DefaultCellStyle = new DataGridViewCellStyle { Format = "N2", Alignment = DataGridViewContentAlignment.MiddleRight } });
            dgvData.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "UpiAmount", HeaderText = "UPI (₹)", Width = 110, DefaultCellStyle = new DataGridViewCellStyle { Format = "N2", Alignment = DataGridViewContentAlignment.MiddleRight } });
            dgvData.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "CardAmount", HeaderText = "Card (₹)", Width = 110, DefaultCellStyle = new DataGridViewCellStyle { Format = "N2", Alignment = DataGridViewContentAlignment.MiddleRight } });
            dgvData.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "BankTransferAmount", HeaderText = "Bank Trf (₹)", Width = 110, DefaultCellStyle = new DataGridViewCellStyle { Format = "N2", Alignment = DataGridViewContentAlignment.MiddleRight } });
            dgvData.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "OtherAmount", HeaderText = "Other (₹)", Width = 110, DefaultCellStyle = new DataGridViewCellStyle { Format = "N2", Alignment = DataGridViewContentAlignment.MiddleRight } });
            dgvData.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "TotalCollection",
                HeaderText = "Total Collection (₹)",
                Width = 140,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "N2", Alignment = DataGridViewContentAlignment.MiddleRight, Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold) }
            });
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

                var filter = new DateWiseCollectionFilterDto
                {
                    FromDate = dtpFrom.Value.Date,
                    ToDate = dtpTo.Value.Date
                };

                var data = await _reportService.GetDateWiseCollectionReportAsync(filter);
                dgvData.DataSource = data;

                decimal total = data.Sum(x => x.TotalCollection);
                int totalCount = data.Sum(x => x.PaymentCount);
                var inCulture = CultureInfo.CreateSpecificCulture("hi-IN");
                lblSummary.Text = $"Days: {data.Count}   |   Total Transactions: {totalCount}";
                lblTotalAmount.Text = $"Total Collection: {total.ToString("C2", inCulture)}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load date-wise collection report: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            ReportExportHelper.ExportToExcel(dgvData, "Date-Wise Collection Report", $"Period: {dtpFrom.Value:dd-MMM-yyyy} to {dtpTo.Value:dd-MMM-yyyy} | {lblTotalAmount.Text}");
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            ReportExportHelper.PrintReport(dgvData, "Date-Wise Collection Report", $"Period: {dtpFrom.Value:dd-MMM-yyyy} to {dtpTo.Value:dd-MMM-yyyy} | {lblTotalAmount.Text}");
        }
    }
}
