using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Laibrary_Payment_App.Data;
using Laibrary_Payment_App.Reports;
using Laibrary_Payment_App.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Laibrary_Payment_App.Forms.Reports
{
    public partial class MembershipExpiryReportForm : Form
    {
        private readonly IReportService _reportService;

        public MembershipExpiryReportForm()
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
            cmbDays.Items.Add("Next 7 Days");
            cmbDays.Items.Add("Next 15 Days");
            cmbDays.Items.Add("Next 30 Days");
            cmbDays.Items.Add("Next 60 Days");
            cmbDays.Items.Add("Custom Date Range");
            cmbDays.SelectedIndex = 0;

            dtpFrom.Value = DateTime.Today;
            dtpTo.Value = DateTime.Today.AddDays(7);

            SetupGrid();
        }

        private void cmbDays_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool isCustom = cmbDays.SelectedIndex == 4;
            dtpFrom.Enabled = isCustom;
            dtpTo.Enabled = isCustom;
            dtpFrom.Checked = isCustom;
            dtpTo.Checked = isCustom;
        }

        private void SetupGrid()
        {
            dgvData.Columns.Clear();
            dgvData.AutoGenerateColumns = false;

            dgvData.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "StudentName", HeaderText = "Student Name", Width = 170 });
            dgvData.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Mobile", HeaderText = "Mobile", Width = 110 });
            dgvData.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "PlanName", HeaderText = "Plan", Width = 130 });
            dgvData.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "SeatNumber", HeaderText = "Seat", Width = 90 });
            dgvData.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "StartDate", HeaderText = "Start Date", Width = 100, DefaultCellStyle = new DataGridViewCellStyle { Format = "dd-MMM-yyyy" } });
            dgvData.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "EndDate", HeaderText = "Expiry Date", Width = 110, DefaultCellStyle = new DataGridViewCellStyle { Format = "dd-MMM-yyyy", Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold) } });
            dgvData.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "DaysRemaining",
                HeaderText = "Days Left",
                Width = 90,
                DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight }
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

                var filter = new MembershipExpiryFilterDto();

                if (cmbDays.SelectedIndex == 0) filter.ExpiryWithinDays = 7;
                else if (cmbDays.SelectedIndex == 1) filter.ExpiryWithinDays = 15;
                else if (cmbDays.SelectedIndex == 2) filter.ExpiryWithinDays = 30;
                else if (cmbDays.SelectedIndex == 3) filter.ExpiryWithinDays = 60;
                else
                {
                    filter.ExpiryWithinDays = null;
                    filter.ExpiryFrom = dtpFrom.Checked ? dtpFrom.Value.Date : DateTime.Today;
                    filter.ExpiryTo = dtpTo.Checked ? dtpTo.Value.Date : DateTime.Today.AddDays(30);
                }

                var data = await _reportService.GetMembershipExpiryReportAsync(filter);
                dgvData.DataSource = data;

                lblSummary.Text = $"Expiring Memberships Found: {data.Count} (Sorted nearest expiry first)";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load membership expiry report: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            ReportExportHelper.ExportToExcel(dgvData, "Membership Expiry Report", $"{lblSummary.Text}");
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            ReportExportHelper.PrintReport(dgvData, "Membership Expiry Report", $"{lblSummary.Text}");
        }
    }
}
