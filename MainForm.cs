using System;
using System.Drawing;
using System.Windows.Forms;

namespace SchedualApp
{
    public class MainForm : Form
    {
        private CoursManagementControl _coursControl;

        public MainForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // MainForm
            // 
            this.ClientSize = new System.Drawing.Size(1000, 600);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SchedualApp - Courses Management";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);

        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }
    }
}
