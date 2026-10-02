using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Laibrary_Payment_App.Data;
using Laibrary_Payment_App.Forms;
using Laibrary_Payment_App.Models;
using Laibrary_Payment_App.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Laibrary_Payment_App
{
    public partial class StudentMasterForm : Form
    {
        private readonly IStudentService _studentService;
        private int? _selectedStudentId;
        private bool _isPopulatingForm;

        public StudentMasterForm()
        {
            InitializeComponent();

            _studentService = ResolveStudentService();
            SetupGridColumns();
        }

        private static IStudentService ResolveStudentService()
        {
            if (Program.ServiceProvider != null)
            {
                var service = Program.ServiceProvider.GetService<IStudentService>();
                if (service != null)
                {
                    return service;
                }
            }

            return new StudentService(new LibraryDbContext());
        }

        protected override async void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            await LoadStudentsAsync();
            await GenerateCodeIfEmptyAsync();
        }

        private void SetupGridColumns()
        {
            dgvStudents.AutoGenerateColumns = false;
            dgvStudents.Columns.Clear();

            dgvStudents.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "StudentId",
                HeaderText = "ID",
                Width = 50,
                Visible = false
            });

            dgvStudents.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "StudentCode",
                HeaderText = "Code",
                Width = 90
            });

            dgvStudents.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "FullName",
                HeaderText = "Full Name",
                Width = 160
            });

            dgvStudents.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "MobileNo",
                HeaderText = "Mobile",
                Width = 110
            });

            dgvStudents.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Email",
                HeaderText = "Email",
                Width = 140
            });

            dgvStudents.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "CollegeName",
                HeaderText = "College",
                Width = 120
            });

            dgvStudents.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "CourseName",
                HeaderText = "Course",
                Width = 100
            });

            dgvStudents.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "StatusDisplay",
                HeaderText = "Status",
                Width = 80
            });

            dgvStudents.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "CreatedDateDisplay",
                HeaderText = "Created Date",
                Width = 110
            });
        }

        private async Task LoadStudentsAsync(string? searchTerm = null)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                var list = await _studentService.GetAllStudentsAsync(searchTerm);

                var displayList = list.Select(s => new
                {
                    s.StudentId,
                    s.StudentCode,
                    s.FullName,
                    s.MobileNo,
                    Email = s.Email ?? "-",
                    CollegeName = s.CollegeName ?? "-",
                    CourseName = s.CourseName ?? "-",
                    s.Address,
                    s.IsActive,
                    StatusDisplay = s.IsActive ? "Active" : "Inactive",
                    CreatedDateDisplay = s.CreatedDate.ToLocalTime().ToString("dd-MMM-yyyy")
                }).ToList();

                dgvStudents.DataSource = displayList;

                int totalCount = list.Count;
                int activeCount = list.Count(s => s.IsActive);
                int inactiveCount = totalCount - activeCount;

                lblRecordCount.Text = $"Showing {totalCount} student(s) ({activeCount} Active, {inactiveCount} Inactive)";
            }
            catch (Exception ex)
            {
                ShowStatus($"Error loading students: {ex.Message}", isError: true);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private async Task GenerateCodeIfEmptyAsync()
        {
            if (string.IsNullOrWhiteSpace(txtStudentCode.Text) && !_selectedStudentId.HasValue)
            {
                txtStudentCode.Text = await _studentService.GenerateNextStudentCodeAsync();
            }
        }

        private async void btnAutoCode_Click(object sender, EventArgs e)
        {
            txtStudentCode.Text = await _studentService.GenerateNextStudentCodeAsync();
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            HideStatus();

            var student = new StudentMaster
            {
                StudentCode = txtStudentCode.Text,
                FullName = txtFullName.Text,
                MobileNo = txtMobile.Text,
                Email = txtEmail.Text,
                CollegeName = txtCollege.Text,
                CourseName = txtCourse.Text,
                Address = txtAddress.Text,
                IsActive = chkIsActive.Checked
            };

            try
            {
                btnSave.Enabled = false;
                Cursor = Cursors.WaitCursor;

                var result = await _studentService.AddStudentAsync(student);
                if (result.IsSuccess)
                {
                    ShowStatus(result.Message, isError: false);
                    ResetFormInputs();
                    await LoadStudentsAsync();
                    await GenerateCodeIfEmptyAsync();
                }
                else
                {
                    ShowStatus(result.Message, isError: true);
                }
            }
            finally
            {
                btnSave.Enabled = true;
                Cursor = Cursors.Default;
            }
        }

        private async void btnUpdate_Click(object sender, EventArgs e)
        {
            if (!_selectedStudentId.HasValue)
            {
                ShowStatus("Please select a student from the list to update.", isError: true);
                return;
            }

            HideStatus();

            var student = new StudentMaster
            {
                StudentId = _selectedStudentId.Value,
                StudentCode = txtStudentCode.Text,
                FullName = txtFullName.Text,
                MobileNo = txtMobile.Text,
                Email = txtEmail.Text,
                CollegeName = txtCollege.Text,
                CourseName = txtCourse.Text,
                Address = txtAddress.Text,
                IsActive = chkIsActive.Checked
            };

            try
            {
                btnUpdate.Enabled = false;
                Cursor = Cursors.WaitCursor;

                var result = await _studentService.UpdateStudentAsync(student);
                if (result.IsSuccess)
                {
                    ShowStatus(result.Message, isError: false);
                    await LoadStudentsAsync(txtSearch.Text);
                }
                else
                {
                    ShowStatus(result.Message, isError: true);
                }
            }
            finally
            {
                btnUpdate.Enabled = true;
                Cursor = Cursors.Default;
            }
        }

        private async void btnDeactivate_Click(object sender, EventArgs e)
        {
            if (!_selectedStudentId.HasValue)
            {
                ShowStatus("Please select a student from the list.", isError: true);
                return;
            }

            string action = chkIsActive.Checked ? "deactivate" : "activate";
            var confirm = MessageBox.Show(
                $"Are you sure you want to {action} student '{txtFullName.Text}'?",
                "Confirm Status Change",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes)
            {
                return;
            }

            try
            {
                btnDeactivate.Enabled = false;
                Cursor = Cursors.WaitCursor;

                var result = await _studentService.ToggleActiveStatusAsync(_selectedStudentId.Value);
                if (result.IsSuccess)
                {
                    chkIsActive.Checked = result.NewStatus;
                    btnDeactivate.Text = result.NewStatus ? "Deactivate" : "Activate";
                    btnDeactivate.BackColor = result.NewStatus ? Color.FromArgb(198, 40, 40) : Color.FromArgb(46, 125, 50);
                    ShowStatus(result.Message, isError: false);
                    await LoadStudentsAsync(txtSearch.Text);
                }
                else
                {
                    ShowStatus(result.Message, isError: true);
                }
            }
            finally
            {
                btnDeactivate.Enabled = true;
                Cursor = Cursors.Default;
            }
        }

        private async void btnClear_Click(object sender, EventArgs e)
        {
            ResetFormInputs();
            HideStatus();
            await GenerateCodeIfEmptyAsync();
        }

        private void ResetFormInputs()
        {
            _selectedStudentId = null;
            txtStudentCode.Clear();
            txtFullName.Clear();
            txtMobile.Clear();
            txtEmail.Clear();
            txtCollege.Clear();
            txtCourse.Clear();
            txtAddress.Clear();
            chkIsActive.Checked = true;

            btnSave.Enabled = true;
            btnUpdate.Enabled = false;
            btnDeactivate.Enabled = false;
            btnViewHistory.Enabled = false;
            btnPaymentLedger.Enabled = false;
            btnDeactivate.Text = "Deactivate";
            btnDeactivate.BackColor = Color.FromArgb(198, 40, 40);

            if (dgvStudents.CurrentRow != null)
            {
                dgvStudents.ClearSelection();
            }
        }

        private void dgvStudents_SelectionChanged(object sender, EventArgs e)
        {
            if (_isPopulatingForm || dgvStudents.SelectedRows.Count == 0)
            {
                btnViewHistory.Enabled = false;
                btnPaymentLedger.Enabled = false;
                return;
            }

            var row = dgvStudents.SelectedRows[0];
            if (row.DataBoundItem == null)
            {
                btnViewHistory.Enabled = false;
                btnPaymentLedger.Enabled = false;
                return;
            }

            dynamic item = row.DataBoundItem;
            _selectedStudentId = (int)item.StudentId;

            _isPopulatingForm = true;
            try
            {
                txtStudentCode.Text = (string)item.StudentCode;
                txtFullName.Text = (string)item.FullName;
                txtMobile.Text = (string)item.MobileNo;
                txtEmail.Text = item.Email == "-" ? string.Empty : (string)item.Email;
                txtCollege.Text = item.CollegeName == "-" ? string.Empty : (string)item.CollegeName;
                txtCourse.Text = item.CourseName == "-" ? string.Empty : (string)item.CourseName;
                txtAddress.Text = item.Address ?? string.Empty;
                chkIsActive.Checked = (bool)item.IsActive;

                btnSave.Enabled = false;
                btnUpdate.Enabled = true;
                btnDeactivate.Enabled = true;
                btnViewHistory.Enabled = true;
                btnPaymentLedger.Enabled = true;

                if (chkIsActive.Checked)
                {
                    btnDeactivate.Text = "Deactivate";
                    btnDeactivate.BackColor = Color.FromArgb(198, 40, 40);
                }
                else
                {
                    btnDeactivate.Text = "Activate";
                    btnDeactivate.BackColor = Color.FromArgb(46, 125, 50);
                }

                HideStatus();
            }
            finally
            {
                _isPopulatingForm = false;
            }
        }

        private void btnViewHistory_Click(object sender, EventArgs e)
        {
            if (!_selectedStudentId.HasValue)
            {
                ShowStatus("Please select a student from the list to view membership history.", isError: true);
                return;
            }

            using var historyForm = new MembershipHistoryForm(_selectedStudentId.Value);
            historyForm.ShowDialog(this);
        }

        private void btnPaymentLedger_Click(object sender, EventArgs e)
        {
            if (!_selectedStudentId.HasValue)
            {
                ShowStatus("Please select a student from the list to view payment history.", isError: true);
                return;
            }

            using var ledgerForm = new StudentPaymentLedgerForm(_selectedStudentId.Value);
            ledgerForm.ShowDialog(this);
        }

        private async void btnSearch_Click(object sender, EventArgs e)
        {
            await LoadStudentsAsync(txtSearch.Text);
        }

        private async void btnRefresh_Click(object sender, EventArgs e)
        {
            txtSearch.Clear();
            await LoadStudentsAsync();
        }

        private async void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
                await LoadStudentsAsync(txtSearch.Text);
            }
        }

        private void ShowStatus(string message, bool isError)
        {
            lblStatusMessage.Text = message;
            lblStatusMessage.ForeColor = isError ? Color.FromArgb(198, 40, 40) : Color.FromArgb(46, 125, 50);
            lblStatusMessage.Visible = true;
        }

        private void HideStatus()
        {
            lblStatusMessage.Visible = false;
            lblStatusMessage.Text = string.Empty;
        }
    }
}
