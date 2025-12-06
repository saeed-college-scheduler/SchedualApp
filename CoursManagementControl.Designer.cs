namespace SchedualApp
{
    partial class CoursManagementControl
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

        #region Component Designer generated code
        private void InitializeComponent()
        {
            this._mainLayout = new System.Windows.Forms.TableLayoutPanel();
            this._formPanel = new System.Windows.Forms.Panel();
            this._formLayout = new System.Windows.Forms.TableLayoutPanel();
            this.lblCode = new System.Windows.Forms.Label();
            this._txtCode = new System.Windows.Forms.TextBox();
            this.lblTitle = new System.Windows.Forms.Label();
            this._txtTitle = new System.Windows.Forms.TextBox();
            this.lblLecHrs = new System.Windows.Forms.Label();
            this._txtLectureHours = new System.Windows.Forms.TextBox();
            this.lblPracHrs = new System.Windows.Forms.Label();
            this._txtPracticalHours = new System.Windows.Forms.TextBox();
            this.lblIsPrac = new System.Windows.Forms.Label();
            this._chkIsPractical = new System.Windows.Forms.CheckBox();
            this._buttonsPanel = new System.Windows.Forms.FlowLayoutPanel();
            this._btnDelete = new System.Windows.Forms.Button();
            this._btnSave = new System.Windows.Forms.Button();
            this._btnNew = new System.Windows.Forms.Button();
            this._dataGridView = new System.Windows.Forms.DataGridView();
            this._mainLayout.SuspendLayout();
            this._formPanel.SuspendLayout();
            this._formLayout.SuspendLayout();
            this._buttonsPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._dataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // _mainLayout
            // 
            this._mainLayout.ColumnCount = 2;
            this._mainLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this._mainLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this._mainLayout.Controls.Add(this._formPanel, 0, 0);
            this._mainLayout.Controls.Add(this._dataGridView, 1, 0);
            this._mainLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this._mainLayout.Location = new System.Drawing.Point(0, 0);
            this._mainLayout.Name = "_mainLayout";
            this._mainLayout.RowCount = 1;
            this._mainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._mainLayout.Size = new System.Drawing.Size(914, 640);
            this._mainLayout.TabIndex = 0;
            // 
            // _formPanel
            // 
            this._formPanel.BackColor = System.Drawing.SystemColors.Control;
            this._formPanel.Controls.Add(this._formLayout);
            this._formPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._formPanel.Location = new System.Drawing.Point(552, 3);
            this._formPanel.Name = "_formPanel";
            this._formPanel.Size = new System.Drawing.Size(359, 634);
            this._formPanel.TabIndex = 1;
            // 
            // _formLayout
            // 
            this._formLayout.ColumnCount = 2;
            this._formLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this._formLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 65F));
            this._formLayout.Controls.Add(this.lblCode, 0, 0);
            this._formLayout.Controls.Add(this._txtCode, 1, 0);
            this._formLayout.Controls.Add(this.lblTitle, 0, 1);
            this._formLayout.Controls.Add(this._txtTitle, 1, 1);
            this._formLayout.Controls.Add(this.lblLecHrs, 0, 2);
            this._formLayout.Controls.Add(this._txtLectureHours, 1, 2);
            this._formLayout.Controls.Add(this.lblPracHrs, 0, 3);
            this._formLayout.Controls.Add(this._txtPracticalHours, 1, 3);
            this._formLayout.Controls.Add(this.lblIsPrac, 0, 4);
            this._formLayout.Controls.Add(this._chkIsPractical, 1, 4);
            this._formLayout.Controls.Add(this._buttonsPanel, 0, 6);
            this._formLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this._formLayout.Location = new System.Drawing.Point(0, 0);
            this._formLayout.Name = "_formLayout";
            this._formLayout.Padding = new System.Windows.Forms.Padding(11);
            this._formLayout.RowCount = 7;
            this._formLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this._formLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this._formLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this._formLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this._formLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this._formLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this._formLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._formLayout.Size = new System.Drawing.Size(359, 634);
            this._formLayout.TabIndex = 0;
            this._formLayout.Paint += new System.Windows.Forms.PaintEventHandler(this._formLayout_Paint);
            // 
            // lblCode
            // 
            this.lblCode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCode.Location = new System.Drawing.Point(234, 11);
            this.lblCode.Name = "lblCode";
            this.lblCode.Size = new System.Drawing.Size(111, 35);
            this.lblCode.TabIndex = 0;
            this.lblCode.Text = "رمز المقرر";
            this.lblCode.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _txtCode
            // 
            this._txtCode.Dock = System.Windows.Forms.DockStyle.Fill;
            this._txtCode.Font = new System.Drawing.Font("Segoe UI", 9F);
            this._txtCode.Location = new System.Drawing.Point(14, 14);
            this._txtCode.Name = "_txtCode";
            this._txtCode.Size = new System.Drawing.Size(214, 27);
            this._txtCode.TabIndex = 1;
            this._txtCode.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this._txtCode.TextChanged += new System.EventHandler(this._txtCode_TextChanged);
            // 
            // lblTitle
            // 
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTitle.Location = new System.Drawing.Point(234, 46);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(111, 35);
            this.lblTitle.TabIndex = 2;
            this.lblTitle.Text = "اسم المقرر";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _txtTitle
            // 
            this._txtTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this._txtTitle.Font = new System.Drawing.Font("Segoe UI", 9F);
            this._txtTitle.Location = new System.Drawing.Point(14, 49);
            this._txtTitle.Name = "_txtTitle";
            this._txtTitle.Size = new System.Drawing.Size(214, 27);
            this._txtTitle.TabIndex = 3;
            this._txtTitle.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblLecHrs
            // 
            this.lblLecHrs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblLecHrs.Location = new System.Drawing.Point(234, 81);
            this.lblLecHrs.Name = "lblLecHrs";
            this.lblLecHrs.Size = new System.Drawing.Size(111, 35);
            this.lblLecHrs.TabIndex = 4;
            this.lblLecHrs.Text = "ساعات نظري";
            this.lblLecHrs.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _txtLectureHours
            // 
            this._txtLectureHours.Dock = System.Windows.Forms.DockStyle.Fill;
            this._txtLectureHours.Font = new System.Drawing.Font("Segoe UI", 9F);
            this._txtLectureHours.Location = new System.Drawing.Point(14, 84);
            this._txtLectureHours.Name = "_txtLectureHours";
            this._txtLectureHours.Size = new System.Drawing.Size(214, 27);
            this._txtLectureHours.TabIndex = 5;
            this._txtLectureHours.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblPracHrs
            // 
            this.lblPracHrs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblPracHrs.Location = new System.Drawing.Point(234, 116);
            this.lblPracHrs.Name = "lblPracHrs";
            this.lblPracHrs.Size = new System.Drawing.Size(111, 35);
            this.lblPracHrs.TabIndex = 6;
            this.lblPracHrs.Text = "ساعات عملي";
            this.lblPracHrs.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _txtPracticalHours
            // 
            this._txtPracticalHours.Dock = System.Windows.Forms.DockStyle.Fill;
            this._txtPracticalHours.Font = new System.Drawing.Font("Segoe UI", 9F);
            this._txtPracticalHours.Location = new System.Drawing.Point(14, 119);
            this._txtPracticalHours.Name = "_txtPracticalHours";
            this._txtPracticalHours.Size = new System.Drawing.Size(214, 27);
            this._txtPracticalHours.TabIndex = 7;
            this._txtPracticalHours.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblIsPrac
            // 
            this.lblIsPrac.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblIsPrac.Location = new System.Drawing.Point(234, 151);
            this.lblIsPrac.Name = "lblIsPrac";
            this.lblIsPrac.Size = new System.Drawing.Size(111, 35);
            this.lblIsPrac.TabIndex = 8;
            this.lblIsPrac.Text = "هل له عملي؟";
            this.lblIsPrac.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _chkIsPractical
            // 
            this._chkIsPractical.AutoSize = true;
            this._chkIsPractical.Checked = true;
            this._chkIsPractical.CheckState = System.Windows.Forms.CheckState.Checked;
            this._chkIsPractical.Dock = System.Windows.Forms.DockStyle.Fill;
            this._chkIsPractical.Location = new System.Drawing.Point(14, 154);
            this._chkIsPractical.Name = "_chkIsPractical";
            this._chkIsPractical.Size = new System.Drawing.Size(214, 29);
            this._chkIsPractical.TabIndex = 9;
            this._chkIsPractical.Text = "نعم";
            // 
            // _buttonsPanel
            // 
            this._buttonsPanel.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this._formLayout.SetColumnSpan(this._buttonsPanel, 2);
            this._buttonsPanel.Controls.Add(this._btnDelete);
            this._buttonsPanel.Controls.Add(this._btnSave);
            this._buttonsPanel.Controls.Add(this._btnNew);
            this._buttonsPanel.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this._buttonsPanel.Location = new System.Drawing.Point(14, 217);
            this._buttonsPanel.Name = "_buttonsPanel";
            this._buttonsPanel.Size = new System.Drawing.Size(331, 42);
            this._buttonsPanel.TabIndex = 10;
            this._buttonsPanel.Paint += new System.Windows.Forms.PaintEventHandler(this._buttonsPanel_Paint);
            // 
            // _btnDelete
            // 
            this._btnDelete.BackColor = System.Drawing.Color.Red;
            this._btnDelete.ForeColor = System.Drawing.Color.White;
            this._btnDelete.Location = new System.Drawing.Point(3, 3);
            this._btnDelete.Name = "_btnDelete";
            this._btnDelete.Size = new System.Drawing.Size(103, 32);
            this._btnDelete.TabIndex = 0;
            this._btnDelete.Text = "حذف";
            this._btnDelete.UseVisualStyleBackColor = false;
            this._btnDelete.Click += new System.EventHandler(this.BtnDelete_Click);
            // 
            // _btnSave
            // 
            this._btnSave.BackColor = System.Drawing.Color.Green;
            this._btnSave.ForeColor = System.Drawing.Color.White;
            this._btnSave.Location = new System.Drawing.Point(112, 3);
            this._btnSave.Name = "_btnSave";
            this._btnSave.Size = new System.Drawing.Size(103, 32);
            this._btnSave.TabIndex = 1;
            this._btnSave.Text = "حفظ";
            this._btnSave.UseVisualStyleBackColor = false;
            this._btnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // _btnNew
            // 
            this._btnNew.BackColor = System.Drawing.Color.LightBlue;
            this._btnNew.Location = new System.Drawing.Point(221, 3);
            this._btnNew.Name = "_btnNew";
            this._btnNew.Size = new System.Drawing.Size(103, 32);
            this._btnNew.TabIndex = 2;
            this._btnNew.Text = "جديد";
            this._btnNew.UseVisualStyleBackColor = false;
            this._btnNew.Click += new System.EventHandler(this.BtnNew_Click);
            // 
            // _dataGridView
            // 
            this._dataGridView.AllowUserToAddRows = false;
            this._dataGridView.AllowUserToDeleteRows = false;
            this._dataGridView.BackgroundColor = System.Drawing.SystemColors.Window;
            this._dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this._dataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this._dataGridView.Location = new System.Drawing.Point(3, 3);
            this._dataGridView.MultiSelect = false;
            this._dataGridView.Name = "_dataGridView";
            this._dataGridView.ReadOnly = true;
            this._dataGridView.RowHeadersWidth = 51;
            this._dataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this._dataGridView.Size = new System.Drawing.Size(543, 634);
            this._dataGridView.TabIndex = 0;
            this._dataGridView.SelectionChanged += new System.EventHandler(this.DataGridView_SelectionChanged);
            // 
            // CoursManagementControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._mainLayout);
            this.Name = "CoursManagementControl";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Size = new System.Drawing.Size(914, 640);
            this._mainLayout.ResumeLayout(false);
            this._formPanel.ResumeLayout(false);
            this._formLayout.ResumeLayout(false);
            this._formLayout.PerformLayout();
            this._buttonsPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._dataGridView)).EndInit();
            this.ResumeLayout(false);

        }
        #endregion

        private System.Windows.Forms.TableLayoutPanel _mainLayout;
        private System.Windows.Forms.DataGridView _dataGridView;
        private System.Windows.Forms.Panel _formPanel;
        private System.Windows.Forms.TableLayoutPanel _formLayout;
        private System.Windows.Forms.TextBox _txtCode;
        private System.Windows.Forms.TextBox _txtTitle;
        private System.Windows.Forms.TextBox _txtLectureHours;
        private System.Windows.Forms.TextBox _txtPracticalHours;
        private System.Windows.Forms.CheckBox _chkIsPractical;
        private System.Windows.Forms.FlowLayoutPanel _buttonsPanel;
        private System.Windows.Forms.Button _btnNew;
        private System.Windows.Forms.Button _btnSave;
        private System.Windows.Forms.Button _btnDelete;
        private System.Windows.Forms.Label lblCode;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblLecHrs;
        private System.Windows.Forms.Label lblPracHrs;
        private System.Windows.Forms.Label lblIsPrac;

        // Columns definitions - يمكن حذفها إذا كانت تُنشأ تلقائياً من الداتابيس
        private System.Windows.Forms.DataGridViewTextBoxColumn colID;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTitle;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLecHrs;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPracHrs;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colIsPrac;
    }
}