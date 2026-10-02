namespace Laibrary_Payment_App.Forms
{
    partial class MembershipForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panelHeader = new System.Windows.Forms.Panel();
            this.lblHeaderSubtitle = new System.Windows.Forms.Label();
            this.lblHeaderTitle = new System.Windows.Forms.Label();
            this.panelLeft = new System.Windows.Forms.Panel();
            this.lblStatusMessage = new System.Windows.Forms.Label();
            this.btnCancelMembership = new System.Windows.Forms.Button();
            this.btnRenew = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.txtRemarks = new System.Windows.Forms.TextBox();
            this.lblRemarks = new System.Windows.Forms.Label();
            this.txtFinalFee = new System.Windows.Forms.TextBox();
            this.lblFinalFee = new System.Windows.Forms.Label();
            this.txtDiscount = new System.Windows.Forms.TextBox();
            this.lblDiscount = new System.Windows.Forms.Label();
            this.txtPlanFee = new System.Windows.Forms.TextBox();
            this.lblPlanFee = new System.Windows.Forms.Label();
            this.cmbSeat = new System.Windows.Forms.ComboBox();
            this.lblSeat = new System.Windows.Forms.Label();
            this.dtpEndDate = new System.Windows.Forms.DateTimePicker();
            this.lblEndDate = new System.Windows.Forms.Label();
            this.dtpStartDate = new System.Windows.Forms.DateTimePicker();
            this.lblStartDate = new System.Windows.Forms.Label();
            this.cmbPlan = new System.Windows.Forms.ComboBox();
            this.lblPlan = new System.Windows.Forms.Label();
            this.cmbStudent = new System.Windows.Forms.ComboBox();
            this.lblStudent = new System.Windows.Forms.Label();
            this.lblFormSectionTitle = new System.Windows.Forms.Label();
            this.panelRight = new System.Windows.Forms.Panel();
            this.dgvMemberships = new System.Windows.Forms.DataGridView();
            this.panelFilters = new System.Windows.Forms.Panel();
            this.lblRecordCount = new System.Windows.Forms.Label();
            this.btnResetFilters = new System.Windows.Forms.Button();
            this.btnFilter = new System.Windows.Forms.Button();
            this.cmbFilterStatus = new System.Windows.Forms.ComboBox();
            this.lblFilterStatus = new System.Windows.Forms.Label();
            this.cmbFilterPlan = new System.Windows.Forms.ComboBox();
            this.lblFilterPlan = new System.Windows.Forms.Label();
            this.txtFilterSearch = new System.Windows.Forms.TextBox();
            this.lblFilterSearch = new System.Windows.Forms.Label();
            this.panelHeader.SuspendLayout();
            this.panelLeft.SuspendLayout();
            this.panelRight.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMemberships)).BeginInit();
            this.panelFilters.SuspendLayout();
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
            this.panelHeader.Size = new System.Drawing.Size(1134, 75);
            this.panelHeader.TabIndex = 0;
            // 
            // lblHeaderSubtitle
            // 
            this.lblHeaderSubtitle.AutoSize = true;
            this.lblHeaderSubtitle.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblHeaderSubtitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(235)))), ((int)(((byte)(252)))));
            this.lblHeaderSubtitle.Location = new System.Drawing.Point(24, 43);
            this.lblHeaderSubtitle.Name = "lblHeaderSubtitle";
            this.lblHeaderSubtitle.Size = new System.Drawing.Size(460, 17);
            this.lblHeaderSubtitle.TabIndex = 1;
            this.lblHeaderSubtitle.Text = "Assign student subscriptions, reading space seats, renewals, and status tracking";
            // 
            // lblHeaderTitle
            // 
            this.lblHeaderTitle.AutoSize = true;
            this.lblHeaderTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblHeaderTitle.ForeColor = System.Drawing.Color.White;
            this.lblHeaderTitle.Location = new System.Drawing.Point(22, 12);
            this.lblHeaderTitle.Name = "lblHeaderTitle";
            this.lblHeaderTitle.Size = new System.Drawing.Size(490, 30);
            this.lblHeaderTitle.TabIndex = 0;
            this.lblHeaderTitle.Text = "Library Management System - Membership & Seats";
            // 
            // panelLeft
            // 
            this.panelLeft.AutoScroll = true;
            this.panelLeft.BackColor = System.Drawing.Color.White;
            this.panelLeft.Controls.Add(this.lblStatusMessage);
            this.panelLeft.Controls.Add(this.btnCancelMembership);
            this.panelLeft.Controls.Add(this.btnRenew);
            this.panelLeft.Controls.Add(this.btnClear);
            this.panelLeft.Controls.Add(this.btnSave);
            this.panelLeft.Controls.Add(this.txtRemarks);
            this.panelLeft.Controls.Add(this.lblRemarks);
            this.panelLeft.Controls.Add(this.txtFinalFee);
            this.panelLeft.Controls.Add(this.lblFinalFee);
            this.panelLeft.Controls.Add(this.txtDiscount);
            this.panelLeft.Controls.Add(this.lblDiscount);
            this.panelLeft.Controls.Add(this.txtPlanFee);
            this.panelLeft.Controls.Add(this.lblPlanFee);
            this.panelLeft.Controls.Add(this.cmbSeat);
            this.panelLeft.Controls.Add(this.lblSeat);
            this.panelLeft.Controls.Add(this.dtpEndDate);
            this.panelLeft.Controls.Add(this.lblEndDate);
            this.panelLeft.Controls.Add(this.dtpStartDate);
            this.panelLeft.Controls.Add(this.lblStartDate);
            this.panelLeft.Controls.Add(this.cmbPlan);
            this.panelLeft.Controls.Add(this.lblPlan);
            this.panelLeft.Controls.Add(this.cmbStudent);
            this.panelLeft.Controls.Add(this.lblStudent);
            this.panelLeft.Controls.Add(this.lblFormSectionTitle);
            this.panelLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelLeft.Location = new System.Drawing.Point(0, 75);
            this.panelLeft.Name = "panelLeft";
            this.panelLeft.Padding = new System.Windows.Forms.Padding(18);
            this.panelLeft.Size = new System.Drawing.Size(380, 606);
            this.panelLeft.TabIndex = 1;
            // 
            // lblStatusMessage
            // 
            this.lblStatusMessage.Font = new System.Drawing.Font("Segoe UI Semibold", 8.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblStatusMessage.Location = new System.Drawing.Point(18, 545);
            this.lblStatusMessage.Name = "lblStatusMessage";
            this.lblStatusMessage.Size = new System.Drawing.Size(340, 48);
            this.lblStatusMessage.TabIndex = 23;
            this.lblStatusMessage.Visible = false;
            // 
            // btnCancelMembership
            // 
            this.btnCancelMembership.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(198)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.btnCancelMembership.Enabled = false;
            this.btnCancelMembership.FlatAppearance.BorderSize = 0;
            this.btnCancelMembership.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelMembership.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnCancelMembership.ForeColor = System.Drawing.Color.White;
            this.btnCancelMembership.Location = new System.Drawing.Point(190, 502);
            this.btnCancelMembership.Name = "btnCancelMembership";
            this.btnCancelMembership.Size = new System.Drawing.Size(168, 34);
            this.btnCancelMembership.TabIndex = 22;
            this.btnCancelMembership.Text = "Cancel Membership";
            this.btnCancelMembership.UseVisualStyleBackColor = false;
            this.btnCancelMembership.Click += new System.EventHandler(this.btnCancelMembership_Click);
            // 
            // btnRenew
            // 
            this.btnRenew.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(132)))), ((int)(((byte)(199)))));
            this.btnRenew.Enabled = false;
            this.btnRenew.FlatAppearance.BorderSize = 0;
            this.btnRenew.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRenew.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnRenew.ForeColor = System.Drawing.Color.White;
            this.btnRenew.Location = new System.Drawing.Point(18, 502);
            this.btnRenew.Name = "btnRenew";
            this.btnRenew.Size = new System.Drawing.Size(165, 34);
            this.btnRenew.TabIndex = 21;
            this.btnRenew.Text = "Renew Membership";
            this.btnRenew.UseVisualStyleBackColor = false;
            this.btnRenew.Click += new System.EventHandler(this.btnRenew_Click);
            // 
            // btnClear
            // 
            this.btnClear.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.btnClear.FlatAppearance.BorderSize = 0;
            this.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClear.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnClear.ForeColor = System.Drawing.Color.White;
            this.btnClear.Location = new System.Drawing.Point(190, 460);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(168, 34);
            this.btnClear.TabIndex = 20;
            this.btnClear.Text = "Clear / New Entry";
            this.btnClear.UseVisualStyleBackColor = false;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(76)))), ((int)(((byte)(120)))));
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(18, 460);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(165, 34);
            this.btnSave.TabIndex = 19;
            this.btnSave.Text = "Assign Membership";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // txtRemarks
            // 
            this.txtRemarks.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtRemarks.Location = new System.Drawing.Point(18, 420);
            this.txtRemarks.MaxLength = 500;
            this.txtRemarks.Name = "txtRemarks";
            this.txtRemarks.PlaceholderText = "Optional notes or cancellation reason";
            this.txtRemarks.Size = new System.Drawing.Size(340, 23);
            this.txtRemarks.TabIndex = 18;
            // 
            // lblRemarks
            // 
            this.lblRemarks.AutoSize = true;
            this.lblRemarks.Font = new System.Drawing.Font("Segoe UI Semibold", 8.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblRemarks.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.lblRemarks.Location = new System.Drawing.Point(15, 403);
            this.lblRemarks.Name = "lblRemarks";
            this.lblRemarks.Size = new System.Drawing.Size(51, 15);
            this.lblRemarks.TabIndex = 17;
            this.lblRemarks.Text = "Remarks";
            // 
            // txtFinalFee
            // 
            this.txtFinalFee.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(253)))), ((int)(((byte)(244)))));
            this.txtFinalFee.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblFinalFee.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(101)))), ((int)(((byte)(52)))));
            this.txtFinalFee.Location = new System.Drawing.Point(235, 365);
            this.txtFinalFee.Name = "txtFinalFee";
            this.txtFinalFee.ReadOnly = true;
            this.txtFinalFee.Size = new System.Drawing.Size(123, 24);
            this.txtFinalFee.TabIndex = 16;
            this.txtFinalFee.Text = "₹0.00";
            // 
            // lblFinalFee
            // 
            this.lblFinalFee.AutoSize = true;
            this.lblFinalFee.Font = new System.Drawing.Font("Segoe UI Semibold", 8.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblFinalFee.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(101)))), ((int)(((byte)(52)))));
            this.lblFinalFee.Location = new System.Drawing.Point(232, 348);
            this.lblFinalFee.Name = "lblFinalFee";
            this.lblFinalFee.Size = new System.Drawing.Size(53, 15);
            this.lblFinalFee.TabIndex = 15;
            this.lblFinalFee.Text = "Final Fee";
            // 
            // txtDiscount
            // 
            this.txtDiscount.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtDiscount.Location = new System.Drawing.Point(125, 365);
            this.txtDiscount.Name = "txtDiscount";
            this.txtDiscount.Size = new System.Drawing.Size(100, 24);
            this.txtDiscount.TabIndex = 14;
            this.txtDiscount.Text = "0.00";
            this.txtDiscount.TextChanged += new System.EventHandler(this.txtDiscount_TextChanged);
            // 
            // lblDiscount
            // 
            this.lblDiscount.AutoSize = true;
            this.lblDiscount.Font = new System.Drawing.Font("Segoe UI Semibold", 8.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblDiscount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.lblDiscount.Location = new System.Drawing.Point(122, 348);
            this.lblDiscount.Name = "lblDiscount";
            this.lblDiscount.Size = new System.Drawing.Size(73, 15);
            this.lblDiscount.TabIndex = 13;
            this.lblDiscount.Text = "Discount (₹)";
            // 
            // txtPlanFee
            // 
            this.txtPlanFee.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.txtPlanFee.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.txtPlanFee.Location = new System.Drawing.Point(18, 365);
            this.txtPlanFee.Name = "txtPlanFee";
            this.txtPlanFee.ReadOnly = true;
            this.txtPlanFee.Size = new System.Drawing.Size(98, 24);
            this.txtPlanFee.TabIndex = 12;
            this.txtPlanFee.Text = "₹0.00";
            // 
            // lblPlanFee
            // 
            this.lblPlanFee.AutoSize = true;
            this.lblPlanFee.Font = new System.Drawing.Font("Segoe UI Semibold", 8.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblPlanFee.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.lblPlanFee.Location = new System.Drawing.Point(15, 348);
            this.lblPlanFee.Name = "lblPlanFee";
            this.lblPlanFee.Size = new System.Drawing.Size(52, 15);
            this.lblPlanFee.TabIndex = 11;
            this.lblPlanFee.Text = "Plan Fee";
            // 
            // cmbSeat
            // 
            this.cmbSeat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSeat.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.cmbSeat.FormattingEnabled = true;
            this.cmbSeat.Location = new System.Drawing.Point(18, 305);
            this.cmbSeat.Name = "cmbSeat";
            this.cmbSeat.Size = new System.Drawing.Size(340, 24);
            this.cmbSeat.TabIndex = 10;
            // 
            // lblSeat
            // 
            this.lblSeat.AutoSize = true;
            this.lblSeat.Font = new System.Drawing.Font("Segoe UI Semibold", 8.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblSeat.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.lblSeat.Location = new System.Drawing.Point(15, 287);
            this.lblSeat.Name = "lblSeat";
            this.lblSeat.Size = new System.Drawing.Size(128, 15);
            this.lblSeat.TabIndex = 9;
            this.lblSeat.Text = "Library Seat Assignment";
            // 
            // dtpEndDate
            // 
            this.dtpEndDate.CustomFormat = "dd-MMM-yyyy";
            this.dtpEndDate.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.dtpEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpEndDate.Location = new System.Drawing.Point(190, 245);
            this.dtpEndDate.Name = "dtpEndDate";
            this.dtpEndDate.Size = new System.Drawing.Size(168, 24);
            this.dtpEndDate.TabIndex = 8;
            this.dtpEndDate.ValueChanged += new System.EventHandler(this.dtpEndDate_ValueChanged);
            // 
            // lblEndDate
            // 
            this.lblEndDate.AutoSize = true;
            this.lblEndDate.Font = new System.Drawing.Font("Segoe UI Semibold", 8.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblEndDate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.lblEndDate.Location = new System.Drawing.Point(187, 227);
            this.lblEndDate.Name = "lblEndDate";
            this.lblEndDate.Size = new System.Drawing.Size(126, 15);
            this.lblEndDate.TabIndex = 7;
            this.lblEndDate.Text = "End Date (Calculated)*";
            // 
            // dtpStartDate
            // 
            this.dtpStartDate.CustomFormat = "dd-MMM-yyyy";
            this.dtpStartDate.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.dtpStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpStartDate.Location = new System.Drawing.Point(18, 245);
            this.dtpStartDate.Name = "dtpStartDate";
            this.dtpStartDate.Size = new System.Drawing.Size(160, 24);
            this.dtpStartDate.TabIndex = 6;
            this.dtpStartDate.ValueChanged += new System.EventHandler(this.dtpStartDate_ValueChanged);
            // 
            // lblStartDate
            // 
            this.lblStartDate.AutoSize = true;
            this.lblStartDate.Font = new System.Drawing.Font("Segoe UI Semibold", 8.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblStartDate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.lblStartDate.Location = new System.Drawing.Point(15, 227);
            this.lblStartDate.Name = "lblStartDate";
            this.lblStartDate.Size = new System.Drawing.Size(65, 15);
            this.lblStartDate.TabIndex = 5;
            this.lblStartDate.Text = "Start Date*";
            // 
            // cmbPlan
            // 
            this.cmbPlan.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPlan.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.cmbPlan.FormattingEnabled = true;
            this.cmbPlan.Location = new System.Drawing.Point(18, 185);
            this.cmbPlan.Name = "cmbPlan";
            this.cmbPlan.Size = new System.Drawing.Size(340, 24);
            this.cmbPlan.TabIndex = 4;
            this.cmbPlan.SelectedIndexChanged += new System.EventHandler(this.cmbPlan_SelectedIndexChanged);
            // 
            // lblPlan
            // 
            this.lblPlan.AutoSize = true;
            this.lblPlan.Font = new System.Drawing.Font("Segoe UI Semibold", 8.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblPlan.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.lblPlan.Location = new System.Drawing.Point(15, 167);
            this.lblPlan.Name = "lblPlan";
            this.lblPlan.Size = new System.Drawing.Size(104, 15);
            this.lblPlan.TabIndex = 3;
            this.lblPlan.Text = "Membership Plan*";
            // 
            // cmbStudent
            // 
            this.cmbStudent.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStudent.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.cmbStudent.FormattingEnabled = true;
            this.cmbStudent.Location = new System.Drawing.Point(18, 125);
            this.cmbStudent.Name = "cmbStudent";
            this.cmbStudent.Size = new System.Drawing.Size(340, 24);
            this.cmbStudent.TabIndex = 2;
            this.cmbStudent.SelectedIndexChanged += new System.EventHandler(this.cmbStudent_SelectedIndexChanged);
            // 
            // lblStudent
            // 
            this.lblStudent.AutoSize = true;
            this.lblStudent.Font = new System.Drawing.Font("Segoe UI Semibold", 8.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblStudent.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.lblStudent.Location = new System.Drawing.Point(15, 107);
            this.lblStudent.Name = "lblStudent";
            this.lblStudent.Size = new System.Drawing.Size(142, 15);
            this.lblStudent.TabIndex = 1;
            this.lblStudent.Text = "Select Student (Active)*";
            // 
            // lblFormSectionTitle
            // 
            this.lblFormSectionTitle.AutoSize = true;
            this.lblFormSectionTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblFormSectionTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblFormSectionTitle.Location = new System.Drawing.Point(14, 16);
            this.lblFormSectionTitle.Name = "lblFormSectionTitle";
            this.lblFormSectionTitle.Size = new System.Drawing.Size(229, 21);
            this.lblFormSectionTitle.TabIndex = 0;
            this.lblFormSectionTitle.Text = "Membership Assignment Form";
            // 
            // panelRight
            // 
            this.panelRight.Controls.Add(this.dgvMemberships);
            this.panelRight.Controls.Add(this.panelFilters);
            this.panelRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelRight.Location = new System.Drawing.Point(380, 75);
            this.panelRight.Name = "panelRight";
            this.panelRight.Padding = new System.Windows.Forms.Padding(12);
            this.panelRight.Size = new System.Drawing.Size(754, 606);
            this.panelRight.TabIndex = 2;
            // 
            // dgvMemberships
            // 
            this.dgvMemberships.AllowUserToAddRows = false;
            this.dgvMemberships.AllowUserToDeleteRows = false;
            this.dgvMemberships.AllowUserToResizeRows = false;
            this.dgvMemberships.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvMemberships.BackgroundColor = System.Drawing.Color.White;
            this.dgvMemberships.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvMemberships.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvMemberships.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Padding = new System.Windows.Forms.Padding(6);
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvMemberships.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvMemberships.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            dataGridViewCellStyle2.Padding = new System.Windows.Forms.Padding(4);
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(242)))), ((int)(((byte)(254)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(74)))), ((int)(((byte)(110)))));
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvMemberships.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvMemberships.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvMemberships.EnableHeadersVisualStyles = false;
            this.dgvMemberships.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.dgvMemberships.Location = new System.Drawing.Point(12, 77);
            this.dgvMemberships.MultiSelect = false;
            this.dgvMemberships.Name = "dgvMemberships";
            this.dgvMemberships.ReadOnly = true;
            this.dgvMemberships.RowHeadersVisible = false;
            this.dgvMemberships.RowTemplate.Height = 36;
            this.dgvMemberships.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvMemberships.Size = new System.Drawing.Size(730, 517);
            this.dgvMemberships.TabIndex = 1;
            this.dgvMemberships.SelectionChanged += new System.EventHandler(this.dgvMemberships_SelectionChanged);
            // 
            // panelFilters
            // 
            this.panelFilters.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.panelFilters.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelFilters.Controls.Add(this.lblRecordCount);
            this.panelFilters.Controls.Add(this.btnResetFilters);
            this.panelFilters.Controls.Add(this.btnFilter);
            this.panelFilters.Controls.Add(this.cmbFilterStatus);
            this.panelFilters.Controls.Add(this.lblFilterStatus);
            this.panelFilters.Controls.Add(this.cmbFilterPlan);
            this.panelFilters.Controls.Add(this.lblFilterPlan);
            this.panelFilters.Controls.Add(this.txtFilterSearch);
            this.panelFilters.Controls.Add(this.lblFilterSearch);
            this.panelFilters.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelFilters.Location = new System.Drawing.Point(12, 12);
            this.panelFilters.Name = "panelFilters";
            this.panelFilters.Size = new System.Drawing.Size(730, 65);
            this.panelFilters.TabIndex = 0;
            // 
            // lblRecordCount
            // 
            this.lblRecordCount.AutoSize = true;
            this.lblRecordCount.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblRecordCount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblRecordCount.Location = new System.Drawing.Point(10, 42);
            this.lblRecordCount.Name = "lblRecordCount";
            this.lblRecordCount.Size = new System.Drawing.Size(130, 13);
            this.lblRecordCount.TabIndex = 8;
            this.lblRecordCount.Text = "Showing 0 memberships";
            // 
            // btnResetFilters
            // 
            this.btnResetFilters.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.btnResetFilters.FlatAppearance.BorderSize = 0;
            this.btnResetFilters.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnResetFilters.Font = new System.Drawing.Font("Segoe UI Semibold", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnResetFilters.ForeColor = System.Drawing.Color.White;
            this.btnResetFilters.Location = new System.Drawing.Point(620, 8);
            this.btnResetFilters.Name = "btnResetFilters";
            this.btnResetFilters.Size = new System.Drawing.Size(60, 26);
            this.btnResetFilters.TabIndex = 7;
            this.btnResetFilters.Text = "Reset";
            this.btnResetFilters.UseVisualStyleBackColor = false;
            this.btnResetFilters.Click += new System.EventHandler(this.btnResetFilters_Click);
            // 
            // btnFilter
            // 
            this.btnFilter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(76)))), ((int)(((byte)(120)))));
            this.btnFilter.FlatAppearance.BorderSize = 0;
            this.btnFilter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFilter.Font = new System.Drawing.Font("Segoe UI Semibold", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnFilter.ForeColor = System.Drawing.Color.White;
            this.btnFilter.Location = new System.Drawing.Point(550, 8);
            this.btnFilter.Name = "btnFilter";
            this.btnFilter.Size = new System.Drawing.Size(65, 26);
            this.btnFilter.TabIndex = 6;
            this.btnFilter.Text = "Filter";
            this.btnFilter.UseVisualStyleBackColor = false;
            this.btnFilter.Click += new System.EventHandler(this.btnFilter_Click);
            // 
            // cmbFilterStatus
            // 
            this.cmbFilterStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFilterStatus.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.cmbFilterStatus.FormattingEnabled = true;
            this.cmbFilterStatus.Items.AddRange(new object[] {
            "All",
            "Active",
            "Expired",
            "Cancelled"});
            this.cmbFilterStatus.Location = new System.Drawing.Point(440, 9);
            this.cmbFilterStatus.Name = "cmbFilterStatus";
            this.cmbFilterStatus.Size = new System.Drawing.Size(100, 23);
            this.cmbFilterStatus.TabIndex = 5;
            this.cmbFilterStatus.SelectedIndexChanged += new System.EventHandler(this.cmbFilterStatus_SelectedIndexChanged);
            // 
            // lblFilterStatus
            // 
            this.lblFilterStatus.AutoSize = true;
            this.lblFilterStatus.Font = new System.Drawing.Font("Segoe UI Semibold", 8.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblFilterStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblFilterStatus.Location = new System.Drawing.Point(395, 13);
            this.lblFilterStatus.Name = "lblFilterStatus";
            this.lblFilterStatus.Size = new System.Drawing.Size(43, 15);
            this.lblFilterStatus.TabIndex = 4;
            this.lblFilterStatus.Text = "Status:";
            // 
            // cmbFilterPlan
            // 
            this.cmbFilterPlan.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFilterPlan.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.cmbFilterPlan.FormattingEnabled = true;
            this.cmbFilterPlan.Location = new System.Drawing.Point(265, 9);
            this.cmbFilterPlan.Name = "cmbFilterPlan";
            this.cmbFilterPlan.Size = new System.Drawing.Size(120, 23);
            this.cmbFilterPlan.TabIndex = 3;
            this.cmbFilterPlan.SelectedIndexChanged += new System.EventHandler(this.cmbFilterPlan_SelectedIndexChanged);
            // 
            // lblFilterPlan
            // 
            this.lblFilterPlan.AutoSize = true;
            this.lblFilterPlan.Font = new System.Drawing.Font("Segoe UI Semibold", 8.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblFilterPlan.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblFilterPlan.Location = new System.Drawing.Point(230, 13);
            this.lblFilterPlan.Name = "lblFilterPlan";
            this.lblFilterPlan.Size = new System.Drawing.Size(33, 15);
            this.lblFilterPlan.TabIndex = 2;
            this.lblFilterPlan.Text = "Plan:";
            // 
            // txtFilterSearch
            // 
            this.txtFilterSearch.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtFilterSearch.Location = new System.Drawing.Point(55, 9);
            this.txtFilterSearch.Name = "txtFilterSearch";
            this.txtFilterSearch.PlaceholderText = "Student / Seat...";
            this.txtFilterSearch.Size = new System.Drawing.Size(165, 23);
            this.txtFilterSearch.TabIndex = 1;
            this.txtFilterSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtFilterSearch_KeyDown);
            // 
            // lblFilterSearch
            // 
            this.lblFilterSearch.AutoSize = true;
            this.lblFilterSearch.Font = new System.Drawing.Font("Segoe UI Semibold", 8.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblFilterSearch.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblFilterSearch.Location = new System.Drawing.Point(8, 13);
            this.lblFilterSearch.Name = "lblFilterSearch";
            this.lblFilterSearch.Size = new System.Drawing.Size(46, 15);
            this.lblFilterSearch.TabIndex = 0;
            this.lblFilterSearch.Text = "Search:";
            // 
            // MembershipForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.ClientSize = new System.Drawing.Size(1134, 681);
            this.Controls.Add(this.panelRight);
            this.Controls.Add(this.panelLeft);
            this.Controls.Add(this.panelHeader);
            this.MinimumSize = new System.Drawing.Size(1000, 600);
            this.Name = "MembershipForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Library Management System - Membership Management";
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            this.panelLeft.ResumeLayout(false);
            this.panelLeft.PerformLayout();
            this.panelRight.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMemberships)).EndInit();
            this.panelFilters.ResumeLayout(false);
            this.panelFilters.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Label lblHeaderSubtitle;
        private System.Windows.Forms.Label lblHeaderTitle;
        private System.Windows.Forms.Panel panelLeft;
        private System.Windows.Forms.Label lblFormSectionTitle;
        private System.Windows.Forms.Label lblStudent;
        private System.Windows.Forms.ComboBox cmbStudent;
        private System.Windows.Forms.Label lblPlan;
        private System.Windows.Forms.ComboBox cmbPlan;
        private System.Windows.Forms.Label lblStartDate;
        private System.Windows.Forms.DateTimePicker dtpStartDate;
        private System.Windows.Forms.Label lblEndDate;
        private System.Windows.Forms.DateTimePicker dtpEndDate;
        private System.Windows.Forms.Label lblSeat;
        private System.Windows.Forms.ComboBox cmbSeat;
        private System.Windows.Forms.Label lblPlanFee;
        private System.Windows.Forms.TextBox txtPlanFee;
        private System.Windows.Forms.Label lblDiscount;
        private System.Windows.Forms.TextBox txtDiscount;
        private System.Windows.Forms.Label lblFinalFee;
        private System.Windows.Forms.TextBox txtFinalFee;
        private System.Windows.Forms.Label lblRemarks;
        private System.Windows.Forms.TextBox txtRemarks;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnRenew;
        private System.Windows.Forms.Button btnCancelMembership;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Label lblStatusMessage;
        private System.Windows.Forms.Panel panelRight;
        private System.Windows.Forms.Panel panelFilters;
        private System.Windows.Forms.Label lblFilterSearch;
        private System.Windows.Forms.TextBox txtFilterSearch;
        private System.Windows.Forms.Label lblFilterPlan;
        private System.Windows.Forms.ComboBox cmbFilterPlan;
        private System.Windows.Forms.Label lblFilterStatus;
        private System.Windows.Forms.ComboBox cmbFilterStatus;
        private System.Windows.Forms.Button btnFilter;
        private System.Windows.Forms.Button btnResetFilters;
        private System.Windows.Forms.Label lblRecordCount;
        private System.Windows.Forms.DataGridView dgvMemberships;
    }
}
