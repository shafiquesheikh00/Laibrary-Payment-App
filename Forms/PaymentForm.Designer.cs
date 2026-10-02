namespace Laibrary_Payment_App.Forms
{
    partial class PaymentForm
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
            this.panelActions = new System.Windows.Forms.Panel();
            this.btnHistory = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnSavePayment = new System.Windows.Forms.Button();
            this.btnSaveAndPrint = new System.Windows.Forms.Button();
            this.panelInvoiceSummary = new System.Windows.Forms.Panel();
            this.lblSummaryBalanceVal = new System.Windows.Forms.Label();
            this.lblSummaryBalanceLbl = new System.Windows.Forms.Label();
            this.lineSepSummary = new System.Windows.Forms.Label();
            this.lblSummaryPaidVal = new System.Windows.Forms.Label();
            this.lblSummaryPaidLbl = new System.Windows.Forms.Label();
            this.lblSummaryGrandVal = new System.Windows.Forms.Label();
            this.lblSummaryGrandLbl = new System.Windows.Forms.Label();
            this.lblSummarySeatVal = new System.Windows.Forms.Label();
            this.lblSummarySeatLbl = new System.Windows.Forms.Label();
            this.lblSummaryPlanVal = new System.Windows.Forms.Label();
            this.lblSummaryPlanLbl = new System.Windows.Forms.Label();
            this.lblSummaryStudentVal = new System.Windows.Forms.Label();
            this.lblSummaryStudentLbl = new System.Windows.Forms.Label();
            this.lblSummaryInvDateVal = new System.Windows.Forms.Label();
            this.lblSummaryInvDateLbl = new System.Windows.Forms.Label();
            this.lblSummaryInvNoVal = new System.Windows.Forms.Label();
            this.lblSummaryInvNoLbl = new System.Windows.Forms.Label();
            this.lblSummaryTitle = new System.Windows.Forms.Label();
            this.panelLeft = new System.Windows.Forms.Panel();
            this.panelPaymentEntry = new System.Windows.Forms.Panel();
            this.txtReceivedBy = new System.Windows.Forms.TextBox();
            this.lblReceivedBy = new System.Windows.Forms.Label();
            this.txtRemarks = new System.Windows.Forms.TextBox();
            this.lblRemarks = new System.Windows.Forms.Label();
            this.txtReferenceNo = new System.Windows.Forms.TextBox();
            this.lblReferenceNo = new System.Windows.Forms.Label();
            this.cmbPaymentMode = new System.Windows.Forms.ComboBox();
            this.lblPaymentMode = new System.Windows.Forms.Label();
            this.dtpPaymentDate = new System.Windows.Forms.DateTimePicker();
            this.lblPaymentDate = new System.Windows.Forms.Label();
            this.btnPayFullBalance = new System.Windows.Forms.Button();
            this.numAmount = new System.Windows.Forms.NumericUpDown();
            this.lblAmount = new System.Windows.Forms.Label();
            this.lblPaymentEntryTitle = new System.Windows.Forms.Label();
            this.panelInvoices = new System.Windows.Forms.Panel();
            this.dgvOutstandingInvoices = new System.Windows.Forms.DataGridView();
            this.lblOutstandingInvoicesTitle = new System.Windows.Forms.Label();
            this.panelStudentSelect = new System.Windows.Forms.Panel();
            this.cmbStudents = new System.Windows.Forms.ComboBox();
            this.txtSearchStudent = new System.Windows.Forms.TextBox();
            this.lblSelectStudent = new System.Windows.Forms.Label();
            this.panelHeader.SuspendLayout();
            this.panelContent.SuspendLayout();
            this.panelRight.SuspendLayout();
            this.panelActions.SuspendLayout();
            this.panelInvoiceSummary.SuspendLayout();
            this.panelLeft.SuspendLayout();
            this.panelPaymentEntry.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numAmount)).BeginInit();
            this.panelInvoices.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOutstandingInvoices)).BeginInit();
            this.panelStudentSelect.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelHeader
            // 
            this.panelHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.panelHeader.Controls.Add(this.lblHeaderSubtitle);
            this.panelHeader.Controls.Add(this.lblHeaderTitle);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(0, 0);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Padding = new System.Windows.Forms.Padding(24, 16, 24, 16);
            this.panelHeader.Size = new System.Drawing.Size(1084, 65);
            this.panelHeader.TabIndex = 0;
            // 
            // lblHeaderSubtitle
            // 
            this.lblHeaderSubtitle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblHeaderSubtitle.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblHeaderSubtitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(163)))), ((int)(((byte)(184)))));
            this.lblHeaderSubtitle.Location = new System.Drawing.Point(620, 22);
            this.lblHeaderSubtitle.Name = "lblHeaderSubtitle";
            this.lblHeaderSubtitle.Size = new System.Drawing.Size(440, 20);
            this.lblHeaderSubtitle.TabIndex = 1;
            this.lblHeaderSubtitle.Text = "Receive fee collections, allocate payments, and issue official receipts";
            this.lblHeaderSubtitle.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblHeaderTitle
            // 
            this.lblHeaderTitle.AutoSize = true;
            this.lblHeaderTitle.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblHeaderTitle.ForeColor = System.Drawing.Color.White;
            this.lblHeaderTitle.Location = new System.Drawing.Point(20, 18);
            this.lblHeaderTitle.Name = "lblHeaderTitle";
            this.lblHeaderTitle.Size = new System.Drawing.Size(325, 25);
            this.lblHeaderTitle.TabIndex = 0;
            this.lblHeaderTitle.Text = "Payment Collection & Receipts (Phase 5)";
            // 
            // panelContent
            // 
            this.panelContent.AutoScroll = true;
            this.panelContent.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.panelContent.Controls.Add(this.panelRight);
            this.panelContent.Controls.Add(this.panelLeft);
            this.panelContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContent.Location = new System.Drawing.Point(0, 65);
            this.panelContent.Name = "panelContent";
            this.panelContent.Padding = new System.Windows.Forms.Padding(20);
            this.panelContent.Size = new System.Drawing.Size(1084, 635);
            this.panelContent.TabIndex = 1;
            // 
            // panelRight
            // 
            this.panelRight.Controls.Add(this.panelActions);
            this.panelRight.Controls.Add(this.panelInvoiceSummary);
            this.panelRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelRight.Location = new System.Drawing.Point(674, 20);
            this.panelRight.Name = "panelRight";
            this.panelRight.Size = new System.Drawing.Size(390, 595);
            this.panelRight.TabIndex = 1;
            // 
            // panelActions
            // 
            this.panelActions.BackColor = System.Drawing.Color.White;
            this.panelActions.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelActions.Controls.Add(this.btnHistory);
            this.panelActions.Controls.Add(this.btnClear);
            this.panelActions.Controls.Add(this.btnSavePayment);
            this.panelActions.Controls.Add(this.btnSaveAndPrint);
            this.panelActions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelActions.Location = new System.Drawing.Point(0, 360);
            this.panelActions.Name = "panelActions";
            this.panelActions.Padding = new System.Windows.Forms.Padding(18);
            this.panelActions.Size = new System.Drawing.Size(390, 235);
            this.panelActions.TabIndex = 1;
            // 
            // btnHistory
            // 
            this.btnHistory.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.btnHistory.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(213)))), ((int)(((byte)(225)))));
            this.btnHistory.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHistory.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnHistory.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.btnHistory.Location = new System.Drawing.Point(198, 175);
            this.btnHistory.Name = "btnHistory";
            this.btnHistory.Size = new System.Drawing.Size(170, 38);
            this.btnHistory.TabIndex = 3;
            this.btnHistory.Text = "Payment History";
            this.btnHistory.UseVisualStyleBackColor = false;
            this.btnHistory.Click += new System.EventHandler(this.btnHistory_Click);
            // 
            // btnClear
            // 
            this.btnClear.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.btnClear.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(213)))), ((int)(((byte)(225)))));
            this.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClear.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnClear.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.btnClear.Location = new System.Drawing.Point(18, 175);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(170, 38);
            this.btnClear.TabIndex = 2;
            this.btnClear.Text = "Clear / Reset";
            this.btnClear.UseVisualStyleBackColor = false;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnSavePayment
            // 
            this.btnSavePayment.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(76)))), ((int)(((byte)(120)))));
            this.btnSavePayment.FlatAppearance.BorderSize = 0;
            this.btnSavePayment.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSavePayment.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnSavePayment.ForeColor = System.Drawing.Color.White;
            this.btnSavePayment.Location = new System.Drawing.Point(18, 105);
            this.btnSavePayment.Name = "btnSavePayment";
            this.btnSavePayment.Size = new System.Drawing.Size(350, 48);
            this.btnSavePayment.TabIndex = 1;
            this.btnSavePayment.Text = "Save Payment";
            this.btnSavePayment.UseVisualStyleBackColor = false;
            this.btnSavePayment.Click += new System.EventHandler(this.btnSavePayment_Click);
            // 
            // btnSaveAndPrint
            // 
            this.btnSaveAndPrint.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(185)))), ((int)(((byte)(129)))));
            this.btnSaveAndPrint.FlatAppearance.BorderSize = 0;
            this.btnSaveAndPrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSaveAndPrint.Font = new System.Drawing.Font("Segoe UI Semibold", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnSaveAndPrint.ForeColor = System.Drawing.Color.White;
            this.btnSaveAndPrint.Location = new System.Drawing.Point(18, 25);
            this.btnSaveAndPrint.Name = "btnSaveAndPrint";
            this.btnSaveAndPrint.Size = new System.Drawing.Size(350, 56);
            this.btnSaveAndPrint.TabIndex = 0;
            this.btnSaveAndPrint.Text = "✓ Save && Print Receipt";
            this.btnSaveAndPrint.UseVisualStyleBackColor = false;
            this.btnSaveAndPrint.Click += new System.EventHandler(this.btnSaveAndPrint_Click);
            // 
            // panelInvoiceSummary
            // 
            this.panelInvoiceSummary.BackColor = System.Drawing.Color.White;
            this.panelInvoiceSummary.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelInvoiceSummary.Controls.Add(this.lblSummaryBalanceVal);
            this.panelInvoiceSummary.Controls.Add(this.lblSummaryBalanceLbl);
            this.panelInvoiceSummary.Controls.Add(this.lineSepSummary);
            this.panelInvoiceSummary.Controls.Add(this.lblSummaryPaidVal);
            this.panelInvoiceSummary.Controls.Add(this.lblSummaryPaidLbl);
            this.panelInvoiceSummary.Controls.Add(this.lblSummaryGrandVal);
            this.panelInvoiceSummary.Controls.Add(this.lblSummaryGrandLbl);
            this.panelInvoiceSummary.Controls.Add(this.lblSummarySeatVal);
            this.panelInvoiceSummary.Controls.Add(this.lblSummarySeatLbl);
            this.panelInvoiceSummary.Controls.Add(this.lblSummaryPlanVal);
            this.panelInvoiceSummary.Controls.Add(this.lblSummaryPlanLbl);
            this.panelInvoiceSummary.Controls.Add(this.lblSummaryStudentVal);
            this.panelInvoiceSummary.Controls.Add(this.lblSummaryStudentLbl);
            this.panelInvoiceSummary.Controls.Add(this.lblSummaryInvDateVal);
            this.panelInvoiceSummary.Controls.Add(this.lblSummaryInvDateLbl);
            this.panelInvoiceSummary.Controls.Add(this.lblSummaryInvNoVal);
            this.panelInvoiceSummary.Controls.Add(this.lblSummaryInvNoLbl);
            this.panelInvoiceSummary.Controls.Add(this.lblSummaryTitle);
            this.panelInvoiceSummary.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelInvoiceSummary.Location = new System.Drawing.Point(0, 0);
            this.panelInvoiceSummary.Name = "panelInvoiceSummary";
            this.panelInvoiceSummary.Padding = new System.Windows.Forms.Padding(18);
            this.panelInvoiceSummary.Size = new System.Drawing.Size(390, 360);
            this.panelInvoiceSummary.TabIndex = 0;
            // 
            // lblSummaryBalanceVal
            // 
            this.lblSummaryBalanceVal.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblSummaryBalanceVal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this.lblSummaryBalanceVal.Location = new System.Drawing.Point(180, 305);
            this.lblSummaryBalanceVal.Name = "lblSummaryBalanceVal";
            this.lblSummaryBalanceVal.Size = new System.Drawing.Size(190, 28);
            this.lblSummaryBalanceVal.TabIndex = 17;
            this.lblSummaryBalanceVal.Text = "₹0.00";
            this.lblSummaryBalanceVal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblSummaryBalanceLbl
            // 
            this.lblSummaryBalanceLbl.AutoSize = true;
            this.lblSummaryBalanceLbl.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblSummaryBalanceLbl.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblSummaryBalanceLbl.Location = new System.Drawing.Point(18, 308);
            this.lblSummaryBalanceLbl.Name = "lblSummaryBalanceLbl";
            this.lblSummaryBalanceLbl.Size = new System.Drawing.Size(155, 20);
            this.lblSummaryBalanceLbl.TabIndex = 16;
            this.lblSummaryBalanceLbl.Text = "Outstanding Balance:";
            // 
            // lineSepSummary
            // 
            this.lineSepSummary.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lineSepSummary.Location = new System.Drawing.Point(18, 292);
            this.lineSepSummary.Name = "lineSepSummary";
            this.lineSepSummary.Size = new System.Drawing.Size(350, 2);
            this.lineSepSummary.TabIndex = 15;
            // 
            // lblSummaryPaidVal
            // 
            this.lblSummaryPaidVal.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblSummaryPaidVal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(185)))), ((int)(((byte)(129)))));
            this.lblSummaryPaidVal.Location = new System.Drawing.Point(200, 260);
            this.lblSummaryPaidVal.Name = "lblSummaryPaidVal";
            this.lblSummaryPaidVal.Size = new System.Drawing.Size(170, 20);
            this.lblSummaryPaidVal.TabIndex = 14;
            this.lblSummaryPaidVal.Text = "₹0.00";
            this.lblSummaryPaidVal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblSummaryPaidLbl
            // 
            this.lblSummaryPaidLbl.AutoSize = true;
            this.lblSummaryPaidLbl.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblSummaryPaidLbl.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblSummaryPaidLbl.Location = new System.Drawing.Point(18, 260);
            this.lblSummaryPaidLbl.Name = "lblSummaryPaidLbl";
            this.lblSummaryPaidLbl.Size = new System.Drawing.Size(84, 17);
            this.lblSummaryPaidLbl.TabIndex = 13;
            this.lblSummaryPaidLbl.Text = "Already Paid:";
            // 
            // lblSummaryGrandVal
            // 
            this.lblSummaryGrandVal.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblSummaryGrandVal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblSummaryGrandVal.Location = new System.Drawing.Point(200, 230);
            this.lblSummaryGrandVal.Name = "lblSummaryGrandVal";
            this.lblSummaryGrandVal.Size = new System.Drawing.Size(170, 20);
            this.lblSummaryGrandVal.TabIndex = 12;
            this.lblSummaryGrandVal.Text = "₹0.00";
            this.lblSummaryGrandVal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblSummaryGrandLbl
            // 
            this.lblSummaryGrandLbl.AutoSize = true;
            this.lblSummaryGrandLbl.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblSummaryGrandLbl.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblSummaryGrandLbl.Location = new System.Drawing.Point(18, 230);
            this.lblSummaryGrandLbl.Name = "lblSummaryGrandLbl";
            this.lblSummaryGrandLbl.Size = new System.Drawing.Size(79, 17);
            this.lblSummaryGrandLbl.TabIndex = 11;
            this.lblSummaryGrandLbl.Text = "Grand Total:";
            // 
            // lblSummarySeatVal
            // 
            this.lblSummarySeatVal.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblSummarySeatVal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblSummarySeatVal.Location = new System.Drawing.Point(130, 195);
            this.lblSummarySeatVal.Name = "lblSummarySeatVal";
            this.lblSummarySeatVal.Size = new System.Drawing.Size(240, 18);
            this.lblSummarySeatVal.TabIndex = 10;
            this.lblSummarySeatVal.Text = "-";
            // 
            // lblSummarySeatLbl
            // 
            this.lblSummarySeatLbl.AutoSize = true;
            this.lblSummarySeatLbl.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblSummarySeatLbl.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblSummarySeatLbl.Location = new System.Drawing.Point(18, 195);
            this.lblSummarySeatLbl.Name = "lblSummarySeatLbl";
            this.lblSummarySeatLbl.Size = new System.Drawing.Size(32, 15);
            this.lblSummarySeatLbl.TabIndex = 9;
            this.lblSummarySeatLbl.Text = "Seat:";
            // 
            // lblSummaryPlanVal
            // 
            this.lblSummaryPlanVal.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblSummaryPlanVal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblSummaryPlanVal.Location = new System.Drawing.Point(130, 165);
            this.lblSummaryPlanVal.Name = "lblSummaryPlanVal";
            this.lblSummaryPlanVal.Size = new System.Drawing.Size(240, 18);
            this.lblSummaryPlanVal.TabIndex = 8;
            this.lblSummaryPlanVal.Text = "-";
            // 
            // lblSummaryPlanLbl
            // 
            this.lblSummaryPlanLbl.AutoSize = true;
            this.lblSummaryPlanLbl.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblSummaryPlanLbl.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblSummaryPlanLbl.Location = new System.Drawing.Point(18, 165);
            this.lblSummaryPlanLbl.Name = "lblSummaryPlanLbl";
            this.lblSummaryPlanLbl.Size = new System.Drawing.Size(77, 15);
            this.lblSummaryPlanLbl.TabIndex = 7;
            this.lblSummaryPlanLbl.Text = "Membership:";
            // 
            // lblSummaryStudentVal
            // 
            this.lblSummaryStudentVal.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblSummaryStudentVal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblSummaryStudentVal.Location = new System.Drawing.Point(130, 135);
            this.lblSummaryStudentVal.Name = "lblSummaryStudentVal";
            this.lblSummaryStudentVal.Size = new System.Drawing.Size(240, 18);
            this.lblSummaryStudentVal.TabIndex = 6;
            this.lblSummaryStudentVal.Text = "-";
            // 
            // lblSummaryStudentLbl
            // 
            this.lblSummaryStudentLbl.AutoSize = true;
            this.lblSummaryStudentLbl.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblSummaryStudentLbl.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblSummaryStudentLbl.Location = new System.Drawing.Point(18, 135);
            this.lblSummaryStudentLbl.Name = "lblSummaryStudentLbl";
            this.lblSummaryStudentLbl.Size = new System.Drawing.Size(51, 15);
            this.lblSummaryStudentLbl.TabIndex = 5;
            this.lblSummaryStudentLbl.Text = "Student:";
            // 
            // lblSummaryInvDateVal
            // 
            this.lblSummaryInvDateVal.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblSummaryInvDateVal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblSummaryInvDateVal.Location = new System.Drawing.Point(130, 95);
            this.lblSummaryInvDateVal.Name = "lblSummaryInvDateVal";
            this.lblSummaryInvDateVal.Size = new System.Drawing.Size(240, 18);
            this.lblSummaryInvDateVal.TabIndex = 4;
            this.lblSummaryInvDateVal.Text = "-";
            // 
            // lblSummaryInvDateLbl
            // 
            this.lblSummaryInvDateLbl.AutoSize = true;
            this.lblSummaryInvDateLbl.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblSummaryInvDateLbl.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblSummaryInvDateLbl.Location = new System.Drawing.Point(18, 95);
            this.lblSummaryInvDateLbl.Name = "lblSummaryInvDateLbl";
            this.lblSummaryInvDateLbl.Size = new System.Drawing.Size(75, 15);
            this.lblSummaryInvDateLbl.TabIndex = 3;
            this.lblSummaryInvDateLbl.Text = "Invoice Date:";
            // 
            // lblSummaryInvNoVal
            // 
            this.lblSummaryInvNoVal.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblSummaryInvNoVal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(76)))), ((int)(((byte)(120)))));
            this.lblSummaryInvNoVal.Location = new System.Drawing.Point(130, 58);
            this.lblSummaryInvNoVal.Name = "lblSummaryInvNoVal";
            this.lblSummaryInvNoVal.Size = new System.Drawing.Size(240, 22);
            this.lblSummaryInvNoVal.TabIndex = 2;
            this.lblSummaryInvNoVal.Text = "Select an invoice";
            // 
            // lblSummaryInvNoLbl
            // 
            this.lblSummaryInvNoLbl.AutoSize = true;
            this.lblSummaryInvNoLbl.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblSummaryInvNoLbl.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblSummaryInvNoLbl.Location = new System.Drawing.Point(18, 62);
            this.lblSummaryInvNoLbl.Name = "lblSummaryInvNoLbl";
            this.lblSummaryInvNoLbl.Size = new System.Drawing.Size(67, 15);
            this.lblSummaryInvNoLbl.TabIndex = 1;
            this.lblSummaryInvNoLbl.Text = "Invoice No:";
            // 
            // lblSummaryTitle
            // 
            this.lblSummaryTitle.AutoSize = true;
            this.lblSummaryTitle.Font = new System.Drawing.Font("Segoe UI", 11.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblSummaryTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblSummaryTitle.Location = new System.Drawing.Point(16, 16);
            this.lblSummaryTitle.Name = "lblSummaryTitle";
            this.lblSummaryTitle.Size = new System.Drawing.Size(188, 21);
            this.lblSummaryTitle.TabIndex = 0;
            this.lblSummaryTitle.Text = "Selected Invoice Details";
            // 
            // panelLeft
            // 
            this.panelLeft.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelLeft.Controls.Add(this.panelPaymentEntry);
            this.panelLeft.Controls.Add(this.panelInvoices);
            this.panelLeft.Controls.Add(this.panelStudentSelect);
            this.panelLeft.Location = new System.Drawing.Point(20, 20);
            this.panelLeft.Name = "panelLeft";
            this.panelLeft.Size = new System.Drawing.Size(640, 595);
            this.panelLeft.TabIndex = 0;
            // 
            // panelPaymentEntry
            // 
            this.panelPaymentEntry.BackColor = System.Drawing.Color.White;
            this.panelPaymentEntry.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelPaymentEntry.Controls.Add(this.txtReceivedBy);
            this.panelPaymentEntry.Controls.Add(this.lblReceivedBy);
            this.panelPaymentEntry.Controls.Add(this.txtRemarks);
            this.panelPaymentEntry.Controls.Add(this.lblRemarks);
            this.panelPaymentEntry.Controls.Add(this.txtReferenceNo);
            this.panelPaymentEntry.Controls.Add(this.lblReferenceNo);
            this.panelPaymentEntry.Controls.Add(this.cmbPaymentMode);
            this.panelPaymentEntry.Controls.Add(this.lblPaymentMode);
            this.panelPaymentEntry.Controls.Add(this.dtpPaymentDate);
            this.panelPaymentEntry.Controls.Add(this.lblPaymentDate);
            this.panelPaymentEntry.Controls.Add(this.btnPayFullBalance);
            this.panelPaymentEntry.Controls.Add(this.numAmount);
            this.panelPaymentEntry.Controls.Add(this.lblAmount);
            this.panelPaymentEntry.Controls.Add(this.lblPaymentEntryTitle);
            this.panelPaymentEntry.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelPaymentEntry.Location = new System.Drawing.Point(0, 315);
            this.panelPaymentEntry.Name = "panelPaymentEntry";
            this.panelPaymentEntry.Padding = new System.Windows.Forms.Padding(18);
            this.panelPaymentEntry.Size = new System.Drawing.Size(640, 280);
            this.panelPaymentEntry.TabIndex = 2;
            // 
            // txtReceivedBy
            // 
            this.txtReceivedBy.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.txtReceivedBy.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtReceivedBy.Location = new System.Drawing.Point(340, 235);
            this.txtReceivedBy.Name = "txtReceivedBy";
            this.txtReceivedBy.ReadOnly = true;
            this.txtReceivedBy.Size = new System.Drawing.Size(280, 24);
            this.txtReceivedBy.TabIndex = 13;
            // 
            // lblReceivedBy
            // 
            this.lblReceivedBy.AutoSize = true;
            this.lblReceivedBy.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblReceivedBy.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblReceivedBy.Location = new System.Drawing.Point(340, 215);
            this.lblReceivedBy.Name = "lblReceivedBy";
            this.lblReceivedBy.Size = new System.Drawing.Size(73, 15);
            this.lblReceivedBy.TabIndex = 12;
            this.lblReceivedBy.Text = "Received By:";
            // 
            // txtRemarks
            // 
            this.txtRemarks.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtRemarks.Location = new System.Drawing.Point(20, 235);
            this.txtRemarks.Name = "txtRemarks";
            this.txtRemarks.PlaceholderText = "Optional payment remarks";
            this.txtRemarks.Size = new System.Drawing.Size(295, 24);
            this.txtRemarks.TabIndex = 11;
            // 
            // lblRemarks
            // 
            this.lblRemarks.AutoSize = true;
            this.lblRemarks.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblRemarks.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblRemarks.Location = new System.Drawing.Point(20, 215);
            this.lblRemarks.Name = "lblRemarks";
            this.lblRemarks.Size = new System.Drawing.Size(55, 15);
            this.lblRemarks.TabIndex = 10;
            this.lblRemarks.Text = "Remarks:";
            // 
            // txtReferenceNo
            // 
            this.txtReferenceNo.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtReferenceNo.Location = new System.Drawing.Point(340, 165);
            this.txtReferenceNo.Name = "txtReferenceNo";
            this.txtReferenceNo.PlaceholderText = "UPI Txn ID / Card Last 4 / UTR No";
            this.txtReferenceNo.Size = new System.Drawing.Size(280, 24);
            this.txtReferenceNo.TabIndex = 9;
            // 
            // lblReferenceNo
            // 
            this.lblReferenceNo.AutoSize = true;
            this.lblReferenceNo.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblReferenceNo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblReferenceNo.Location = new System.Drawing.Point(340, 145);
            this.lblReferenceNo.Name = "lblReferenceNo";
            this.lblReferenceNo.Size = new System.Drawing.Size(155, 15);
            this.lblReferenceNo.TabIndex = 8;
            this.lblReferenceNo.Text = "Reference / Transaction No:";
            // 
            // cmbPaymentMode
            // 
            this.cmbPaymentMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPaymentMode.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.cmbPaymentMode.FormattingEnabled = true;
            this.cmbPaymentMode.Location = new System.Drawing.Point(20, 165);
            this.cmbPaymentMode.Name = "cmbPaymentMode";
            this.cmbPaymentMode.Size = new System.Drawing.Size(295, 24);
            this.cmbPaymentMode.TabIndex = 7;
            this.cmbPaymentMode.SelectedIndexChanged += new System.EventHandler(this.cmbPaymentMode_SelectedIndexChanged);
            // 
            // lblPaymentMode
            // 
            this.lblPaymentMode.AutoSize = true;
            this.lblPaymentMode.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblPaymentMode.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblPaymentMode.Location = new System.Drawing.Point(20, 145);
            this.lblPaymentMode.Name = "lblPaymentMode";
            this.lblPaymentMode.Size = new System.Drawing.Size(92, 15);
            this.lblPaymentMode.TabIndex = 6;
            this.lblPaymentMode.Text = "Payment Mode:*";
            // 
            // dtpPaymentDate
            // 
            this.dtpPaymentDate.CustomFormat = "dd-MMM-yyyy";
            this.dtpPaymentDate.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.dtpPaymentDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpPaymentDate.Location = new System.Drawing.Point(340, 95);
            this.dtpPaymentDate.Name = "dtpPaymentDate";
            this.dtpPaymentDate.Size = new System.Drawing.Size(280, 24);
            this.dtpPaymentDate.TabIndex = 5;
            // 
            // lblPaymentDate
            // 
            this.lblPaymentDate.AutoSize = true;
            this.lblPaymentDate.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblPaymentDate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblPaymentDate.Location = new System.Drawing.Point(340, 75);
            this.lblPaymentDate.Name = "lblPaymentDate";
            this.lblPaymentDate.Size = new System.Drawing.Size(86, 15);
            this.lblPaymentDate.TabIndex = 4;
            this.lblPaymentDate.Text = "Payment Date:";
            // 
            // btnPayFullBalance
            // 
            this.btnPayFullBalance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(242)))), ((int)(((byte)(255)))));
            this.btnPayFullBalance.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(199)))), ((int)(((byte)(210)))), ((int)(((byte)(254)))));
            this.btnPayFullBalance.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPayFullBalance.Font = new System.Drawing.Font("Segoe UI Semibold", 8.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnPayFullBalance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(67)))), ((int)(((byte)(56)))), ((int)(((byte)(202)))));
            this.btnPayFullBalance.Location = new System.Drawing.Point(205, 95);
            this.btnPayFullBalance.Name = "btnPayFullBalance";
            this.btnPayFullBalance.Size = new System.Drawing.Size(110, 28);
            this.btnPayFullBalance.TabIndex = 3;
            this.btnPayFullBalance.Text = "Pay Full Balance";
            this.btnPayFullBalance.UseVisualStyleBackColor = false;
            this.btnPayFullBalance.Click += new System.EventHandler(this.btnPayFullBalance_Click);
            // 
            // numAmount
            // 
            this.numAmount.DecimalPlaces = 2;
            this.numAmount.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.numAmount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.numAmount.Location = new System.Drawing.Point(20, 95);
            this.numAmount.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numAmount.Name = "numAmount";
            this.numAmount.Size = new System.Drawing.Size(175, 27);
            this.numAmount.TabIndex = 2;
            this.numAmount.ThousandsSeparator = true;
            // 
            // lblAmount
            // 
            this.lblAmount.AutoSize = true;
            this.lblAmount.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblAmount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblAmount.Location = new System.Drawing.Point(20, 75);
            this.lblAmount.Name = "lblAmount";
            this.lblAmount.Size = new System.Drawing.Size(126, 15);
            this.lblAmount.TabIndex = 1;
            this.lblAmount.Text = "Payment Amount (₹):*";
            // 
            // lblPaymentEntryTitle
            // 
            this.lblPaymentEntryTitle.AutoSize = true;
            this.lblPaymentEntryTitle.Font = new System.Drawing.Font("Segoe UI", 11.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblPaymentEntryTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblPaymentEntryTitle.Location = new System.Drawing.Point(16, 16);
            this.lblPaymentEntryTitle.Name = "lblPaymentEntryTitle";
            this.lblPaymentEntryTitle.Size = new System.Drawing.Size(200, 21);
            this.lblPaymentEntryTitle.TabIndex = 0;
            this.lblPaymentEntryTitle.Text = "Receive Payment Entry";
            // 
            // panelInvoices
            // 
            this.panelInvoices.BackColor = System.Drawing.Color.White;
            this.panelInvoices.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelInvoices.Controls.Add(this.dgvOutstandingInvoices);
            this.panelInvoices.Controls.Add(this.lblOutstandingInvoicesTitle);
            this.panelInvoices.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelInvoices.Location = new System.Drawing.Point(0, 105);
            this.panelInvoices.Name = "panelInvoices";
            this.panelInvoices.Padding = new System.Windows.Forms.Padding(18);
            this.panelInvoices.Size = new System.Drawing.Size(640, 210);
            this.panelInvoices.TabIndex = 1;
            // 
            // dgvOutstandingInvoices
            // 
            this.dgvOutstandingInvoices.AllowUserToAddRows = false;
            this.dgvOutstandingInvoices.AllowUserToDeleteRows = false;
            this.dgvOutstandingInvoices.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvOutstandingInvoices.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvOutstandingInvoices.BackgroundColor = System.Drawing.Color.White;
            this.dgvOutstandingInvoices.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvOutstandingInvoices.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvOutstandingInvoices.Location = new System.Drawing.Point(18, 48);
            this.dgvOutstandingInvoices.MultiSelect = false;
            this.dgvOutstandingInvoices.Name = "dgvOutstandingInvoices";
            this.dgvOutstandingInvoices.ReadOnly = true;
            this.dgvOutstandingInvoices.RowHeadersVisible = false;
            this.dgvOutstandingInvoices.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvOutstandingInvoices.Size = new System.Drawing.Size(602, 142);
            this.dgvOutstandingInvoices.TabIndex = 1;
            this.dgvOutstandingInvoices.SelectionChanged += new System.EventHandler(this.dgvOutstandingInvoices_SelectionChanged);
            // 
            // lblOutstandingInvoicesTitle
            // 
            this.lblOutstandingInvoicesTitle.AutoSize = true;
            this.lblOutstandingInvoicesTitle.Font = new System.Drawing.Font("Segoe UI", 11.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblOutstandingInvoicesTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblOutstandingInvoicesTitle.Location = new System.Drawing.Point(16, 16);
            this.lblOutstandingInvoicesTitle.Name = "lblOutstandingInvoicesTitle";
            this.lblOutstandingInvoicesTitle.Size = new System.Drawing.Size(262, 21);
            this.lblOutstandingInvoicesTitle.TabIndex = 0;
            this.lblOutstandingInvoicesTitle.Text = "Outstanding Invoices for Student";
            // 
            // panelStudentSelect
            // 
            this.panelStudentSelect.BackColor = System.Drawing.Color.White;
            this.panelStudentSelect.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelStudentSelect.Controls.Add(this.cmbStudents);
            this.panelStudentSelect.Controls.Add(this.txtSearchStudent);
            this.panelStudentSelect.Controls.Add(this.lblSelectStudent);
            this.panelStudentSelect.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelStudentSelect.Location = new System.Drawing.Point(0, 0);
            this.panelStudentSelect.Name = "panelStudentSelect";
            this.panelStudentSelect.Padding = new System.Windows.Forms.Padding(18);
            this.panelStudentSelect.Size = new System.Drawing.Size(640, 105);
            this.panelStudentSelect.TabIndex = 0;
            // 
            // cmbStudents
            // 
            this.cmbStudents.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbStudents.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStudents.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.cmbStudents.FormattingEnabled = true;
            this.cmbStudents.Location = new System.Drawing.Point(235, 52);
            this.cmbStudents.Name = "cmbStudents";
            this.cmbStudents.Size = new System.Drawing.Size(385, 24);
            this.cmbStudents.TabIndex = 2;
            this.cmbStudents.SelectedIndexChanged += new System.EventHandler(this.cmbStudents_SelectedIndexChanged);
            // 
            // txtSearchStudent
            // 
            this.txtSearchStudent.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtSearchStudent.Location = new System.Drawing.Point(20, 52);
            this.txtSearchStudent.Name = "txtSearchStudent";
            this.txtSearchStudent.PlaceholderText = "Search by code / name / mobile...";
            this.txtSearchStudent.Size = new System.Drawing.Size(200, 24);
            this.txtSearchStudent.TabIndex = 1;
            this.txtSearchStudent.TextChanged += new System.EventHandler(this.txtSearchStudent_TextChanged);
            // 
            // lblSelectStudent
            // 
            this.lblSelectStudent.AutoSize = true;
            this.lblSelectStudent.Font = new System.Drawing.Font("Segoe UI", 11.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblSelectStudent.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblSelectStudent.Location = new System.Drawing.Point(16, 16);
            this.lblSelectStudent.Name = "lblSelectStudent";
            this.lblSelectStudent.Size = new System.Drawing.Size(127, 21);
            this.lblSelectStudent.TabIndex = 0;
            this.lblSelectStudent.Text = "Select Student:*";
            // 
            // PaymentForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1084, 700);
            this.Controls.Add(this.panelContent);
            this.Controls.Add(this.panelHeader);
            this.MinimumSize = new System.Drawing.Size(1000, 650);
            this.Name = "PaymentForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Library Management System - Payment Collection";
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            this.panelContent.ResumeLayout(false);
            this.panelRight.ResumeLayout(false);
            this.panelActions.ResumeLayout(false);
            this.panelInvoiceSummary.ResumeLayout(false);
            this.panelInvoiceSummary.PerformLayout();
            this.panelLeft.ResumeLayout(false);
            this.panelPaymentEntry.ResumeLayout(false);
            this.panelPaymentEntry.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numAmount)).EndInit();
            this.panelInvoices.ResumeLayout(false);
            this.panelInvoices.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOutstandingInvoices)).EndInit();
            this.panelStudentSelect.ResumeLayout(false);
            this.panelStudentSelect.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Label lblHeaderTitle;
        private System.Windows.Forms.Label lblHeaderSubtitle;
        private System.Windows.Forms.Panel panelContent;
        private System.Windows.Forms.Panel panelLeft;
        private System.Windows.Forms.Panel panelStudentSelect;
        private System.Windows.Forms.Label lblSelectStudent;
        private System.Windows.Forms.TextBox txtSearchStudent;
        private System.Windows.Forms.ComboBox cmbStudents;
        private System.Windows.Forms.Panel panelInvoices;
        private System.Windows.Forms.Label lblOutstandingInvoicesTitle;
        private System.Windows.Forms.DataGridView dgvOutstandingInvoices;
        private System.Windows.Forms.Panel panelPaymentEntry;
        private System.Windows.Forms.Label lblPaymentEntryTitle;
        private System.Windows.Forms.Label lblAmount;
        private System.Windows.Forms.NumericUpDown numAmount;
        private System.Windows.Forms.Button btnPayFullBalance;
        private System.Windows.Forms.Label lblPaymentDate;
        private System.Windows.Forms.DateTimePicker dtpPaymentDate;
        private System.Windows.Forms.Label lblPaymentMode;
        private System.Windows.Forms.ComboBox cmbPaymentMode;
        private System.Windows.Forms.Label lblReferenceNo;
        private System.Windows.Forms.TextBox txtReferenceNo;
        private System.Windows.Forms.Label lblRemarks;
        private System.Windows.Forms.TextBox txtRemarks;
        private System.Windows.Forms.Label lblReceivedBy;
        private System.Windows.Forms.TextBox txtReceivedBy;
        private System.Windows.Forms.Panel panelRight;
        private System.Windows.Forms.Panel panelInvoiceSummary;
        private System.Windows.Forms.Label lblSummaryTitle;
        private System.Windows.Forms.Label lblSummaryInvNoLbl;
        private System.Windows.Forms.Label lblSummaryInvNoVal;
        private System.Windows.Forms.Label lblSummaryInvDateLbl;
        private System.Windows.Forms.Label lblSummaryInvDateVal;
        private System.Windows.Forms.Label lblSummaryStudentLbl;
        private System.Windows.Forms.Label lblSummaryStudentVal;
        private System.Windows.Forms.Label lblSummaryPlanLbl;
        private System.Windows.Forms.Label lblSummaryPlanVal;
        private System.Windows.Forms.Label lblSummarySeatLbl;
        private System.Windows.Forms.Label lblSummarySeatVal;
        private System.Windows.Forms.Label lblSummaryGrandLbl;
        private System.Windows.Forms.Label lblSummaryGrandVal;
        private System.Windows.Forms.Label lblSummaryPaidLbl;
        private System.Windows.Forms.Label lblSummaryPaidVal;
        private System.Windows.Forms.Label lineSepSummary;
        private System.Windows.Forms.Label lblSummaryBalanceLbl;
        private System.Windows.Forms.Label lblSummaryBalanceVal;
        private System.Windows.Forms.Panel panelActions;
        private System.Windows.Forms.Button btnSaveAndPrint;
        private System.Windows.Forms.Button btnSavePayment;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnHistory;
    }
}
