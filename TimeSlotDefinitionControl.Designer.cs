namespace SchedualApp
{
    partial class TimeSlotDefinitionControl
    {
        private System.ComponentModel.IContainer components = null;

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing && (components != null))
        //    {
        //        components.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}

        #region Component Designer generated code

        private void InitializeComponent()
        {
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panelForm = new System.Windows.Forms.Panel();
            this._formLayout = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.txtSlotNumber = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtStartTime = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtEndTime = new System.Windows.Forms.TextBox();
            this._buttonsPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnNew = new System.Windows.Forms.Button();
            this.dgvTimeSlots = new System.Windows.Forms.DataGridView();
            this.tableLayoutPanel1.SuspendLayout();
            this.panelForm.SuspendLayout();
            this._formLayout.SuspendLayout();
            this._buttonsPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTimeSlots)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tableLayoutPanel1.Controls.Add(this.panelForm, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.dgvTimeSlots, 1, 0);
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
            this.panelForm.Controls.Add(this._formLayout);
            this.panelForm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelForm.Location = new System.Drawing.Point(483, 3);
            this.panelForm.Name = "panelForm";
            this.panelForm.Size = new System.Drawing.Size(314, 594);
            this.panelForm.TabIndex = 1;
            // 
            // _formLayout
            // 
            this._formLayout.ColumnCount = 2;
            this._formLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this._formLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 65F));
            this._formLayout.Controls.Add(this.label1, 0, 0);
            this._formLayout.Controls.Add(this.txtSlotNumber, 1, 0);
            this._formLayout.Controls.Add(this.label2, 0, 1);
            this._formLayout.Controls.Add(this.txtStartTime, 1, 1);
            this._formLayout.Controls.Add(this.label3, 0, 2);
            this._formLayout.Controls.Add(this.txtEndTime, 1, 2);
            this._formLayout.Controls.Add(this._buttonsPanel, 0, 4);
            this._formLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this._formLayout.Location = new System.Drawing.Point(0, 0);
            this._formLayout.Name = "_formLayout";
            this._formLayout.Padding = new System.Windows.Forms.Padding(10);
            this._formLayout.RowCount = 6;
            this._formLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this._formLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this._formLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this._formLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this._formLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this._formLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._formLayout.Size = new System.Drawing.Size(314, 594);
            this._formLayout.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(205, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 40);
            this.label1.TabIndex = 0;
            this.label1.Text = "رقم الفترة";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtSlotNumber
            // 
            this.txtSlotNumber.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtSlotNumber.Location = new System.Drawing.Point(13, 13);
            this.txtSlotNumber.Name = "txtSlotNumber";
            this.txtSlotNumber.Size = new System.Drawing.Size(186, 22);
            this.txtSlotNumber.TabIndex = 1;
            this.txtSlotNumber.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Location = new System.Drawing.Point(205, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(96, 40);
            this.label2.TabIndex = 2;
            this.label2.Text = "وقت البدء";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtStartTime
            // 
            this.txtStartTime.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtStartTime.Location = new System.Drawing.Point(13, 53);
            this.txtStartTime.Name = "txtStartTime";
            this.txtStartTime.Size = new System.Drawing.Size(186, 22);
            this.txtStartTime.TabIndex = 3;
            this.txtStartTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Location = new System.Drawing.Point(205, 90);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(96, 40);
            this.label3.TabIndex = 4;
            this.label3.Text = "وقت النهاية";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtEndTime
            // 
            this.txtEndTime.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtEndTime.Location = new System.Drawing.Point(13, 93);
            this.txtEndTime.Name = "txtEndTime";
            this.txtEndTime.Size = new System.Drawing.Size(186, 22);
            this.txtEndTime.TabIndex = 5;
            this.txtEndTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // _buttonsPanel
            // 
            this._formLayout.SetColumnSpan(this._buttonsPanel, 2);
            this._buttonsPanel.Controls.Add(this.btnDelete);
            this._buttonsPanel.Controls.Add(this.btnSave);
            this._buttonsPanel.Controls.Add(this.btnNew);
            this._buttonsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._buttonsPanel.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this._buttonsPanel.Location = new System.Drawing.Point(13, 153);
            this._buttonsPanel.Name = "_buttonsPanel";
            this._buttonsPanel.Size = new System.Drawing.Size(288, 44);
            this._buttonsPanel.TabIndex = 9;
            // 
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.Color.Red;
            this.btnDelete.ForeColor = System.Drawing.Color.White;
            this.btnDelete.Location = new System.Drawing.Point(3, 3);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(85, 35);
            this.btnDelete.TabIndex = 7;
            this.btnDelete.Text = "حذف";
            this.btnDelete.UseVisualStyleBackColor = false;
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.Green;
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(94, 3);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(85, 35);
            this.btnSave.TabIndex = 6;
            this.btnSave.Text = "حفظ";
            this.btnSave.UseVisualStyleBackColor = false;
            // 
            // btnNew
            // 
            this.btnNew.BackColor = System.Drawing.Color.LightBlue;
            this.btnNew.Location = new System.Drawing.Point(185, 3);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(85, 35);
            this.btnNew.TabIndex = 8;
            this.btnNew.Text = "جديد";
            this.btnNew.UseVisualStyleBackColor = false;
            // 
            // dgvTimeSlots
            // 
            this.dgvTimeSlots.AllowUserToAddRows = false;
            this.dgvTimeSlots.AllowUserToDeleteRows = false;
            this.dgvTimeSlots.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTimeSlots.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvTimeSlots.Location = new System.Drawing.Point(3, 3);
            this.dgvTimeSlots.MultiSelect = false;
            this.dgvTimeSlots.Name = "dgvTimeSlots";
            this.dgvTimeSlots.ReadOnly = true;
            this.dgvTimeSlots.RowHeadersWidth = 51;
            this.dgvTimeSlots.RowTemplate.Height = 24;
            this.dgvTimeSlots.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvTimeSlots.Size = new System.Drawing.Size(474, 594);
            this.dgvTimeSlots.TabIndex = 0;
            // 
            // TimeSlotDefinitionControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "TimeSlotDefinitionControl";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Size = new System.Drawing.Size(800, 600);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panelForm.ResumeLayout(false);
            this._formLayout.ResumeLayout(false);
            this._formLayout.PerformLayout();
            this._buttonsPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTimeSlots)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.DataGridView dgvTimeSlots;
        private System.Windows.Forms.Panel panelForm;
        private System.Windows.Forms.TableLayoutPanel _formLayout;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.TextBox txtEndTime;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtStartTime;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtSlotNumber;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.FlowLayoutPanel _buttonsPanel;
    }
}