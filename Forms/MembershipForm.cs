using System;
using System.Collections.Generic;
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
    public partial class MembershipForm : Form
    {
        private readonly IMembershipService _membershipService;
        private readonly IMembershipPlanService _planService;
        private readonly IStudentService _studentService;
        private readonly ISeatService _seatService;

        private List<StudentMaster> _activeStudents = new();
        private List<MembershipPlan> _activePlans = new();
        private int? _selectedMembershipId;
        private bool _isPopulatingForm;
        private bool _isCalculatingDates;

        private sealed record StudentComboItem(int StudentId, string Display);
        private sealed record PlanComboItem(int PlanId, string Display, decimal Fee, int DurationValue, string DurationUnit);
        private sealed record SeatComboItem(int? SeatId, string Display);

        public MembershipForm()
        {
            InitializeComponent();

            _membershipService = ResolveMembershipService();
            _planService = ResolvePlanService();
            _studentService = ResolveStudentService();
            _seatService = ResolveSeatService();

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

        private static IMembershipPlanService ResolvePlanService()
        {
            if (Program.ServiceProvider != null)
            {
                var s = Program.ServiceProvider.GetService<IMembershipPlanService>();
                if (s != null) return s;
            }
            return new MembershipPlanService(new LibraryDbContext());
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

        private static ISeatService ResolveSeatService()
        {
            if (Program.ServiceProvider != null)
            {
                var s = Program.ServiceProvider.GetService<ISeatService>();
                if (s != null) return s;
            }
            return new SeatService(new LibraryDbContext());
        }

        protected override async void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            dtpStartDate.Value = DateTime.Today;
            cmbFilterStatus.SelectedIndex = 0; // All

            await LoadInitialDropdownsAsync();
            await LoadMembershipsGridAsync();
        }

        private void SetupGridColumns()
        {
            dgvMemberships.AutoGenerateColumns = false;
            dgvMemberships.Columns.Clear();

            dgvMemberships.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "MembershipId",
                HeaderText = "ID",
                Width = 45
            });

            dgvMemberships.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "StudentCode",
                HeaderText = "Code",
                Width = 85
            });

            dgvMemberships.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "StudentName",
                HeaderText = "Student Name",
                Width = 140
            });

            dgvMemberships.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "PlanName",
                HeaderText = "Plan",
                Width = 95
            });

            dgvMemberships.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "SeatDisplay",
                HeaderText = "Seat",
                Width = 70
            });

            dgvMemberships.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "StartDateDisplay",
                HeaderText = "Start Date",
                Width = 95
            });

            dgvMemberships.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "EndDateDisplay",
                HeaderText = "End Date",
                Width = 95
            });

            dgvMemberships.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "FeeDisplay",
                HeaderText = "Fee",
                Width = 75
            });

            dgvMemberships.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "DiscountDisplay",
                HeaderText = "Disc",
                Width = 65
            });

            dgvMemberships.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "FinalFeeDisplay",
                HeaderText = "Final Fee",
                Width = 85
            });

            dgvMemberships.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Status",
                HeaderText = "Status",
                Width = 80
            });
        }

        private async Task LoadInitialDropdownsAsync()
        {
            try
            {
                // Active students
                var allStudents = await _studentService.GetAllStudentsAsync();
                _activeStudents = allStudents.Where(s => s.IsActive).OrderBy(s => s.FullName).ToList();

                cmbStudent.Items.Clear();
                cmbStudent.Items.Add("-- Select Active Student --");
                foreach (var s in _activeStudents)
                {
                    cmbStudent.Items.Add(new StudentComboItem(s.StudentId, $"[{s.StudentCode}] {s.FullName} ({s.MobileNo})"));
                }
                cmbStudent.SelectedIndex = 0;

                // Active plans
                _activePlans = await _planService.GetAllPlansAsync(includeInactive: false);
                cmbPlan.Items.Clear();
                cmbFilterPlan.Items.Clear();

                cmbPlan.Items.Add("-- Select Membership Plan --");
                cmbFilterPlan.Items.Add("All Plans");

                foreach (var p in _activePlans)
                {
                    var display = $"{p.PlanName} ({p.DurationValue} {p.DurationUnit}) - ₹{p.Fee:N2}";
                    cmbPlan.Items.Add(new PlanComboItem(p.PlanId, display, p.Fee, p.DurationValue, p.DurationUnit));
                    cmbFilterPlan.Items.Add(p.PlanName);
                }
                cmbPlan.SelectedIndex = 0;
                cmbFilterPlan.SelectedIndex = 0;

                await RefreshAvailableSeatsAsync();
            }
            catch (Exception ex)
            {
                ShowStatus($"Error loading dropdown data: {ex.Message}", isError: true);
            }
        }

        private async Task RefreshAvailableSeatsAsync(int? includeCurrentSeatId = null)
        {
            try
            {
                var startDate = dtpStartDate.Value.Date;
                var endDate = dtpEndDate.Value.Date;

                var availableSeats = await _membershipService.GetAvailableSeatsAsync(
                    startDate,
                    endDate,
                    currentMembershipId: _selectedMembershipId);

                int? currentSelectedSeatId = (cmbSeat.SelectedItem as SeatComboItem)?.SeatId ?? includeCurrentSeatId;

                cmbSeat.Items.Clear();
                cmbSeat.Items.Add(new SeatComboItem(null, "No Seat (Not Required)"));

                int selectIndex = 0;
                for (int i = 0; i < availableSeats.Count; i++)
                {
                    var s = availableSeats[i];
                    var item = new SeatComboItem(s.SeatId, $"{s.SeatNumber} ({s.Area ?? "General"})");
                    cmbSeat.Items.Add(item);

                    if (currentSelectedSeatId.HasValue && s.SeatId == currentSelectedSeatId.Value)
                    {
                        selectIndex = i + 1;
                    }
                }

                // If currently selected seat is not in available list (e.g. occupied by this membership during edit)
                if (currentSelectedSeatId.HasValue && selectIndex == 0)
                {
                    var currentSeat = await _seatService.GetSeatByIdAsync(currentSelectedSeatId.Value);
                    if (currentSeat != null)
                    {
                        var item = new SeatComboItem(currentSeat.SeatId, $"{currentSeat.SeatNumber} ({currentSeat.Area ?? "General"}) [Current]");
                        cmbSeat.Items.Add(item);
                        selectIndex = cmbSeat.Items.Count - 1;
                    }
                }

                cmbSeat.SelectedIndex = selectIndex;
            }
            catch
            {
                // Silently ignore during early initialization
            }
        }

        private void RecalculateEndDate()
        {
            if (_isCalculatingDates || _isPopulatingForm)
            {
                return;
            }

            if (cmbPlan.SelectedItem is not PlanComboItem planItem)
            {
                return;
            }

            _isCalculatingDates = true;
            try
            {
                var startDate = dtpStartDate.Value.Date;
                var calculatedEnd = MembershipDateCalculator.CalculateEndDate(
                    startDate,
                    planItem.DurationValue,
                    planItem.DurationUnit);

                dtpEndDate.Value = calculatedEnd;
            }
            finally
            {
                _isCalculatingDates = false;
            }
        }

        private void UpdateFeeCalculations()
        {
            decimal fee = 0m;
            if (cmbPlan.SelectedItem is PlanComboItem planItem)
            {
                fee = planItem.Fee;
            }

            txtPlanFee.Text = $"₹{fee:N2}";

            decimal.TryParse(txtDiscount.Text.Trim(), out decimal discount);
            if (discount < 0) discount = 0;
            if (discount > fee) discount = fee;

            decimal finalFee = fee - discount;
            txtFinalFee.Text = $"₹{finalFee:N2}";
        }

        private async void cmbPlan_SelectedIndexChanged(object sender, EventArgs e)
        {
            RecalculateEndDate();
            UpdateFeeCalculations();
            await RefreshAvailableSeatsAsync();
        }

        private async void dtpStartDate_ValueChanged(object sender, EventArgs e)
        {
            RecalculateEndDate();
            await RefreshAvailableSeatsAsync();
        }

        private async void dtpEndDate_ValueChanged(object sender, EventArgs e)
        {
            await RefreshAvailableSeatsAsync();
        }

        private void txtDiscount_TextChanged(object sender, EventArgs e)
        {
            UpdateFeeCalculations();
        }

        private async void cmbStudent_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbStudent.SelectedItem is StudentComboItem studentItem && studentItem.StudentId > 0)
            {
                var activeMembership = await _membershipService.GetActiveMembershipAsync(studentItem.StudentId);
                if (activeMembership != null)
                {
                    ShowStatus($"Note: Student already has active membership #{activeMembership.MembershipId} ({activeMembership.StartDate:dd-MMM-yyyy} to {activeMembership.EndDate:dd-MMM-yyyy}, Seat: {activeMembership.Seat?.SeatNumber ?? "None"}).", isError: false);
                }
                else
                {
                    HideStatus();
                }
            }
        }

        private async Task LoadMembershipsGridAsync()
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                string? search = string.IsNullOrWhiteSpace(txtFilterSearch.Text) ? null : txtFilterSearch.Text.Trim();
                string? planFilter = cmbFilterPlan.SelectedIndex > 0 ? cmbFilterPlan.SelectedItem?.ToString() : null;
                string? statusFilter = cmbFilterStatus.SelectedIndex > 0 ? cmbFilterStatus.SelectedItem?.ToString() : null;

                int? planId = null;
                if (!string.IsNullOrWhiteSpace(planFilter))
                {
                    var matchedPlan = _activePlans.FirstOrDefault(p => p.PlanName == planFilter);
                    if (matchedPlan != null) planId = matchedPlan.PlanId;
                }

                var list = await _membershipService.GetAllMembershipsAsync(
                    planId: planId,
                    status: statusFilter,
                    searchTerm: search);

                var displayList = list.Select(m => new
                {
                    m.MembershipId,
                    StudentCode = m.Student?.StudentCode ?? "-",
                    StudentName = m.Student?.FullName ?? "-",
                    PlanName = m.Plan?.PlanName ?? "-",
                    SeatDisplay = m.Seat?.SeatNumber ?? "None",
                    StartDateDisplay = m.StartDate.ToString("dd-MMM-yyyy"),
                    EndDateDisplay = m.EndDate.ToString("dd-MMM-yyyy"),
                    FeeDisplay = $"₹{m.ActualFee:N2}",
                    DiscountDisplay = $"₹{m.DiscountAmount:N2}",
                    FinalFeeDisplay = $"₹{m.FinalFee:N2}",
                    m.Status,
                    m.Remarks,
                    m.StudentId,
                    m.PlanId,
                    m.SeatId,
                    m.StartDate,
                    m.EndDate,
                    m.ActualFee,
                    m.DiscountAmount
                }).ToList();

                dgvMemberships.DataSource = displayList;

                int total = list.Count;
                int active = list.Count(m => m.Status == "Active");
                int expired = list.Count(m => m.Status == "Expired");
                int cancelled = list.Count(m => m.Status == "Cancelled");

                lblRecordCount.Text = $"Showing {total} membership(s) ({active} Active, {expired} Expired, {cancelled} Cancelled)";
            }
            catch (Exception ex)
            {
                ShowStatus($"Error loading memberships: {ex.Message}", isError: true);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            HideStatus();

            if (cmbStudent.SelectedItem is not StudentComboItem studentItem || studentItem.StudentId <= 0)
            {
                ShowStatus("Please select an active student.", isError: true);
                return;
            }

            if (cmbPlan.SelectedItem is not PlanComboItem planItem || planItem.PlanId <= 0)
            {
                ShowStatus("Please select a membership plan.", isError: true);
                return;
            }

            if (!decimal.TryParse(txtDiscount.Text.Trim(), out decimal discount) || discount < 0)
            {
                ShowStatus("Discount amount cannot be negative.", isError: true);
                return;
            }

            if (discount > planItem.Fee)
            {
                ShowStatus($"Discount (₹{discount:N2}) cannot exceed the plan fee (₹{planItem.Fee:N2}).", isError: true);
                return;
            }

            int? seatId = (cmbSeat.SelectedItem as SeatComboItem)?.SeatId;

            var membership = new StudentMembership
            {
                StudentId = studentItem.StudentId,
                PlanId = planItem.PlanId,
                SeatId = seatId,
                StartDate = dtpStartDate.Value.Date,
                EndDate = dtpEndDate.Value.Date,
                ActualFee = planItem.Fee,
                DiscountAmount = discount,
                FinalFee = planItem.Fee - discount,
                Status = "Active",
                Remarks = string.IsNullOrWhiteSpace(txtRemarks.Text) ? null : txtRemarks.Text.Trim()
            };

            try
            {
                btnSave.Enabled = false;
                Cursor = Cursors.WaitCursor;

                var result = await _membershipService.CreateMembershipAsync(membership);
                if (result.IsSuccess)
                {
                    ShowStatus(result.Message, isError: false);
                    ResetFormInputs();
                    await LoadMembershipsGridAsync();
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

        private async void btnRenew_Click(object sender, EventArgs e)
        {
            if (!_selectedMembershipId.HasValue)
            {
                ShowStatus("Please select an existing membership from the grid to renew.", isError: true);
                return;
            }

            if (cmbPlan.SelectedItem is not PlanComboItem planItem || planItem.PlanId <= 0)
            {
                ShowStatus("Please select a valid membership plan for renewal.", isError: true);
                return;
            }

            if (!decimal.TryParse(txtDiscount.Text.Trim(), out decimal discount) || discount < 0)
            {
                ShowStatus("Discount amount cannot be negative.", isError: true);
                return;
            }

            int? seatId = (cmbSeat.SelectedItem as SeatComboItem)?.SeatId;

            var confirm = MessageBox.Show(
                $"Renew membership #{_selectedMembershipId.Value} starting on {dtpStartDate.Value:dd-MMM-yyyy}?",
                "Confirm Membership Renewal",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes)
            {
                return;
            }

            try
            {
                btnRenew.Enabled = false;
                Cursor = Cursors.WaitCursor;

                var result = await _membershipService.RenewMembershipAsync(
                    _selectedMembershipId.Value,
                    planItem.PlanId,
                    seatId,
                    explicitStartDate: dtpStartDate.Value.Date,
                    discountAmount: discount,
                    remarks: txtRemarks.Text.Trim());

                if (result.IsSuccess)
                {
                    ShowStatus(result.Message, isError: false);
                    ResetFormInputs();
                    await LoadMembershipsGridAsync();
                }
                else
                {
                    ShowStatus(result.Message, isError: true);
                }
            }
            finally
            {
                btnRenew.Enabled = true;
                Cursor = Cursors.Default;
            }
        }

        private async void btnCancelMembership_Click(object sender, EventArgs e)
        {
            if (!_selectedMembershipId.HasValue)
            {
                ShowStatus("Please select an active membership to cancel.", isError: true);
                return;
            }

            string reason = txtRemarks.Text.Trim();
            if (string.IsNullOrWhiteSpace(reason))
            {
                reason = "Cancelled by Administrator";
            }

            var confirm = MessageBox.Show(
                $"Are you sure you want to cancel membership #{_selectedMembershipId.Value}? Any assigned seat will be released.",
                "Confirm Membership Cancellation",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (confirm != DialogResult.Yes)
            {
                return;
            }

            try
            {
                btnCancelMembership.Enabled = false;
                Cursor = Cursors.WaitCursor;

                var result = await _membershipService.CancelMembershipAsync(_selectedMembershipId.Value, reason);
                if (result.IsSuccess)
                {
                    ShowStatus(result.Message, isError: false);
                    ResetFormInputs();
                    await LoadMembershipsGridAsync();
                }
                else
                {
                    ShowStatus(result.Message, isError: true);
                }
            }
            finally
            {
                btnCancelMembership.Enabled = true;
                Cursor = Cursors.Default;
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ResetFormInputs();
            HideStatus();
        }

        private void ResetFormInputs()
        {
            _selectedMembershipId = null;
            cmbStudent.Enabled = true;
            cmbStudent.SelectedIndex = 0;
            cmbPlan.SelectedIndex = 0;
            dtpStartDate.Value = DateTime.Today;
            dtpEndDate.Value = DateTime.Today;
            txtPlanFee.Text = "₹0.00";
            txtDiscount.Text = "0.00";
            txtFinalFee.Text = "₹0.00";
            txtRemarks.Clear();

            btnSave.Enabled = true;
            btnRenew.Enabled = false;
            btnCancelMembership.Enabled = false;

            if (dgvMemberships.CurrentRow != null)
            {
                dgvMemberships.ClearSelection();
            }

            _ = RefreshAvailableSeatsAsync();
        }

        private async void dgvMemberships_SelectionChanged(object sender, EventArgs e)
        {
            if (_isPopulatingForm || dgvMemberships.SelectedRows.Count == 0)
            {
                return;
            }

            var row = dgvMemberships.SelectedRows[0];
            if (row.DataBoundItem == null)
            {
                return;
            }

            dynamic item = row.DataBoundItem;
            _selectedMembershipId = (int)item.MembershipId;
            int studentId = (int)item.StudentId;
            int planId = (int)item.PlanId;
            int? seatId = (int?)item.SeatId;
            string status = (string)item.Status;
            DateTime startDate = (DateTime)item.StartDate;
            DateTime endDate = (DateTime)item.EndDate;
            decimal discount = (decimal)item.DiscountAmount;

            _isPopulatingForm = true;
            try
            {
                // Select student
                for (int i = 0; i < cmbStudent.Items.Count; i++)
                {
                    if (cmbStudent.Items[i] is StudentComboItem sc && sc.StudentId == studentId)
                    {
                        cmbStudent.SelectedIndex = i;
                        break;
                    }
                }

                // Select plan
                for (int i = 0; i < cmbPlan.Items.Count; i++)
                {
                    if (cmbPlan.Items[i] is PlanComboItem pc && pc.PlanId == planId)
                    {
                        cmbPlan.SelectedIndex = i;
                        break;
                    }
                }

                dtpStartDate.Value = startDate;
                dtpEndDate.Value = endDate;
                txtDiscount.Text = discount.ToString("F2");
                txtRemarks.Text = item.Remarks ?? string.Empty;

                btnSave.Enabled = false; // To prevent accidentally creating duplicate when viewing existing
                btnRenew.Enabled = true;
                btnCancelMembership.Enabled = status == "Active";

                UpdateFeeCalculations();
                HideStatus();
            }
            finally
            {
                _isPopulatingForm = false;
            }

            await RefreshAvailableSeatsAsync(includeCurrentSeatId: seatId);
        }

        private async void btnFilter_Click(object sender, EventArgs e)
        {
            await LoadMembershipsGridAsync();
        }

        private async void btnResetFilters_Click(object sender, EventArgs e)
        {
            txtFilterSearch.Clear();
            cmbFilterPlan.SelectedIndex = 0;
            cmbFilterStatus.SelectedIndex = 0;
            await LoadMembershipsGridAsync();
        }

        private async void txtFilterSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
                await LoadMembershipsGridAsync();
            }
        }

        private async void cmbFilterPlan_SelectedIndexChanged(object sender, EventArgs e)
        {
            await LoadMembershipsGridAsync();
        }

        private async void cmbFilterStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            await LoadMembershipsGridAsync();
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
