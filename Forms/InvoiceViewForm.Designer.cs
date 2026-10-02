namespace Laibrary_Payment_App.Forms
{
    partial class InvoiceViewForm
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
            this.btnReceivePayment = new System.Windows.Forms.Button();
            this.btnPrintNotice = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.lblTopTitle = new System.Windows.Forms.Label();
            this.panelScrollContainer = new System.Windows.Forms.Panel();
            this.panelInvoiceSheet = new System.Windows.Forms.Panel();
            this.panelPaymentStatus = new System.Windows.Forms.Panel();
            this.lblBalanceValue = new System.Windows.Forms.Label();
            this.lblBalanceTitle = new System.Windows.Forms.Label();
            this.lblPaidValue = new System.Windows.Forms.Label();
            this.lblPaidTitle = new System.Windows.Forms.Label();
            this.lblPaymentStatusBadge = new System.Windows.Forms.Label();
            this.lblPaymentStatusHeading = new System.Windows.Forms.Label();
            this.panelTotals = new System.Windows.Forms.Panel();
            this.lblGrandTotalValue = new System.Windows.Forms.Label();
            this.lblGrandTotalTitle = new System.Windows.Forms.Label();
            this.panelDivider3 = new System.Windows.Forms.Panel();
            this.lblPreviousOutstandingValue = new System.Windows.Forms.Label();
            this.lblPreviousOutstandingTitle = new System.Windows.Forms.Label();
            this.lblCurrentChargesValue = new System.Windows.Forms.Label();
            this.lblCurrentChargesTitle = new System.Windows.Forms.Label();
            this.lblDiscountValue = new System.Windows.Forms.Label();
            this.lblDiscountTitle = new System.Windows.Forms.Label();
            this.lblSubtotalValue = new System.Windows.Forms.Label();
            this.lblSubtotalTitle = new System.Windows.Forms.Label();
            this.dgvItems = new System.Windows.Forms.DataGridView();
            this.panelDetails = new System.Windows.Forms.Panel();
            this.lblMembershipEnd = new System.Windows.Forms.Label();
            this.lblMembershipStart = new System.Windows.Forms.Label();
            this.lblMembershipPlan = new System.Windows.Forms.Label();
            this.lblMembershipHeading = new System.Windows.Forms.Label();
            this.lblStudentSeat = new System.Windows.Forms.Label();
            this.lblStudentMobile = new System.Windows.Forms.Label();
            this.lblStudentName = new System.Windows.Forms.Label();
            this.lblStudentCode = new System.Windows.Forms.Label();
            this.lblStudentHeading = new System.Windows.Forms.Label();
            this.panelInvoiceMeta = new System.Windows.Forms.Panel();
            this.lblDueDate = new System.Windows.Forms.Label();
            this.lblInvoiceDate = new System.Windows.Forms.Label();
            this.lblInvoiceNo = new System.Windows.Forms.Label();
            this.lblStatusBadge = new System.Windows.Forms.Label();
            this.panelDivider1 = new System.Windows.Forms.Panel();
            this.panelLibraryHeader = new System.Windows.Forms.Panel();
            this.lblLibrarySubtitle = new System.Windows.Forms.Label();
            this.lblLibraryName = new System.Windows.Forms.Label();
            this.panelTopBar.SuspendLayout();
            this.panelScrollContainer.SuspendLayout();
            this.panelInvoiceSheet.SuspendLayout();
            this.panelPaymentStatus.SuspendLayout();
            this.panelTotals.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItems)).BeginInit();
            this.panelDetails.SuspendLayout();
            this.panelInvoiceMeta.SuspendLayout();
            this.panelLibraryHeader.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelTopBar
            // 
            this.panelTopBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(76)))), ((int)(((byte)(120)))));
            this.panelTopBar.Controls.Add(this.btnReceivePayment);
            this.panelTopBar.Controls.Add(this.btnPrintNotice);
            this.panelTopBar.Controls.Add(this.btnClose);
            this.panelTopBar.Controls.Add(this.lblTopTitle);
            this.panelTopBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTopBar.Location = new System.Drawing.Point(0, 0);
            this.panelTopBar.Name = "panelTopBar";
            this.panelTopBar.Size = new System.Drawing.Size(834, 52);
            this.panelTopBar.TabIndex = 0;
            // 
            // btnReceivePayment
            // 
            this.btnReceivePayment.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnReceivePayment.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(163)))), ((int)(((byte)(74)))));
            this.btnReceivePayment.FlatAppearance.BorderSize = 0;
            this.btnReceivePayment.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReceivePayment.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnReceivePayment.ForeColor = System.Drawing.Color.White;
            this.btnReceivePayment.Location = new System.Drawing.Point(475, 10);
            this.btnReceivePayment.Name = "btnReceivePayment";
            this.btnReceivePayment.Size = new System.Drawing.Size(135, 32);
            this.btnReceivePayment.TabIndex = 3;
            this.btnReceivePayment.Text = "Receive Payment";
            this.btnReceivePayment.UseVisualStyleBackColor = false;
            this.btnReceivePayment.Click += new System.EventHandler(this.btnReceivePayment_Click);
            // 
            // btnPrintNotice
            // 
            this.btnPrintNotice.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPrintNotice.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(116)))), ((int)(((byte)(144)))));
            this.btnPrintNotice.FlatAppearance.BorderSize = 0;
            this.btnPrintNotice.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrintNotice.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnPrintNotice.ForeColor = System.Drawing.Color.White;
            this.btnPrintNotice.Location = new System.Drawing.Point(620, 10);
            this.btnPrintNotice.Name = "btnPrintNotice";
            this.btnPrintNotice.Size = new System.Drawing.Size(110, 32);
            this.btnPrintNotice.TabIndex = 2;
            this.btnPrintNotice.Text = "Print / Export";
            this.btnPrintNotice.UseVisualStyleBackColor = false;
            this.btnPrintNotice.Click += new System.EventHandler(this.btnPrintNotice_Click);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnClose.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.btnClose.Location = new System.Drawing.Point(742, 10);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(80, 32);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // lblTopTitle
            // 
            this.lblTopTitle.AutoSize = true;
            this.lblTopTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblTopTitle.ForeColor = System.Drawing.Color.White;
            this.lblTopTitle.Location = new System.Drawing.Point(18, 15);
            this.lblTopTitle.Name = "lblTopTitle";
            this.lblTopTitle.Size = new System.Drawing.Size(193, 21);
            this.lblTopTitle.TabIndex = 0;
            this.lblTopTitle.Text = "Official Student Invoice";
            // 
            // panelScrollContainer
            // 
            this.panelScrollContainer.AutoScroll = true;
            this.panelScrollContainer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.panelScrollContainer.Controls.Add(this.panelInvoiceSheet);
            this.panelScrollContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelScrollContainer.Location = new System.Drawing.Point(0, 52);
            this.panelScrollContainer.Name = "panelScrollContainer";
            this.panelScrollContainer.Padding = new System.Windows.Forms.Padding(20);
            this.panelScrollContainer.Size = new System.Drawing.Size(834, 759);
            this.panelScrollContainer.TabIndex = 1;
            // 
            // panelInvoiceSheet
            // 
            this.panelInvoiceSheet.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.panelInvoiceSheet.BackColor = System.Drawing.Color.White;
            this.panelInvoiceSheet.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelInvoiceSheet.Controls.Add(this.panelPaymentStatus);
            this.panelInvoiceSheet.Controls.Add(this.panelTotals);
            this.panelInvoiceSheet.Controls.Add(this.dgvItems);
            this.panelInvoiceSheet.Controls.Add(this.panelDetails);
            this.panelInvoiceSheet.Controls.Add(this.panelInvoiceMeta);
            this.panelInvoiceSheet.Controls.Add(this.panelDivider1);
            this.panelInvoiceSheet.Controls.Add(this.panelLibraryHeader);
            this.panelInvoiceSheet.Location = new System.Drawing.Point(40, 20);
            this.panelInvoiceSheet.Name = "panelInvoiceSheet";
            this.panelInvoiceSheet.Padding = new System.Windows.Forms.Padding(24);
            this.panelInvoiceSheet.Size = new System.Drawing.Size(750, 715);
            this.panelInvoiceSheet.TabIndex = 0;
            // 
            // panelPaymentStatus
            // 
            this.panelPaymentStatus.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.panelPaymentStatus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelPaymentStatus.Controls.Add(this.lblBalanceValue);
            this.panelPaymentStatus.Controls.Add(this.lblBalanceTitle);
            this.panelPaymentStatus.Controls.Add(this.lblPaidValue);
            this.panelPaymentStatus.Controls.Add(this.lblPaidTitle);
            this.panelPaymentStatus.Controls.Add(this.lblPaymentStatusBadge);
            this.panelPaymentStatus.Controls.Add(this.lblPaymentStatusHeading);
            this.panelPaymentStatus.Location = new System.Drawing.Point(24, 545);
            this.panelPaymentStatus.Name = "panelPaymentStatus";
            this.panelPaymentStatus.Padding = new System.Windows.Forms.Padding(16);
            this.panelPaymentStatus.Size = new System.Drawing.Size(320, 145);
            this.panelPaymentStatus.TabIndex = 6;
            // 
            // lblBalanceValue
            // 
            this.lblBalanceValue.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblBalanceValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this.lblBalanceValue.Location = new System.Drawing.Point(120, 105);
            this.lblBalanceValue.Name = "lblBalanceValue";
            this.lblBalanceValue.Size = new System.Drawing.Size(180, 22);
            this.lblBalanceValue.TabIndex = 5;
            this.lblBalanceValue.Text = "?0.00";
            this.lblBalanceValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblBalanceTitle
            // 
            this.lblBalanceTitle.AutoSize = true;
            this.lblBalanceTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblBalanceTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblBalanceTitle.Location = new System.Drawing.Point(16, 108);
            this.lblBalanceTitle.Name = "lblBalanceTitle";
            this.lblBalanceTitle.Size = new System.Drawing.Size(56, 17);
            this.lblBalanceTitle.TabIndex = 4;
            this.lblBalanceTitle.Text = "Balance:";
            // 
            // lblPaidValue
            // 
            this.lblPaidValue.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblPaidValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblPaidValue.Location = new System.Drawing.Point(120, 72);
            this.lblPaidValue.Name = "lblPaidValue";
            this.lblPaidValue.Size = new System.Drawing.Size(180, 20);
            this.lblPaidValue.TabIndex = 3;
            this.lblPaidValue.Text = "?0.00";
            this.lblPaidValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblPaidTitle
            // 
            this.lblPaidTitle.AutoSize = true;
            this.lblPaidTitle.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblPaidTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblPaidTitle.Location = new System.Drawing.Point(16, 74);
            this.lblPaidTitle.Name = "lblPaidTitle";
            this.lblPaidTitle.Size = new System.Drawing.Size(36, 17);
            this.lblPaidTitle.TabIndex = 2;
            this.lblPaidTitle.Text = "Paid:";
            // 
            // lblPaymentStatusBadge
            // 
            this.lblPaymentStatusBadge.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.lblPaymentStatusBadge.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblPaymentStatusBadge.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this.lblPaymentStatusBadge.Location = new System.Drawing.Point(160, 14);
            this.lblPaymentStatusBadge.Name = "lblPaymentStatusBadge";
            this.lblPaymentStatusBadge.Size = new System.Drawing.Size(140, 30);
            this.lblPaymentStatusBadge.TabIndex = 1;
            this.lblPaymentStatusBadge.Text = "UNPAID";
            this.lblPaymentStatusBadge.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblPaymentStatusHeading
            // 
            this.lblPaymentStatusHeading.AutoSize = true;
            this.lblPaymentStatusHeading.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblPaymentStatusHeading.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblPaymentStatusHeading.Location = new System.Drawing.Point(16, 20);
            this.lblPaymentStatusHeading.Name = "lblPaymentStatusHeading";
            this.lblPaymentStatusHeading.Size = new System.Drawing.Size(109, 19);
            this.lblPaymentStatusHeading.TabIndex = 0;
            this.lblPaymentStatusHeading.Text = "Payment Status:";
            // 
            // panelTotals
            // 
            this.panelTotals.Controls.Add(this.lblGrandTotalValue);
            this.panelTotals.Controls.Add(this.lblGrandTotalTitle);
            this.panelTotals.Controls.Add(this.panelDivider3);
            this.panelTotals.Controls.Add(this.lblPreviousOutstandingValue);
            this.panelTotals.Controls.Add(this.lblPreviousOutstandingTitle);
            this.panelTotals.Controls.Add(this.lblCurrentChargesValue);
            this.panelTotals.Controls.Add(this.lblCurrentChargesTitle);
            this.panelTotals.Controls.Add(this.lblDiscountValue);
            this.panelTotals.Controls.Add(this.lblDiscountTitle);
            this.panelTotals.Controls.Add(this.lblSubtotalValue);
            this.panelTotals.Controls.Add(this.lblSubtotalTitle);
            this.panelTotals.Location = new System.Drawing.Point(375, 490);
            this.panelTotals.Name = "panelTotals";
            this.panelTotals.Size = new System.Drawing.Size(350, 200);
            this.panelTotals.TabIndex = 5;
            // 
            // lblGrandTotalValue
            // 
            this.lblGrandTotalValue.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblGrandTotalValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(76)))), ((int)(((byte)(120)))));
            this.lblGrandTotalValue.Location = new System.Drawing.Point(170, 155);
            this.lblGrandTotalValue.Name = "lblGrandTotalValue";
            this.lblGrandTotalValue.Size = new System.Drawing.Size(175, 25);
            this.lblGrandTotalValue.TabIndex = 10;
            this.lblGrandTotalValue.Text = "?0.00";
            this.lblGrandTotalValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblGrandTotalTitle
            // 
            this.lblGrandTotalTitle.AutoSize = true;
            this.lblGrandTotalTitle.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblGrandTotalTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblGrandTotalTitle.Location = new System.Drawing.Point(10, 158);
            this.lblGrandTotalTitle.Name = "lblGrandTotalTitle";
            this.lblGrandTotalTitle.Size = new System.Drawing.Size(91, 20);
            this.lblGrandTotalTitle.TabIndex = 9;
            this.lblGrandTotalTitle.Text = "Grand Total";
            // 
            // panelDivider3
            // 
            this.panelDivider3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(213)))), ((int)(((byte)(225)))));
            this.panelDivider3.Location = new System.Drawing.Point(10, 142);
            this.panelDivider3.Name = "panelDivider3";
            this.panelDivider3.Size = new System.Drawing.Size(335, 1);
            this.panelDivider3.TabIndex = 8;
            // 
            // lblPreviousOutstandingValue
            // 
            this.lblPreviousOutstandingValue.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblPreviousOutstandingValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(83)))), ((int)(((byte)(9)))));
            this.lblPreviousOutstandingValue.Location = new System.Drawing.Point(170, 110);
            this.lblPreviousOutstandingValue.Name = "lblPreviousOutstandingValue";
            this.lblPreviousOutstandingValue.Size = new System.Drawing.Size(175, 20);
            this.lblPreviousOutstandingValue.TabIndex = 7;
            this.lblPreviousOutstandingValue.Text = "?0.00";
            this.lblPreviousOutstandingValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblPreviousOutstandingTitle
            // 
            this.lblPreviousOutstandingTitle.AutoSize = true;
            this.lblPreviousOutstandingTitle.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblPreviousOutstandingTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblPreviousOutstandingTitle.Location = new System.Drawing.Point(10, 110);
            this.lblPreviousOutstandingTitle.Name = "lblPreviousOutstandingTitle";
            this.lblPreviousOutstandingTitle.Size = new System.Drawing.Size(133, 17);
            this.lblPreviousOutstandingTitle.TabIndex = 6;
            this.lblPreviousOutstandingTitle.Text = "Previous Outstanding";
            // 
            // lblCurrentChargesValue
            // 
            this.lblCurrentChargesValue.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblCurrentChargesValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblCurrentChargesValue.Location = new System.Drawing.Point(170, 75);
            this.lblCurrentChargesValue.Name = "lblCurrentChargesValue";
            this.lblCurrentChargesValue.Size = new System.Drawing.Size(175, 20);
            this.lblCurrentChargesValue.TabIndex = 5;
            this.lblCurrentChargesValue.Text = "?0.00";
            this.lblCurrentChargesValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblCurrentChargesTitle
            // 
            this.lblCurrentChargesTitle.AutoSize = true;
            this.lblCurrentChargesTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblCurrentChargesTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblCurrentChargesTitle.Location = new System.Drawing.Point(10, 75);
            this.lblCurrentChargesTitle.Name = "lblCurrentChargesTitle";
            this.lblCurrentChargesTitle.Size = new System.Drawing.Size(104, 17);
            this.lblCurrentChargesTitle.TabIndex = 4;
            this.lblCurrentChargesTitle.Text = "Current Charges";
            // 
            // lblDiscountValue
            // 
            this.lblDiscountValue.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblDiscountValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this.lblDiscountValue.Location = new System.Drawing.Point(170, 42);
            this.lblDiscountValue.Name = "lblDiscountValue";
            this.lblDiscountValue.Size = new System.Drawing.Size(175, 20);
            this.lblDiscountValue.TabIndex = 3;
            this.lblDiscountValue.Text = "-?0.00";
            this.lblDiscountValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblDiscountTitle
            // 
            this.lblDiscountTitle.AutoSize = true;
            this.lblDiscountTitle.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblDiscountTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblDiscountTitle.Location = new System.Drawing.Point(10, 42);
            this.lblDiscountTitle.Name = "lblDiscountTitle";
            this.lblDiscountTitle.Size = new System.Drawing.Size(58, 17);
            this.lblDiscountTitle.TabIndex = 2;
            this.lblDiscountTitle.Text = "Discount";
            // 
            // lblSubtotalValue
            // 
            this.lblSubtotalValue.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblSubtotalValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblSubtotalValue.Location = new System.Drawing.Point(170, 10);
            this.lblSubtotalValue.Name = "lblSubtotalValue";
            this.lblSubtotalValue.Size = new System.Drawing.Size(175, 20);
            this.lblSubtotalValue.TabIndex = 1;
            this.lblSubtotalValue.Text = "?0.00";
            this.lblSubtotalValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblSubtotalTitle
            // 
            this.lblSubtotalTitle.AutoSize = true;
            this.lblSubtotalTitle.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblSubtotalTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblSubtotalTitle.Location = new System.Drawing.Point(10, 10);
            this.lblSubtotalTitle.Name = "lblSubtotalTitle";
            this.lblSubtotalTitle.Size = new System.Drawing.Size(57, 17);
            this.lblSubtotalTitle.TabIndex = 0;
            this.lblSubtotalTitle.Text = "Subtotal";
            // 
            // dgvItems
            // 
            this.dgvItems.AllowUserToAddRows = false;
            this.dgvItems.AllowUserToDeleteRows = false;
            this.dgvItems.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvItems.BackgroundColor = System.Drawing.Color.White;
            this.dgvItems.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvItems.Location = new System.Drawing.Point(24, 280);
            this.dgvItems.Name = "dgvItems";
            this.dgvItems.ReadOnly = true;
            this.dgvItems.RowHeadersVisible = false;
            this.dgvItems.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvItems.Size = new System.Drawing.Size(700, 190);
            this.dgvItems.TabIndex = 4;
            // 
            // panelDetails
            // 
            this.panelDetails.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.panelDetails.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelDetails.Controls.Add(this.lblMembershipEnd);
            this.panelDetails.Controls.Add(this.lblMembershipStart);
            this.panelDetails.Controls.Add(this.lblMembershipPlan);
            this.panelDetails.Controls.Add(this.lblMembershipHeading);
            this.panelDetails.Controls.Add(this.lblStudentSeat);
            this.panelDetails.Controls.Add(this.lblStudentMobile);
            this.panelDetails.Controls.Add(this.lblStudentName);
            this.panelDetails.Controls.Add(this.lblStudentCode);
            this.panelDetails.Controls.Add(this.lblStudentHeading);
            this.panelDetails.Location = new System.Drawing.Point(24, 155);
            this.panelDetails.Name = "panelDetails";
            this.panelDetails.Padding = new System.Windows.Forms.Padding(16);
            this.panelDetails.Size = new System.Drawing.Size(700, 110);
            this.panelDetails.TabIndex = 3;
            // 
            // lblMembershipEnd
            // 
            this.lblMembershipEnd.AutoSize = true;
            this.lblMembershipEnd.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblMembershipEnd.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.lblMembershipEnd.Location = new System.Drawing.Point(370, 78);
            this.lblMembershipEnd.Name = "lblMembershipEnd";
            this.lblMembershipEnd.Size = new System.Drawing.Size(84, 15);
            this.lblMembershipEnd.TabIndex = 8;
            this.lblMembershipEnd.Text = "End Date: -";
            // 
            // lblMembershipStart
            // 
            this.lblMembershipStart.AutoSize = true;
            this.lblMembershipStart.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblMembershipStart.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.lblMembershipStart.Location = new System.Drawing.Point(370, 56);
            this.lblMembershipStart.Name = "lblMembershipStart";
            this.lblMembershipStart.Size = new System.Drawing.Size(88, 15);
            this.lblMembershipStart.TabIndex = 7;
            this.lblMembershipStart.Text = "Start Date: -";
            // 
            // lblMembershipPlan
            // 
            this.lblMembershipPlan.AutoSize = true;
            this.lblMembershipPlan.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblMembershipPlan.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.lblMembershipPlan.Location = new System.Drawing.Point(370, 34);
            this.lblMembershipPlan.Name = "lblMembershipPlan";
            this.lblMembershipPlan.Size = new System.Drawing.Size(65, 15);
            this.lblMembershipPlan.TabIndex = 6;
            this.lblMembershipPlan.Text = "Plan: None";
            // 
            // lblMembershipHeading
            // 
            this.lblMembershipHeading.AutoSize = true;
            this.lblMembershipHeading.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblMembershipHeading.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(76)))), ((int)(((byte)(120)))));
            this.lblMembershipHeading.Location = new System.Drawing.Point(370, 10);
            this.lblMembershipHeading.Name = "lblMembershipHeading";
            this.lblMembershipHeading.Size = new System.Drawing.Size(129, 17);
            this.lblMembershipHeading.TabIndex = 5;
            this.lblMembershipHeading.Text = "Membership Details";
            // 
            // lblStudentSeat
            // 
            this.lblStudentSeat.AutoSize = true;
            this.lblStudentSeat.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblStudentSeat.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.lblStudentSeat.Location = new System.Drawing.Point(14, 78);
            this.lblStudentSeat.Name = "lblStudentSeat";
            this.lblStudentSeat.Size = new System.Drawing.Size(117, 15);
            this.lblStudentSeat.TabIndex = 4;
            this.lblStudentSeat.Text = "Assigned Seat: None";
            // 
            // lblStudentMobile
            // 
            this.lblStudentMobile.AutoSize = true;
            this.lblStudentMobile.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblStudentMobile.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.lblStudentMobile.Location = new System.Drawing.Point(14, 56);
            this.lblStudentMobile.Name = "lblStudentMobile";
            this.lblStudentMobile.Size = new System.Drawing.Size(56, 15);
            this.lblStudentMobile.TabIndex = 3;
            this.lblStudentMobile.Text = "Mobile: -";
            // 
            // lblStudentName
            // 
            this.lblStudentName.AutoSize = true;
            this.lblStudentName.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblStudentName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblStudentName.Location = new System.Drawing.Point(14, 34);
            this.lblStudentName.Name = "lblStudentName";
            this.lblStudentName.Size = new System.Drawing.Size(51, 15);
            this.lblStudentName.TabIndex = 2;
            this.lblStudentName.Text = "Name: -";
            // 
            // lblStudentCode
            // 
            this.lblStudentCode.AutoSize = true;
            this.lblStudentCode.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblStudentCode.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblStudentCode.Location = new System.Drawing.Point(100, 11);
            this.lblStudentCode.Name = "lblStudentCode";
            this.lblStudentCode.Size = new System.Drawing.Size(43, 15);
            this.lblStudentCode.TabIndex = 1;
            this.lblStudentCode.Text = "[STU-]";
            // 
            // lblStudentHeading
            // 
            this.lblStudentHeading.AutoSize = true;
            this.lblStudentHeading.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblStudentHeading.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(76)))), ((int)(((byte)(120)))));
            this.lblStudentHeading.Location = new System.Drawing.Point(14, 10);
            this.lblStudentHeading.Name = "lblStudentHeading";
            this.lblStudentHeading.Size = new System.Drawing.Size(81, 17);
            this.lblStudentHeading.TabIndex = 0;
            this.lblStudentHeading.Text = "Student Info";
            // 
            // panelInvoiceMeta
            // 
            this.panelInvoiceMeta.Controls.Add(this.lblDueDate);
            this.panelInvoiceMeta.Controls.Add(this.lblInvoiceDate);
            this.panelInvoiceMeta.Controls.Add(this.lblInvoiceNo);
            this.panelInvoiceMeta.Controls.Add(this.lblStatusBadge);
            this.panelInvoiceMeta.Location = new System.Drawing.Point(24, 90);
            this.panelInvoiceMeta.Name = "panelInvoiceMeta";
            this.panelInvoiceMeta.Size = new System.Drawing.Size(700, 50);
            this.panelInvoiceMeta.TabIndex = 2;
            // 
            // lblDueDate
            // 
            this.lblDueDate.AutoSize = true;
            this.lblDueDate.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblDueDate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblDueDate.Location = new System.Drawing.Point(370, 26);
            this.lblDueDate.Name = "lblDueDate";
            this.lblDueDate.Size = new System.Drawing.Size(74, 17);
            this.lblDueDate.TabIndex = 3;
            this.lblDueDate.Text = "Due Date: -";
            // 
            // lblInvoiceDate
            // 
            this.lblInvoiceDate.AutoSize = true;
            this.lblInvoiceDate.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblInvoiceDate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblInvoiceDate.Location = new System.Drawing.Point(370, 4);
            this.lblInvoiceDate.Name = "lblInvoiceDate";
            this.lblInvoiceDate.Size = new System.Drawing.Size(91, 17);
            this.lblInvoiceDate.TabIndex = 2;
            this.lblInvoiceDate.Text = "Invoice Date: -";
            // 
            // lblInvoiceNo
            // 
            this.lblInvoiceNo.AutoSize = true;
            this.lblInvoiceNo.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblInvoiceNo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblInvoiceNo.Location = new System.Drawing.Point(14, 14);
            this.lblInvoiceNo.Name = "lblInvoiceNo";
            this.lblInvoiceNo.Size = new System.Drawing.Size(184, 21);
            this.lblInvoiceNo.TabIndex = 1;
            this.lblInvoiceNo.Text = "Invoice No: INV-XXXXX";
            // 
            // lblStatusBadge
            // 
            this.lblStatusBadge.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblStatusBadge.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(243)))), ((int)(((byte)(199)))));
            this.lblStatusBadge.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblStatusBadge.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(83)))), ((int)(((byte)(9)))));
            this.lblStatusBadge.Location = new System.Drawing.Point(580, 10);
            this.lblStatusBadge.Name = "lblStatusBadge";
            this.lblStatusBadge.Size = new System.Drawing.Size(110, 28);
            this.lblStatusBadge.TabIndex = 0;
            this.lblStatusBadge.Text = "UNPAID";
            this.lblStatusBadge.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panelDivider1
            // 
            this.panelDivider1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.panelDivider1.Location = new System.Drawing.Point(24, 76);
            this.panelDivider1.Name = "panelDivider1";
            this.panelDivider1.Size = new System.Drawing.Size(700, 1);
            this.panelDivider1.TabIndex = 1;
            // 
            // panelLibraryHeader
            // 
            this.panelLibraryHeader.Controls.Add(this.lblLibrarySubtitle);
            this.panelLibraryHeader.Controls.Add(this.lblLibraryName);
            this.panelLibraryHeader.Location = new System.Drawing.Point(24, 10);
            this.panelLibraryHeader.Name = "panelLibraryHeader";
            this.panelLibraryHeader.Size = new System.Drawing.Size(700, 60);
            this.panelLibraryHeader.TabIndex = 0;
            // 
            // lblLibrarySubtitle
            // 
            this.lblLibrarySubtitle.AutoSize = true;
            this.lblLibrarySubtitle.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblLibrarySubtitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblLibrarySubtitle.Location = new System.Drawing.Point(14, 35);
            this.lblLibrarySubtitle.Name = "lblLibrarySubtitle";
            this.lblLibrarySubtitle.Size = new System.Drawing.Size(434, 15);
            this.lblLibrarySubtitle.TabIndex = 1;
            this.lblLibrarySubtitle.Text = "Official Student Tax Invoice • Reading Space & Library Management Services";
            // 
            // lblLibraryName
            // 
            this.lblLibraryName.AutoSize = true;
            this.lblLibraryName.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblLibraryName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(76)))), ((int)(((byte)(120)))));
            this.lblLibraryName.Location = new System.Drawing.Point(12, 5);
            this.lblLibraryName.Name = "lblLibraryName";
            this.lblLibraryName.Size = new System.Drawing.Size(347, 30);
            this.lblLibraryName.TabIndex = 0;
            this.lblLibraryName.Text = "LIBRARY MANAGEMENT SYSTEM";
            // 
            // InvoiceViewForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(834, 811);
            this.Controls.Add(this.panelScrollContainer);
            this.Controls.Add(this.panelTopBar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "InvoiceViewForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Library Management System - Invoice Preview";
            this.Load += new System.EventHandler(this.InvoiceViewForm_Load);
            this.panelTopBar.ResumeLayout(false);
            this.panelTopBar.PerformLayout();
            this.panelScrollContainer.ResumeLayout(false);
            this.panelInvoiceSheet.ResumeLayout(false);
            this.panelPaymentStatus.ResumeLayout(false);
            this.panelPaymentStatus.PerformLayout();
            this.panelTotals.ResumeLayout(false);
            this.panelTotals.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItems)).EndInit();
            this.panelDetails.ResumeLayout(false);
            this.panelDetails.PerformLayout();
            this.panelInvoiceMeta.ResumeLayout(false);
            this.panelInvoiceMeta.PerformLayout();
            this.panelLibraryHeader.ResumeLayout(false);
            this.panelLibraryHeader.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelTopBar;
        private System.Windows.Forms.Label lblTopTitle;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnPrintNotice;
        private System.Windows.Forms.Button btnReceivePayment;
        private System.Windows.Forms.Panel panelScrollContainer;
        private System.Windows.Forms.Panel panelInvoiceSheet;
        private System.Windows.Forms.Panel panelLibraryHeader;
        private System.Windows.Forms.Label lblLibraryName;
        private System.Windows.Forms.Label lblLibrarySubtitle;
        private System.Windows.Forms.Panel panelDivider1;
        private System.Windows.Forms.Panel panelInvoiceMeta;
        private System.Windows.Forms.Label lblInvoiceNo;
        private System.Windows.Forms.Label lblStatusBadge;
        private System.Windows.Forms.Label lblInvoiceDate;
        private System.Windows.Forms.Label lblDueDate;
        private System.Windows.Forms.Panel panelDetails;
        private System.Windows.Forms.Label lblStudentHeading;
        private System.Windows.Forms.Label lblStudentCode;
        private System.Windows.Forms.Label lblStudentName;
        private System.Windows.Forms.Label lblStudentMobile;
        private System.Windows.Forms.Label lblStudentSeat;
        private System.Windows.Forms.Label lblMembershipHeading;
        private System.Windows.Forms.Label lblMembershipPlan;
        private System.Windows.Forms.Label lblMembershipStart;
        private System.Windows.Forms.Label lblMembershipEnd;
        private System.Windows.Forms.DataGridView dgvItems;
        private System.Windows.Forms.Panel panelTotals;
        private System.Windows.Forms.Label lblSubtotalTitle;
        private System.Windows.Forms.Label lblSubtotalValue;
        private System.Windows.Forms.Label lblDiscountTitle;
        private System.Windows.Forms.Label lblDiscountValue;
        private System.Windows.Forms.Label lblCurrentChargesTitle;
        private System.Windows.Forms.Label lblCurrentChargesValue;
        private System.Windows.Forms.Label lblPreviousOutstandingTitle;
        private System.Windows.Forms.Label lblPreviousOutstandingValue;
        private System.Windows.Forms.Panel panelDivider3;
        private System.Windows.Forms.Label lblGrandTotalTitle;
        private System.Windows.Forms.Label lblGrandTotalValue;
        private System.Windows.Forms.Panel panelPaymentStatus;
        private System.Windows.Forms.Label lblPaymentStatusHeading;
        private System.Windows.Forms.Label lblPaymentStatusBadge;
        private System.Windows.Forms.Label lblPaidTitle;
        private System.Windows.Forms.Label lblPaidValue;
        private System.Windows.Forms.Label lblBalanceTitle;
        private System.Windows.Forms.Label lblBalanceValue;
    }
}
