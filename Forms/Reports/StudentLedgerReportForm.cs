using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore;
using Laibrary_Payment_App.Data;
using Laibrary_Payment_App.Reports;
using Laibrary_Payment_App.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Laibrary_Payment_App.Forms.Reports
{
    public partial class StudentLedgerReportForm : Form
    {
        private readonly IReportService _reportService;
        private readonly int? _initialStudentId;

        public StudentLedgerReportForm(int? initialStudentId = null)
        {
            InitializeComponent();
            _initialStudentId = initialStudentId;
            _reportService = ResolveReportService();
            SetupGrid();
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

        private class StudentComboItem
        {
            public int StudentId { get; set; }
            public string DisplayText { get; set; } = string.Empty;
            public override string ToString() => DisplayText;
        }

        private void SetupGrid()
        {
            dgvData.Columns.Clear();
            dgvData.AutoGenerateColumns = false;

            dgvData.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Date", HeaderText = "Date", Width = 110, DefaultCellStyle = new DataGridViewCellStyle { Format = "dd-MMM-yyyy" } });
            dgvData.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Type", HeaderText = "Type", Width = 90 });
            dgvData.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Reference", HeaderText = "Reference", Width = 130 });
            dgvData.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Description", HeaderText = "Description", Width = 220 });
            dgvData.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Debit",
                HeaderText = "Debit (₹)",
                Width = 110,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "N2", Alignment = DataGridViewContentAlignment.MiddleRight }
            });
            dgvData.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Credit",
                HeaderText = "Credit (₹)",
                Width = 110,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "N2", Alignment = DataGridViewContentAlignment.MiddleRight }
            });
            dgvData.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "RunningBalance",
                HeaderText = "Running Balance (₹)",
                Width = 140,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "N2", Alignment = DataGridViewContentAlignment.MiddleRight, Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold) }
            });
            dgvData.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "ModeOrStatus", HeaderText = "Mode/Status", Width = 100 });
        }

        protected override async void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            await LoadStudentsComboAsync();
        }

        private async Task LoadStudentsComboAsync()
        {
            try
            {
                using var db = new LibraryDbContext();
                var students = await db.Students
                    .AsNoTracking()
                    .OrderBy(s => s.FullName)
                    .Select(s => new StudentComboItem
                    {
                        StudentId = s.StudentId,
                        DisplayText = $"{s.FullName} ({s.StudentCode} - {s.MobileNo})"
                    })
                    .ToListAsync();

                cmbStudent.Items.Clear();
                int selectIndex = 0;
                for (int i = 0; i < students.Count; i++)
                {
                    cmbStudent.Items.Add(students[i]);
                    if (_initialStudentId.HasValue && students[i].StudentId == _initialStudentId.Value)
                    {
                        selectIndex = i;
                    }
                }

                if (cmbStudent.Items.Count > 0)
                {
                    cmbStudent.SelectedIndex = selectIndex;
                    await LoadReportAsync();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Could not load students: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void cmbStudent_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbStudent.SelectedItem is StudentComboItem)
            {
                await LoadReportAsync();
            }
        }

        private async Task LoadReportAsync()
        {
            if (cmbStudent.SelectedItem is not StudentComboItem item)
            {
                return;
            }

            try
            {
                Cursor = Cursors.WaitCursor;
                btnApply.Enabled = false;

                var filter = new StudentLedgerFilterDto
                {
                    StudentId = item.StudentId,
                    FromDate = dtpFrom.Checked ? dtpFrom.Value.Date : null,
                    ToDate = dtpTo.Checked ? dtpTo.Value.Date : null
                };

                var data = await _reportService.GetStudentLedgerReportAsync(filter);
                dgvData.DataSource = data;

                decimal closingBalance = data.Count > 0 ? data.Last().RunningBalance : 0m;
                decimal totalDebit = data.Sum(x => x.Debit);
                decimal totalCredit = data.Sum(x => x.Credit);

                var inCulture = CultureInfo.CreateSpecificCulture("hi-IN");
                lblSummary.Text = $"Transactions: {data.Count}   |   Total Debits: {totalDebit.ToString("C2", inCulture)}   |   Total Credits: {totalCredit.ToString("C2", inCulture)}";

                lblClosingBalance.Text = $"Closing Balance: {closingBalance.ToString("C2", inCulture)}";
                lblClosingBalance.ForeColor = closingBalance > 0
                    ? System.Drawing.Color.FromArgb(220, 38, 38)
                    : System.Drawing.Color.FromArgb(16, 185, 129);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load student ledger report: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            string studentName = cmbStudent.Text;
            ReportExportHelper.ExportToExcel(dgvData, "Student Financial Ledger", $"Student: {studentName} | {lblClosingBalance.Text}");
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            string studentName = cmbStudent.Text;
            ReportExportHelper.PrintReport(dgvData, "Student Financial Ledger", $"Student: {studentName} | {lblClosingBalance.Text}");
        }
    }
}
