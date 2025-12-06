namespace SchedualApp
{
    partial class CourseLevelManagementControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing && (components != null))
        //    {
        //        components.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panelForm = new System.Windows.Forms.Panel();
            this.cmbCourse = new System.Windows.Forms.ComboBox();
            this.coursesBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.schedualDBDataSet3 = new SchedualApp.SchedualDBDataSet3();
            this.lblCourse = new System.Windows.Forms.Label();
            this.cmbLevel = new System.Windows.Forms.ComboBox();
            this.lblLevel = new System.Windows.Forms.Label();
            this.cmbDepartment = new System.Windows.Forms.ComboBox();
            this.lblDepartment = new System.Windows.Forms.Label();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnNew = new System.Windows.Forms.Button();
            this.dataGridViewCourseLevels = new System.Windows.Forms.DataGridView();
            this.coursesTableAdapter = new SchedualApp.SchedualDBDataSet3TableAdapters.CoursesTableAdapter();
            this.tableLayoutPanel1.SuspendLayout();
            this.panelForm.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.coursesBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.schedualDBDataSet3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewCourseLevels)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tableLayoutPanel1.Controls.Add(this.panelForm, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.dataGridViewCourseLevels, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(800, 600);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // panelForm
            // 
            this.panelForm.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.panelForm.Controls.Add(this.cmbCourse);
            this.panelForm.Controls.Add(this.lblCourse);
            this.panelForm.Controls.Add(this.cmbLevel);
            this.panelForm.Controls.Add(this.lblLevel);
            this.panelForm.Controls.Add(this.cmbDepartment);
            this.panelForm.Controls.Add(this.lblDepartment);
            this.panelForm.Controls.Add(this.btnDelete);
            this.panelForm.Controls.Add(this.btnSave);
            this.panelForm.Controls.Add(this.btnNew);
            this.panelForm.Location = new System.Drawing.Point(483, 3);
            this.panelForm.Name = "panelForm";
            this.panelForm.Size = new System.Drawing.Size(314, 594);
            this.panelForm.TabIndex = 1;
            // 
            // cmbCourse
            // 
            this.cmbCourse.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.cmbCourse.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.coursesBindingSource, "Title", true));
            this.cmbCourse.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCourse.FormattingEnabled = true;
            this.cmbCourse.Location = new System.Drawing.Point(15, 159);
            this.cmbCourse.Name = "cmbCourse";
            this.cmbCourse.Size = new System.Drawing.Size(199, 24);
            this.cmbCourse.TabIndex = 6;
            // 
            // coursesBindingSource
            // 
            this.coursesBindingSource.DataMember = "Courses";
            this.coursesBindingSource.DataSource = this.schedualDBDataSet3;
            // 
            // schedualDBDataSet3
            // 
            this.schedualDBDataSet3.DataSetName = "SchedualDBDataSet3";
            this.schedualDBDataSet3.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // lblCourse
            // 
            this.lblCourse.AutoSize = true;
            this.lblCourse.Location = new System.Drawing.Point(260, 162);
            this.lblCourse.Name = "lblCourse";
            this.lblCourse.Size = new System.Drawing.Size(38, 16);
            this.lblCourse.TabIndex = 5;
            this.lblCourse.Text = "المادة:";
            // 
            // cmbLevel
            // 
            this.cmbLevel.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.cmbLevel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLevel.FormattingEnabled = true;
            this.cmbLevel.Location = new System.Drawing.Point(15, 99);
            this.cmbLevel.Name = "cmbLevel";
            this.cmbLevel.Size = new System.Drawing.Size(199, 24);
            this.cmbLevel.TabIndex = 4;
            // 
            // lblLevel
            // 
            this.lblLevel.AutoSize = true;
            this.lblLevel.Location = new System.Drawing.Point(247, 102);
            this.lblLevel.Name = "lblLevel";
            this.lblLevel.Size = new System.Drawing.Size(51, 16);
            this.lblLevel.TabIndex = 3;
            this.lblLevel.Text = "المستوى:";
            // 
            // cmbDepartment
            // 
            this.cmbDepartment.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.cmbDepartment.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDepartment.FormattingEnabled = true;
            this.cmbDepartment.Location = new System.Drawing.Point(15, 39);
            this.cmbDepartment.Name = "cmbDepartment";
            this.cmbDepartment.Size = new System.Drawing.Size(199, 24);
            this.cmbDepartment.TabIndex = 2;
            // 
            // lblDepartment
            // 
            this.lblDepartment.AutoSize = true;
            this.lblDepartment.Location = new System.Drawing.Point(263, 42);
            this.lblDepartment.Name = "lblDepartment";
            this.lblDepartment.Size = new System.Drawing.Size(35, 16);
            this.lblDepartment.TabIndex = 1;
            this.lblDepartment.Text = "القسم:";
            // 
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.Color.Red;
            this.btnDelete.Location = new System.Drawing.Point(15, 306);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(280, 35);
            this.btnDelete.TabIndex = 9;
            this.btnDelete.Text = "حذف";
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new System.EventHandler(this.BtnDelete_ClickAsync);
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.SystemColors.ControlDark;
            this.btnSave.Location = new System.Drawing.Point(15, 265);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(280, 35);
            this.btnSave.TabIndex = 8;
            this.btnSave.Text = "حفظ";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.BtnSave_ClickAsync);
            // 
            // btnNew
            // 
            this.btnNew.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnNew.Location = new System.Drawing.Point(15, 229);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(280, 30);
            this.btnNew.TabIndex = 0;
            this.btnNew.Text = "جديد";
            this.btnNew.UseVisualStyleBackColor = false;
            this.btnNew.Click += new System.EventHandler(this.BtnNew_Click);
            // 
            // dataGridViewCourseLevels
            // 
            this.dataGridViewCourseLevels.AllowUserToAddRows = false;
            this.dataGridViewCourseLevels.AllowUserToDeleteRows = false;
            this.dataGridViewCourseLevels.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewCourseLevels.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewCourseLevels.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewCourseLevels.Location = new System.Drawing.Point(3, 3);
            this.dataGridViewCourseLevels.MultiSelect = false;
            this.dataGridViewCourseLevels.Name = "dataGridViewCourseLevels";
            this.dataGridViewCourseLevels.ReadOnly = true;
            this.dataGridViewCourseLevels.RowHeadersWidth = 51;
            this.dataGridViewCourseLevels.RowTemplate.Height = 24;
            this.dataGridViewCourseLevels.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewCourseLevels.Size = new System.Drawing.Size(474, 594);
            this.dataGridViewCourseLevels.TabIndex = 0;
            this.dataGridViewCourseLevels.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewCourseLevels_CellContentClick);
            this.dataGridViewCourseLevels.SelectionChanged += new System.EventHandler(this.DataGridViewCourseLevels_SelectionChanged);
            // 
            // coursesTableAdapter
            // 
            this.coursesTableAdapter.ClearBeforeFill = true;
            // 
            // CourseLevelManagementControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "CourseLevelManagementControl";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Size = new System.Drawing.Size(800, 600);
            this.Load += new System.EventHandler(this.CourseLevelManagementControl_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panelForm.ResumeLayout(false);
            this.panelForm.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.coursesBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.schedualDBDataSet3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewCourseLevels)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.DataGridView dataGridViewCourseLevels;
        private System.Windows.Forms.Panel panelForm;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.ComboBox cmbCourse;
        private System.Windows.Forms.Label lblCourse;
        private System.Windows.Forms.ComboBox cmbLevel;
        private System.Windows.Forms.Label lblLevel;
        private System.Windows.Forms.ComboBox cmbDepartment;
        private System.Windows.Forms.Label lblDepartment;
        private System.Windows.Forms.BindingSource coursesBindingSource;
        private SchedualDBDataSet3 schedualDBDataSet3;
        private SchedualDBDataSet3TableAdapters.CoursesTableAdapter coursesTableAdapter;
    }
}
