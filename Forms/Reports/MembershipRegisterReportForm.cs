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
    public partial class MembershipRegisterReportForm : Form
    {
        private readonly IReportService _reportService;

        public MembershipRegisterReportForm()
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
            cmbStatus.Items.Add("Active");
            cmbStatus.Items.Add("Expired");
            cmbStatus.Items.Add("Cancelled");
            cmbStatus.SelectedIndex = 0;

            SetupGrid();
        }

        private void SetupGrid()
        {
            dgvData.Columns.Clear();
            dgvData.AutoGenerateColumns = false;

            dgvData.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "MembershipId", HeaderText = "ID", Width = 60 });
            dgvData.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "StudentCode", HeaderText = "Student Code", Width = 110 });
            dgvData.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "StudentName", HeaderText = "Student Name", Width = 160 });
            dgvData.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "PlanName", HeaderText = "Plan", Width = 120 });
            dgvData.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "SeatNumber", HeaderText = "Seat", Width = 80 });
            dgvData.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "StartDate", HeaderText = "Start Date", Width = 100, DefaultCellStyle = new DataGridViewCellStyle { Format = "dd-MMM-yyyy" } });
            dgvData.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "EndDate", HeaderText = "End Date", Width = 100, DefaultCellStyle = new DataGridViewCellStyle { Format = "dd-MMM-yyyy" } });
            dgvData.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Fee",
                HeaderText = "Fee (₹)",
                Width = 95,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "N2", Alignment = DataGridViewContentAlignment.MiddleRight }
            });
            dgvData.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Discount",
                HeaderText = "Disc (₹)",
                Width = 90,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "N2", Alignment = DataGridViewContentAlignment.MiddleRight }
            });
            dgvData.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "FinalFee",
                HeaderText = "Final Fee (₹)",
                Width = 110,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "N2", Alignment = DataGridViewContentAlignment.MiddleRight, Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold) }
            });
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

                var filter = new MembershipRegisterFilterDto
                {
                    StartDateFrom = dtpFrom.Checked ? dtpFrom.Value.Date : null,
                    StartDateTo = dtpTo.Checked ? dtpTo.Value.Date : null,
                    Status = cmbStatus.SelectedItem?.ToString()
                };

                var data = await _reportService.GetMembershipRegisterReportAsync(filter);
                dgvData.DataSource = data;

                decimal totalFinal = data.Sum(x => x.FinalFee);
                var inCulture = CultureInfo.CreateSpecificCulture("hi-IN");
                lblSummary.Text = $"Total Memberships: {data.Count}";
                lblTotalFees.Text = $"Total Final Fee: {totalFinal.ToString("C2", inCulture)}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load membership register: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            ReportExportHelper.ExportToExcel(dgvData, "Membership Register Report", $"{lblSummary.Text} | {lblTotalFees.Text}");
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            ReportExportHelper.PrintReport(dgvData, "Membership Register Report", $"{lblSummary.Text} | {lblTotalFees.Text}");
        }
    }
}
