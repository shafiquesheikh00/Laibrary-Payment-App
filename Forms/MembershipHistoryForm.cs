using System;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Laibrary_Payment_App.Data;
using Laibrary_Payment_App.Models;
using Laibrary_Payment_App.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Laibrary_Payment_App.Forms
{
    public partial class MembershipHistoryForm : Form
    {
        private readonly int _studentId;
        private readonly IMembershipService _membershipService;
        private readonly IStudentService _studentService;

        public MembershipHistoryForm(int studentId)
        {
            _studentId = studentId;
            InitializeComponent();

            _membershipService = ResolveMembershipService();
            _studentService = ResolveStudentService();

            SetupGridColumns();
        }

        private static IMembershipService ResolveMembershipService()
        {
            if (Program.ServiceProvider != null)
            {
                var s = Program.ServiceProvider.GetService<IMembershipService>();
                if (s != null) return s;
            }
            return new MembershipService(new LibraryDbContext());
        }

        private static IStudentService ResolveStudentService()
        {
            if (Program.ServiceProvider != null)
            {
                var s = Program.ServiceProvider.GetService<IStudentService>();
                if (s != null) return s;
            }
            return new StudentService(new LibraryDbContext());
        }

        protected override async void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            await LoadStudentInfoAndHistoryAsync();
        }

        private void SetupGridColumns()
        {
            dgvHistory.AutoGenerateColumns = false;
            dgvHistory.Columns.Clear();

            dgvHistory.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "MembershipId",
                HeaderText = "ID",
                Width = 50
            });

            dgvHistory.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "PlanName",
                HeaderText = "Plan",
                Width = 120
            });

            dgvHistory.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "SeatNumber",
                HeaderText = "Seat",
                Width = 85
            });

            dgvHistory.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "StartDateDisplay",
                HeaderText = "Start Date",
                Width = 105
            });

            dgvHistory.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "EndDateDisplay",
                HeaderText = "End Date",
                Width = 105
            });

            dgvHistory.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "FinalFeeDisplay",
                HeaderText = "Final Fee",
                Width = 90
            });

            dgvHistory.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Status",
                HeaderText = "Status",
                Width = 90
            });

            dgvHistory.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "RemarksDisplay",
                HeaderText = "Remarks",
                Width = 150
            });
        }

        private async Task LoadStudentInfoAndHistoryAsync()
        {
            try
            {
                Cursor = Cursors.WaitCursor;

                var student = await _studentService.GetByIdAsync(_studentId);
                if (student != null)
                {
                    lblStudentName.Text = student.FullName;
                    lblStudentDetails.Text = $"Student Code: {student.StudentCode}   |   Mobile: {student.MobileNo}   |   College: {student.CollegeName ?? "-"}";
                }

                var history = await _membershipService.GetMembershipHistoryAsync(_studentId);

                var activeMembership = history.FirstOrDefault(m => m.Status == "Active" && m.EndDate.Date >= DateTime.UtcNow.Date);
                if (activeMembership != null)
                {
                    lblCurrentActiveStatus.Text = $"Active Plan: {activeMembership.Plan?.PlanName ?? "-"} | Seat: {activeMembership.Seat?.SeatNumber ?? "None"} (Expires: {activeMembership.EndDate:dd-MMM-yyyy})";
                    lblCurrentActiveStatus.ForeColor = Color.FromArgb(22, 101, 52);
                }
                else
                {
                    lblCurrentActiveStatus.Text = "No Active Membership currently";
                    lblCurrentActiveStatus.ForeColor = Color.FromArgb(198, 40, 40);
                }

                var displayList = history.Select(h => new
                {
                    h.MembershipId,
                    PlanName = h.Plan?.PlanName ?? "-",
                    SeatNumber = h.Seat?.SeatNumber ?? "None",
                    StartDateDisplay = h.StartDate.ToString("dd-MMM-yyyy"),
                    EndDateDisplay = h.EndDate.ToString("dd-MMM-yyyy"),
                    FinalFeeDisplay = $"₹{h.FinalFee:N2}",
                    h.Status,
                    RemarksDisplay = string.IsNullOrWhiteSpace(h.Remarks) ? "-" : h.Remarks
                }).ToList();

                dgvHistory.DataSource = displayList;
                lblHistoryCount.Text = $"Total historical records found: {displayList.Count}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading membership history: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
