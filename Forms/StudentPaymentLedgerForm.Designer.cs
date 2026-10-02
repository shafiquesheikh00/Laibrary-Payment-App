namespace Laibrary_Payment_App.Forms
{
    partial class StudentPaymentLedgerForm
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
            this.panelStudentInfo = new System.Windows.Forms.Panel();
            this.lblOutstandingVal = new System.Windows.Forms.Label();
            this.lblOutstandingLbl = new System.Windows.Forms.Label();
            this.lblPaidVal = new System.Windows.Forms.Label();
            this.lblPaidLbl = new System.Windows.Forms.Label();
            this.lblInvoicedVal = new System.Windows.Forms.Label();
            this.lblInvoicedLbl = new System.Windows.Forms.Label();
            this.lblStudentDetails = new System.Windows.Forms.Label();
            this.lblStudentName = new System.Windows.Forms.Label();
            this.panelBottom = new System.Windows.Forms.Panel();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnViewReceipt = new System.Windows.Forms.Button();
            this.btnReceivePayment = new System.Windows.Forms.Button();
            this.panelGrid = new System.Windows.Forms.Panel();
            this.dgvLedger = new System.Windows.Forms.DataGridView();
            this.panelHeader.SuspendLayout();
            this.panelStudentInfo.SuspendLayout();
            this.panelBottom.SuspendLayout();
            this.panelGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLedger)).BeginInit();
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
            this.panelHeader.Size = new System.Drawing.Size(1004, 65);
            this.panelHeader.TabIndex = 0;
            // 
            // lblHeaderSubtitle
            // 
            this.lblHeaderSubtitle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblHeaderSubtitle.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblHeaderSubtitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(163)))), ((int)(((byte)(184)))));
            this.lblHeaderSubtitle.Location = new System.Drawing.Point(540, 22);
            this.lblHeaderSubtitle.Name = "lblHeaderSubtitle";
            this.lblHeaderSubtitle.Size = new System.Drawing.Size(440, 20);
            this.lblHeaderSubtitle.TabIndex = 1;
            this.lblHeaderSubtitle.Text = "Student payment ledger, payment timeline, and running balance";
            this.lblHeaderSubtitle.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblHeaderTitle
            // 
            this.lblHeaderTitle.AutoSize = true;
            this.lblHeaderTitle.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblHeaderTitle.ForeColor = System.Drawing.Color.White;
            this.lblHeaderTitle.Location = new System.Drawing.Point(20, 18);
            this.lblHeaderTitle.Name = "lblHeaderTitle";
            this.lblHeaderTitle.Size = new System.Drawing.Size(227, 25);
            this.lblHeaderTitle.TabIndex = 0;
            this.lblHeaderTitle.Text = "Student Payment Ledger";
            // 
            // panelStudentInfo
            // 
            this.panelStudentInfo.BackColor = System.Drawing.Color.White;
            this.panelStudentInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelStudentInfo.Controls.Add(this.lblOutstandingVal);
            this.panelStudentInfo.Controls.Add(this.lblOutstandingLbl);
            this.panelStudentInfo.Controls.Add(this.lblPaidVal);
            this.panelStudentInfo.Controls.Add(this.lblPaidLbl);
            this.panelStudentInfo.Controls.Add(this.lblInvoicedVal);
            this.panelStudentInfo.Controls.Add(this.lblInvoicedLbl);
            this.panelStudentInfo.Controls.Add(this.lblStudentDetails);
            this.panelStudentInfo.Controls.Add(this.lblStudentName);
            this.panelStudentInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelStudentInfo.Location = new System.Drawing.Point(0, 65);
            this.panelStudentInfo.Name = "panelStudentInfo";
            this.panelStudentInfo.Padding = new System.Windows.Forms.Padding(20, 14, 20, 14);
            this.panelStudentInfo.Size = new System.Drawing.Size(1004, 72);
            this.panelStudentInfo.TabIndex = 1;
            // 
            // lblOutstandingVal
            // 
            this.lblOutstandingVal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblOutstandingVal.Font = new System.Drawing.Font("Segoe UI", 11.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblOutstandingVal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this.lblOutstandingVal.Location = new System.Drawing.Point(850, 36);
            this.lblOutstandingVal.Name = "lblOutstandingVal";
            this.lblOutstandingVal.Size = new System.Drawing.Size(130, 20);
            this.lblOutstandingVal.TabIndex = 7;
            this.lblOutstandingVal.Text = "₹0.00";
            this.lblOutstandingVal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblOutstandingLbl
            // 
            this.lblOutstandingLbl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblOutstandingLbl.Font = new System.Drawing.Font("Segoe UI Semibold", 8.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblOutstandingLbl.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblOutstandingLbl.Location = new System.Drawing.Point(850, 14);
            this.lblOutstandingLbl.Name = "lblOutstandingLbl";
            this.lblOutstandingLbl.Size = new System.Drawing.Size(130, 18);
            this.lblOutstandingLbl.TabIndex = 6;
            this.lblOutstandingLbl.Text = "Outstanding Due:";
            this.lblOutstandingLbl.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblPaidVal
            // 
            this.lblPaidVal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPaidVal.Font = new System.Drawing.Font("Segoe UI", 11.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblPaidVal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(163)))), ((int)(((byte)(74)))));
            this.lblPaidVal.Location = new System.Drawing.Point(710, 36);
            this.lblPaidVal.Name = "lblPaidVal";
            this.lblPaidVal.Size = new System.Drawing.Size(120, 20);
            this.lblPaidVal.TabIndex = 5;
            this.lblPaidVal.Text = "₹0.00";
            this.lblPaidVal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblPaidLbl
            // 
            this.lblPaidLbl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPaidLbl.Font = new System.Drawing.Font("Segoe UI Semibold", 8.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblPaidLbl.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblPaidLbl.Location = new System.Drawing.Point(710, 14);
            this.lblPaidLbl.Name = "lblPaidLbl";
            this.lblPaidLbl.Size = new System.Drawing.Size(120, 18);
            this.lblPaidLbl.TabIndex = 4;
            this.lblPaidLbl.Text = "Total Paid:";
            this.lblPaidLbl.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblInvoicedVal
            // 
            this.lblInvoicedVal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblInvoicedVal.Font = new System.Drawing.Font("Segoe UI", 11.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblInvoicedVal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblInvoicedVal.Location = new System.Drawing.Point(570, 36);
            this.lblInvoicedVal.Name = "lblInvoicedVal";
            this.lblInvoicedVal.Size = new System.Drawing.Size(120, 20);
            this.lblInvoicedVal.TabIndex = 3;
            this.lblInvoicedVal.Text = "₹0.00";
            this.lblInvoicedVal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblInvoicedLbl
            // 
            this.lblInvoicedLbl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblInvoicedLbl.Font = new System.Drawing.Font("Segoe UI Semibold", 8.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblInvoicedLbl.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblInvoicedLbl.Location = new System.Drawing.Point(570, 14);
            this.lblInvoicedLbl.Name = "lblInvoicedLbl";
            this.lblInvoicedLbl.Size = new System.Drawing.Size(120, 18);
            this.lblInvoicedLbl.TabIndex = 2;
            this.lblInvoicedLbl.Text = "Total Invoiced:";
            this.lblInvoicedLbl.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblStudentDetails
            // 
            this.lblStudentDetails.AutoSize = true;
            this.lblStudentDetails.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblStudentDetails.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblStudentDetails.Location = new System.Drawing.Point(20, 38);
            this.lblStudentDetails.Name = "lblStudentDetails";
            this.lblStudentDetails.Size = new System.Drawing.Size(127, 15);
            this.lblStudentDetails.TabIndex = 1;
            this.lblStudentDetails.Text = "Code: - | Mobile: -";
            // 
            // lblStudentName
            // 
            this.lblStudentName.AutoSize = true;
            this.lblStudentName.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblStudentName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblStudentName.Location = new System.Drawing.Point(20, 14);
            this.lblStudentName.Name = "lblStudentName";
            this.lblStudentName.Size = new System.Drawing.Size(119, 21);
            this.lblStudentName.TabIndex = 0;
            this.lblStudentName.Text = "Student Name";
            // 
            // panelBottom
            // 
            this.panelBottom.BackColor = System.Drawing.Color.White;
            this.panelBottom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelBottom.Controls.Add(this.btnClose);
            this.panelBottom.Controls.Add(this.btnViewReceipt);
            this.panelBottom.Controls.Add(this.btnReceivePayment);
            this.panelBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelBottom.Location = new System.Drawing.Point(0, 505);
            this.panelBottom.Name = "panelBottom";
            this.panelBottom.Padding = new System.Windows.Forms.Padding(18, 12, 18, 12);
            this.panelBottom.Size = new System.Drawing.Size(1004, 60);
            this.panelBottom.TabIndex = 2;
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.btnClose.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(213)))), ((int)(((byte)(225)))));
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnClose.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.btnClose.Location = new System.Drawing.Point(900, 12);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(85, 34);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnViewReceipt
            // 
            this.btnViewReceipt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnViewReceipt.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(253)))), ((int)(((byte)(244)))));
            this.btnViewReceipt.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(187)))), ((int)(((byte)(247)))), ((int)(((byte)(208)))));
            this.btnViewReceipt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnViewReceipt.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnViewReceipt.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(128)))), ((int)(((byte)(61)))));
            this.btnViewReceipt.Location = new System.Drawing.Point(760, 12);
            this.btnViewReceipt.Name = "btnViewReceipt";
            this.btnViewReceipt.Size = new System.Drawing.Size(125, 34);
            this.btnViewReceipt.TabIndex = 1;
            this.btnViewReceipt.Text = "View Receipt";
            this.btnViewReceipt.UseVisualStyleBackColor = false;
            this.btnViewReceipt.Click += new System.EventHandler(this.btnViewReceipt_Click);
            // 
            // btnReceivePayment
            // 
            this.btnReceivePayment.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnReceivePayment.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(76)))), ((int)(((byte)(120)))));
            this.btnReceivePayment.FlatAppearance.BorderSize = 0;
            this.btnReceivePayment.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReceivePayment.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnReceivePayment.ForeColor = System.Drawing.Color.White;
            this.btnReceivePayment.Location = new System.Drawing.Point(600, 12);
            this.btnReceivePayment.Name = "btnReceivePayment";
            this.btnReceivePayment.Size = new System.Drawing.Size(145, 34);
            this.btnReceivePayment.TabIndex = 0;
            this.btnReceivePayment.Text = "+ Receive Payment";
            this.btnReceivePayment.UseVisualStyleBackColor = false;
            this.btnReceivePayment.Click += new System.EventHandler(this.btnReceivePayment_Click);
            // 
            // panelGrid
            // 
            this.panelGrid.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.panelGrid.Controls.Add(this.dgvLedger);
            this.panelGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelGrid.Location = new System.Drawing.Point(0, 137);
            this.panelGrid.Name = "panelGrid";
            this.panelGrid.Padding = new System.Windows.Forms.Padding(18);
            this.panelGrid.Size = new System.Drawing.Size(1004, 368);
            this.panelGrid.TabIndex = 3;
            // 
            // dgvLedger
            // 
            this.dgvLedger.AllowUserToAddRows = false;
            this.dgvLedger.AllowUserToDeleteRows = false;
            this.dgvLedger.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvLedger.BackgroundColor = System.Drawing.Color.White;
            this.dgvLedger.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvLedger.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLedger.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvLedger.Location = new System.Drawing.Point(18, 18);
            this.dgvLedger.MultiSelect = false;
            this.dgvLedger.Name = "dgvLedger";
            this.dgvLedger.ReadOnly = true;
            this.dgvLedger.RowHeadersVisible = false;
            this.dgvLedger.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvLedger.Size = new System.Drawing.Size(968, 332);
            this.dgvLedger.TabIndex = 0;
            this.dgvLedger.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvLedger_CellDoubleClick);
            // 
            // StudentPaymentLedgerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1004, 565);
            this.Controls.Add(this.panelGrid);
            this.Controls.Add(this.panelBottom);
            this.Controls.Add(this.panelStudentInfo);
            this.Controls.Add(this.panelHeader);
            this.MinimumSize = new System.Drawing.Size(900, 500);
            this.Name = "StudentPaymentLedgerForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Library Management System - Student Payment Ledger";
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            this.panelStudentInfo.ResumeLayout(false);
            this.panelStudentInfo.PerformLayout();
            this.panelBottom.ResumeLayout(false);
            this.panelGrid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvLedger)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Label lblHeaderTitle;
        private System.Windows.Forms.Label lblHeaderSubtitle;
        private System.Windows.Forms.Panel panelStudentInfo;
        private System.Windows.Forms.Label lblStudentName;
        private System.Windows.Forms.Label lblStudentDetails;
        private System.Windows.Forms.Label lblInvoicedLbl;
        private System.Windows.Forms.Label lblInvoicedVal;
        private System.Windows.Forms.Label lblPaidLbl;
        private System.Windows.Forms.Label lblPaidVal;
        private System.Windows.Forms.Label lblOutstandingLbl;
        private System.Windows.Forms.Label lblOutstandingVal;
        private System.Windows.Forms.Panel panelBottom;
        private System.Windows.Forms.Button btnReceivePayment;
        private System.Windows.Forms.Button btnViewReceipt;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Panel panelGrid;
        private System.Windows.Forms.DataGridView dgvLedger;
    }
}
