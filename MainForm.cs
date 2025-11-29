using System;
using System.Drawing;
using System.Windows.Forms;

namespace SchedualApp
{
    // لا يجب أن تكون كلمة partial موجودة هنا
    public class MainForm : Form
    {
        private MenuStrip _mainMenu;
        private ToolStripMenuItem _managementMenu;
        private ToolStripMenuItem _coursesMenuItem;
        private ToolStripMenuItem _lecturersMenuItem;
        private Panel _contentPanel; // اللوحة التي ستستضيف الـ UserControls

        public MainForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this._mainMenu = new System.Windows.Forms.MenuStrip();
            this._managementMenu = new System.Windows.Forms.ToolStripMenuItem();
            this._coursesMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._lecturersMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._contentPanel = new System.Windows.Forms.Panel();
            this._mainMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // _mainMenu
            // 
            this._mainMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this._mainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._managementMenu});
            this._mainMenu.Location = new System.Drawing.Point(0, 0);
            this._mainMenu.Name = "_mainMenu";
            this._mainMenu.Size = new System.Drawing.Size(1000, 30);
            this._mainMenu.TabIndex = 1;
            // 
            // _managementMenu
            // 
            this._managementMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._coursesMenuItem,
            this._lecturersMenuItem});
            this._managementMenu.Name = "_managementMenu";
            this._managementMenu.Size = new System.Drawing.Size(111, 26);
            this._managementMenu.Text = "Management";
            // 
            // _coursesMenuItem
            // 
            this._coursesMenuItem.Name = "_coursesMenuItem";
            this._coursesMenuItem.Size = new System.Drawing.Size(151, 26);
            this._coursesMenuItem.Text = "Courses";
            // 
            // _lecturersMenuItem
            // 
            this._lecturersMenuItem.Name = "_lecturersMenuItem";
            this._lecturersMenuItem.Size = new System.Drawing.Size(151, 26);
            this._lecturersMenuItem.Text = "Lecturers";
            // 
            // _contentPanel
            // 
            this._contentPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._contentPanel.Location = new System.Drawing.Point(0, 30);
            this._contentPanel.Name = "_contentPanel";
            this._contentPanel.Size = new System.Drawing.Size(1000, 570);
            this._contentPanel.TabIndex = 0;
            this._contentPanel.Paint += new System.Windows.Forms.PaintEventHandler(this._contentPanel_Paint);
            // 
            // MainForm
            // 
            this.ClientSize = new System.Drawing.Size(1000, 600);
            this.Controls.Add(this._contentPanel);
            this.Controls.Add(this._mainMenu);
            this.MainMenuStrip = this._mainMenu;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SchedualApp - Main Menu";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this._mainMenu.ResumeLayout(false);
            this._mainMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // عند تحميل النموذج، اعرض واجهة المقررات كواجهة افتراضية
            LoadUserControl(new CoursManagementControl());
        }

        // دالة مساعدة لتبديل الـ UserControl
        private void LoadUserControl(UserControl control)
        {
            // 1. إزالة أي كنترول موجود حالياً
            _contentPanel.Controls.Clear();

            // 2. ضبط خصائص الكنترول الجديد
            control.Dock = DockStyle.Fill;

            // 3. إضافة الكنترول الجديد إلى اللوحة
            _contentPanel.Controls.Add(control);
        }

        // حدث النقر على "Courses"
        private void CoursesMenuItem_Click(object sender, EventArgs e)
        {
            LoadUserControl(new CoursManagementControl());
            this.Text = "SchedualApp - Courses Management";
        }

        // حدث النقر على "Lecturers"
        private void LecturersMenuItem_Click(object sender, EventArgs e)
        {
            LoadUserControl(new LecturerManagementControl());
            this.Text = "SchedualApp - Lecturers Management";
        }

        private void _contentPanel_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
