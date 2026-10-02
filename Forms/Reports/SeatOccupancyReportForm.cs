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
    public partial class SeatOccupancyReportForm : Form
    {
        private readonly IReportService _reportService;

        public SeatOccupancyReportForm()
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
            cmbStatus.Items.Add("Available");
            cmbStatus.Items.Add("Occupied");
            cmbStatus.Items.Add("Maintenance");
            cmbStatus.Items.Add("Inactive");
            cmbStatus.SelectedIndex = 0;

            SetupGrid();
        }

        private void SetupGrid()
        {
            dgvData.Columns.Clear();
            dgvData.AutoGenerateColumns = false;

            dgvData.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "SeatNumber", HeaderText = "Seat", Width = 80 });
            dgvData.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Area", HeaderText = "Area", Width = 100 });
            dgvData.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Status", HeaderText = "Status", Width = 100 });
            dgvData.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "StudentCode", HeaderText = "Student Code", Width = 110 });
            dgvData.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "StudentName", HeaderText = "Student Name", Width = 170 });
            dgvData.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "MembershipPlan", HeaderText = "Membership Plan", Width = 130 });
            dgvData.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "StartDate",
                HeaderText = "Start Date",
                Width = 100,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "dd-MMM-yyyy", NullValue = "" }
            });
            dgvData.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "EndDate",
                HeaderText = "End Date",
                Width = 100,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "dd-MMM-yyyy", NullValue = "" }
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

                var filter = new SeatOccupancyFilterDto
                {
                    Status = cmbStatus.SelectedItem?.ToString()
                };

                var summary = await _reportService.GetSeatOccupancyReportAsync(filter);
                dgvData.DataSource = summary.Seats;

                lblSummary.Text = $"Total: {summary.TotalSeats} | Available: {summary.AvailableSeats} | Occupied: {summary.OccupiedSeats} | Maintenance: {summary.MaintenanceSeats} | Inactive: {summary.InactiveSeats}";
                lblOccupancyRate.Text = $"Occupancy Rate: {summary.OccupancyPercentage:F1}%";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load seat occupancy report: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            ReportExportHelper.ExportToExcel(dgvData, "Seat Occupancy Report", lblSummary.Text + " | " + lblOccupancyRate.Text);
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            ReportExportHelper.PrintReport(dgvData, "Seat Occupancy Report", lblSummary.Text + " | " + lblOccupancyRate.Text);
        }
    }
}
