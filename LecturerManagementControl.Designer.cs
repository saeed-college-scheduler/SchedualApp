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
            this._dataGridsLayout = new System.Windows.Forms.TableLayoutPanel();
            this._lecturerFormPanel = new System.Windows.Forms.Panel();
            this._btnDelete = new System.Windows.Forms.Button();
            this._btnSave = new System.Windows.Forms.Button();
            this._btnNew = new System.Windows.Forms.Button();
            this._txtAcademicRank = new System.Windows.Forms.TextBox();
            this._txtLastName = new System.Windows.Forms.TextBox();
            this._txtFirstName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this._courseLecturerGrid = new System.Windows.Forms.DataGridView();
            this._availabilityGrid = new System.Windows.Forms.DataGridView();
            this._lecturerGrid = new System.Windows.Forms.DataGridView();
            this._detailsLayout = new System.Windows.Forms.TableLayoutPanel();
            this._mainLayout = new System.Windows.Forms.TableLayoutPanel();
            this._formsLayout = new System.Windows.Forms.TableLayoutPanel();
            this._availabilityFormPanel = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this._availabilityListBox = new System.Windows.Forms.CheckedListBox();
            this._courseLecturerFormPanel = new System.Windows.Forms.Panel();
            this.label8 = new System.Windows.Forms.Label();
            this._courseListBox = new System.Windows.Forms.CheckedListBox();
            this._dataGridsLayout.SuspendLayout();
            this._lecturerFormPanel.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._courseLecturerGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._availabilityGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._lecturerGrid)).BeginInit();
            this._detailsLayout.SuspendLayout();
            this._mainLayout.SuspendLayout();
            this._formsLayout.SuspendLayout();
            this._availabilityFormPanel.SuspendLayout();
            this._courseLecturerFormPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // _dataGridsLayout
            // 
            this._dataGridsLayout.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this._dataGridsLayout.ColumnCount = 1;
            this._dataGridsLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._dataGridsLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this._dataGridsLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this._dataGridsLayout.Controls.Add(this._lecturerFormPanel, 0, 1);
            this._dataGridsLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this._dataGridsLayout.Location = new System.Drawing.Point(762, 6);
            this._dataGridsLayout.Margin = new System.Windows.Forms.Padding(4);
            this._dataGridsLayout.Name = "_dataGridsLayout";
            this._dataGridsLayout.RowCount = 2;
            this._dataGridsLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 3.693931F));
            this._dataGridsLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 96.30607F));
            this._dataGridsLayout.Size = new System.Drawing.Size(634, 401);
            this._dataGridsLayout.TabIndex = 0;
            // 
            // _lecturerFormPanel
            // 
            this._lecturerFormPanel.Controls.Add(this._btnDelete);
            this._lecturerFormPanel.Controls.Add(this._btnSave);
            this._lecturerFormPanel.Controls.Add(this._btnNew);
            this._lecturerFormPanel.Controls.Add(this._txtAcademicRank);
            this._lecturerFormPanel.Controls.Add(this._txtLastName);
            this._lecturerFormPanel.Controls.Add(this._txtFirstName);
            this._lecturerFormPanel.Controls.Add(this.label4);
            this._lecturerFormPanel.Controls.Add(this.label3);
            this._lecturerFormPanel.Controls.Add(this.label2);
            this._lecturerFormPanel.Controls.Add(this.label1);
            this._lecturerFormPanel.Location = new System.Drawing.Point(5, 20);
            this._lecturerFormPanel.Margin = new System.Windows.Forms.Padding(4);
            this._lecturerFormPanel.Name = "_lecturerFormPanel";
            this._lecturerFormPanel.Size = new System.Drawing.Size(624, 376);
            this._lecturerFormPanel.TabIndex = 0;
            // 
            // _btnDelete
            // 
            this._btnDelete.BackColor = System.Drawing.Color.Red;
            this._btnDelete.Location = new System.Drawing.Point(43, 150);
            this._btnDelete.Margin = new System.Windows.Forms.Padding(4);
            this._btnDelete.Name = "_btnDelete";
            this._btnDelete.Size = new System.Drawing.Size(119, 28);
            this._btnDelete.TabIndex = 12;
            this._btnDelete.Text = "حذف";
            this._btnDelete.UseVisualStyleBackColor = false;
            // 
            // _btnSave
            // 
            this._btnSave.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this._btnSave.Location = new System.Drawing.Point(188, 150);
            this._btnSave.Margin = new System.Windows.Forms.Padding(4);
            this._btnSave.Name = "_btnSave";
            this._btnSave.Size = new System.Drawing.Size(103, 28);
            this._btnSave.TabIndex = 11;
            this._btnSave.Text = "حفظ";
            this._btnSave.UseVisualStyleBackColor = false;
            // 
            // _btnNew
            // 
            this._btnNew.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this._btnNew.Location = new System.Drawing.Point(316, 150);
            this._btnNew.Margin = new System.Windows.Forms.Padding(4);
            this._btnNew.Name = "_btnNew";
            this._btnNew.Size = new System.Drawing.Size(110, 28);
            this._btnNew.TabIndex = 10;
            this._btnNew.Text = "جديد";
            this._btnNew.UseVisualStyleBackColor = false;
            // 
            // _txtAcademicRank
            // 
            this._txtAcademicRank.Location = new System.Drawing.Point(107, 84);
            this._txtAcademicRank.Margin = new System.Windows.Forms.Padding(4);
            this._txtAcademicRank.Name = "_txtAcademicRank";
            this._txtAcademicRank.Size = new System.Drawing.Size(152, 22);
            this._txtAcademicRank.TabIndex = 7;
            this._txtAcademicRank.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // _txtLastName
            // 
            this._txtLastName.Location = new System.Drawing.Point(107, 48);
            this._txtLastName.Margin = new System.Windows.Forms.Padding(4);
            this._txtLastName.Name = "_txtLastName";
            this._txtLastName.Size = new System.Drawing.Size(152, 22);
            this._txtLastName.TabIndex = 6;
            this._txtLastName.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // _txtFirstName
            // 
            this._txtFirstName.Location = new System.Drawing.Point(105, 12);
            this._txtFirstName.Margin = new System.Windows.Forms.Padding(4);
            this._txtFirstName.Name = "_txtFirstName";
            this._txtFirstName.Size = new System.Drawing.Size(152, 22);
            this._txtFirstName.TabIndex = 5;
            this._txtFirstName.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(267, 84);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(85, 16);
            this.label4.TabIndex = 3;
            this.label4.Text = "الرتبة الأكاديمية:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(267, 48);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 16);
            this.label3.TabIndex = 2;
            this.label3.Text = "اسم العائلة:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(266, 12);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 16);
            this.label2.TabIndex = 1;
            this.label2.Text = "الاسم الأول:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 12);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 16);
            this.label1.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this._courseLecturerGrid, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this._availabilityGrid, 0, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(5, 416);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 356F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(748, 356);
            this.tableLayoutPanel1.TabIndex = 1;
            this.tableLayoutPanel1.Paint += new System.Windows.Forms.PaintEventHandler(this.tableLayoutPanel1_Paint);
            // 
            // _courseLecturerGrid
            // 
            this._courseLecturerGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this._courseLecturerGrid.Location = new System.Drawing.Point(8, 4);
            this._courseLecturerGrid.Margin = new System.Windows.Forms.Padding(4);
            this._courseLecturerGrid.Name = "_courseLecturerGrid";
            this._courseLecturerGrid.RowHeadersWidth = 51;
            this._courseLecturerGrid.Size = new System.Drawing.Size(362, 291);
            this._courseLecturerGrid.TabIndex = 0;
            // 
            // _availabilityGrid
            // 
            this._availabilityGrid.AllowUserToOrderColumns = true;
            this._availabilityGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this._availabilityGrid.Location = new System.Drawing.Point(378, 4);
            this._availabilityGrid.Margin = new System.Windows.Forms.Padding(4);
            this._availabilityGrid.Name = "_availabilityGrid";
            this._availabilityGrid.RowHeadersWidth = 51;
            this._availabilityGrid.Size = new System.Drawing.Size(366, 292);
            this._availabilityGrid.TabIndex = 0;
            // 
            // _lecturerGrid
            // 
            this._lecturerGrid.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._lecturerGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this._lecturerGrid.Location = new System.Drawing.Point(4, 4);
            this._lecturerGrid.Margin = new System.Windows.Forms.Padding(4);
            this._lecturerGrid.Name = "_lecturerGrid";
            this._lecturerGrid.RowHeadersWidth = 51;
            this._lecturerGrid.Size = new System.Drawing.Size(738, 372);
            this._lecturerGrid.TabIndex = 0;
            // 
            // _detailsLayout
            // 
            this._detailsLayout.ColumnCount = 1;
            this._detailsLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this._detailsLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this._detailsLayout.Controls.Add(this._lecturerGrid, 0, 0);
            this._detailsLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this._detailsLayout.Location = new System.Drawing.Point(6, 6);
            this._detailsLayout.Margin = new System.Windows.Forms.Padding(4);
            this._detailsLayout.Name = "_detailsLayout";
            this._detailsLayout.RowCount = 1;
            this._detailsLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._detailsLayout.Size = new System.Drawing.Size(746, 401);
            this._detailsLayout.TabIndex = 0;
            // 
            // _mainLayout
            // 
            this._mainLayout.AutoSize = true;
            this._mainLayout.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this._mainLayout.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Outset;
            this._mainLayout.ColumnCount = 2;
            this._mainLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 46.01687F));
            this._mainLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 53.98313F));
            this._mainLayout.Controls.Add(this._dataGridsLayout, 0, 0);
            this._mainLayout.Controls.Add(this._detailsLayout, 1, 0);
            this._mainLayout.Controls.Add(this._formsLayout, 0, 1);
            this._mainLayout.Controls.Add(this.tableLayoutPanel1, 1, 1);
            this._mainLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this._mainLayout.Location = new System.Drawing.Point(0, 0);
            this._mainLayout.Margin = new System.Windows.Forms.Padding(4);
            this._mainLayout.Name = "_mainLayout";
            this._mainLayout.RowCount = 2;
            this._mainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 53.11653F));
            this._mainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 46.88347F));
            this._mainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this._mainLayout.Size = new System.Drawing.Size(1402, 777);
            this._mainLayout.TabIndex = 1;
            // 
            // _formsLayout
            // 
            this._formsLayout.ColumnCount = 2;
            this._formsLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this._formsLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this._formsLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this._formsLayout.Controls.Add(this._availabilityFormPanel, 1, 0);
            this._formsLayout.Controls.Add(this._courseLecturerFormPanel, 0, 0);
            this._formsLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this._formsLayout.Location = new System.Drawing.Point(762, 417);
            this._formsLayout.Margin = new System.Windows.Forms.Padding(4);
            this._formsLayout.Name = "_formsLayout";
            this._formsLayout.RowCount = 1;
            this._formsLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._formsLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 336F));
            this._formsLayout.Size = new System.Drawing.Size(634, 354);
            this._formsLayout.TabIndex = 0;
            // 
            // _availabilityFormPanel
            // 
            this._availabilityFormPanel.Controls.Add(this.label6);
            this._availabilityFormPanel.Controls.Add(this._availabilityListBox);
            this._availabilityFormPanel.Location = new System.Drawing.Point(4, 4);
            this._availabilityFormPanel.Margin = new System.Windows.Forms.Padding(4);
            this._availabilityFormPanel.Name = "_availabilityFormPanel";
            this._availabilityFormPanel.Size = new System.Drawing.Size(309, 346);
            this._availabilityFormPanel.TabIndex = 1;
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(169, 12);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(83, 16);
            this.label6.TabIndex = 1;
            this.label6.Text = "الأوقات المتاحة:";
            this.label6.Click += new System.EventHandler(this.label6_Click);
            // 
            // _availabilityListBox
            // 
            this._availabilityListBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._availabilityListBox.FormattingEnabled = true;
            this._availabilityListBox.Location = new System.Drawing.Point(10, 32);
            this._availabilityListBox.Margin = new System.Windows.Forms.Padding(4);
            this._availabilityListBox.Name = "_availabilityListBox";
            this._availabilityListBox.Size = new System.Drawing.Size(282, 310);
            this._availabilityListBox.TabIndex = 0;
            // 
            // _courseLecturerFormPanel
            // 
            this._courseLecturerFormPanel.Controls.Add(this.label8);
            this._courseLecturerFormPanel.Controls.Add(this._courseListBox);
            this._courseLecturerFormPanel.Location = new System.Drawing.Point(321, 4);
            this._courseLecturerFormPanel.Margin = new System.Windows.Forms.Padding(4);
            this._courseLecturerFormPanel.Name = "_courseLecturerFormPanel";
            this._courseLecturerFormPanel.Size = new System.Drawing.Size(309, 346);
            this._courseLecturerFormPanel.TabIndex = 2;
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(167, 12);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(90, 16);
            this.label8.TabIndex = 1;
            this.label8.Text = "المقررات المسندة:";
            // 
            // _courseListBox
            // 
            this._courseListBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._courseListBox.FormattingEnabled = true;
            this._courseListBox.Location = new System.Drawing.Point(12, 32);
            this._courseListBox.Margin = new System.Windows.Forms.Padding(4);
            this._courseListBox.Name = "_courseListBox";
            this._courseListBox.Size = new System.Drawing.Size(285, 310);
            this._courseListBox.TabIndex = 0;
            // 
            // LecturerManagementControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._mainLayout);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "LecturerManagementControl";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Size = new System.Drawing.Size(1402, 777);
            this._dataGridsLayout.ResumeLayout(false);
            this._lecturerFormPanel.ResumeLayout(false);
            this._lecturerFormPanel.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._courseLecturerGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._availabilityGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._lecturerGrid)).EndInit();
            this._detailsLayout.ResumeLayout(false);
            this._mainLayout.ResumeLayout(false);
            this._formsLayout.ResumeLayout(false);
            this._availabilityFormPanel.ResumeLayout(false);
            this._availabilityFormPanel.PerformLayout();
            this._courseLecturerFormPanel.ResumeLayout(false);
            this._courseLecturerFormPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel _dataGridsLayout;
        private System.Windows.Forms.Panel _lecturerFormPanel;
        private System.Windows.Forms.Button _btnDelete;
        private System.Windows.Forms.Button _btnSave;
        private System.Windows.Forms.Button _btnNew;
        private System.Windows.Forms.TextBox _txtAcademicRank;
        private System.Windows.Forms.TextBox _txtLastName;
        private System.Windows.Forms.TextBox _txtFirstName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.DataGridView _courseLecturerGrid;
        private System.Windows.Forms.DataGridView _lecturerGrid;
        private System.Windows.Forms.TableLayoutPanel _detailsLayout;
        private System.Windows.Forms.TableLayoutPanel _mainLayout;
        private System.Windows.Forms.TableLayoutPanel _formsLayout;
        private System.Windows.Forms.Panel _availabilityFormPanel;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckedListBox _availabilityListBox;
        private System.Windows.Forms.Panel _courseLecturerFormPanel;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.CheckedListBox _courseListBox;
        private System.Windows.Forms.DataGridView _availabilityGrid;
    }
}
