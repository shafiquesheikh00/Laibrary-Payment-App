using System;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Laibrary_Payment_App.Data;
using Laibrary_Payment_App.Models;
using Laibrary_Payment_App.Reports;
using Laibrary_Payment_App.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Laibrary_Payment_App.Forms.Reports
{
    public partial class PaymentHistoryReportForm : Form
    {
        private readonly IReportService _reportService;

        public PaymentHistoryReportForm()
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

            cmbMode.Items.Add("All");
            foreach (var m in PaymentModes.All)
            {
                cmbMode.Items.Add(m);
            }
            cmbMode.SelectedIndex = 0;

            cmbStatus.Items.Add("All");
            cmbStatus.Items.Add("Active");
            cmbStatus.Items.Add("Cancelled");
            cmbStatus.SelectedIndex = 0;

            SetupGrid();
        }

        private void SetupGrid()
        {
            dgvData.Columns.Clear();
            dgvData.AutoGenerateColumns = false;

            dgvData.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "PaymentNo", HeaderText = "Payment No", Width = 130 });
            dgvData.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Date", HeaderText = "Date", Width = 100, DefaultCellStyle = new DataGridViewCellStyle { Format = "dd-MMM-yyyy" } });
            dgvData.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "InvoiceNo", HeaderText = "Invoice No", Width = 130 });
            dgvData.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "StudentCode", HeaderText = "Student Code", Width = 110 });
            dgvData.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "StudentName", HeaderText = "Student Name", Width = 160 });
            dgvData.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Amount",
                HeaderText = "Amount (₹)",
                Width = 110,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "N2", Alignment = DataGridViewContentAlignment.MiddleRight }
            });
            dgvData.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Mode", HeaderText = "Mode", Width = 90 });
            dgvData.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Reference", HeaderText = "Reference", Width = 120 });
            dgvData.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "ReceivedBy", HeaderText = "Received By", Width = 110 });
            dgvData.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Status", HeaderText = "Status", Width = 90 });
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

                var filter = new PaymentHistoryReportFilterDto
                {
                    FromDate = dtpFrom.Value.Date,
                    ToDate = dtpTo.Value.Date,
                    PaymentMode = cmbMode.SelectedItem?.ToString(),
                    Status = cmbStatus.SelectedItem?.ToString()
                };

                var data = await _reportService.GetPaymentHistoryReportAsync(filter);
                dgvData.DataSource = data;

                decimal total = data.Sum(x => x.Amount);
                var inCulture = CultureInfo.CreateSpecificCulture("hi-IN");
                lblSummary.Text = $"Total Payments: {data.Count}   |   Period: {dtpFrom.Value:dd-MMM} to {dtpTo.Value:dd-MMM}";
                lblTotalAmount.Text = $"Total Amount: {total.ToString("C2", inCulture)}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load payment history report: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            ReportExportHelper.ExportToExcel(dgvData, "Payment History Report", $"{lblSummary.Text} | {lblTotalAmount.Text}");
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            ReportExportHelper.PrintReport(dgvData, "Payment History Report", $"{lblSummary.Text} | {lblTotalAmount.Text}");
        }
    }
}
