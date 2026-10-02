using System;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Laibrary_Payment_App.Data;
using Laibrary_Payment_App.Models;
using Laibrary_Payment_App.Services;

namespace Laibrary_Payment_App.Forms
{
    public partial class InvoiceViewForm : Form
    {
        private readonly int _invoiceId;
        private readonly IBillingService _billingService;
        private readonly LibraryDbContext _dbContext;

        public InvoiceViewForm(int invoiceId)
        {
            InitializeComponent();
            _invoiceId = invoiceId;
            _dbContext = new LibraryDbContext();
            _billingService = new BillingService(_dbContext);
        }

        private async void InvoiceViewForm_Load(object sender, EventArgs e)
        {
            SetupItemsGrid();
            await LoadInvoiceDataAsync();
        }

        private void SetupItemsGrid()
        {
            dgvItems.AutoGenerateColumns = false;
            dgvItems.Columns.Clear();

            dgvItems.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(InvoiceDetail.Description),
                HeaderText = "Description",
                FillWeight = 50,
                ReadOnly = true
            });

            dgvItems.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(InvoiceDetail.Quantity),
                HeaderText = "Qty",
                Width = 70,
                DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter },
                ReadOnly = true
            });

            dgvItems.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(InvoiceDetail.Rate),
                HeaderText = "Rate (?)",
                Width = 120,
                DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight, Format = "N2" },
                ReadOnly = true
            });

            dgvItems.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(InvoiceDetail.Amount),
                HeaderText = "Amount (?)",
                Width = 130,
                DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight, Format = "N2" },
                ReadOnly = true
            });
        }

        private async Task LoadInvoiceDataAsync()
        {
            try
            {
                var invoice = await _billingService.GetInvoiceByIdWithDetailsAsync(_invoiceId);
                if (invoice == null)
                {
                    MessageBox.Show("Invoice not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Close();
                    return;
                }

                // Invoice Meta
                lblInvoiceNo.Text = $"Invoice No: {invoice.InvoiceNo}";
                lblInvoiceDate.Text = $"Invoice Date: {invoice.InvoiceDate:dd-MMM-yyyy}";
                lblDueDate.Text = $"Due Date: {invoice.DueDate:dd-MMM-yyyy}";
                lblTopTitle.Text = $"Official Student Invoice - {invoice.InvoiceNo}";

                // Status Badge
                lblStatusBadge.Text = invoice.Status.ToUpper();
                lblPaymentStatusBadge.Text = invoice.Status.ToUpper();

                if (invoice.Status == "Paid")
                {
                    lblStatusBadge.BackColor = Color.FromArgb(209, 250, 229);
                    lblStatusBadge.ForeColor = Color.FromArgb(6, 95, 70);
                    lblPaymentStatusBadge.BackColor = Color.FromArgb(209, 250, 229);
                    lblPaymentStatusBadge.ForeColor = Color.FromArgb(6, 95, 70);
                }
                else if (invoice.Status == "Cancelled")
                {
                    lblStatusBadge.BackColor = Color.FromArgb(241, 245, 249);
                    lblStatusBadge.ForeColor = Color.FromArgb(100, 116, 139);
                    lblPaymentStatusBadge.BackColor = Color.FromArgb(241, 245, 249);
                    lblPaymentStatusBadge.ForeColor = Color.FromArgb(100, 116, 139);
                }
                else
                {
                    lblStatusBadge.BackColor = Color.FromArgb(254, 242, 242);
                    lblStatusBadge.ForeColor = Color.FromArgb(185, 28, 28);
                    lblPaymentStatusBadge.BackColor = Color.FromArgb(254, 242, 242);
                    lblPaymentStatusBadge.ForeColor = Color.FromArgb(185, 28, 28);
                }

                // Student Details
                lblStudentCode.Text = $"[{invoice.Student?.StudentCode ?? "N/A"}]";
                lblStudentName.Text = $"Name: {invoice.Student?.FullName ?? "N/A"}";
                lblStudentMobile.Text = $"Mobile: {invoice.Student?.MobileNo ?? "N/A"}";
                lblStudentSeat.Text = $"Seat: {invoice.Membership?.Seat?.SeatNumber ?? "None"}";

                // Membership Details
                if (invoice.Membership != null)
                {
                    lblMembershipPlan.Text = $"Plan: {invoice.Membership.Plan?.PlanName ?? "General"}";
                    lblMembershipStart.Text = $"Start Date: {invoice.Membership.StartDate:dd-MMM-yyyy}";
                    lblMembershipEnd.Text = $"End Date: {invoice.Membership.EndDate:dd-MMM-yyyy}";
                }
                else
                {
                    lblMembershipPlan.Text = "Plan: General Library Charges";
                    lblMembershipStart.Text = "Start Date: N/A";
                    lblMembershipEnd.Text = "End Date: N/A";
                }

                // Line items
                dgvItems.DataSource = invoice.InvoiceDetails.ToList();

                // Totals
                lblSubtotalValue.Text = $"?{invoice.SubTotal:N2}";
                lblDiscountValue.Text = $"-?{invoice.DiscountAmount:N2}";
                lblCurrentChargesValue.Text = $"?{invoice.CurrentCharges:N2}";
                lblPreviousOutstandingValue.Text = $"?{invoice.PreviousOutstanding:N2}";
                lblGrandTotalValue.Text = $"?{invoice.GrandTotal:N2}";
                lblPaidValue.Text = $"?{invoice.PaidAmount:N2}";
                lblBalanceValue.Text = $"?{invoice.BalanceAmount:N2}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error displaying invoice: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnPrintNotice_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                "Invoice Preview ready.\n\nPrinting to physical printers, thermal receipt generation, and PDF export will be enabled in Phase 5.\nAll financial calculation and immutable record structures are validated for Phase 4.",
                "Print / Export Notice",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private async void btnReceivePayment_Click(object sender, EventArgs e)
        {
            using (var payForm = new PaymentForm(initialInvoiceId: _invoiceId))
            {
                payForm.ShowDialog(this);
            }

            await LoadInvoiceDataAsync();
        }
    }
}
