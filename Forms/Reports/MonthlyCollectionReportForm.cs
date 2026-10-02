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
    public partial class MonthlyCollectionReportForm : Form
    {
        private readonly IReportService _reportService;

        public MonthlyCollectionReportForm()
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
            nudYear.Value = DateTime.Today.Year;

            cmbMonth.Items.Add(new MonthItem { Name = "All Months (Full Year)", Value = null });
            for (int i = 1; i <= 12; i++)
            {
                cmbMonth.Items.Add(new MonthItem
                {
                    Name = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(i),
                    Value = i
                });
            }
            cmbMonth.SelectedIndex = 0; // Default to full year

            SetupGrid();
        }

        private class MonthItem
        {
            public string Name { get; set; } = string.Empty;
            public int? Value { get; set; }
            public override string ToString() => Name;
        }

        private void SetupGrid()
        {
            dgvData.Columns.Clear();
            dgvData.AutoGenerateColumns = false;

            dgvData.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "MonthName", HeaderText = "Month", Width = 140 });
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

                var selectedMonth = cmbMonth.SelectedItem as MonthItem;
                var filter = new MonthlyCollectionFilterDto
                {
                    Year = (int)nudYear.Value,
                    Month = selectedMonth?.Value
                };

                var data = await _reportService.GetMonthlyCollectionReportAsync(filter);
                dgvData.DataSource = data;

                decimal total = data.Sum(x => x.TotalCollection);
                int totalCount = data.Sum(x => x.PaymentCount);
                var inCulture = CultureInfo.CreateSpecificCulture("hi-IN");
                lblSummary.Text = $"Months: {data.Count}   |   Total Payments: {totalCount}   |   Year: {nudYear.Value}";
                lblTotalAmount.Text = $"Total Collection: {total.ToString("C2", inCulture)}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load monthly collection report: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            ReportExportHelper.ExportToExcel(dgvData, "Monthly Collection Report", $"Year: {nudYear.Value} | {lblTotalAmount.Text}");
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            ReportExportHelper.PrintReport(dgvData, "Monthly Collection Report", $"Year: {nudYear.Value} | {lblTotalAmount.Text}");
        }
    }
}
