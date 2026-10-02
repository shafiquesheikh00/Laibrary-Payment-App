using System;
using System.Collections.Generic;
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
    public partial class DailyCollectionReportForm : Form
    {
        private readonly IReportService _reportService;

        public DailyCollectionReportForm()
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
            cmbMode.Items.Add("All");
            foreach (var m in PaymentModes.All)
            {
                cmbMode.Items.Add(m);
            }
            cmbMode.SelectedIndex = 0;
            dtpDate.Value = DateTime.Today;

            SetupGrid();
        }

        private void SetupGrid()
        {
            dgvData.Columns.Clear();
            dgvData.AutoGenerateColumns = false;

            dgvData.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "PaymentNo", HeaderText = "Payment No", Width = 130 });
            dgvData.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "PaymentDate", HeaderText = "Time", Width = 90, DefaultCellStyle = new DataGridViewCellStyle { Format = "hh:mm tt" } });
            dgvData.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "InvoiceNo", HeaderText = "Invoice No", Width = 130 });
            dgvData.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "StudentCode", HeaderText = "Student Code", Width = 110 });
            dgvData.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "StudentName", HeaderText = "Student Name", Width = 170 });
            dgvData.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "PaymentMode", HeaderText = "Mode", Width = 90 });
            dgvData.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "ReferenceNo", HeaderText = "Reference No", Width = 130 });
            dgvData.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Amount",
                HeaderText = "Amount (₹)",
                Width = 110,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "N2", Alignment = DataGridViewContentAlignment.MiddleRight }
            });
            dgvData.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "ReceivedBy", HeaderText = "Received By", Width = 120 });
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

                var filter = new DailyCollectionFilterDto
                {
                    Date = dtpDate.Value.Date,
                    PaymentMode = cmbMode.SelectedItem?.ToString()
                };

                var data = await _reportService.GetDailyCollectionReportAsync(filter);
                dgvData.DataSource = data;

                decimal total = data.Sum(x => x.Amount);
                var inCulture = CultureInfo.CreateSpecificCulture("hi-IN");
                lblSummary.Text = $"Total Payments: {data.Count}   |   Date: {dtpDate.Value:dd-MMM-yyyy}";
                lblTotalAmount.Text = $"Total Collection: {total.ToString("C2", inCulture)}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load daily collection report: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            ReportExportHelper.ExportToExcel(dgvData, "Daily Collection Report", $"Date: {dtpDate.Value:dd-MMM-yyyy} | {lblTotalAmount.Text}");
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            ReportExportHelper.PrintReport(dgvData, "Daily Collection Report", $"Date: {dtpDate.Value:dd-MMM-yyyy} | {lblTotalAmount.Text}");
        }
    }
}
