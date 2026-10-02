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
    public partial class SeatMasterForm : Form
    {
        private readonly ISeatService _seatService;
        private int? _selectedSeatId;
        private bool _isPopulatingForm;

        public SeatMasterForm()
        {
            InitializeComponent();

            _seatService = ResolveSeatService();
            SetupGridColumns();
        }

        private static ISeatService ResolveSeatService()
        {
            if (Program.ServiceProvider != null)
            {
                var service = Program.ServiceProvider.GetService<ISeatService>();
                if (service != null)
                {
                    return service;
                }
            }

            return new SeatService(new LibraryDbContext());
        }

        protected override async void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            cmbStatus.SelectedIndex = 0; // Available
            cmbStatusFilter.SelectedIndex = 0; // All
            await LoadAreasFilterAsync();
            await LoadSeatsAsync();
        }

        private void SetupGridColumns()
        {
            dgvSeats.AutoGenerateColumns = false;
            dgvSeats.Columns.Clear();

            dgvSeats.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "SeatId",
                HeaderText = "ID",
                Width = 50,
                Visible = false
            });

            dgvSeats.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "SeatNumber",
                HeaderText = "Seat #",
                Width = 100
            });

            dgvSeats.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "AreaDisplay",
                HeaderText = "Area / Section",
                Width = 180
            });

            dgvSeats.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Status",
                HeaderText = "Status",
                Width = 120
            });

            dgvSeats.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "ActiveDisplay",
                HeaderText = "Active",
                Width = 80
            });

            dgvSeats.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "CreatedDateDisplay",
                HeaderText = "Created",
                Width = 110
            });
        }

        private async Task LoadAreasFilterAsync()
        {
            try
            {
                var areas = await _seatService.GetAllAreasAsync();
                cmbAreaFilter.Items.Clear();
                cmbAreaFilter.Items.Add("All Areas");
                foreach (var area in areas)
                {
                    cmbAreaFilter.Items.Add(area);
                }
                cmbAreaFilter.SelectedIndex = 0;
            }
            catch
            {
                // Silently fall back to default
            }
        }

        private async Task LoadSeatsAsync()
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                string? search = string.IsNullOrWhiteSpace(txtSearch.Text) ? null : txtSearch.Text.Trim();
                string? selectedArea = cmbAreaFilter.SelectedItem?.ToString();
                string? selectedStatus = cmbStatusFilter.SelectedItem?.ToString();

                var seats = await _seatService.GetAllSeatsAsync(
                    includeInactive: true,
                    area: selectedArea,
                    status: selectedStatus,
                    searchTerm: search);

                var displayList = seats.Select(s => new
                {
                    s.SeatId,
                    s.SeatNumber,
                    s.Area,
                    AreaDisplay = string.IsNullOrWhiteSpace(s.Area) ? "-" : s.Area,
                    s.Status,
                    s.IsActive,
                    ActiveDisplay = s.IsActive ? "Yes" : "No",
                    CreatedDateDisplay = s.CreatedDate.ToLocalTime().ToString("dd-MMM-yyyy")
                }).ToList();

                dgvSeats.DataSource = displayList;

                int total = seats.Count;
                int available = seats.Count(s => s.Status == "Available" && s.IsActive);
                int occupied = seats.Count(s => s.Status == "Occupied");
                int maintenance = seats.Count(s => s.Status == "Maintenance");
                int inactive = seats.Count(s => !s.IsActive || s.Status == "Inactive");

                lblRecordCount.Text = $"Showing {total} seat(s) ({available} Avail, {occupied} Occ, {maintenance} Maint, {inactive} Inactive)";
            }
            catch (Exception ex)
            {
                ShowStatus($"Error loading seats: {ex.Message}", isError: true);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            HideStatus();

            var seat = new SeatMaster
            {
                SeatNumber = txtSeatNumber.Text.Trim(),
                Area = txtArea.Text.Trim(),
                Status = cmbStatus.SelectedItem?.ToString() ?? "Available",
                IsActive = chkIsActive.Checked
            };

            try
            {
                btnSave.Enabled = false;
                Cursor = Cursors.WaitCursor;

                var result = await _seatService.AddSeatAsync(seat);
                if (result.IsSuccess)
                {
                    ShowStatus(result.Message, isError: false);
                    ResetFormInputs();
                    await LoadAreasFilterAsync();
                    await LoadSeatsAsync();
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
            if (!_selectedSeatId.HasValue)
            {
                ShowStatus("Please select a seat from the list to update.", isError: true);
                return;
            }

            HideStatus();

            var seat = new SeatMaster
            {
                SeatId = _selectedSeatId.Value,
                SeatNumber = txtSeatNumber.Text.Trim(),
                Area = txtArea.Text.Trim(),
                Status = cmbStatus.SelectedItem?.ToString() ?? "Available",
                IsActive = chkIsActive.Checked
            };

            try
            {
                btnUpdate.Enabled = false;
                Cursor = Cursors.WaitCursor;

                var result = await _seatService.UpdateSeatAsync(seat);
                if (result.IsSuccess)
                {
                    ShowStatus(result.Message, isError: false);
                    await LoadAreasFilterAsync();
                    await LoadSeatsAsync();
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
            if (!_selectedSeatId.HasValue)
            {
                ShowStatus("Please select a seat from the list.", isError: true);
                return;
            }

            string action = chkIsActive.Checked ? "deactivate" : "activate";
            var confirm = MessageBox.Show(
                $"Are you sure you want to {action} seat '{txtSeatNumber.Text}'?",
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

                var result = await _seatService.ToggleActiveStatusAsync(_selectedSeatId.Value);
                if (result.IsSuccess)
                {
                    chkIsActive.Checked = result.NewStatus;
                    btnDeactivate.Text = result.NewStatus ? "Deactivate" : "Activate";
                    btnDeactivate.BackColor = result.NewStatus ? Color.FromArgb(198, 40, 40) : Color.FromArgb(46, 125, 50);
                    ShowStatus(result.Message, isError: false);
                    await LoadSeatsAsync();
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
            _selectedSeatId = null;
            txtSeatNumber.Clear();
            txtArea.Clear();
            cmbStatus.SelectedIndex = 0; // Available
            chkIsActive.Checked = true;

            btnSave.Enabled = true;
            btnUpdate.Enabled = false;
            btnDeactivate.Enabled = false;
            btnDeactivate.Text = "Deactivate";
            btnDeactivate.BackColor = Color.FromArgb(198, 40, 40);

            if (dgvSeats.CurrentRow != null)
            {
                dgvSeats.ClearSelection();
            }
        }

        private void dgvSeats_SelectionChanged(object sender, EventArgs e)
        {
            if (_isPopulatingForm || dgvSeats.SelectedRows.Count == 0)
            {
                return;
            }

            var row = dgvSeats.SelectedRows[0];
            if (row.DataBoundItem == null)
            {
                return;
            }

            dynamic item = row.DataBoundItem;
            _selectedSeatId = (int)item.SeatId;

            _isPopulatingForm = true;
            try
            {
                txtSeatNumber.Text = (string)item.SeatNumber;
                txtArea.Text = item.Area ?? string.Empty;

                var status = (string)item.Status;
                int statusIndex = cmbStatus.FindStringExact(status);
                if (statusIndex >= 0)
                {
                    cmbStatus.SelectedIndex = statusIndex;
                }

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
            await LoadSeatsAsync();
        }

        private async void btnRefresh_Click(object sender, EventArgs e)
        {
            txtSearch.Clear();
            cmbAreaFilter.SelectedIndex = 0;
            cmbStatusFilter.SelectedIndex = 0;
            await LoadSeatsAsync();
        }

        private async void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
                await LoadSeatsAsync();
            }
        }

        private async void cmbStatusFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            await LoadSeatsAsync();
        }

        private async void cmbAreaFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            await LoadSeatsAsync();
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
