namespace SchedualApp
{
    partial class LecturerManagementControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this._mainLayout = new System.Windows.Forms.TableLayoutPanel();
            this._dataGridView = new System.Windows.Forms.DataGridView();
            this._formPanel = new System.Windows.Forms.Panel();
            this._btnDelete = new System.Windows.Forms.Button();
            this._btnSave = new System.Windows.Forms.Button();
            this._btnNew = new System.Windows.Forms.Button();
            this._chkIsActive = new System.Windows.Forms.CheckBox();
            this._txtMaxWorkload = new System.Windows.Forms.TextBox();
            this._txtAcademicRank = new System.Windows.Forms.TextBox();
            this._txtLastName = new System.Windows.Forms.TextBox();
            this._txtFirstName = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this._mainLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._dataGridView)).BeginInit();
            this._formPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // _mainLayout
            // 
            this._mainLayout.ColumnCount = 2;
            this._mainLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 65F));
            this._mainLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this._mainLayout.Controls.Add(this._dataGridView, 0, 0);
            this._mainLayout.Controls.Add(this._formPanel, 1, 0);
            this._mainLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this._mainLayout.Location = new System.Drawing.Point(0, 0);
            this._mainLayout.Name = "_mainLayout";
            this._mainLayout.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this._mainLayout.RowCount = 1;
            this._mainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._mainLayout.Size = new System.Drawing.Size(800, 600);
            this._mainLayout.TabIndex = 0;
            // 
            // _dataGridView
            // 
            this._dataGridView.AllowUserToAddRows = false;
            this._dataGridView.AllowUserToDeleteRows = false;
            this._dataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this._dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this._dataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this._dataGridView.Location = new System.Drawing.Point(283, 3);
            this._dataGridView.MultiSelect = false;
            this._dataGridView.Name = "_dataGridView";
            this._dataGridView.ReadOnly = true;
            this._dataGridView.RowHeadersWidth = 51;
            this._dataGridView.RowTemplate.Height = 24;
            this._dataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this._dataGridView.Size = new System.Drawing.Size(514, 594);
            this._dataGridView.TabIndex = 0;
            this._dataGridView.SelectionChanged += new System.EventHandler(this.DataGridView_SelectionChanged);
            // 
            // _formPanel
            // 
            this._formPanel.Controls.Add(this.label1);
            this._formPanel.Controls.Add(this.label2);
            this._formPanel.Controls.Add(this.label3);
            this._formPanel.Controls.Add(this.label4);
            this._formPanel.Controls.Add(this.label5);
            this._formPanel.Controls.Add(this._txtFirstName);
            this._formPanel.Controls.Add(this._txtLastName);
            this._formPanel.Controls.Add(this._txtAcademicRank);
            this._formPanel.Controls.Add(this._txtMaxWorkload);
            this._formPanel.Controls.Add(this._chkIsActive);
            this._formPanel.Controls.Add(this._btnNew);
            this._formPanel.Controls.Add(this._btnSave);
            this._formPanel.Controls.Add(this._btnDelete);
            this._formPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._formPanel.Location = new System.Drawing.Point(3, 3);
            this._formPanel.Name = "_formPanel";
            this._formPanel.Size = new System.Drawing.Size(274, 594);
            this._formPanel.TabIndex = 1;
            // 
            // _btnDelete
            // 
            this._btnDelete.Location = new System.Drawing.Point(15, 360);
            this._btnDelete.Name = "_btnDelete";
            this._btnDelete.Size = new System.Drawing.Size(244, 35);
            this._btnDelete.TabIndex = 12;
            this._btnDelete.Text = "حذف";
            this._btnDelete.UseVisualStyleBackColor = true;
            this._btnDelete.Click += new System.EventHandler(this.BtnDelete_Click);
            // 
            // _btnSave
            // 
            this._btnSave.Location = new System.Drawing.Point(15, 319);
            this._btnSave.Name = "_btnSave";
            this._btnSave.Size = new System.Drawing.Size(244, 35);
            this._btnSave.TabIndex = 11;
            this._btnSave.Text = "حفظ";
            this._btnSave.UseVisualStyleBackColor = true;
            this._btnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // _btnNew
            // 
            this._btnNew.Location = new System.Drawing.Point(15, 20);
            this._btnNew.Name = "_btnNew";
            this._btnNew.Size = new System.Drawing.Size(244, 30);
            this._btnNew.TabIndex = 0;
            this._btnNew.Text = "جديد";
            this._btnNew.UseVisualStyleBackColor = true;
            this._btnNew.Click += new System.EventHandler(this.BtnNew_Click);
            // 
            // _chkIsActive
            // 
            this._chkIsActive.AutoSize = true;
            this._chkIsActive.Location = new System.Drawing.Point(15, 275);
            this._chkIsActive.Name = "_chkIsActive";
            this._chkIsActive.Size = new System.Drawing.Size(56, 20);
            this._chkIsActive.TabIndex = 10;
            this._chkIsActive.Text = "نشط";
            this._chkIsActive.UseVisualStyleBackColor = true;
            // 
            // _txtMaxWorkload
            // 
            this._txtMaxWorkload.Location = new System.Drawing.Point(15, 240);
            this._txtMaxWorkload.Name = "_txtMaxWorkload";
            this._txtMaxWorkload.Size = new System.Drawing.Size(244, 22);
            this._txtMaxWorkload.TabIndex = 8;
            // 
            // _txtAcademicRank
            // 
            this._txtAcademicRank.Location = new System.Drawing.Point(15, 185);
            this._txtAcademicRank.Name = "_txtAcademicRank";
            this._txtAcademicRank.Size = new System.Drawing.Size(244, 22);
            this._txtAcademicRank.TabIndex = 6;
            // 
            // _txtLastName
            // 
            this._txtLastName.Location = new System.Drawing.Point(15, 130);
            this._txtLastName.Name = "_txtLastName";
            this._txtLastName.Size = new System.Drawing.Size(244, 22);
            this._txtLastName.TabIndex = 4;
            // 
            // _txtFirstName
            // 
            this._txtFirstName.Location = new System.Drawing.Point(15, 75);
            this._txtFirstName.Name = "_txtFirstName";
            this._txtFirstName.Size = new System.Drawing.Size(244, 22);
            this._txtFirstName.TabIndex = 2;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(180, 221);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(79, 16);
            this.label5.TabIndex = 7;
            this.label5.Text = "العبء الأقصى:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(177, 166);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(82, 16);
            this.label4.TabIndex = 5;
            this.label4.Text = "الرتبة الأكاديمية:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(188, 111);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 16);
            this.label3.TabIndex = 3;
            this.label3.Text = "اسم العائلة:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(188, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 16);
            this.label2.TabIndex = 1;
            this.label2.Text = "الاسم الأول:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 276);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 16);
            this.label1.TabIndex = 9;
            // 
            // LecturerManagementControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._mainLayout);
            this.Name = "LecturerManagementControl";
            this.Size = new System.Drawing.Size(800, 600);
            this._mainLayout.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._dataGridView)).EndInit();
            this._formPanel.ResumeLayout(false);
            this._formPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel _mainLayout;
        private System.Windows.Forms.DataGridView _dataGridView;
        private System.Windows.Forms.Panel _formPanel;
        private System.Windows.Forms.Button _btnDelete;
        private System.Windows.Forms.Button _btnSave;
        private System.Windows.Forms.Button _btnNew;
        private System.Windows.Forms.CheckBox _chkIsActive;
        private System.Windows.Forms.TextBox _txtMaxWorkload;
        private System.Windows.Forms.TextBox _txtAcademicRank;
        private System.Windows.Forms.TextBox _txtLastName;
        private System.Windows.Forms.TextBox _txtFirstName;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}
