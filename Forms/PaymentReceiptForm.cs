using System;
using System.Drawing;
using System.Drawing.Printing;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore;
using Laibrary_Payment_App.Data;
using Laibrary_Payment_App.Models;

namespace Laibrary_Payment_App.Forms
{
    public partial class PaymentReceiptForm : Form
    {
        private readonly int _paymentId;
        private readonly LibraryDbContext _dbContext;
        private Payment? _payment;
        private decimal _invoiceTotal;
        private decimal _previousPaid;
        private decimal _paymentReceived;
        private decimal _totalPaid;
        private decimal _balance;

        public PaymentReceiptForm(int paymentId)
        {
            InitializeComponent();
            _paymentId = paymentId;
            _dbContext = new LibraryDbContext();
        }

        protected override async void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            await LoadReceiptDataAsync();
        }

        private async Task LoadReceiptDataAsync()
        {
            try
            {
                Cursor = Cursors.WaitCursor;

                _payment = await _dbContext.Payments
                    .Include(p => p.Invoice)
                        .ThenInclude(i => i.Membership)
                            .ThenInclude(m => m!.Plan)
                    .Include(p => p.Invoice)
                        .ThenInclude(i => i.Membership)
                            .ThenInclude(m => m!.Seat)
                    .Include(p => p.Student)
                    .AsNoTracking()
                    .FirstOrDefaultAsync(p => p.PaymentId == _paymentId);

                if (_payment == null)
                {
                    MessageBox.Show("Payment record was not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Close();
                    return;
                }

                var inv = _payment.Invoice;
                _invoiceTotal = inv.GrandTotal;
                _paymentReceived = _payment.Amount;

                // Calculate previous paid accurately: sum of active payments on this invoice before this payment
                _previousPaid = await _dbContext.Payments
                    .Where(p => p.InvoiceId == _payment.InvoiceId &&
                                p.Status == "Active" &&
                                (p.PaymentDate < _payment.PaymentDate || (p.PaymentDate == _payment.PaymentDate && p.PaymentId < _payment.PaymentId)))
                    .SumAsync(p => (decimal?)p.Amount) ?? 0.00m;

                if (string.Equals(_payment.Status, "Active", StringComparison.OrdinalIgnoreCase))
                {
                    _totalPaid = _previousPaid + _paymentReceived;
                    _balance = Math.Max(0.00m, _invoiceTotal - _totalPaid);
                }
                else
                {
                    // If cancelled, show remaining active total
                    _totalPaid = _previousPaid;
                    _balance = Math.Max(0.00m, _invoiceTotal - _totalPaid);
                }

                var inCulture = CultureInfo.CreateSpecificCulture("hi-IN");

                lblReceiptNoVal.Text = _payment.PaymentNo;
                lblDateVal.Text = _payment.PaymentDate.ToString("dd-MMM-yyyy");
                lblStudentNameVal.Text = _payment.Student.FullName;
                lblStudentCodeVal.Text = _payment.Student.StudentCode;
                lblInvoiceNoVal.Text = inv.InvoiceNo;
                lblMembershipVal.Text = inv.Membership?.Plan?.PlanName ?? "General / Reading Space";
                lblSeatVal.Text = inv.Membership?.Seat?.SeatNumber ?? "N/A";

                lblInvoiceTotalVal.Text = _invoiceTotal.ToString("C2", inCulture);
                lblPreviousPaidVal.Text = _previousPaid.ToString("C2", inCulture);
                lblPaymentReceivedVal.Text = _paymentReceived.ToString("C2", inCulture);
                lblTotalPaidVal.Text = _totalPaid.ToString("C2", inCulture);
                lblBalanceVal.Text = _balance.ToString("C2", inCulture);

                lblPaymentModeVal.Text = _payment.PaymentMode;
                lblReferenceVal.Text = string.IsNullOrWhiteSpace(_payment.ReferenceNo) ? "N/A" : _payment.ReferenceNo;
                lblReceivedByVal.Text = _payment.ReceivedBy;

                if (string.Equals(_payment.Status, "Cancelled", StringComparison.OrdinalIgnoreCase))
                {
                    lblStatusBadge.Text = "CANCELLED";
                    lblStatusBadge.BackColor = Color.FromArgb(254, 226, 226);
                    lblStatusBadge.ForeColor = Color.FromArgb(220, 38, 38);
                }
                else
                {
                    lblStatusBadge.Text = "PAID";
                    lblStatusBadge.BackColor = Color.FromArgb(220, 252, 231);
                    lblStatusBadge.ForeColor = Color.FromArgb(22, 101, 52);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load receipt details: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void btnPrintPreview_Click(object sender, EventArgs e)
        {
            try
            {
                using var printDoc = CreatePrintDocument();
                using var previewDlg = new PrintPreviewDialog
                {
                    Document = printDoc,
                    Width = 800,
                    Height = 650,
                    StartPosition = FormStartPosition.CenterParent
                };
                previewDlg.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Unable to display print preview: {ex.Message}", "Printing Notice", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnPrintReceipt_Click(object sender, EventArgs e)
        {
            try
            {
                using var printDoc = CreatePrintDocument();
                using var printDlg = new PrintDialog
                {
                    Document = printDoc,
                    UseEXDialog = true
                };

                if (printDlg.ShowDialog(this) == DialogResult.OK)
                {
                    printDoc.Print();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Printing completed or simulator notice: {ex.Message}", "Print Status", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private PrintDocument CreatePrintDocument()
        {
            var doc = new PrintDocument();
            doc.DocumentName = $"Receipt_{lblReceiptNoVal.Text}";
            doc.PrintPage += PrintDoc_PrintPage;
            return doc;
        }

        private void PrintDoc_PrintPage(object sender, PrintPageEventArgs e)
        {
            var g = e.Graphics;
            if (g == null) return;

            var inCulture = CultureInfo.CreateSpecificCulture("hi-IN");
            using var fontTitle = new Font("Segoe UI", 16, FontStyle.Bold);
            using var fontSubtitle = new Font("Segoe UI", 9, FontStyle.Regular);
            using var fontHeader = new Font("Segoe UI", 12, FontStyle.Bold);
            using var fontRegular = new Font("Segoe UI", 9.5f, FontStyle.Regular);
            using var fontBold = new Font("Segoe UI", 9.5f, FontStyle.Bold);
            using var fontBigBold = new Font("Segoe UI", 11, FontStyle.Bold);
            using var brushDark = new SolidBrush(Color.FromArgb(15, 23, 42));
            using var brushMuted = new SolidBrush(Color.FromArgb(100, 116, 139));
            using var brushGreen = new SolidBrush(Color.FromArgb(16, 185, 129));
            using var brushRed = new SolidBrush(Color.FromArgb(220, 38, 38));
            using var penSep = new Pen(Color.FromArgb(203, 213, 225), 1);

            int x = 60;
            int y = 50;
            int width = 500;

            // Brand Header
            g.DrawString("LIBRARY MANAGEMENT SYSTEM", fontTitle, brushDark, x, y);
            y += 30;
            g.DrawString("Central Reading Space & Study Hall, University Road", fontSubtitle, brushMuted, x, y);
            y += 18;
            g.DrawString("Phone: +91 98765 43210   |   Email: library@app.local", fontSubtitle, brushMuted, x, y);
            y += 28;
            g.DrawLine(penSep, x, y, x + width, y);
            y += 15;

            // Document Title
            g.DrawString("PAYMENT RECEIPT", fontHeader, brushDark, x, y);
            y += 25;

            // Meta Info
            g.DrawString($"Receipt No: {lblReceiptNoVal.Text}", fontBold, brushDark, x, y);
            g.DrawString($"Date: {lblDateVal.Text}", fontRegular, brushDark, x + 300, y);
            y += 22;
            g.DrawString($"Status: {lblStatusBadge.Text}", fontBold, lblStatusBadge.Text == "PAID" ? brushGreen : brushRed, x, y);
            y += 22;
            g.DrawLine(penSep, x, y, x + width, y);
            y += 15;

            // Student & Invoice Details
            g.DrawString("Student:", fontRegular, brushMuted, x, y);
            g.DrawString(lblStudentNameVal.Text, fontBold, brushDark, x + 110, y);
            g.DrawString("Invoice No:", fontRegular, brushMuted, x + 300, y);
            g.DrawString(lblInvoiceNoVal.Text, fontBold, brushDark, x + 380, y);
            y += 20;

            g.DrawString("Student Code:", fontRegular, brushMuted, x, y);
            g.DrawString(lblStudentCodeVal.Text, fontBold, brushDark, x + 110, y);
            g.DrawString("Membership:", fontRegular, brushMuted, x + 300, y);
            g.DrawString(lblMembershipVal.Text, fontBold, brushDark, x + 380, y);
            y += 20;

            g.DrawString("Seat:", fontRegular, brushMuted, x + 300, y);
            g.DrawString(lblSeatVal.Text, fontBold, brushDark, x + 380, y);
            y += 25;
            g.DrawLine(penSep, x, y, x + width, y);
            y += 15;

            // Financial Summary
            DrawFinancialRow(g, fontRegular, fontBold, brushDark, x, ref y, width, "Invoice Total", _invoiceTotal.ToString("C2", inCulture));
            DrawFinancialRow(g, fontRegular, fontBold, brushMuted, x, ref y, width, "Previous Paid", _previousPaid.ToString("C2", inCulture));
            DrawFinancialRow(g, fontBold, fontBold, brushGreen, x, ref y, width, "Payment Received", _paymentReceived.ToString("C2", inCulture));
            y += 4;
            g.DrawLine(penSep, x, y, x + width, y);
            y += 8;
            DrawFinancialRow(g, fontBold, fontBold, brushDark, x, ref y, width, "Total Paid", _totalPaid.ToString("C2", inCulture));
            DrawFinancialRow(g, fontBigBold, fontBigBold, _balance > 0 ? brushRed : brushGreen, x, ref y, width, "Outstanding Balance", _balance.ToString("C2", inCulture));
            y += 15;
            g.DrawLine(penSep, x, y, x + width, y);
            y += 15;

            // Payment Mode & Info
            g.DrawString("Payment Mode:", fontRegular, brushMuted, x, y);
            g.DrawString(lblPaymentModeVal.Text, fontBold, brushDark, x + 110, y);
            y += 20;

            g.DrawString("Reference No:", fontRegular, brushMuted, x, y);
            g.DrawString(lblReferenceVal.Text, fontBold, brushDark, x + 110, y);
            y += 20;

            g.DrawString("Received By:", fontRegular, brushMuted, x, y);
            g.DrawString(lblReceivedByVal.Text, fontBold, brushDark, x + 110, y);
            y += 30;
            g.DrawLine(penSep, x, y, x + width, y);
            y += 15;

            // Footer
            g.DrawString("Thank you for your payment! Please retain this receipt for future reference.", fontSubtitle, brushMuted, x + 20, y);

            e.HasMorePages = false;
        }

        private static void DrawFinancialRow(Graphics g, Font labelFont, Font valFont, Brush valBrush, int x, ref int y, int width, string label, string value)
        {
            g.DrawString(label, labelFont, Brushes.Black, x, y);
            var size = g.MeasureString(value, valFont);
            g.DrawString(value, valFont, valBrush, x + width - size.Width, y);
            y += 22;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
