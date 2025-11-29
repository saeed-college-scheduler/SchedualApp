namespace SchedualApp
{
    partial class CourseLecturerManagementControl
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.dataGridViewCourseLecturers = new System.Windows.Forms.DataGridView();
            this.panelForm = new System.Windows.Forms.Panel();
            this.cmbTeachingType = new System.Windows.Forms.ComboBox();
            this.lblTeachingType = new System.Windows.Forms.Label();
            this.cmbLecturer = new System.Windows.Forms.ComboBox();
            this.lblLecturer = new System.Windows.Forms.Label();
            this.cmbCourse = new System.Windows.Forms.ComboBox();
            this.lblCourse = new System.Windows.Forms.Label();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnNew = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewCourseLecturers)).BeginInit();
            this.panelForm.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanel1.Controls.Add(this.dataGridViewCourseLecturers, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panelForm, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(800, 600);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // dataGridViewCourseLecturers
            // 
            this.dataGridViewCourseLecturers.AllowUserToAddRows = false;
            this.dataGridViewCourseLecturers.AllowUserToDeleteRows = false;
            this.dataGridViewCourseLecturers.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewCourseLecturers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewCourseLecturers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewCourseLecturers.Location = new System.Drawing.Point(3, 3);
            this.dataGridViewCourseLecturers.MultiSelect = false;
            this.dataGridViewCourseLecturers.Name = "dataGridViewCourseLecturers";
            this.dataGridViewCourseLecturers.ReadOnly = true;
            this.dataGridViewCourseLecturers.RowHeadersWidth = 51;
            this.dataGridViewCourseLecturers.RowTemplate.Height = 24;
            this.dataGridViewCourseLecturers.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewCourseLecturers.Size = new System.Drawing.Size(474, 594);
            this.dataGridViewCourseLecturers.TabIndex = 0;
            this.dataGridViewCourseLecturers.SelectionChanged += new System.EventHandler(this.DataGridViewCourseLecturers_SelectionChanged);
            // 
            // panelForm
            // 
            this.panelForm.Controls.Add(this.cmbTeachingType);
            this.panelForm.Controls.Add(this.lblTeachingType);
            this.panelForm.Controls.Add(this.cmbLecturer);
            this.panelForm.Controls.Add(this.lblLecturer);
            this.panelForm.Controls.Add(this.cmbCourse);
            this.panelForm.Controls.Add(this.lblCourse);
            this.panelForm.Controls.Add(this.btnDelete);
            this.panelForm.Controls.Add(this.btnSave);
            this.panelForm.Controls.Add(this.btnNew);
            this.panelForm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelForm.Location = new System.Drawing.Point(483, 3);
            this.panelForm.Name = "panelForm";
            this.panelForm.Size = new System.Drawing.Size(314, 594);
            this.panelForm.TabIndex = 1;
            // 
            // cmbTeachingType
            // 
            this.cmbTeachingType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTeachingType.FormattingEnabled = true;
            this.cmbTeachingType.Items.AddRange(new object[] {
            "Lecture",
            "Tutorial",
            "Lab"});
            this.cmbTeachingType.Location = new System.Drawing.Point(15, 196);
            this.cmbTeachingType.Name = "cmbTeachingType";
            this.cmbTeachingType.Size = new System.Drawing.Size(280, 24);
            this.cmbTeachingType.TabIndex = 6;
            // 
            // lblTeachingType
            // 
            this.lblTeachingType.AutoSize = true;
            this.lblTeachingType.Location = new System.Drawing.Point(12, 177);
            this.lblTeachingType.Name = "lblTeachingType";
            this.lblTeachingType.Size = new System.Drawing.Size(75, 16);
            this.lblTeachingType.TabIndex = 5;
            this.lblTeachingType.Text = "نوع التدريس:";
            // 
            // cmbLecturer
            // 
            this.cmbLecturer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLecturer.FormattingEnabled = true;
            this.cmbLecturer.Location = new System.Drawing.Point(15, 136);
            this.cmbLecturer.Name = "cmbLecturer";
            this.cmbLecturer.Size = new System.Drawing.Size(280, 24);
            this.cmbLecturer.TabIndex = 4;
            // 
            // lblLecturer
            // 
            this.lblLecturer.AutoSize = true;
            this.lblLecturer.Location = new System.Drawing.Point(12, 117);
            this.lblLecturer.Name = "lblLecturer";
            this.lblLecturer.Size = new System.Drawing.Size(47, 16);
            this.lblLecturer.TabIndex = 3;
            this.lblLecturer.Text = "المحاضر:";
            // 
            // cmbCourse
            // 
            this.cmbCourse.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCourse.FormattingEnabled = true;
            this.cmbCourse.Location = new System.Drawing.Point(15, 76);
            this.cmbCourse.Name = "cmbCourse";
            this.cmbCourse.Size = new System.Drawing.Size(280, 24);
            this.cmbCourse.TabIndex = 2;
            // 
            // lblCourse
            // 
            this.lblCourse.AutoSize = true;
            this.lblCourse.Location = new System.Drawing.Point(12, 57);
            this.lblCourse.Name = "lblCourse";
            this.lblCourse.Size = new System.Drawing.Size(43, 16);
            this.lblCourse.TabIndex = 1;
            this.lblCourse.Text = "المادة:";
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(15, 306);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(280, 35);
            this.btnDelete.TabIndex = 9;
            this.btnDelete.Text = "حذف";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.BtnDelete_ClickAsync);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(15, 265);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(280, 35);
            this.btnSave.TabIndex = 8;
            this.btnSave.Text = "حفظ";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.BtnSave_ClickAsync);
            // 
            // btnNew
            // 
            this.btnNew.Location = new System.Drawing.Point(15, 24);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(280, 30);
            this.btnNew.TabIndex = 0;
            this.btnNew.Text = "جديد";
            this.btnNew.UseVisualStyleBackColor = true;
            this.btnNew.Click += new System.EventHandler(this.BtnNew_Click);
            // 
            // CourseLecturerManagementControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "CourseLecturerManagementControl";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Size = new System.Drawing.Size(800, 600);
            this.Load += new System.EventHandler(this.CourseLecturerManagementControl_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewCourseLecturers)).EndInit();
            this.panelForm.ResumeLayout(false);
            this.panelForm.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.DataGridView dataGridViewCourseLecturers;
        private System.Windows.Forms.Panel panelForm;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.ComboBox cmbCourse;
        private System.Windows.Forms.Label lblCourse;
        private System.Windows.Forms.ComboBox cmbLecturer;
        private System.Windows.Forms.Label lblLecturer;
        private System.Windows.Forms.ComboBox cmbTeachingType;
        private System.Windows.Forms.Label lblTeachingType;
    }
}
