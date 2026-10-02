using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using Laibrary_Payment_App.Data;
using Laibrary_Payment_App.Forms;
using Laibrary_Payment_App.Models;
using Laibrary_Payment_App.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Laibrary_Payment_App
{
    public partial class Form1 : Form
    {
        private readonly IDashboardService _dashboardService;

        public Form1()
        {
            InitializeComponent();
            _dashboardService = ResolveDashboardService();
        }

        private static IDashboardService ResolveDashboardService()
        {
            if (Program.ServiceProvider != null)
            {
                var service = Program.ServiceProvider.GetService<IDashboardService>();
                if (service != null)
                {
                    return service;
                }
            }

            return new DashboardService(new LibraryDbContext());
        }

        protected override async void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            UpdateUserInterface();
            await RefreshDashboardStatsAsync();
        }

        private void UpdateUserInterface()
        {
            var user = CurrentSession.CurrentUser;
            if (user != null)
            {
                Text = $"Library Management System - Dashboard | Logged in as: {user.FullName} ({user.Role})";
                lblCurrentUserHeader.Text = $"{user.FullName} ({user.Role})";
                lblWelcomeTitle.Text = $"Welcome, {user.FullName}";
                lblUserInfo.Text = $"Username: {user.Username}   |   Role: {user.Role}   |   System: Library Management System (Phase 6: Reports & Analytics Active)";
            }
            else
            {
                Text = "Library Management System - Dashboard";
                lblCurrentUserHeader.Text = "Administrator";
                lblWelcomeTitle.Text = "Welcome to Library Management System";
                lblUserInfo.Text = "System: Library Management System (Phase 6: Reports & Analytics Active)";
            }
        }

        public async Task RefreshDashboardStatsAsync()
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                var summary = await _dashboardService.GetSummaryAsync();

                lblCardTotalValue.Text = summary.TotalStudents.ToString();
                lblCardActiveValue.Text = summary.ActiveStudents.ToString();
                lblCardMembershipsValue.Text = summary.ActiveMemberships.ToString();
                lblCardSeatsValue.Text = summary.AvailableSeats.ToString();
                lblCardOccupiedValue.Text = summary.OccupiedSeats.ToString();
                lblCardExpiringValue.Text = summary.ExpiringMemberships.ToString();

                var inCulture = System.Globalization.CultureInfo.CreateSpecificCulture("hi-IN");
                lblCardTodayInvoicesValue.Text = summary.TodayInvoicesCount.ToString();
                lblCardTodayBillingValue.Text = summary.TodayBillingAmount.ToString("C2", inCulture);
                lblCardTodayBillingSub.Text = $"Collected: {summary.TodayCollectionAmount.ToString("C2", inCulture)} ({summary.TodayPaymentCount} pay)";
                lblCardOutstandingValue.Text = summary.CurrentOutstandingAmount.ToString("C2", inCulture);
                lblCardOutstandingSub.Text = $"Unpaid: {summary.UnpaidInvoicesCount} | Partial: {summary.PartiallyPaidInvoicesCount}";

                // Repurpose total invoices card as collection summary
                lblCardTotalInvoicesValue.Text = summary.ThisMonthCollectionAmount.ToString("C2", inCulture);
                lblCardTotalInvoicesSub.Text = $"Today: {summary.TodayCollectionAmount.ToString("C2", inCulture)} | Invoices: {summary.TotalInvoicesCount} (Paid: {summary.PaidInvoicesCount})";
            }
            catch (Exception ex)
            {
                lblCardTotalValue.Text = "0";
                lblCardActiveValue.Text = "0";
                lblCardMembershipsValue.Text = "0";
                lblCardSeatsValue.Text = "0";
                lblCardOccupiedValue.Text = "0";
                lblCardExpiringValue.Text = "0";
                lblCardTodayInvoicesValue.Text = "0";
                lblCardTodayBillingValue.Text = "₹0.00";
                lblCardOutstandingValue.Text = "₹0.00";
                lblCardTotalInvoicesValue.Text = "₹0.00";
                ShowModuleNotice($"Could not refresh dashboard statistics: {ex.Message}");
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private async void btnNavDashboard_Click(object sender, EventArgs e)
        {
            HighlightNavButton(btnNavDashboard);
            HideModuleNotice();
            await RefreshDashboardStatsAsync();
        }

        private async void btnNavStudents_Click(object sender, EventArgs e)
        {
            HighlightNavButton(btnNavStudents);
            HideModuleNotice();

            using (var studentForm = new StudentMasterForm())
            {
                studentForm.ShowDialog(this);
            }

            HighlightNavButton(btnNavDashboard);
            await RefreshDashboardStatsAsync();
        }

        private async void btnNavPlans_Click(object sender, EventArgs e)
        {
            HighlightNavButton(btnNavPlans);
            HideModuleNotice();

            using (var planForm = new MembershipPlanForm())
            {
                planForm.ShowDialog(this);
            }

            HighlightNavButton(btnNavDashboard);
            await RefreshDashboardStatsAsync();
        }

        private async void btnNavSeats_Click(object sender, EventArgs e)
        {
            HighlightNavButton(btnNavSeats);
            HideModuleNotice();

            using (var seatForm = new SeatMasterForm())
            {
                seatForm.ShowDialog(this);
            }

            HighlightNavButton(btnNavDashboard);
            await RefreshDashboardStatsAsync();
        }

        private async void btnNavMemberships_Click(object sender, EventArgs e)
        {
            HighlightNavButton(btnNavMemberships);
            HideModuleNotice();

            using (var membershipForm = new MembershipForm())
            {
                membershipForm.ShowDialog(this);
            }

            HighlightNavButton(btnNavDashboard);
            await RefreshDashboardStatsAsync();
        }

        private async void btnNavBilling_Click(object sender, EventArgs e)
        {
            HighlightNavButton(btnNavBilling);
            HideModuleNotice();

            using (var billingForm = new BillingForm())
            {
                billingForm.ShowDialog(this);
            }

            HighlightNavButton(btnNavDashboard);
            await RefreshDashboardStatsAsync();
        }

        private async void btnNavInvoiceHistory_Click(object sender, EventArgs e)
        {
            HighlightNavButton(btnNavInvoiceHistory);
            HideModuleNotice();

            using (var historyForm = new InvoiceHistoryForm())
            {
                historyForm.ShowDialog(this);
            }

            HighlightNavButton(btnNavDashboard);
            await RefreshDashboardStatsAsync();
        }

        private async void btnNavPayments_Click(object sender, EventArgs e)
        {
            HighlightNavButton(btnNavPayments);
            HideModuleNotice();

            using (var historyForm = new PaymentHistoryForm())
            {
                historyForm.ShowDialog(this);
            }

            HighlightNavButton(btnNavDashboard);
            await RefreshDashboardStatsAsync();
        }

        private async void btnQuickReceivePayment_Click(object sender, EventArgs e)
        {
            HideModuleNotice();

            using (var payForm = new PaymentForm())
            {
                payForm.ShowDialog(this);
            }

            await RefreshDashboardStatsAsync();
        }

        private void btnNavModule_Click(object sender, EventArgs e)
        {
            if (sender is Button btn)
            {
                HighlightNavButton(btn);

                switch (btn.Text.Trim())
                {
                    case "Reports":
                        ShowReportsMenu(btn);
                        break;
                    case "Settings":
                        ShowModuleNotice("Settings module will be available in a future phase.");
                        break;
                    default:
                        ShowModuleNotice($"{btn.Text} will be available in a future phase.");
                        break;
                }
            }
        }

        private void ShowReportsMenu(Button anchorBtn)
        {
            HideModuleNotice();

            var menu = new ContextMenuStrip();
            menu.Font = new Font("Segoe UI", 9.5F);
            menu.BackColor = Color.FromArgb(248, 250, 252);
            menu.ShowImageMargin = false;

            var items = new (string text, Action action)[]
            {
                ("Daily Collection", () => OpenReportForm(new Forms.Reports.DailyCollectionReportForm())),
                ("Date-wise Collection", () => OpenReportForm(new Forms.Reports.DateWiseCollectionReportForm())),
                ("Monthly Collection", () => OpenReportForm(new Forms.Reports.MonthlyCollectionReportForm())),
                ("-", () => {}),
                ("Outstanding", () => OpenReportForm(new Forms.Reports.OutstandingReportForm())),
                ("Student Ledger", () => OpenReportForm(new Forms.Reports.StudentLedgerReportForm())),
                ("-", () => {}),
                ("Payment History", () => OpenReportForm(new Forms.Reports.PaymentHistoryReportForm())),
                ("Invoice Register", () => OpenReportForm(new Forms.Reports.InvoiceRegisterReportForm())),
                ("-", () => {}),
                ("Membership Register", () => OpenReportForm(new Forms.Reports.MembershipRegisterReportForm())),
                ("Membership Expiry", () => OpenReportForm(new Forms.Reports.MembershipExpiryReportForm())),
                ("-", () => {}),
                ("Seat Occupancy", () => OpenReportForm(new Forms.Reports.SeatOccupancyReportForm())),
                ("Student Register", () => OpenReportForm(new Forms.Reports.StudentRegisterReportForm())),
            };

            foreach (var (text, action) in items)
            {
                if (text == "-")
                {
                    menu.Items.Add(new ToolStripSeparator());
                }
                else
                {
                    var item = new ToolStripMenuItem(text);
                    item.Click += (s, ev) => action();
                    menu.Items.Add(item);
                }
            }

            // Show menu to the right of the Reports button
            var pt = anchorBtn.PointToScreen(new Point(anchorBtn.Width, 0));
            menu.Show(pt);
        }

        private async void OpenReportForm(Form reportForm)
        {
            using (reportForm)
            {
                reportForm.ShowDialog(this);
            }

            HighlightNavButton(btnNavDashboard);
            await RefreshDashboardStatsAsync();
        }

        private void ShowModuleNotice(string message)
        {
            lblModuleNotice.Text = message;
            panelModuleNotice.Visible = true;
        }

        private void HideModuleNotice()
        {
            panelModuleNotice.Visible = false;
        }

        private void HighlightNavButton(Button activeButton)
        {
            Button[] navButtons = {
                btnNavDashboard,
                btnNavStudents,
                btnNavPlans,
                btnNavSeats,
                btnNavMemberships,
                btnNavBilling,
                btnNavInvoiceHistory,
                btnNavPayments,
                btnNavReports,
                btnNavSettings
            };

            foreach (var b in navButtons)
            {
                if (b == activeButton)
                {
                    b.BackColor = Color.FromArgb(24, 76, 120);
                    b.ForeColor = Color.White;
                    b.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold);
                }
                else
                {
                    b.BackColor = Color.FromArgb(30, 41, 59);
                    b.ForeColor = Color.FromArgb(203, 213, 225);
                    b.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular);
                }
            }
        }

        private async void btnQuickRefreshStats_Click(object sender, EventArgs e)
        {
            await RefreshDashboardStatsAsync();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            var confirm = MessageBox.Show(
                "Are you sure you want to log out?",
                "Confirm Logout - Library Management System",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirm == DialogResult.Yes)
            {
                CurrentSession.RequestLogout = true;
                CurrentSession.Clear();
                Close();
            }
        }
    }
}
