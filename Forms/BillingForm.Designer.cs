namespace Laibrary_Payment_App.Forms
{
    partial class BillingForm
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
            this.panelHeader = new System.Windows.Forms.Panel();
            this.lblHeaderSubtitle = new System.Windows.Forms.Label();
            this.lblHeaderTitle = new System.Windows.Forms.Label();
            this.panelContent = new System.Windows.Forms.Panel();
            this.panelRight = new System.Windows.Forms.Panel();
            this.panelSummaryCard = new System.Windows.Forms.Panel();
            this.btnHistory = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnGenerateInvoice = new System.Windows.Forms.Button();
            this.lblBalanceValue = new System.Windows.Forms.Label();
            this.lblBalanceTitle = new System.Windows.Forms.Label();
            this.lblPaidValue = new System.Windows.Forms.Label();
            this.lblPaidTitle = new System.Windows.Forms.Label();
            this.lblGrandTotalValue = new System.Windows.Forms.Label();
            this.lblGrandTotalTitle = new System.Windows.Forms.Label();
            this.panelDivider2 = new System.Windows.Forms.Panel();
            this.lblPreviousOutstandingValue = new System.Windows.Forms.Label();
            this.lblPreviousOutstandingTitle = new System.Windows.Forms.Label();
            this.lblCurrentChargesValue = new System.Windows.Forms.Label();
            this.lblCurrentChargesTitle = new System.Windows.Forms.Label();
            this.lblTaxValue = new System.Windows.Forms.Label();
            this.lblTaxTitle = new System.Windows.Forms.Label();
            this.lblAdditionalChargesValue = new System.Windows.Forms.Label();
            this.lblAdditionalChargesTitle = new System.Windows.Forms.Label();
            this.lblDiscountSummaryValue = new System.Windows.Forms.Label();
            this.lblDiscountSummaryTitle = new System.Windows.Forms.Label();
            this.lblSubTotalValue = new System.Windows.Forms.Label();
            this.lblSubTotalTitle = new System.Windows.Forms.Label();
            this.panelDivider1 = new System.Windows.Forms.Panel();
            this.lblSummaryHeading = new System.Windows.Forms.Label();
            this.panelLeft = new System.Windows.Forms.Panel();
            this.grpAdditionalCharges = new System.Windows.Forms.GroupBox();
            this.btnRemoveItem = new System.Windows.Forms.Button();
            this.btnAddCharge = new System.Windows.Forms.Button();
            this.numChargeRate = new System.Windows.Forms.NumericUpDown();
            this.lblChargeRate = new System.Windows.Forms.Label();
            this.numChargeQty = new System.Windows.Forms.NumericUpDown();
            this.lblChargeQty = new System.Windows.Forms.Label();
            this.cmbChargeType = new System.Windows.Forms.ComboBox();
            this.lblChargeType = new System.Windows.Forms.Label();
            this.dgvInvoiceItems = new System.Windows.Forms.DataGridView();
            this.grpInvoiceSettings = new System.Windows.Forms.GroupBox();
            this.numDiscount = new System.Windows.Forms.NumericUpDown();
            this.lblDiscount = new System.Windows.Forms.Label();
            this.dtpDueDate = new System.Windows.Forms.DateTimePicker();
            this.lblDueDate = new System.Windows.Forms.Label();
            this.dtpInvoiceDate = new System.Windows.Forms.DateTimePicker();
            this.lblInvoiceDate = new System.Windows.Forms.Label();
            this.grpMembership = new System.Windows.Forms.GroupBox();
            this.lblNoMembershipAlert = new System.Windows.Forms.Label();
            this.lblFinalFeeValue = new System.Windows.Forms.Label();
            this.lblFinalFeeTitle = new System.Windows.Forms.Label();
            this.lblDiscountValue = new System.Windows.Forms.Label();
            this.lblDiscountTitle = new System.Windows.Forms.Label();
            this.lblActualFeeValue = new System.Windows.Forms.Label();
            this.lblActualFeeTitle = new System.Windows.Forms.Label();
            this.lblEndDateValue = new System.Windows.Forms.Label();
            this.lblEndDateTitle = new System.Windows.Forms.Label();
            this.lblStartDateValue = new System.Windows.Forms.Label();
            this.lblStartDateTitle = new System.Windows.Forms.Label();
            this.lblSeatValue = new System.Windows.Forms.Label();
            this.lblSeatTitle = new System.Windows.Forms.Label();
            this.lblPlanValue = new System.Windows.Forms.Label();
            this.lblPlanTitle = new System.Windows.Forms.Label();
            this.lblMembershipIdValue = new System.Windows.Forms.Label();
            this.lblMembershipIdTitle = new System.Windows.Forms.Label();
            this.grpStudentSelection = new System.Windows.Forms.GroupBox();
            this.lblStudentDetails = new System.Windows.Forms.Label();
            this.cmbStudents = new System.Windows.Forms.ComboBox();
            this.lblSelectStudent = new System.Windows.Forms.Label();
            this.txtSearchStudent = new System.Windows.Forms.TextBox();
            this.lblSearchStudent = new System.Windows.Forms.Label();
            this.panelHeader.SuspendLayout();
            this.panelContent.SuspendLayout();
            this.panelRight.SuspendLayout();
            this.panelSummaryCard.SuspendLayout();
            this.panelLeft.SuspendLayout();
            this.grpAdditionalCharges.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numChargeRate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numChargeQty)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvInvoiceItems)).BeginInit();
            this.grpInvoiceSettings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numDiscount)).BeginInit();
            this.grpMembership.SuspendLayout();
            this.grpStudentSelection.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelHeader
            // 
            this.panelHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(76)))), ((int)(((byte)(120)))));
            this.panelHeader.Controls.Add(this.lblHeaderSubtitle);
            this.panelHeader.Controls.Add(this.lblHeaderTitle);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(0, 0);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(1184, 60);
            this.panelHeader.TabIndex = 0;
            // 
            // lblHeaderSubtitle
            // 
            this.lblHeaderSubtitle.AutoSize = true;
            this.lblHeaderSubtitle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblHeaderSubtitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(186)))), ((int)(((byte)(230)))), ((int)(((byte)(253)))));
            this.lblHeaderSubtitle.Location = new System.Drawing.Point(24, 35);
            this.lblHeaderSubtitle.Name = "lblHeaderSubtitle";
            this.lblHeaderSubtitle.Size = new System.Drawing.Size(390, 15);
            this.lblHeaderSubtitle.TabIndex = 1;
            this.lblHeaderSubtitle.Text = "Generate official student invoices with membership & additional library charges";
            // 
            // lblHeaderTitle
            // 
            this.lblHeaderTitle.AutoSize = true;
            this.lblHeaderTitle.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblHeaderTitle.ForeColor = System.Drawing.Color.White;
            this.lblHeaderTitle.Location = new System.Drawing.Point(22, 10);
            this.lblHeaderTitle.Name = "lblHeaderTitle";
            this.lblHeaderTitle.Size = new System.Drawing.Size(400, 25);
            this.lblHeaderTitle.TabIndex = 0;
            this.lblHeaderTitle.Text = "Library Management System - Student Billing";
            // 
            // panelContent
            // 
            this.panelContent.AutoScroll = true;
            this.panelContent.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.panelContent.Controls.Add(this.panelRight);
            this.panelContent.Controls.Add(this.panelLeft);
            this.panelContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContent.Location = new System.Drawing.Point(0, 60);
            this.panelContent.Name = "panelContent";
            this.panelContent.Padding = new System.Windows.Forms.Padding(16);
            this.panelContent.Size = new System.Drawing.Size(1184, 691);
            this.panelContent.TabIndex = 1;
            // 
            // panelRight
            // 
            this.panelRight.Controls.Add(this.panelSummaryCard);
            this.panelRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelRight.Location = new System.Drawing.Point(798, 16);
            this.panelRight.Name = "panelRight";
            this.panelRight.Size = new System.Drawing.Size(370, 659);
            this.panelRight.TabIndex = 1;
            // 
            // panelSummaryCard
            // 
            this.panelSummaryCard.BackColor = System.Drawing.Color.White;
            this.panelSummaryCard.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelSummaryCard.Controls.Add(this.btnHistory);
            this.panelSummaryCard.Controls.Add(this.btnClear);
            this.panelSummaryCard.Controls.Add(this.btnGenerateInvoice);
            this.panelSummaryCard.Controls.Add(this.lblBalanceValue);
            this.panelSummaryCard.Controls.Add(this.lblBalanceTitle);
            this.panelSummaryCard.Controls.Add(this.lblPaidValue);
            this.panelSummaryCard.Controls.Add(this.lblPaidTitle);
            this.panelSummaryCard.Controls.Add(this.lblGrandTotalValue);
            this.panelSummaryCard.Controls.Add(this.lblGrandTotalTitle);
            this.panelSummaryCard.Controls.Add(this.panelDivider2);
            this.panelSummaryCard.Controls.Add(this.lblPreviousOutstandingValue);
            this.panelSummaryCard.Controls.Add(this.lblPreviousOutstandingTitle);
            this.panelSummaryCard.Controls.Add(this.lblCurrentChargesValue);
            this.panelSummaryCard.Controls.Add(this.lblCurrentChargesTitle);
            this.panelSummaryCard.Controls.Add(this.lblTaxValue);
            this.panelSummaryCard.Controls.Add(this.lblTaxTitle);
            this.panelSummaryCard.Controls.Add(this.lblAdditionalChargesValue);
            this.panelSummaryCard.Controls.Add(this.lblAdditionalChargesTitle);
            this.panelSummaryCard.Controls.Add(this.lblDiscountSummaryValue);
            this.panelSummaryCard.Controls.Add(this.lblDiscountSummaryTitle);
            this.panelSummaryCard.Controls.Add(this.lblSubTotalValue);
            this.panelSummaryCard.Controls.Add(this.lblSubTotalTitle);
            this.panelSummaryCard.Controls.Add(this.panelDivider1);
            this.panelSummaryCard.Controls.Add(this.lblSummaryHeading);
            this.panelSummaryCard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelSummaryCard.Location = new System.Drawing.Point(0, 0);
            this.panelSummaryCard.Name = "panelSummaryCard";
            this.panelSummaryCard.Padding = new System.Windows.Forms.Padding(18);
            this.panelSummaryCard.Size = new System.Drawing.Size(370, 659);
            this.panelSummaryCard.TabIndex = 0;
            // 
            // btnHistory
            // 
            this.btnHistory.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnHistory.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.btnHistory.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(213)))), ((int)(((byte)(225)))));
            this.btnHistory.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHistory.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnHistory.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.btnHistory.Location = new System.Drawing.Point(18, 605);
            this.btnHistory.Name = "btnHistory";
            this.btnHistory.Size = new System.Drawing.Size(332, 38);
            this.btnHistory.TabIndex = 23;
            this.btnHistory.Text = "View Invoice History";
            this.btnHistory.UseVisualStyleBackColor = false;
            this.btnHistory.Click += new System.EventHandler(this.btnHistory_Click);
            // 
            // btnClear
            // 
            this.btnClear.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClear.BackColor = System.Drawing.Color.White;
            this.btnClear.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(213)))), ((int)(((byte)(225)))));
            this.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClear.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnClear.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.btnClear.Location = new System.Drawing.Point(18, 560);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(332, 38);
            this.btnClear.TabIndex = 22;
            this.btnClear.Text = "Reset Form";
            this.btnClear.UseVisualStyleBackColor = false;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnGenerateInvoice
            // 
            this.btnGenerateInvoice.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGenerateInvoice.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(185)))), ((int)(((byte)(129)))));
            this.btnGenerateInvoice.FlatAppearance.BorderSize = 0;
            this.btnGenerateInvoice.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGenerateInvoice.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnGenerateInvoice.ForeColor = System.Drawing.Color.White;
            this.btnGenerateInvoice.Location = new System.Drawing.Point(18, 505);
            this.btnGenerateInvoice.Name = "btnGenerateInvoice";
            this.btnGenerateInvoice.Size = new System.Drawing.Size(332, 48);
            this.btnGenerateInvoice.TabIndex = 21;
            this.btnGenerateInvoice.Text = "Generate Invoice";
            this.btnGenerateInvoice.UseVisualStyleBackColor = false;
            this.btnGenerateInvoice.Click += new System.EventHandler(this.btnGenerateInvoice_Click);
            // 
            // lblBalanceValue
            // 
            this.lblBalanceValue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblBalanceValue.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblBalanceValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this.lblBalanceValue.Location = new System.Drawing.Point(180, 440);
            this.lblBalanceValue.Name = "lblBalanceValue";
            this.lblBalanceValue.Size = new System.Drawing.Size(170, 24);
            this.lblBalanceValue.TabIndex = 20;
            this.lblBalanceValue.Text = "?0.00";
            this.lblBalanceValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblBalanceTitle
            // 
            this.lblBalanceTitle.AutoSize = true;
            this.lblBalanceTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblBalanceTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblBalanceTitle.Location = new System.Drawing.Point(18, 442);
            this.lblBalanceTitle.Name = "lblBalanceTitle";
            this.lblBalanceTitle.Size = new System.Drawing.Size(107, 19);
            this.lblBalanceTitle.TabIndex = 19;
            this.lblBalanceTitle.Text = "Balance Amount";
            // 
            // lblPaidValue
            // 
            this.lblPaidValue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPaidValue.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblPaidValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblPaidValue.Location = new System.Drawing.Point(180, 405);
            this.lblPaidValue.Name = "lblPaidValue";
            this.lblPaidValue.Size = new System.Drawing.Size(170, 20);
            this.lblPaidValue.TabIndex = 18;
            this.lblPaidValue.Text = "?0.00";
            this.lblPaidValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblPaidTitle
            // 
            this.lblPaidTitle.AutoSize = true;
            this.lblPaidTitle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblPaidTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblPaidTitle.Location = new System.Drawing.Point(18, 405);
            this.lblPaidTitle.Name = "lblPaidTitle";
            this.lblPaidTitle.Size = new System.Drawing.Size(89, 19);
            this.lblPaidTitle.TabIndex = 17;
            this.lblPaidTitle.Text = "Paid Amount";
            // 
            // lblGrandTotalValue
            // 
            this.lblGrandTotalValue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblGrandTotalValue.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblGrandTotalValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(76)))), ((int)(((byte)(120)))));
            this.lblGrandTotalValue.Location = new System.Drawing.Point(180, 355);
            this.lblGrandTotalValue.Name = "lblGrandTotalValue";
            this.lblGrandTotalValue.Size = new System.Drawing.Size(170, 30);
            this.lblGrandTotalValue.TabIndex = 16;
            this.lblGrandTotalValue.Text = "?0.00";
            this.lblGrandTotalValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblGrandTotalTitle
            // 
            this.lblGrandTotalTitle.AutoSize = true;
            this.lblGrandTotalTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblGrandTotalTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblGrandTotalTitle.Location = new System.Drawing.Point(18, 360);
            this.lblGrandTotalTitle.Name = "lblGrandTotalTitle";
            this.lblGrandTotalTitle.Size = new System.Drawing.Size(98, 21);
            this.lblGrandTotalTitle.TabIndex = 15;
            this.lblGrandTotalTitle.Text = "Grand Total";
            // 
            // panelDivider2
            // 
            this.panelDivider2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelDivider2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.panelDivider2.Location = new System.Drawing.Point(18, 340);
            this.panelDivider2.Name = "panelDivider2";
            this.panelDivider2.Size = new System.Drawing.Size(332, 1);
            this.panelDivider2.TabIndex = 14;
            // 
            // lblPreviousOutstandingValue
            // 
            this.lblPreviousOutstandingValue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPreviousOutstandingValue.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblPreviousOutstandingValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(83)))), ((int)(((byte)(9)))));
            this.lblPreviousOutstandingValue.Location = new System.Drawing.Point(180, 295);
            this.lblPreviousOutstandingValue.Name = "lblPreviousOutstandingValue";
            this.lblPreviousOutstandingValue.Size = new System.Drawing.Size(170, 20);
            this.lblPreviousOutstandingValue.TabIndex = 13;
            this.lblPreviousOutstandingValue.Text = "?0.00";
            this.lblPreviousOutstandingValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblPreviousOutstandingTitle
            // 
            this.lblPreviousOutstandingTitle.AutoSize = true;
            this.lblPreviousOutstandingTitle.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblPreviousOutstandingTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblPreviousOutstandingTitle.Location = new System.Drawing.Point(18, 295);
            this.lblPreviousOutstandingTitle.Name = "lblPreviousOutstandingTitle";
            this.lblPreviousOutstandingTitle.Size = new System.Drawing.Size(133, 17);
            this.lblPreviousOutstandingTitle.TabIndex = 12;
            this.lblPreviousOutstandingTitle.Text = "Previous Outstanding";
            // 
            // lblCurrentChargesValue
            // 
            this.lblCurrentChargesValue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCurrentChargesValue.Font = new System.Drawing.Font("Segoe UI Semibold", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblCurrentChargesValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblCurrentChargesValue.Location = new System.Drawing.Point(180, 255);
            this.lblCurrentChargesValue.Name = "lblCurrentChargesValue";
            this.lblCurrentChargesValue.Size = new System.Drawing.Size(170, 20);
            this.lblCurrentChargesValue.TabIndex = 11;
            this.lblCurrentChargesValue.Text = "?0.00";
            this.lblCurrentChargesValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblCurrentChargesTitle
            // 
            this.lblCurrentChargesTitle.AutoSize = true;
            this.lblCurrentChargesTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblCurrentChargesTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblCurrentChargesTitle.Location = new System.Drawing.Point(18, 255);
            this.lblCurrentChargesTitle.Name = "lblCurrentChargesTitle";
            this.lblCurrentChargesTitle.Size = new System.Drawing.Size(104, 17);
            this.lblCurrentChargesTitle.TabIndex = 10;
            this.lblCurrentChargesTitle.Text = "Current Charges";
            // 
            // lblTaxValue
            // 
            this.lblTaxValue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTaxValue.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblTaxValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblTaxValue.Location = new System.Drawing.Point(180, 215);
            this.lblTaxValue.Name = "lblTaxValue";
            this.lblTaxValue.Size = new System.Drawing.Size(170, 20);
            this.lblTaxValue.TabIndex = 9;
            this.lblTaxValue.Text = "?0.00";
            this.lblTaxValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblTaxTitle
            // 
            this.lblTaxTitle.AutoSize = true;
            this.lblTaxTitle.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblTaxTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblTaxTitle.Location = new System.Drawing.Point(18, 215);
            this.lblTaxTitle.Name = "lblTaxTitle";
            this.lblTaxTitle.Size = new System.Drawing.Size(59, 17);
            this.lblTaxTitle.TabIndex = 8;
            this.lblTaxTitle.Text = "Tax (GST)";
            // 
            // lblAdditionalChargesValue
            // 
            this.lblAdditionalChargesValue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblAdditionalChargesValue.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblAdditionalChargesValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblAdditionalChargesValue.Location = new System.Drawing.Point(180, 175);
            this.lblAdditionalChargesValue.Name = "lblAdditionalChargesValue";
            this.lblAdditionalChargesValue.Size = new System.Drawing.Size(170, 20);
            this.lblAdditionalChargesValue.TabIndex = 7;
            this.lblAdditionalChargesValue.Text = "?0.00";
            this.lblAdditionalChargesValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblAdditionalChargesTitle
            // 
            this.lblAdditionalChargesTitle.AutoSize = true;
            this.lblAdditionalChargesTitle.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblAdditionalChargesTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblAdditionalChargesTitle.Location = new System.Drawing.Point(18, 175);
            this.lblAdditionalChargesTitle.Name = "lblAdditionalChargesTitle";
            this.lblAdditionalChargesTitle.Size = new System.Drawing.Size(120, 17);
            this.lblAdditionalChargesTitle.TabIndex = 6;
            this.lblAdditionalChargesTitle.Text = "Additional Charges";
            // 
            // lblDiscountSummaryValue
            // 
            this.lblDiscountSummaryValue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDiscountSummaryValue.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblDiscountSummaryValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this.lblDiscountSummaryValue.Location = new System.Drawing.Point(180, 135);
            this.lblDiscountSummaryValue.Name = "lblDiscountSummaryValue";
            this.lblDiscountSummaryValue.Size = new System.Drawing.Size(170, 20);
            this.lblDiscountSummaryValue.TabIndex = 5;
            this.lblDiscountSummaryValue.Text = "-?0.00";
            this.lblDiscountSummaryValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblDiscountSummaryTitle
            // 
            this.lblDiscountSummaryTitle.AutoSize = true;
            this.lblDiscountSummaryTitle.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblDiscountSummaryTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblDiscountSummaryTitle.Location = new System.Drawing.Point(18, 135);
            this.lblDiscountSummaryTitle.Name = "lblDiscountSummaryTitle";
            this.lblDiscountSummaryTitle.Size = new System.Drawing.Size(58, 17);
            this.lblDiscountSummaryTitle.TabIndex = 4;
            this.lblDiscountSummaryTitle.Text = "Discount";
            // 
            // lblSubTotalValue
            // 
            this.lblSubTotalValue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSubTotalValue.Font = new System.Drawing.Font("Segoe UI Semibold", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblSubTotalValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblSubTotalValue.Location = new System.Drawing.Point(180, 95);
            this.lblSubTotalValue.Name = "lblSubTotalValue";
            this.lblSubTotalValue.Size = new System.Drawing.Size(170, 20);
            this.lblSubTotalValue.TabIndex = 3;
            this.lblSubTotalValue.Text = "?0.00";
            this.lblSubTotalValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblSubTotalTitle
            // 
            this.lblSubTotalTitle.AutoSize = true;
            this.lblSubTotalTitle.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblSubTotalTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblSubTotalTitle.Location = new System.Drawing.Point(18, 95);
            this.lblSubTotalTitle.Name = "lblSubTotalTitle";
            this.lblSubTotalTitle.Size = new System.Drawing.Size(57, 17);
            this.lblSubTotalTitle.TabIndex = 2;
            this.lblSubTotalTitle.Text = "SubTotal";
            // 
            // panelDivider1
            // 
            this.panelDivider1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelDivider1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.panelDivider1.Location = new System.Drawing.Point(18, 70);
            this.panelDivider1.Name = "panelDivider1";
            this.panelDivider1.Size = new System.Drawing.Size(332, 1);
            this.panelDivider1.TabIndex = 1;
            // 
            // lblSummaryHeading
            // 
            this.lblSummaryHeading.AutoSize = true;
            this.lblSummaryHeading.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblSummaryHeading.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblSummaryHeading.Location = new System.Drawing.Point(14, 18);
            this.lblSummaryHeading.Name = "lblSummaryHeading";
            this.lblSummaryHeading.Size = new System.Drawing.Size(142, 21);
            this.lblSummaryHeading.TabIndex = 0;
            this.lblSummaryHeading.Text = "Billing Summary";
            // 
            // panelLeft
            // 
            this.panelLeft.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelLeft.Controls.Add(this.grpAdditionalCharges);
            this.panelLeft.Controls.Add(this.grpInvoiceSettings);
            this.panelLeft.Controls.Add(this.grpMembership);
            this.panelLeft.Controls.Add(this.grpStudentSelection);
            this.panelLeft.Location = new System.Drawing.Point(16, 16);
            this.panelLeft.Name = "panelLeft";
            this.panelLeft.Size = new System.Drawing.Size(766, 659);
            this.panelLeft.TabIndex = 0;
            // 
            // grpAdditionalCharges
            // 
            this.grpAdditionalCharges.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpAdditionalCharges.BackColor = System.Drawing.Color.White;
            this.grpAdditionalCharges.Controls.Add(this.btnRemoveItem);
            this.grpAdditionalCharges.Controls.Add(this.btnAddCharge);
            this.grpAdditionalCharges.Controls.Add(this.numChargeRate);
            this.grpAdditionalCharges.Controls.Add(this.lblChargeRate);
            this.grpAdditionalCharges.Controls.Add(this.numChargeQty);
            this.grpAdditionalCharges.Controls.Add(this.lblChargeQty);
            this.grpAdditionalCharges.Controls.Add(this.cmbChargeType);
            this.grpAdditionalCharges.Controls.Add(this.lblChargeType);
            this.grpAdditionalCharges.Controls.Add(this.dgvInvoiceItems);
            this.grpAdditionalCharges.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.grpAdditionalCharges.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.grpAdditionalCharges.Location = new System.Drawing.Point(0, 395);
            this.grpAdditionalCharges.Name = "grpAdditionalCharges";
            this.grpAdditionalCharges.Padding = new System.Windows.Forms.Padding(14);
            this.grpAdditionalCharges.Size = new System.Drawing.Size(766, 264);
            this.grpAdditionalCharges.TabIndex = 3;
            this.grpAdditionalCharges.TabStop = false;
            this.grpAdditionalCharges.Text = "Billed Line Items & Optional Additional Charges";
            // 
            // btnRemoveItem
            // 
            this.btnRemoveItem.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRemoveItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.btnRemoveItem.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(165)))), ((int)(((byte)(165)))));
            this.btnRemoveItem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRemoveItem.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnRemoveItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(185)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.btnRemoveItem.Location = new System.Drawing.Point(620, 226);
            this.btnRemoveItem.Name = "btnRemoveItem";
            this.btnRemoveItem.Size = new System.Drawing.Size(130, 28);
            this.btnRemoveItem.TabIndex = 8;
            this.btnRemoveItem.Text = "Remove Item";
            this.btnRemoveItem.UseVisualStyleBackColor = false;
            this.btnRemoveItem.Click += new System.EventHandler(this.btnRemoveItem_Click);
            // 
            // btnAddCharge
            // 
            this.btnAddCharge.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(76)))), ((int)(((byte)(120)))));
            this.btnAddCharge.FlatAppearance.BorderSize = 0;
            this.btnAddCharge.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddCharge.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnAddCharge.ForeColor = System.Drawing.Color.White;
            this.btnAddCharge.Location = new System.Drawing.Point(490, 42);
            this.btnAddCharge.Name = "btnAddCharge";
            this.btnAddCharge.Size = new System.Drawing.Size(100, 26);
            this.btnAddCharge.TabIndex = 7;
            this.btnAddCharge.Text = "+ Add Item";
            this.btnAddCharge.UseVisualStyleBackColor = false;
            this.btnAddCharge.Click += new System.EventHandler(this.btnAddCharge_Click);
            // 
            // numChargeRate
            // 
            this.numChargeRate.DecimalPlaces = 2;
            this.numChargeRate.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.numChargeRate.Location = new System.Drawing.Point(365, 43);
            this.numChargeRate.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.numChargeRate.Name = "numChargeRate";
            this.numChargeRate.Size = new System.Drawing.Size(110, 24);
            this.numChargeRate.TabIndex = 6;
            // 
            // lblChargeRate
            // 
            this.lblChargeRate.AutoSize = true;
            this.lblChargeRate.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblChargeRate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblChargeRate.Location = new System.Drawing.Point(365, 25);
            this.lblChargeRate.Name = "lblChargeRate";
            this.lblChargeRate.Size = new System.Drawing.Size(50, 15);
            this.lblChargeRate.TabIndex = 5;
            this.lblChargeRate.Text = "Rate (?)";
            // 
            // numChargeQty
            // 
            this.numChargeQty.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.numChargeQty.Location = new System.Drawing.Point(280, 43);
            this.numChargeQty.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numChargeQty.Name = "numChargeQty";
            this.numChargeQty.Size = new System.Drawing.Size(70, 24);
            this.numChargeQty.TabIndex = 4;
            this.numChargeQty.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // lblChargeQty
            // 
            this.lblChargeQty.AutoSize = true;
            this.lblChargeQty.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblChargeQty.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblChargeQty.Location = new System.Drawing.Point(280, 25);
            this.lblChargeQty.Name = "lblChargeQty";
            this.lblChargeQty.Size = new System.Drawing.Size(26, 15);
            this.lblChargeQty.TabIndex = 3;
            this.lblChargeQty.Text = "Qty";
            // 
            // cmbChargeType
            // 
            this.cmbChargeType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbChargeType.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.cmbChargeType.FormattingEnabled = true;
            this.cmbChargeType.Items.AddRange(new object[] {
            "Registration Fee",
            "Locker Fee",
            "Library Card Fee",
            "Late Return Fine",
            "Other Library Charge"});
            this.cmbChargeType.Location = new System.Drawing.Point(20, 43);
            this.cmbChargeType.Name = "cmbChargeType";
            this.cmbChargeType.Size = new System.Drawing.Size(245, 24);
            this.cmbChargeType.TabIndex = 2;
            this.cmbChargeType.SelectedIndexChanged += new System.EventHandler(this.cmbChargeType_SelectedIndexChanged);
            // 
            // lblChargeType
            // 
            this.lblChargeType.AutoSize = true;
            this.lblChargeType.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblChargeType.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblChargeType.Location = new System.Drawing.Point(20, 25);
            this.lblChargeType.Name = "lblChargeType";
            this.lblChargeType.Size = new System.Drawing.Size(124, 15);
            this.lblChargeType.TabIndex = 1;
            this.lblChargeType.Text = "Additional Charge Item";
            // 
            // dgvInvoiceItems
            // 
            this.dgvInvoiceItems.AllowUserToAddRows = false;
            this.dgvInvoiceItems.AllowUserToDeleteRows = false;
            this.dgvInvoiceItems.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvInvoiceItems.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvInvoiceItems.BackgroundColor = System.Drawing.Color.White;
            this.dgvInvoiceItems.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvInvoiceItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvInvoiceItems.Location = new System.Drawing.Point(20, 78);
            this.dgvInvoiceItems.MultiSelect = false;
            this.dgvInvoiceItems.Name = "dgvInvoiceItems";
            this.dgvInvoiceItems.ReadOnly = true;
            this.dgvInvoiceItems.RowHeadersVisible = false;
            this.dgvInvoiceItems.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvInvoiceItems.Size = new System.Drawing.Size(730, 142);
            this.dgvInvoiceItems.TabIndex = 0;
            // 
            // grpInvoiceSettings
            // 
            this.grpInvoiceSettings.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpInvoiceSettings.BackColor = System.Drawing.Color.White;
            this.grpInvoiceSettings.Controls.Add(this.numDiscount);
            this.grpInvoiceSettings.Controls.Add(this.lblDiscount);
            this.grpInvoiceSettings.Controls.Add(this.dtpDueDate);
            this.grpInvoiceSettings.Controls.Add(this.lblDueDate);
            this.grpInvoiceSettings.Controls.Add(this.dtpInvoiceDate);
            this.grpInvoiceSettings.Controls.Add(this.lblInvoiceDate);
            this.grpInvoiceSettings.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.grpInvoiceSettings.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.grpInvoiceSettings.Location = new System.Drawing.Point(0, 310);
            this.grpInvoiceSettings.Name = "grpInvoiceSettings";
            this.grpInvoiceSettings.Padding = new System.Windows.Forms.Padding(14);
            this.grpInvoiceSettings.Size = new System.Drawing.Size(766, 75);
            this.grpInvoiceSettings.TabIndex = 2;
            this.grpInvoiceSettings.TabStop = false;
            this.grpInvoiceSettings.Text = "Invoice Dates & Discount Adjustment";
            // 
            // numDiscount
            // 
            this.numDiscount.DecimalPlaces = 2;
            this.numDiscount.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.numDiscount.Location = new System.Drawing.Point(535, 36);
            this.numDiscount.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.numDiscount.Name = "numDiscount";
            this.numDiscount.Size = new System.Drawing.Size(140, 24);
            this.numDiscount.TabIndex = 5;
            this.numDiscount.ValueChanged += new System.EventHandler(this.numDiscount_ValueChanged);
            // 
            // lblDiscount
            // 
            this.lblDiscount.AutoSize = true;
            this.lblDiscount.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblDiscount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblDiscount.Location = new System.Drawing.Point(535, 18);
            this.lblDiscount.Name = "lblDiscount";
            this.lblDiscount.Size = new System.Drawing.Size(127, 15);
            this.lblDiscount.TabIndex = 4;
            this.lblDiscount.Text = "Discount Amount (?)";
            // 
            // dtpDueDate
            // 
            this.dtpDueDate.CustomFormat = "dd-MMM-yyyy";
            this.dtpDueDate.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.dtpDueDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDueDate.Location = new System.Drawing.Point(280, 36);
            this.dtpDueDate.Name = "dtpDueDate";
            this.dtpDueDate.Size = new System.Drawing.Size(170, 24);
            this.dtpDueDate.TabIndex = 3;
            // 
            // lblDueDate
            // 
            this.lblDueDate.AutoSize = true;
            this.lblDueDate.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblDueDate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblDueDate.Location = new System.Drawing.Point(280, 18);
            this.lblDueDate.Name = "lblDueDate";
            this.lblDueDate.Size = new System.Drawing.Size(56, 15);
            this.lblDueDate.TabIndex = 2;
            this.lblDueDate.Text = "Due Date";
            // 
            // dtpInvoiceDate
            // 
            this.dtpInvoiceDate.CustomFormat = "dd-MMM-yyyy";
            this.dtpInvoiceDate.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.dtpInvoiceDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpInvoiceDate.Location = new System.Drawing.Point(20, 36);
            this.dtpInvoiceDate.Name = "dtpInvoiceDate";
            this.dtpInvoiceDate.Size = new System.Drawing.Size(170, 24);
            this.dtpInvoiceDate.TabIndex = 1;
            this.dtpInvoiceDate.ValueChanged += new System.EventHandler(this.dtpInvoiceDate_ValueChanged);
            // 
            // lblInvoiceDate
            // 
            this.lblInvoiceDate.AutoSize = true;
            this.lblInvoiceDate.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblInvoiceDate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblInvoiceDate.Location = new System.Drawing.Point(20, 18);
            this.lblInvoiceDate.Name = "lblInvoiceDate";
            this.lblInvoiceDate.Size = new System.Drawing.Size(73, 15);
            this.lblInvoiceDate.TabIndex = 0;
            this.lblInvoiceDate.Text = "Invoice Date";
            // 
            // grpMembership
            // 
            this.grpMembership.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpMembership.BackColor = System.Drawing.Color.White;
            this.grpMembership.Controls.Add(this.lblNoMembershipAlert);
            this.grpMembership.Controls.Add(this.lblFinalFeeValue);
            this.grpMembership.Controls.Add(this.lblFinalFeeTitle);
            this.grpMembership.Controls.Add(this.lblDiscountValue);
            this.grpMembership.Controls.Add(this.lblDiscountTitle);
            this.grpMembership.Controls.Add(this.lblActualFeeValue);
            this.grpMembership.Controls.Add(this.lblActualFeeTitle);
            this.grpMembership.Controls.Add(this.lblEndDateValue);
            this.grpMembership.Controls.Add(this.lblEndDateTitle);
            this.grpMembership.Controls.Add(this.lblStartDateValue);
            this.grpMembership.Controls.Add(this.lblStartDateTitle);
            this.grpMembership.Controls.Add(this.lblSeatValue);
            this.grpMembership.Controls.Add(this.lblSeatTitle);
            this.grpMembership.Controls.Add(this.lblPlanValue);
            this.grpMembership.Controls.Add(this.lblPlanTitle);
            this.grpMembership.Controls.Add(this.lblMembershipIdValue);
            this.grpMembership.Controls.Add(this.lblMembershipIdTitle);
            this.grpMembership.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.grpMembership.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.grpMembership.Location = new System.Drawing.Point(0, 115);
            this.grpMembership.Name = "grpMembership";
            this.grpMembership.Padding = new System.Windows.Forms.Padding(14);
            this.grpMembership.Size = new System.Drawing.Size(766, 185);
            this.grpMembership.TabIndex = 1;
            this.grpMembership.TabStop = false;
            this.grpMembership.Text = "Active Student Membership (Billing Basis)";
            // 
            // lblNoMembershipAlert
            // 
            this.lblNoMembershipAlert.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.lblNoMembershipAlert.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblNoMembershipAlert.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblNoMembershipAlert.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(185)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.lblNoMembershipAlert.Location = new System.Drawing.Point(20, 30);
            this.lblNoMembershipAlert.Name = "lblNoMembershipAlert";
            this.lblNoMembershipAlert.Padding = new System.Windows.Forms.Padding(12);
            this.lblNoMembershipAlert.Size = new System.Drawing.Size(720, 135);
            this.lblNoMembershipAlert.TabIndex = 16;
            this.lblNoMembershipAlert.Text = "No active membership found for this student. Normal membership billing requires a" +
    "n active membership.";
            this.lblNoMembershipAlert.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblNoMembershipAlert.Visible = false;
            // 
            // lblFinalFeeValue
            // 
            this.lblFinalFeeValue.AutoSize = true;
            this.lblFinalFeeValue.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblFinalFeeValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(185)))), ((int)(((byte)(129)))));
            this.lblFinalFeeValue.Location = new System.Drawing.Point(540, 142);
            this.lblFinalFeeValue.Name = "lblFinalFeeValue";
            this.lblFinalFeeValue.Size = new System.Drawing.Size(43, 19);
            this.lblFinalFeeValue.TabIndex = 15;
            this.lblFinalFeeValue.Text = "?0.00";
            // 
            // lblFinalFeeTitle
            // 
            this.lblFinalFeeTitle.AutoSize = true;
            this.lblFinalFeeTitle.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblFinalFeeTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblFinalFeeTitle.Location = new System.Drawing.Point(540, 122);
            this.lblFinalFeeTitle.Name = "lblFinalFeeTitle";
            this.lblFinalFeeTitle.Size = new System.Drawing.Size(54, 15);
            this.lblFinalFeeTitle.TabIndex = 14;
            this.lblFinalFeeTitle.Text = "Final Fee";
            // 
            // lblDiscountValue
            // 
            this.lblDiscountValue.AutoSize = true;
            this.lblDiscountValue.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblDiscountValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this.lblDiscountValue.Location = new System.Drawing.Point(280, 142);
            this.lblDiscountValue.Name = "lblDiscountValue";
            this.lblDiscountValue.Size = new System.Drawing.Size(43, 19);
            this.lblDiscountValue.TabIndex = 13;
            this.lblDiscountValue.Text = "?0.00";
            // 
            // lblDiscountTitle
            // 
            this.lblDiscountTitle.AutoSize = true;
            this.lblDiscountTitle.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblDiscountTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblDiscountTitle.Location = new System.Drawing.Point(280, 122);
            this.lblDiscountTitle.Name = "lblDiscountTitle";
            this.lblDiscountTitle.Size = new System.Drawing.Size(83, 15);
            this.lblDiscountTitle.TabIndex = 12;
            this.lblDiscountTitle.Text = "Plan Discount";
            // 
            // lblActualFeeValue
            // 
            this.lblActualFeeValue.AutoSize = true;
            this.lblActualFeeValue.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblActualFeeValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblActualFeeValue.Location = new System.Drawing.Point(20, 142);
            this.lblActualFeeValue.Name = "lblActualFeeValue";
            this.lblActualFeeValue.Size = new System.Drawing.Size(43, 19);
            this.lblActualFeeValue.TabIndex = 11;
            this.lblActualFeeValue.Text = "?0.00";
            // 
            // lblActualFeeTitle
            // 
            this.lblActualFeeTitle.AutoSize = true;
            this.lblActualFeeTitle.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblActualFeeTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblActualFeeTitle.Location = new System.Drawing.Point(20, 122);
            this.lblActualFeeTitle.Name = "lblActualFeeTitle";
            this.lblActualFeeTitle.Size = new System.Drawing.Size(95, 15);
            this.lblActualFeeTitle.TabIndex = 10;
            this.lblActualFeeTitle.Text = "Membership Fee";
            // 
            // lblEndDateValue
            // 
            this.lblEndDateValue.AutoSize = true;
            this.lblEndDateValue.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblEndDateValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblEndDateValue.Location = new System.Drawing.Point(540, 92);
            this.lblEndDateValue.Name = "lblEndDateValue";
            this.lblEndDateValue.Size = new System.Drawing.Size(15, 19);
            this.lblEndDateValue.TabIndex = 9;
            this.lblEndDateValue.Text = "-";
            // 
            // lblEndDateTitle
            // 
            this.lblEndDateTitle.AutoSize = true;
            this.lblEndDateTitle.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblEndDateTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblEndDateTitle.Location = new System.Drawing.Point(540, 75);
            this.lblEndDateTitle.Name = "lblEndDateTitle";
            this.lblEndDateTitle.Size = new System.Drawing.Size(56, 15);
            this.lblEndDateTitle.TabIndex = 8;
            this.lblEndDateTitle.Text = "End Date";
            // 
            // lblStartDateValue
            // 
            this.lblStartDateValue.AutoSize = true;
            this.lblStartDateValue.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblStartDateValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblStartDateValue.Location = new System.Drawing.Point(280, 92);
            this.lblStartDateValue.Name = "lblStartDateValue";
            this.lblStartDateValue.Size = new System.Drawing.Size(15, 19);
            this.lblStartDateValue.TabIndex = 7;
            this.lblStartDateValue.Text = "-";
            // 
            // lblStartDateTitle
            // 
            this.lblStartDateTitle.AutoSize = true;
            this.lblStartDateTitle.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblStartDateTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblStartDateTitle.Location = new System.Drawing.Point(280, 75);
            this.lblStartDateTitle.Name = "lblStartDateTitle";
            this.lblStartDateTitle.Size = new System.Drawing.Size(60, 15);
            this.lblStartDateTitle.TabIndex = 6;
            this.lblStartDateTitle.Text = "Start Date";
            // 
            // lblSeatValue
            // 
            this.lblSeatValue.AutoSize = true;
            this.lblSeatValue.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblSeatValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblSeatValue.Location = new System.Drawing.Point(20, 92);
            this.lblSeatValue.Name = "lblSeatValue";
            this.lblSeatValue.Size = new System.Drawing.Size(15, 19);
            this.lblSeatValue.TabIndex = 5;
            this.lblSeatValue.Text = "-";
            // 
            // lblSeatTitle
            // 
            this.lblSeatTitle.AutoSize = true;
            this.lblSeatTitle.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblSeatTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblSeatTitle.Location = new System.Drawing.Point(20, 75);
            this.lblSeatTitle.Name = "lblSeatTitle";
            this.lblSeatTitle.Size = new System.Drawing.Size(84, 15);
            this.lblSeatTitle.TabIndex = 4;
            this.lblSeatTitle.Text = "Assigned Seat";
            // 
            // lblPlanValue
            // 
            this.lblPlanValue.AutoSize = true;
            this.lblPlanValue.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblPlanValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(76)))), ((int)(((byte)(120)))));
            this.lblPlanValue.Location = new System.Drawing.Point(280, 47);
            this.lblPlanValue.Name = "lblPlanValue";
            this.lblPlanValue.Size = new System.Drawing.Size(15, 19);
            this.lblPlanValue.TabIndex = 3;
            this.lblPlanValue.Text = "-";
            // 
            // lblPlanTitle
            // 
            this.lblPlanTitle.AutoSize = true;
            this.lblPlanTitle.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblPlanTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblPlanTitle.Location = new System.Drawing.Point(280, 30);
            this.lblPlanTitle.Name = "lblPlanTitle";
            this.lblPlanTitle.Size = new System.Drawing.Size(103, 15);
            this.lblPlanTitle.TabIndex = 2;
            this.lblPlanTitle.Text = "Membership Plan";
            // 
            // lblMembershipIdValue
            // 
            this.lblMembershipIdValue.AutoSize = true;
            this.lblMembershipIdValue.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblMembershipIdValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblMembershipIdValue.Location = new System.Drawing.Point(20, 47);
            this.lblMembershipIdValue.Name = "lblMembershipIdValue";
            this.lblMembershipIdValue.Size = new System.Drawing.Size(15, 19);
            this.lblMembershipIdValue.TabIndex = 1;
            this.lblMembershipIdValue.Text = "-";
            // 
            // lblMembershipIdTitle
            // 
            this.lblMembershipIdTitle.AutoSize = true;
            this.lblMembershipIdTitle.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblMembershipIdTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblMembershipIdTitle.Location = new System.Drawing.Point(20, 30);
            this.lblMembershipIdTitle.Name = "lblMembershipIdTitle";
            this.lblMembershipIdTitle.Size = new System.Drawing.Size(91, 15);
            this.lblMembershipIdTitle.TabIndex = 0;
            this.lblMembershipIdTitle.Text = "Membership ID";
            // 
            // grpStudentSelection
            // 
            this.grpStudentSelection.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpStudentSelection.BackColor = System.Drawing.Color.White;
            this.grpStudentSelection.Controls.Add(this.lblStudentDetails);
            this.grpStudentSelection.Controls.Add(this.cmbStudents);
            this.grpStudentSelection.Controls.Add(this.lblSelectStudent);
            this.grpStudentSelection.Controls.Add(this.txtSearchStudent);
            this.grpStudentSelection.Controls.Add(this.lblSearchStudent);
            this.grpStudentSelection.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.grpStudentSelection.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.grpStudentSelection.Location = new System.Drawing.Point(0, 0);
            this.grpStudentSelection.Name = "grpStudentSelection";
            this.grpStudentSelection.Padding = new System.Windows.Forms.Padding(14);
            this.grpStudentSelection.Size = new System.Drawing.Size(766, 105);
            this.grpStudentSelection.TabIndex = 0;
            this.grpStudentSelection.TabStop = false;
            this.grpStudentSelection.Text = "Student Selection";
            // 
            // lblStudentDetails
            // 
            this.lblStudentDetails.AutoSize = true;
            this.lblStudentDetails.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblStudentDetails.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(116)))), ((int)(((byte)(144)))));
            this.lblStudentDetails.Location = new System.Drawing.Point(20, 75);
            this.lblStudentDetails.Name = "lblStudentDetails";
            this.lblStudentDetails.Size = new System.Drawing.Size(262, 15);
            this.lblStudentDetails.TabIndex = 4;
            this.lblStudentDetails.Text = "Code: -   |   Name: -   |   Mobile: -   |   Status: -";
            // 
            // cmbStudents
            // 
            this.cmbStudents.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbStudents.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStudents.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.cmbStudents.FormattingEnabled = true;
            this.cmbStudents.Location = new System.Drawing.Point(280, 42);
            this.cmbStudents.Name = "cmbStudents";
            this.cmbStudents.Size = new System.Drawing.Size(460, 24);
            this.cmbStudents.TabIndex = 3;
            this.cmbStudents.SelectedIndexChanged += new System.EventHandler(this.cmbStudents_SelectedIndexChanged);
            // 
            // lblSelectStudent
            // 
            this.lblSelectStudent.AutoSize = true;
            this.lblSelectStudent.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblSelectStudent.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblSelectStudent.Location = new System.Drawing.Point(280, 24);
            this.lblSelectStudent.Name = "lblSelectStudent";
            this.lblSelectStudent.Size = new System.Drawing.Size(125, 15);
            this.lblSelectStudent.TabIndex = 2;
            this.lblSelectStudent.Text = "Choose Active Student";
            // 
            // txtSearchStudent
            // 
            this.txtSearchStudent.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtSearchStudent.Location = new System.Drawing.Point(20, 42);
            this.txtSearchStudent.Name = "txtSearchStudent";
            this.txtSearchStudent.PlaceholderText = "Search by Name or Code...";
            this.txtSearchStudent.Size = new System.Drawing.Size(240, 24);
            this.txtSearchStudent.TabIndex = 1;
            this.txtSearchStudent.TextChanged += new System.EventHandler(this.txtSearchStudent_TextChanged);
            // 
            // lblSearchStudent
            // 
            this.lblSearchStudent.AutoSize = true;
            this.lblSearchStudent.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblSearchStudent.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblSearchStudent.Location = new System.Drawing.Point(20, 24);
            this.lblSearchStudent.Name = "lblSearchStudent";
            this.lblSearchStudent.Size = new System.Drawing.Size(86, 15);
            this.lblSearchStudent.TabIndex = 0;
            this.lblSearchStudent.Text = "Quick Filter / Search";
            // 
            // BillingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 751);
            this.Controls.Add(this.panelContent);
            this.Controls.Add(this.panelHeader);
            this.MinimumSize = new System.Drawing.Size(1050, 650);
            this.Name = "BillingForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Library Management System - Student Billing";
            this.Load += new System.EventHandler(this.BillingForm_Load);
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            this.panelContent.ResumeLayout(false);
            this.panelRight.ResumeLayout(false);
            this.panelSummaryCard.ResumeLayout(false);
            this.panelSummaryCard.PerformLayout();
            this.panelLeft.ResumeLayout(false);
            this.grpAdditionalCharges.ResumeLayout(false);
            this.grpAdditionalCharges.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numChargeRate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numChargeQty)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvInvoiceItems)).EndInit();
            this.grpInvoiceSettings.ResumeLayout(false);
            this.grpInvoiceSettings.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numDiscount)).EndInit();
            this.grpMembership.ResumeLayout(false);
            this.grpMembership.PerformLayout();
            this.grpStudentSelection.ResumeLayout(false);
            this.grpStudentSelection.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Label lblHeaderTitle;
        private System.Windows.Forms.Label lblHeaderSubtitle;
        private System.Windows.Forms.Panel panelContent;
        private System.Windows.Forms.Panel panelLeft;
        private System.Windows.Forms.Panel panelRight;
        private System.Windows.Forms.Panel panelSummaryCard;
        private System.Windows.Forms.Label lblSummaryHeading;
        private System.Windows.Forms.Panel panelDivider1;
        private System.Windows.Forms.Label lblSubTotalTitle;
        private System.Windows.Forms.Label lblSubTotalValue;
        private System.Windows.Forms.Label lblDiscountSummaryTitle;
        private System.Windows.Forms.Label lblDiscountSummaryValue;
        private System.Windows.Forms.Label lblAdditionalChargesTitle;
        private System.Windows.Forms.Label lblAdditionalChargesValue;
        private System.Windows.Forms.Label lblTaxTitle;
        private System.Windows.Forms.Label lblTaxValue;
        private System.Windows.Forms.Label lblCurrentChargesTitle;
        private System.Windows.Forms.Label lblCurrentChargesValue;
        private System.Windows.Forms.Label lblPreviousOutstandingTitle;
        private System.Windows.Forms.Label lblPreviousOutstandingValue;
        private System.Windows.Forms.Panel panelDivider2;
        private System.Windows.Forms.Label lblGrandTotalTitle;
        private System.Windows.Forms.Label lblGrandTotalValue;
        private System.Windows.Forms.Label lblPaidTitle;
        private System.Windows.Forms.Label lblPaidValue;
        private System.Windows.Forms.Label lblBalanceTitle;
        private System.Windows.Forms.Label lblBalanceValue;
        private System.Windows.Forms.Button btnGenerateInvoice;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnHistory;
        private System.Windows.Forms.GroupBox grpStudentSelection;
        private System.Windows.Forms.Label lblSearchStudent;
        private System.Windows.Forms.TextBox txtSearchStudent;
        private System.Windows.Forms.Label lblSelectStudent;
        private System.Windows.Forms.ComboBox cmbStudents;
        private System.Windows.Forms.Label lblStudentDetails;
        private System.Windows.Forms.GroupBox grpMembership;
        private System.Windows.Forms.Label lblMembershipIdTitle;
        private System.Windows.Forms.Label lblMembershipIdValue;
        private System.Windows.Forms.Label lblPlanTitle;
        private System.Windows.Forms.Label lblPlanValue;
        private System.Windows.Forms.Label lblSeatTitle;
        private System.Windows.Forms.Label lblSeatValue;
        private System.Windows.Forms.Label lblStartDateTitle;
        private System.Windows.Forms.Label lblStartDateValue;
        private System.Windows.Forms.Label lblEndDateTitle;
        private System.Windows.Forms.Label lblEndDateValue;
        private System.Windows.Forms.Label lblActualFeeTitle;
        private System.Windows.Forms.Label lblActualFeeValue;
        private System.Windows.Forms.Label lblDiscountTitle;
        private System.Windows.Forms.Label lblDiscountValue;
        private System.Windows.Forms.Label lblFinalFeeTitle;
        private System.Windows.Forms.Label lblFinalFeeValue;
        private System.Windows.Forms.Label lblNoMembershipAlert;
        private System.Windows.Forms.GroupBox grpInvoiceSettings;
        private System.Windows.Forms.Label lblInvoiceDate;
        private System.Windows.Forms.DateTimePicker dtpInvoiceDate;
        private System.Windows.Forms.Label lblDueDate;
        private System.Windows.Forms.DateTimePicker dtpDueDate;
        private System.Windows.Forms.Label lblDiscount;
        private System.Windows.Forms.NumericUpDown numDiscount;
        private System.Windows.Forms.GroupBox grpAdditionalCharges;
        private System.Windows.Forms.DataGridView dgvInvoiceItems;
        private System.Windows.Forms.Label lblChargeType;
        private System.Windows.Forms.ComboBox cmbChargeType;
        private System.Windows.Forms.Label lblChargeQty;
        private System.Windows.Forms.NumericUpDown numChargeQty;
        private System.Windows.Forms.Label lblChargeRate;
        private System.Windows.Forms.NumericUpDown numChargeRate;
        private System.Windows.Forms.Button btnAddCharge;
        private System.Windows.Forms.Button btnRemoveItem;
    }
}
