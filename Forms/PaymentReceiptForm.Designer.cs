namespace Laibrary_Payment_App.Forms
{
    partial class PaymentReceiptForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            if (disposing)
            {
                _dbContext?.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.panelTopBar = new System.Windows.Forms.Panel();
            this.btnPrintReceipt = new System.Windows.Forms.Button();
            this.btnPrintPreview = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.lblTitle = new System.Windows.Forms.Label();
            this.panelDocumentScroll = new System.Windows.Forms.Panel();
            this.panelReceiptDoc = new System.Windows.Forms.Panel();
            this.lblFooterThankYou = new System.Windows.Forms.Label();
            this.lineSep4 = new System.Windows.Forms.Label();
            this.panelPaymentInfo = new System.Windows.Forms.Panel();
            this.lblReceivedByVal = new System.Windows.Forms.Label();
            this.lblReceivedByLbl = new System.Windows.Forms.Label();
            this.lblReferenceVal = new System.Windows.Forms.Label();
            this.lblReferenceLbl = new System.Windows.Forms.Label();
            this.lblPaymentModeVal = new System.Windows.Forms.Label();
            this.lblPaymentModeLbl = new System.Windows.Forms.Label();
            this.lineSep3 = new System.Windows.Forms.Label();
            this.panelFinancials = new System.Windows.Forms.Panel();
            this.lblBalanceVal = new System.Windows.Forms.Label();
            this.lblBalanceLbl = new System.Windows.Forms.Label();
            this.lblTotalPaidVal = new System.Windows.Forms.Label();
            this.lblTotalPaidLbl = new System.Windows.Forms.Label();
            this.lineSepFinancial2 = new System.Windows.Forms.Label();
            this.lblPaymentReceivedVal = new System.Windows.Forms.Label();
            this.lblPaymentReceivedLbl = new System.Windows.Forms.Label();
            this.lblPreviousPaidVal = new System.Windows.Forms.Label();
            this.lblPreviousPaidLbl = new System.Windows.Forms.Label();
            this.lblInvoiceTotalVal = new System.Windows.Forms.Label();
            this.lblInvoiceTotalLbl = new System.Windows.Forms.Label();
            this.lineSep2 = new System.Windows.Forms.Label();
            this.panelDetails = new System.Windows.Forms.Panel();
            this.lblSeatVal = new System.Windows.Forms.Label();
            this.lblSeatLbl = new System.Windows.Forms.Label();
            this.lblMembershipVal = new System.Windows.Forms.Label();
            this.lblMembershipLbl = new System.Windows.Forms.Label();
            this.lblInvoiceNoVal = new System.Windows.Forms.Label();
            this.lblInvoiceNoLbl = new System.Windows.Forms.Label();
            this.lblStudentCodeVal = new System.Windows.Forms.Label();
            this.lblStudentCodeLbl = new System.Windows.Forms.Label();
            this.lblStudentNameVal = new System.Windows.Forms.Label();
            this.lblStudentNameLbl = new System.Windows.Forms.Label();
            this.lineSep1 = new System.Windows.Forms.Label();
            this.panelReceiptMeta = new System.Windows.Forms.Panel();
            this.lblStatusBadge = new System.Windows.Forms.Label();
            this.lblDateVal = new System.Windows.Forms.Label();
            this.lblDateLbl = new System.Windows.Forms.Label();
            this.lblReceiptNoVal = new System.Windows.Forms.Label();
            this.lblReceiptNoLbl = new System.Windows.Forms.Label();
            this.lblDocHeader = new System.Windows.Forms.Label();
            this.panelHeaderBrand = new System.Windows.Forms.Panel();
            this.lblLibraryContact = new System.Windows.Forms.Label();
            this.lblLibraryAddress = new System.Windows.Forms.Label();
            this.lblLibraryName = new System.Windows.Forms.Label();
            this.panelTopBar.SuspendLayout();
            this.panelDocumentScroll.SuspendLayout();
            this.panelReceiptDoc.SuspendLayout();
            this.panelPaymentInfo.SuspendLayout();
            this.panelFinancials.SuspendLayout();
            this.panelDetails.SuspendLayout();
            this.panelReceiptMeta.SuspendLayout();
            this.panelHeaderBrand.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelTopBar
            // 
            this.panelTopBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.panelTopBar.Controls.Add(this.btnPrintReceipt);
            this.panelTopBar.Controls.Add(this.btnPrintPreview);
            this.panelTopBar.Controls.Add(this.btnClose);
            this.panelTopBar.Controls.Add(this.lblTitle);
            this.panelTopBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTopBar.Location = new System.Drawing.Point(0, 0);
            this.panelTopBar.Name = "panelTopBar";
            this.panelTopBar.Size = new System.Drawing.Size(684, 55);
            this.panelTopBar.TabIndex = 0;
            // 
            // btnPrintReceipt
            // 
            this.btnPrintReceipt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPrintReceipt.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(185)))), ((int)(((byte)(129)))));
            this.btnPrintReceipt.FlatAppearance.BorderSize = 0;
            this.btnPrintReceipt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrintReceipt.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnPrintReceipt.ForeColor = System.Drawing.Color.White;
            this.btnPrintReceipt.Location = new System.Drawing.Point(344, 12);
            this.btnPrintReceipt.Name = "btnPrintReceipt";
            this.btnPrintReceipt.Size = new System.Drawing.Size(100, 32);
            this.btnPrintReceipt.TabIndex = 3;
            this.btnPrintReceipt.Text = "Print Receipt";
            this.btnPrintReceipt.UseVisualStyleBackColor = false;
            this.btnPrintReceipt.Click += new System.EventHandler(this.btnPrintReceipt_Click);
            // 
            // btnPrintPreview
            // 
            this.btnPrintPreview.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPrintPreview.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(76)))), ((int)(((byte)(120)))));
            this.btnPrintPreview.FlatAppearance.BorderSize = 0;
            this.btnPrintPreview.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrintPreview.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnPrintPreview.ForeColor = System.Drawing.Color.White;
            this.btnPrintPreview.Location = new System.Drawing.Point(455, 12);
            this.btnPrintPreview.Name = "btnPrintPreview";
            this.btnPrintPreview.Size = new System.Drawing.Size(110, 32);
            this.btnPrintPreview.TabIndex = 2;
            this.btnPrintPreview.Text = "Print Preview";
            this.btnPrintPreview.UseVisualStyleBackColor = false;
            this.btnPrintPreview.Click += new System.EventHandler(this.btnPrintPreview_Click);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Location = new System.Drawing.Point(575, 12);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(85, 32);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(18, 16);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(193, 21);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Official Payment Receipt";
            // 
            // panelDocumentScroll
            // 
            this.panelDocumentScroll.AutoScroll = true;
            this.panelDocumentScroll.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.panelDocumentScroll.Controls.Add(this.panelReceiptDoc);
            this.panelDocumentScroll.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelDocumentScroll.Location = new System.Drawing.Point(0, 55);
            this.panelDocumentScroll.Name = "panelDocumentScroll";
            this.panelDocumentScroll.Padding = new System.Windows.Forms.Padding(20);
            this.panelDocumentScroll.Size = new System.Drawing.Size(684, 665);
            this.panelDocumentScroll.TabIndex = 1;
            // 
            // panelReceiptDoc
            // 
            this.panelReceiptDoc.BackColor = System.Drawing.Color.White;
            this.panelReceiptDoc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelReceiptDoc.Controls.Add(this.lblFooterThankYou);
            this.panelReceiptDoc.Controls.Add(this.lineSep4);
            this.panelReceiptDoc.Controls.Add(this.panelPaymentInfo);
            this.panelReceiptDoc.Controls.Add(this.lineSep3);
            this.panelReceiptDoc.Controls.Add(this.panelFinancials);
            this.panelReceiptDoc.Controls.Add(this.lineSep2);
            this.panelReceiptDoc.Controls.Add(this.panelDetails);
            this.panelReceiptDoc.Controls.Add(this.lineSep1);
            this.panelReceiptDoc.Controls.Add(this.panelReceiptMeta);
            this.panelReceiptDoc.Controls.Add(this.panelHeaderBrand);
            this.panelReceiptDoc.Location = new System.Drawing.Point(42, 20);
            this.panelReceiptDoc.Name = "panelReceiptDoc";
            this.panelReceiptDoc.Padding = new System.Windows.Forms.Padding(30);
            this.panelReceiptDoc.Size = new System.Drawing.Size(600, 620);
            this.panelReceiptDoc.TabIndex = 0;
            // 
            // lblFooterThankYou
            // 
            this.lblFooterThankYou.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblFooterThankYou.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point);
            this.lblFooterThankYou.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblFooterThankYou.Location = new System.Drawing.Point(30, 555);
            this.lblFooterThankYou.Name = "lblFooterThankYou";
            this.lblFooterThankYou.Size = new System.Drawing.Size(538, 30);
            this.lblFooterThankYou.TabIndex = 9;
            this.lblFooterThankYou.Text = "Thank you for your payment! Please retain this receipt for future reference.";
            this.lblFooterThankYou.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lineSep4
            // 
            this.lineSep4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lineSep4.Dock = System.Windows.Forms.DockStyle.Top;
            this.lineSep4.Location = new System.Drawing.Point(30, 545);
            this.lineSep4.Name = "lineSep4";
            this.lineSep4.Size = new System.Drawing.Size(538, 10);
            this.lineSep4.TabIndex = 8;
            // 
            // panelPaymentInfo
            // 
            this.panelPaymentInfo.Controls.Add(this.lblReceivedByVal);
            this.panelPaymentInfo.Controls.Add(this.lblReceivedByLbl);
            this.panelPaymentInfo.Controls.Add(this.lblReferenceVal);
            this.panelPaymentInfo.Controls.Add(this.lblReferenceLbl);
            this.panelPaymentInfo.Controls.Add(this.lblPaymentModeVal);
            this.panelPaymentInfo.Controls.Add(this.lblPaymentModeLbl);
            this.panelPaymentInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelPaymentInfo.Location = new System.Drawing.Point(30, 465);
            this.panelPaymentInfo.Name = "panelPaymentInfo";
            this.panelPaymentInfo.Size = new System.Drawing.Size(538, 80);
            this.panelPaymentInfo.TabIndex = 7;
            // 
            // lblReceivedByVal
            // 
            this.lblReceivedByVal.AutoSize = true;
            this.lblReceivedByVal.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblReceivedByVal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblReceivedByVal.Location = new System.Drawing.Point(120, 52);
            this.lblReceivedByVal.Name = "lblReceivedByVal";
            this.lblReceivedByVal.Size = new System.Drawing.Size(124, 15);
            this.lblReceivedByVal.TabIndex = 5;
            this.lblReceivedByVal.Text = "System Administrator";
            // 
            // lblReceivedByLbl
            // 
            this.lblReceivedByLbl.AutoSize = true;
            this.lblReceivedByLbl.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblReceivedByLbl.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblReceivedByLbl.Location = new System.Drawing.Point(10, 52);
            this.lblReceivedByLbl.Name = "lblReceivedByLbl";
            this.lblReceivedByLbl.Size = new System.Drawing.Size(73, 15);
            this.lblReceivedByLbl.TabIndex = 4;
            this.lblReceivedByLbl.Text = "Received By:";
            // 
            // lblReferenceVal
            // 
            this.lblReferenceVal.AutoSize = true;
            this.lblReferenceVal.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblReferenceVal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblReferenceVal.Location = new System.Drawing.Point(120, 28);
            this.lblReferenceVal.Name = "lblReferenceVal";
            this.lblReferenceVal.Size = new System.Drawing.Size(26, 15);
            this.lblReferenceVal.TabIndex = 3;
            this.lblReferenceVal.Text = "N/A";
            // 
            // lblReferenceLbl
            // 
            this.lblReferenceLbl.AutoSize = true;
            this.lblReferenceLbl.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblReferenceLbl.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblReferenceLbl.Location = new System.Drawing.Point(10, 28);
            this.lblReferenceLbl.Name = "lblReferenceLbl";
            this.lblReferenceLbl.Size = new System.Drawing.Size(62, 15);
            this.lblReferenceLbl.TabIndex = 2;
            this.lblReferenceLbl.Text = "Reference:";
            // 
            // lblPaymentModeVal
            // 
            this.lblPaymentModeVal.AutoSize = true;
            this.lblPaymentModeVal.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblPaymentModeVal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblPaymentModeVal.Location = new System.Drawing.Point(120, 6);
            this.lblPaymentModeVal.Name = "lblPaymentModeVal";
            this.lblPaymentModeVal.Size = new System.Drawing.Size(32, 15);
            this.lblPaymentModeVal.TabIndex = 1;
            this.lblPaymentModeVal.Text = "Cash";
            // 
            // lblPaymentModeLbl
            // 
            this.lblPaymentModeLbl.AutoSize = true;
            this.lblPaymentModeLbl.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblPaymentModeLbl.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblPaymentModeLbl.Location = new System.Drawing.Point(10, 6);
            this.lblPaymentModeLbl.Name = "lblPaymentModeLbl";
            this.lblPaymentModeLbl.Size = new System.Drawing.Size(90, 15);
            this.lblPaymentModeLbl.TabIndex = 0;
            this.lblPaymentModeLbl.Text = "Payment Mode:";
            // 
            // lineSep3
            // 
            this.lineSep3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lineSep3.Dock = System.Windows.Forms.DockStyle.Top;
            this.lineSep3.Location = new System.Drawing.Point(30, 455);
            this.lineSep3.Name = "lineSep3";
            this.lineSep3.Size = new System.Drawing.Size(538, 10);
            this.lineSep3.TabIndex = 6;
            // 
            // panelFinancials
            // 
            this.panelFinancials.Controls.Add(this.lblBalanceVal);
            this.panelFinancials.Controls.Add(this.lblBalanceLbl);
            this.panelFinancials.Controls.Add(this.lblTotalPaidVal);
            this.panelFinancials.Controls.Add(this.lblTotalPaidLbl);
            this.panelFinancials.Controls.Add(this.lineSepFinancial2);
            this.panelFinancials.Controls.Add(this.lblPaymentReceivedVal);
            this.panelFinancials.Controls.Add(this.lblPaymentReceivedLbl);
            this.panelFinancials.Controls.Add(this.lblPreviousPaidVal);
            this.panelFinancials.Controls.Add(this.lblPreviousPaidLbl);
            this.panelFinancials.Controls.Add(this.lblInvoiceTotalVal);
            this.panelFinancials.Controls.Add(this.lblInvoiceTotalLbl);
            this.panelFinancials.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelFinancials.Location = new System.Drawing.Point(30, 310);
            this.panelFinancials.Name = "panelFinancials";
            this.panelFinancials.Size = new System.Drawing.Size(538, 145);
            this.panelFinancials.TabIndex = 5;
            // 
            // lblBalanceVal
            // 
            this.lblBalanceVal.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblBalanceVal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this.lblBalanceVal.Location = new System.Drawing.Point(380, 115);
            this.lblBalanceVal.Name = "lblBalanceVal";
            this.lblBalanceVal.Size = new System.Drawing.Size(150, 20);
            this.lblBalanceVal.TabIndex = 10;
            this.lblBalanceVal.Text = "₹0.00";
            this.lblBalanceVal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblBalanceLbl
            // 
            this.lblBalanceLbl.AutoSize = true;
            this.lblBalanceLbl.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblBalanceLbl.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblBalanceLbl.Location = new System.Drawing.Point(10, 115);
            this.lblBalanceLbl.Name = "lblBalanceLbl";
            this.lblBalanceLbl.Size = new System.Drawing.Size(147, 19);
            this.lblBalanceLbl.TabIndex = 9;
            this.lblBalanceLbl.Text = "Outstanding Balance:";
            // 
            // lblTotalPaidVal
            // 
            this.lblTotalPaidVal.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblTotalPaidVal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(185)))), ((int)(((byte)(129)))));
            this.lblTotalPaidVal.Location = new System.Drawing.Point(380, 88);
            this.lblTotalPaidVal.Name = "lblTotalPaidVal";
            this.lblTotalPaidVal.Size = new System.Drawing.Size(150, 20);
            this.lblTotalPaidVal.TabIndex = 8;
            this.lblTotalPaidVal.Text = "₹0.00";
            this.lblTotalPaidVal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblTotalPaidLbl
            // 
            this.lblTotalPaidLbl.AutoSize = true;
            this.lblTotalPaidLbl.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblTotalPaidLbl.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblTotalPaidLbl.Location = new System.Drawing.Point(10, 88);
            this.lblTotalPaidLbl.Name = "lblTotalPaidLbl";
            this.lblTotalPaidLbl.Size = new System.Drawing.Size(70, 17);
            this.lblTotalPaidLbl.TabIndex = 7;
            this.lblTotalPaidLbl.Text = "Total Paid:";
            // 
            // lineSepFinancial2
            // 
            this.lineSepFinancial2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lineSepFinancial2.Location = new System.Drawing.Point(10, 78);
            this.lineSepFinancial2.Name = "lineSepFinancial2";
            this.lineSepFinancial2.Size = new System.Drawing.Size(520, 2);
            this.lineSepFinancial2.TabIndex = 6;
            // 
            // lblPaymentReceivedVal
            // 
            this.lblPaymentReceivedVal.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblPaymentReceivedVal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(185)))), ((int)(((byte)(129)))));
            this.lblPaymentReceivedVal.Location = new System.Drawing.Point(380, 52);
            this.lblPaymentReceivedVal.Name = "lblPaymentReceivedVal";
            this.lblPaymentReceivedVal.Size = new System.Drawing.Size(150, 20);
            this.lblPaymentReceivedVal.TabIndex = 5;
            this.lblPaymentReceivedVal.Text = "₹0.00";
            this.lblPaymentReceivedVal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblPaymentReceivedLbl
            // 
            this.lblPaymentReceivedLbl.AutoSize = true;
            this.lblPaymentReceivedLbl.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblPaymentReceivedLbl.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblPaymentReceivedLbl.Location = new System.Drawing.Point(10, 52);
            this.lblPaymentReceivedLbl.Name = "lblPaymentReceivedLbl";
            this.lblPaymentReceivedLbl.Size = new System.Drawing.Size(123, 17);
            this.lblPaymentReceivedLbl.TabIndex = 4;
            this.lblPaymentReceivedLbl.Text = "Payment Received:";
            // 
            // lblPreviousPaidVal
            // 
            this.lblPreviousPaidVal.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblPreviousPaidVal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblPreviousPaidVal.Location = new System.Drawing.Point(380, 28);
            this.lblPreviousPaidVal.Name = "lblPreviousPaidVal";
            this.lblPreviousPaidVal.Size = new System.Drawing.Size(150, 20);
            this.lblPreviousPaidVal.TabIndex = 3;
            this.lblPreviousPaidVal.Text = "₹0.00";
            this.lblPreviousPaidVal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblPreviousPaidLbl
            // 
            this.lblPreviousPaidLbl.AutoSize = true;
            this.lblPreviousPaidLbl.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblPreviousPaidLbl.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblPreviousPaidLbl.Location = new System.Drawing.Point(10, 28);
            this.lblPreviousPaidLbl.Name = "lblPreviousPaidLbl";
            this.lblPreviousPaidLbl.Size = new System.Drawing.Size(81, 15);
            this.lblPreviousPaidLbl.TabIndex = 2;
            this.lblPreviousPaidLbl.Text = "Previous Paid:";
            // 
            // lblInvoiceTotalVal
            // 
            this.lblInvoiceTotalVal.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblInvoiceTotalVal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblInvoiceTotalVal.Location = new System.Drawing.Point(380, 4);
            this.lblInvoiceTotalVal.Name = "lblInvoiceTotalVal";
            this.lblInvoiceTotalVal.Size = new System.Drawing.Size(150, 20);
            this.lblInvoiceTotalVal.TabIndex = 1;
            this.lblInvoiceTotalVal.Text = "₹0.00";
            this.lblInvoiceTotalVal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblInvoiceTotalLbl
            // 
            this.lblInvoiceTotalLbl.AutoSize = true;
            this.lblInvoiceTotalLbl.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblInvoiceTotalLbl.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblInvoiceTotalLbl.Location = new System.Drawing.Point(10, 4);
            this.lblInvoiceTotalLbl.Name = "lblInvoiceTotalLbl";
            this.lblInvoiceTotalLbl.Size = new System.Drawing.Size(84, 17);
            this.lblInvoiceTotalLbl.TabIndex = 0;
            this.lblInvoiceTotalLbl.Text = "Invoice Total:";
            // 
            // lineSep2
            // 
            this.lineSep2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lineSep2.Dock = System.Windows.Forms.DockStyle.Top;
            this.lineSep2.Location = new System.Drawing.Point(30, 300);
            this.lineSep2.Name = "lineSep2";
            this.lineSep2.Size = new System.Drawing.Size(538, 10);
            this.lineSep2.TabIndex = 4;
            // 
            // panelDetails
            // 
            this.panelDetails.Controls.Add(this.lblSeatVal);
            this.panelDetails.Controls.Add(this.lblSeatLbl);
            this.panelDetails.Controls.Add(this.lblMembershipVal);
            this.panelDetails.Controls.Add(this.lblMembershipLbl);
            this.panelDetails.Controls.Add(this.lblInvoiceNoVal);
            this.panelDetails.Controls.Add(this.lblInvoiceNoLbl);
            this.panelDetails.Controls.Add(this.lblStudentCodeVal);
            this.panelDetails.Controls.Add(this.lblStudentCodeLbl);
            this.panelDetails.Controls.Add(this.lblStudentNameVal);
            this.panelDetails.Controls.Add(this.lblStudentNameLbl);
            this.panelDetails.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelDetails.Location = new System.Drawing.Point(30, 205);
            this.panelDetails.Name = "panelDetails";
            this.panelDetails.Size = new System.Drawing.Size(538, 95);
            this.panelDetails.TabIndex = 3;
            // 
            // lblSeatVal
            // 
            this.lblSeatVal.AutoSize = true;
            this.lblSeatVal.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblSeatVal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblSeatVal.Location = new System.Drawing.Point(380, 52);
            this.lblSeatVal.Name = "lblSeatVal";
            this.lblSeatVal.Size = new System.Drawing.Size(26, 15);
            this.lblSeatVal.TabIndex = 9;
            this.lblSeatVal.Text = "A01";
            // 
            // lblSeatLbl
            // 
            this.lblSeatLbl.AutoSize = true;
            this.lblSeatLbl.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblSeatLbl.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblSeatLbl.Location = new System.Drawing.Point(300, 52);
            this.lblSeatLbl.Name = "lblSeatLbl";
            this.lblSeatLbl.Size = new System.Drawing.Size(32, 15);
            this.lblSeatLbl.TabIndex = 8;
            this.lblSeatLbl.Text = "Seat:";
            // 
            // lblMembershipVal
            // 
            this.lblMembershipVal.AutoSize = true;
            this.lblMembershipVal.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblMembershipVal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblMembershipVal.Location = new System.Drawing.Point(380, 28);
            this.lblMembershipVal.Name = "lblMembershipVal";
            this.lblMembershipVal.Size = new System.Drawing.Size(78, 15);
            this.lblMembershipVal.TabIndex = 7;
            this.lblMembershipVal.Text = "Monthly Plan";
            // 
            // lblMembershipLbl
            // 
            this.lblMembershipLbl.AutoSize = true;
            this.lblMembershipLbl.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblMembershipLbl.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblMembershipLbl.Location = new System.Drawing.Point(300, 28);
            this.lblMembershipLbl.Name = "lblMembershipLbl";
            this.lblMembershipLbl.Size = new System.Drawing.Size(77, 15);
            this.lblMembershipLbl.TabIndex = 6;
            this.lblMembershipLbl.Text = "Membership:";
            // 
            // lblInvoiceNoVal
            // 
            this.lblInvoiceNoVal.AutoSize = true;
            this.lblInvoiceNoVal.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblInvoiceNoVal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(76)))), ((int)(((byte)(120)))));
            this.lblInvoiceNoVal.Location = new System.Drawing.Point(380, 4);
            this.lblInvoiceNoVal.Name = "lblInvoiceNoVal";
            this.lblInvoiceNoVal.Size = new System.Drawing.Size(91, 15);
            this.lblInvoiceNoVal.TabIndex = 5;
            this.lblInvoiceNoVal.Text = "INV-2026-000001";
            // 
            // lblInvoiceNoLbl
            // 
            this.lblInvoiceNoLbl.AutoSize = true;
            this.lblInvoiceNoLbl.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblInvoiceNoLbl.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblInvoiceNoLbl.Location = new System.Drawing.Point(300, 4);
            this.lblInvoiceNoLbl.Name = "lblInvoiceNoLbl";
            this.lblInvoiceNoLbl.Size = new System.Drawing.Size(67, 15);
            this.lblInvoiceNoLbl.TabIndex = 4;
            this.lblInvoiceNoLbl.Text = "Invoice No:";
            // 
            // lblStudentCodeVal
            // 
            this.lblStudentCodeVal.AutoSize = true;
            this.lblStudentCodeVal.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblStudentCodeVal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblStudentCodeVal.Location = new System.Drawing.Point(90, 28);
            this.lblStudentCodeVal.Name = "lblStudentCodeVal";
            this.lblStudentCodeVal.Size = new System.Drawing.Size(59, 15);
            this.lblStudentCodeVal.TabIndex = 3;
            this.lblStudentCodeVal.Text = "STU-0001";
            // 
            // lblStudentCodeLbl
            // 
            this.lblStudentCodeLbl.AutoSize = true;
            this.lblStudentCodeLbl.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblStudentCodeLbl.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblStudentCodeLbl.Location = new System.Drawing.Point(10, 28);
            this.lblStudentCodeLbl.Name = "lblStudentCodeLbl";
            this.lblStudentCodeLbl.Size = new System.Drawing.Size(76, 15);
            this.lblStudentCodeLbl.TabIndex = 2;
            this.lblStudentCodeLbl.Text = "Student Code:";
            // 
            // lblStudentNameVal
            // 
            this.lblStudentNameVal.AutoSize = true;
            this.lblStudentNameVal.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblStudentNameVal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblStudentNameVal.Location = new System.Drawing.Point(90, 4);
            this.lblStudentNameVal.Name = "lblStudentNameVal";
            this.lblStudentNameVal.Size = new System.Drawing.Size(77, 17);
            this.lblStudentNameVal.TabIndex = 1;
            this.lblStudentNameVal.Text = "Rahul Patil";
            // 
            // lblStudentNameLbl
            // 
            this.lblStudentNameLbl.AutoSize = true;
            this.lblStudentNameLbl.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblStudentNameLbl.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblStudentNameLbl.Location = new System.Drawing.Point(10, 4);
            this.lblStudentNameLbl.Name = "lblStudentNameLbl";
            this.lblStudentNameLbl.Size = new System.Drawing.Size(51, 15);
            this.lblStudentNameLbl.TabIndex = 0;
            this.lblStudentNameLbl.Text = "Student:";
            // 
            // lineSep1
            // 
            this.lineSep1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lineSep1.Dock = System.Windows.Forms.DockStyle.Top;
            this.lineSep1.Location = new System.Drawing.Point(30, 195);
            this.lineSep1.Name = "lineSep1";
            this.lineSep1.Size = new System.Drawing.Size(538, 10);
            this.lineSep1.TabIndex = 2;
            // 
            // panelReceiptMeta
            // 
            this.panelReceiptMeta.Controls.Add(this.lblStatusBadge);
            this.panelReceiptMeta.Controls.Add(this.lblDateVal);
            this.panelReceiptMeta.Controls.Add(this.lblDateLbl);
            this.panelReceiptMeta.Controls.Add(this.lblReceiptNoVal);
            this.panelReceiptMeta.Controls.Add(this.lblReceiptNoLbl);
            this.panelReceiptMeta.Controls.Add(this.lblDocHeader);
            this.panelReceiptMeta.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelReceiptMeta.Location = new System.Drawing.Point(30, 110);
            this.panelReceiptMeta.Name = "panelReceiptMeta";
            this.panelReceiptMeta.Size = new System.Drawing.Size(538, 85);
            this.panelReceiptMeta.TabIndex = 1;
            // 
            // lblStatusBadge
            // 
            this.lblStatusBadge.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblStatusBadge.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(252)))), ((int)(((byte)(231)))));
            this.lblStatusBadge.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblStatusBadge.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(101)))), ((int)(((byte)(52)))));
            this.lblStatusBadge.Location = new System.Drawing.Point(438, 48);
            this.lblStatusBadge.Name = "lblStatusBadge";
            this.lblStatusBadge.Size = new System.Drawing.Size(90, 24);
            this.lblStatusBadge.TabIndex = 5;
            this.lblStatusBadge.Text = "ACTIVE";
            this.lblStatusBadge.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblDateVal
            // 
            this.lblDateVal.AutoSize = true;
            this.lblDateVal.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblDateVal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblDateVal.Location = new System.Drawing.Point(90, 56);
            this.lblDateVal.Name = "lblDateVal";
            this.lblDateVal.Size = new System.Drawing.Size(73, 15);
            this.lblDateVal.TabIndex = 4;
            this.lblDateVal.Text = "02-Oct-2026";
            // 
            // lblDateLbl
            // 
            this.lblDateLbl.AutoSize = true;
            this.lblDateLbl.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblDateLbl.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblDateLbl.Location = new System.Drawing.Point(10, 56);
            this.lblDateLbl.Name = "lblDateLbl";
            this.lblDateLbl.Size = new System.Drawing.Size(34, 15);
            this.lblDateLbl.TabIndex = 3;
            this.lblDateLbl.Text = "Date:";
            // 
            // lblReceiptNoVal
            // 
            this.lblReceiptNoVal.AutoSize = true;
            this.lblReceiptNoVal.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblReceiptNoVal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(76)))), ((int)(((byte)(120)))));
            this.lblReceiptNoVal.Location = new System.Drawing.Point(90, 32);
            this.lblReceiptNoVal.Name = "lblReceiptNoVal";
            this.lblReceiptNoVal.Size = new System.Drawing.Size(126, 19);
            this.lblReceiptNoVal.TabIndex = 2;
            this.lblReceiptNoVal.Text = "PAY-2026-000001";
            // 
            // lblReceiptNoLbl
            // 
            this.lblReceiptNoLbl.AutoSize = true;
            this.lblReceiptNoLbl.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblReceiptNoLbl.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblReceiptNoLbl.Location = new System.Drawing.Point(10, 34);
            this.lblReceiptNoLbl.Name = "lblReceiptNoLbl";
            this.lblReceiptNoLbl.Size = new System.Drawing.Size(68, 15);
            this.lblReceiptNoLbl.TabIndex = 1;
            this.lblReceiptNoLbl.Text = "Receipt No:";
            // 
            // lblDocHeader
            // 
            this.lblDocHeader.AutoSize = true;
            this.lblDocHeader.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblDocHeader.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblDocHeader.Location = new System.Drawing.Point(8, 4);
            this.lblDocHeader.Name = "lblDocHeader";
            this.lblDocHeader.Size = new System.Drawing.Size(155, 21);
            this.lblDocHeader.TabIndex = 0;
            this.lblDocHeader.Text = "PAYMENT RECEIPT";
            // 
            // panelHeaderBrand
            // 
            this.panelHeaderBrand.Controls.Add(this.lblLibraryContact);
            this.panelHeaderBrand.Controls.Add(this.lblLibraryAddress);
            this.panelHeaderBrand.Controls.Add(this.lblLibraryName);
            this.panelHeaderBrand.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeaderBrand.Location = new System.Drawing.Point(30, 30);
            this.panelHeaderBrand.Name = "panelHeaderBrand";
            this.panelHeaderBrand.Size = new System.Drawing.Size(538, 80);
            this.panelHeaderBrand.TabIndex = 0;
            // 
            // lblLibraryContact
            // 
            this.lblLibraryContact.AutoSize = true;
            this.lblLibraryContact.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblLibraryContact.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblLibraryContact.Location = new System.Drawing.Point(8, 52);
            this.lblLibraryContact.Name = "lblLibraryContact";
            this.lblLibraryContact.Size = new System.Drawing.Size(252, 13);
            this.lblLibraryContact.TabIndex = 2;
            this.lblLibraryContact.Text = "Phone: +91 98765 43210   |   Email: library@app.local";
            // 
            // lblLibraryAddress
            // 
            this.lblLibraryAddress.AutoSize = true;
            this.lblLibraryAddress.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblLibraryAddress.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblLibraryAddress.Location = new System.Drawing.Point(8, 32);
            this.lblLibraryAddress.Name = "lblLibraryAddress";
            this.lblLibraryAddress.Size = new System.Drawing.Size(273, 15);
            this.lblLibraryAddress.TabIndex = 1;
            this.lblLibraryAddress.Text = "Central Reading Space & Study Hall, University Road";
            // 
            // lblLibraryName
            // 
            this.lblLibraryName.AutoSize = true;
            this.lblLibraryName.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblLibraryName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblLibraryName.Location = new System.Drawing.Point(6, 4);
            this.lblLibraryName.Name = "lblLibraryName";
            this.lblLibraryName.Size = new System.Drawing.Size(296, 25);
            this.lblLibraryName.TabIndex = 0;
            this.lblLibraryName.Text = "LIBRARY MANAGEMENT SYSTEM";
            // 
            // PaymentReceiptForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(684, 720);
            this.Controls.Add(this.panelDocumentScroll);
            this.Controls.Add(this.panelTopBar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PaymentReceiptForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Payment Receipt";
            this.panelTopBar.ResumeLayout(false);
            this.panelTopBar.PerformLayout();
            this.panelDocumentScroll.ResumeLayout(false);
            this.panelReceiptDoc.ResumeLayout(false);
            this.panelPaymentInfo.ResumeLayout(false);
            this.panelPaymentInfo.PerformLayout();
            this.panelFinancials.ResumeLayout(false);
            this.panelFinancials.PerformLayout();
            this.panelDetails.ResumeLayout(false);
            this.panelDetails.PerformLayout();
            this.panelReceiptMeta.ResumeLayout(false);
            this.panelReceiptMeta.PerformLayout();
            this.panelHeaderBrand.ResumeLayout(false);
            this.panelHeaderBrand.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelTopBar;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnPrintPreview;
        private System.Windows.Forms.Button btnPrintReceipt;
        private System.Windows.Forms.Panel panelDocumentScroll;
        private System.Windows.Forms.Panel panelReceiptDoc;
        private System.Windows.Forms.Panel panelHeaderBrand;
        private System.Windows.Forms.Label lblLibraryName;
        private System.Windows.Forms.Label lblLibraryAddress;
        private System.Windows.Forms.Label lblLibraryContact;
        private System.Windows.Forms.Panel panelReceiptMeta;
        private System.Windows.Forms.Label lblDocHeader;
        private System.Windows.Forms.Label lblReceiptNoLbl;
        private System.Windows.Forms.Label lblReceiptNoVal;
        private System.Windows.Forms.Label lblDateLbl;
        private System.Windows.Forms.Label lblDateVal;
        private System.Windows.Forms.Label lblStatusBadge;
        private System.Windows.Forms.Label lineSep1;
        private System.Windows.Forms.Panel panelDetails;
        private System.Windows.Forms.Label lblStudentNameLbl;
        private System.Windows.Forms.Label lblStudentNameVal;
        private System.Windows.Forms.Label lblStudentCodeLbl;
        private System.Windows.Forms.Label lblStudentCodeVal;
        private System.Windows.Forms.Label lblInvoiceNoLbl;
        private System.Windows.Forms.Label lblInvoiceNoVal;
        private System.Windows.Forms.Label lblMembershipLbl;
        private System.Windows.Forms.Label lblMembershipVal;
        private System.Windows.Forms.Label lblSeatLbl;
        private System.Windows.Forms.Label lblSeatVal;
        private System.Windows.Forms.Label lineSep2;
        private System.Windows.Forms.Panel panelFinancials;
        private System.Windows.Forms.Label lblInvoiceTotalLbl;
        private System.Windows.Forms.Label lblInvoiceTotalVal;
        private System.Windows.Forms.Label lblPreviousPaidLbl;
        private System.Windows.Forms.Label lblPreviousPaidVal;
        private System.Windows.Forms.Label lblPaymentReceivedLbl;
        private System.Windows.Forms.Label lblPaymentReceivedVal;
        private System.Windows.Forms.Label lineSepFinancial2;
        private System.Windows.Forms.Label lblTotalPaidLbl;
        private System.Windows.Forms.Label lblTotalPaidVal;
        private System.Windows.Forms.Label lblBalanceLbl;
        private System.Windows.Forms.Label lblBalanceVal;
        private System.Windows.Forms.Label lineSep3;
        private System.Windows.Forms.Panel panelPaymentInfo;
        private System.Windows.Forms.Label lblPaymentModeLbl;
        private System.Windows.Forms.Label lblPaymentModeVal;
        private System.Windows.Forms.Label lblReferenceLbl;
        private System.Windows.Forms.Label lblReferenceVal;
        private System.Windows.Forms.Label lblReceivedByLbl;
        private System.Windows.Forms.Label lblReceivedByVal;
        private System.Windows.Forms.Label lineSep4;
        private System.Windows.Forms.Label lblFooterThankYou;
    }
}
