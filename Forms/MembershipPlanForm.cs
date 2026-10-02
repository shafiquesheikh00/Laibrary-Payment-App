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
    public partial class MembershipPlanForm : Form
    {
        private readonly IMembershipPlanService _planService;
        private int? _selectedPlanId;
        private bool _isPopulatingForm;

        public MembershipPlanForm()
        {
            InitializeComponent();

            _planService = ResolvePlanService();
            SetupGridColumns();
        }

        private static IMembershipPlanService ResolvePlanService()
        {
            if (Program.ServiceProvider != null)
            {
                var service = Program.ServiceProvider.GetService<IMembershipPlanService>();
                if (service != null)
                {
                    return service;
                }
            }

            return new MembershipPlanService(new LibraryDbContext());
        }

        protected override async void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            cmbDurationUnit.SelectedIndex = 2; // Default to "Month"
            cmbStatusFilter.SelectedIndex = 0; // Default to "All"
            await LoadPlansAsync();
        }

        private void SetupGridColumns()
        {
            dgvPlans.AutoGenerateColumns = false;
            dgvPlans.Columns.Clear();

            dgvPlans.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "PlanId",
                HeaderText = "ID",
                Width = 50,
                Visible = false
            });

            dgvPlans.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "PlanName",
                HeaderText = "Plan Name",
                Width = 160
            });

            dgvPlans.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "DurationDisplay",
                HeaderText = "Duration",
                Width = 110
            });

            dgvPlans.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "FeeDisplay",
                HeaderText = "Fee",
                Width = 100
            });

            dgvPlans.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "StatusDisplay",
                HeaderText = "Status",
                Width = 85
            });

            dgvPlans.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "CreatedDateDisplay",
                HeaderText = "Created",
                Width = 110
            });
        }

        private async Task LoadPlansAsync()
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                string? search = string.IsNullOrWhiteSpace(txtSearch.Text) ? null : txtSearch.Text.Trim();
                bool includeInactive = cmbStatusFilter.SelectedIndex != 1; // 1 is "Active" only

                var plans = await _planService.GetAllPlansAsync(includeInactive: true, searchTerm: search);

                if (cmbStatusFilter.SelectedIndex == 1) // Active only
                {
                    plans = plans.Where(p => p.IsActive).ToList();
                }
                else if (cmbStatusFilter.SelectedIndex == 2) // Inactive only
                {
                    plans = plans.Where(p => !p.IsActive).ToList();
                }

                var displayList = plans.Select(p => new
                {
                    p.PlanId,
                    p.PlanName,
                    p.DurationValue,
                    p.DurationUnit,
                    p.Fee,
                    p.IsActive,
                    DurationDisplay = $"{p.DurationValue} {p.DurationUnit}{(p.DurationValue > 1 && !p.DurationUnit.EndsWith("s", StringComparison.OrdinalIgnoreCase) ? "s" : "")}",
                    FeeDisplay = $"₹{p.Fee:N2}",
                    StatusDisplay = p.IsActive ? "Active" : "Inactive",
                    CreatedDateDisplay = p.CreatedDate.ToLocalTime().ToString("dd-MMM-yyyy")
                }).ToList();

                dgvPlans.DataSource = displayList;
                lblRecordCount.Text = $"Showing {displayList.Count} plan(s)";
            }
            catch (Exception ex)
            {
                ShowStatus($"Error loading plans: {ex.Message}", isError: true);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            HideStatus();

            if (!decimal.TryParse(txtFee.Text.Trim(), out decimal fee))
            {
                ShowStatus("Please enter a valid numeric fee.", isError: true);
                return;
            }

            var plan = new MembershipPlan
            {
                PlanName = txtPlanName.Text.Trim(),
                DurationValue = (int)numDurationValue.Value,
                DurationUnit = cmbDurationUnit.SelectedItem?.ToString() ?? "Month",
                Fee = fee,
                IsActive = chkIsActive.Checked
            };

            try
            {
                btnSave.Enabled = false;
                Cursor = Cursors.WaitCursor;

                var result = await _planService.AddPlanAsync(plan);
                if (result.IsSuccess)
                {
                    ShowStatus(result.Message, isError: false);
                    ResetFormInputs();
                    await LoadPlansAsync();
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
            if (!_selectedPlanId.HasValue)
            {
                ShowStatus("Please select a plan to update.", isError: true);
                return;
            }

            HideStatus();

            if (!decimal.TryParse(txtFee.Text.Trim(), out decimal fee))
            {
                ShowStatus("Please enter a valid numeric fee.", isError: true);
                return;
            }

            var plan = new MembershipPlan
            {
                PlanId = _selectedPlanId.Value,
                PlanName = txtPlanName.Text.Trim(),
                DurationValue = (int)numDurationValue.Value,
                DurationUnit = cmbDurationUnit.SelectedItem?.ToString() ?? "Month",
                Fee = fee,
                IsActive = chkIsActive.Checked
            };

            try
            {
                btnUpdate.Enabled = false;
                Cursor = Cursors.WaitCursor;

                var result = await _planService.UpdatePlanAsync(plan);
                if (result.IsSuccess)
                {
                    ShowStatus(result.Message, isError: false);
                    await LoadPlansAsync();
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
            if (!_selectedPlanId.HasValue)
            {
                ShowStatus("Please select a plan from the list.", isError: true);
                return;
            }

            string action = chkIsActive.Checked ? "deactivate" : "activate";
            var confirm = MessageBox.Show(
                $"Are you sure you want to {action} plan '{txtPlanName.Text}'?",
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

                var result = await _planService.ToggleActiveStatusAsync(_selectedPlanId.Value);
                if (result.IsSuccess)
                {
                    chkIsActive.Checked = result.NewStatus;
                    btnDeactivate.Text = result.NewStatus ? "Deactivate" : "Activate";
                    btnDeactivate.BackColor = result.NewStatus ? Color.FromArgb(198, 40, 40) : Color.FromArgb(46, 125, 50);
                    ShowStatus(result.Message, isError: false);
                    await LoadPlansAsync();
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

        private void btnClear_Click(object sender, EventArgs e)
        {
            ResetFormInputs();
            HideStatus();
        }

        private void ResetFormInputs()
        {
            _selectedPlanId = null;
            txtPlanName.Clear();
            numDurationValue.Value = 1;
            cmbDurationUnit.SelectedIndex = 2; // Month
            txtFee.Text = "0.00";
            chkIsActive.Checked = true;

            btnSave.Enabled = true;
            btnUpdate.Enabled = false;
            btnDeactivate.Enabled = false;
            btnDeactivate.Text = "Deactivate";
            btnDeactivate.BackColor = Color.FromArgb(198, 40, 40);

            if (dgvPlans.CurrentRow != null)
            {
                dgvPlans.ClearSelection();
            }
        }

        private void dgvPlans_SelectionChanged(object sender, EventArgs e)
        {
            if (_isPopulatingForm || dgvPlans.SelectedRows.Count == 0)
            {
                return;
            }

            var row = dgvPlans.SelectedRows[0];
            if (row.DataBoundItem == null)
            {
                return;
            }

            dynamic item = row.DataBoundItem;
            _selectedPlanId = (int)item.PlanId;

            _isPopulatingForm = true;
            try
            {
                txtPlanName.Text = (string)item.PlanName;
                numDurationValue.Value = Math.Max(1, Math.Min(365, (int)item.DurationValue));

                var unit = (string)item.DurationUnit;
                int unitIndex = cmbDurationUnit.FindStringExact(unit);
                if (unitIndex >= 0)
                {
                    cmbDurationUnit.SelectedIndex = unitIndex;
                }

                txtFee.Text = ((decimal)item.Fee).ToString("F2");
                chkIsActive.Checked = (bool)item.IsActive;

                btnSave.Enabled = false;
                btnUpdate.Enabled = true;
                btnDeactivate.Enabled = true;

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

        private async void btnSearch_Click(object sender, EventArgs e)
        {
            await LoadPlansAsync();
        }

        private async void btnRefresh_Click(object sender, EventArgs e)
        {
            txtSearch.Clear();
            cmbStatusFilter.SelectedIndex = 0;
            await LoadPlansAsync();
        }

        private async void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
                await LoadPlansAsync();
            }
        }

        private async void cmbStatusFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            await LoadPlansAsync();
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
