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
    public partial class StudentRegisterReportForm : Form
    {
        private readonly IReportService _reportService;

        public StudentRegisterReportForm()
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
            cmbStatus.Items.Add("Inactive");
            cmbStatus.SelectedIndex = 0;

            SetupGrid();
        }

        private void SetupGrid()
        {
            dgvData.Columns.Clear();
            dgvData.AutoGenerateColumns = false;

            dgvData.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "StudentCode", HeaderText = "Student Code", Width = 110 });
            dgvData.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Name", HeaderText = "Name", Width = 180 });
            dgvData.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Mobile", HeaderText = "Mobile", Width = 120 });
            dgvData.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Email", HeaderText = "Email", Width = 170 });
            dgvData.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "College", HeaderText = "College", Width = 150 });
            dgvData.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Course", HeaderText = "Course", Width = 120 });
            dgvData.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "ActiveStatus", HeaderText = "Status", Width = 80 });
            dgvData.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "CreatedDate",
                HeaderText = "Created Date",
                Width = 110,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "dd-MMM-yyyy" }
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

                var filter = new StudentRegisterFilterDto
                {
                    SearchText = txtSearch.Text.Trim(),
                    ActiveStatus = cmbStatus.SelectedItem?.ToString()
                };

                var data = await _reportService.GetStudentRegisterReportAsync(filter);
                dgvData.DataSource = data;

                int activeCount = data.Count(x => x.ActiveStatus == "Active");
                int inactiveCount = data.Count(x => x.ActiveStatus == "Inactive");
                lblSummary.Text = $"Active: {activeCount} | Inactive: {inactiveCount}";
                lblTotalCount.Text = $"Total Students: {data.Count}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load student register report: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            ReportExportHelper.ExportToExcel(dgvData, "Student Register Report", lblSummary.Text);
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            ReportExportHelper.PrintReport(dgvData, "Student Register Report", lblSummary.Text);
        }
    }
}
