using System;
using System.ComponentModel;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SchedualApp
{
    // يجب أن تكون كلمة partial موجودة هنا
    public partial class LecturerManagementControl : UserControl
    {
        private TableLayoutPanel _mainLayout;
        private DataGridView _dataGridView;
        private Panel _formPanel;
        private TextBox _txtFirstName;
        private TextBox _txtLastName;
        private TextBox _txtAcademicRank;
        private TextBox _txtMaxWorkload;
        private CheckBox _chkIsActive;
        private Button _btnNew;
        private Button _btnSave;
        private Button _btnDelete;
        private BindingList<Lecturer> _bindingList;
        private int _selectedLecturerId;

        public LecturerManagementControl()
        {
            // هذا هو استدعاء الدالة التي تنشئ الواجهة برمجياً
            InitializeComponent();
            this.Load += LecturerManagementControl_Load;
        }

        private async void LecturerManagementControl_Load(object sender, EventArgs e)
        {
            await LoadDataAsync();
        }

        // هذه الدالة هي التي تنشئ الواجهة برمجياً
        private void InitializeComponent()
        {
            this.SuspendLayout();

            // TableLayoutPanel لتقسيم الواجهة 60% / 40%
            _mainLayout = new TableLayoutPanel();
            _mainLayout.Dock = DockStyle.Fill;
            _mainLayout.ColumnCount = 2;
            _mainLayout.RowCount = 1;
            _mainLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60F)); // 60% للـ DataGridView
            _mainLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40F)); // 40% للـ Form
            _mainLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));

            // DataGridView on left (Column 0)
            _dataGridView = new DataGridView();
            _dataGridView.Dock = DockStyle.Fill;
            _dataGridView.ReadOnly = true;
            _dataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            _dataGridView.MultiSelect = false;
            _dataGridView.AllowUserToAddRows = false;
            _dataGridView.AllowUserToDeleteRows = false;
            _dataGridView.AutoGenerateColumns = false;
            _dataGridView.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
            _dataGridView.BackgroundColor = SystemColors.Window;
            _dataGridView.SelectionChanged += DataGridView_SelectionChanged;

            // Define columns for Lecturer
            _dataGridView.Columns.AddRange(new DataGridViewColumn[] {
                new DataGridViewTextBoxColumn { DataPropertyName = "LecturerID", HeaderText = "ID", ReadOnly = true, Width = 50 },
                new DataGridViewTextBoxColumn { DataPropertyName = "FirstName", HeaderText = "First Name", ReadOnly = true, Width = 100 },
                new DataGridViewTextBoxColumn { DataPropertyName = "LastName", HeaderText = "Last Name", ReadOnly = true, AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill },
                new DataGridViewTextBoxColumn { DataPropertyName = "AcademicRank", HeaderText = "Rank", ReadOnly = true, Width = 80 },
                new DataGridViewTextBoxColumn { DataPropertyName = "MaxWorkload", HeaderText = "Max Load", ReadOnly = true, Width = 60 },
                new DataGridViewCheckBoxColumn { DataPropertyName = "IsActive", HeaderText = "Active", ReadOnly = true, Width = 50 }
            });

            _mainLayout.Controls.Add(_dataGridView, 0, 0); // إضافة الـ DataGridView إلى العمود الأول

            // Right panel - form (Column 1)
            _formPanel = new Panel();
            _formPanel.Dock = DockStyle.Fill;
            _formPanel.BackColor = SystemColors.Control;

            var table = new TableLayoutPanel();
            table.Dock = DockStyle.Fill;
            table.ColumnCount = 2;
            table.RowCount = 8; // 5 حقول إدخال + 1 مربع اختيار + صف للأزرار + صف مرن
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 35F));
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 65F));
            for (int i = 0; i < 6; i++) table.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            table.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F)); // Buttons
            table.RowStyles.Add(new RowStyle(SizeType.Percent, 100F)); // الصف الأخير مرن
            table.Padding = new Padding(10);

            // Row 0: First Name
            table.Controls.Add(new Label { Text = "First Name:", TextAlign = ContentAlignment.MiddleLeft, Dock = DockStyle.Fill }, 0, 0);
            _txtFirstName = new TextBox { Dock = DockStyle.Fill, Font = new Font("Segoe UI", 9F) };
            table.Controls.Add(_txtFirstName, 1, 0);

            // Row 1: Last Name
            table.Controls.Add(new Label { Text = "Last Name:", TextAlign = ContentAlignment.MiddleLeft, Dock = DockStyle.Fill }, 0, 1);
            _txtLastName = new TextBox { Dock = DockStyle.Fill, Font = new Font("Segoe UI", 9F) };
            table.Controls.Add(_txtLastName, 1, 1);

            // Row 2: Academic Rank
            table.Controls.Add(new Label { Text = "Academic Rank:", TextAlign = ContentAlignment.MiddleLeft, Dock = DockStyle.Fill }, 0, 2);
            _txtAcademicRank = new TextBox { Dock = DockStyle.Fill, Font = new Font("Segoe UI", 9F) };
            table.Controls.Add(_txtAcademicRank, 1, 2);

            // Row 3: Max Workload
            table.Controls.Add(new Label { Text = "Max Workload:", TextAlign = ContentAlignment.MiddleLeft, Dock = DockStyle.Fill }, 0, 3);
            _txtMaxWorkload = new TextBox { Dock = DockStyle.Fill, Font = new Font("Segoe UI", 9F) };
            table.Controls.Add(_txtMaxWorkload, 1, 3);

            // Row 4: Is Active
            table.Controls.Add(new Label { Text = "Is Active:", TextAlign = ContentAlignment.MiddleLeft, Dock = DockStyle.Fill }, 0, 4);
            _chkIsActive = new CheckBox { Dock = DockStyle.Fill, Text = "Lecturer is currently active", AutoSize = true };
            table.Controls.Add(_chkIsActive, 1, 4);

            var buttonsPanel = new FlowLayoutPanel();
            buttonsPanel.FlowDirection = FlowDirection.RightToLeft;
            buttonsPanel.Dock = DockStyle.Fill;

            _btnNew = new Button { Text = "New", BackColor = Color.LightBlue, Width = 90, Height = 30 };
            _btnSave = new Button { Text = "Save", BackColor = Color.Green, ForeColor = Color.White, Width = 90, Height = 30 };
            _btnDelete = new Button { Text = "Delete", BackColor = Color.Red, ForeColor = Color.White, Width = 90, Height = 30 };

            _btnNew.Click += BtnNew_Click;
            _btnSave.Click += async (s, e) => await BtnSave_ClickAsync();
            _btnDelete.Click += async (s, e) => await BtnDelete_ClickAsync();

            buttonsPanel.Controls.Add(_btnDelete);
            buttonsPanel.Controls.Add(_btnSave);
            buttonsPanel.Controls.Add(_btnNew);

            // Row 6: Buttons
            table.Controls.Add(buttonsPanel, 0, 6);
            table.SetColumnSpan(buttonsPanel, 2);

            _formPanel.Controls.Add(table);
            _mainLayout.Controls.Add(_formPanel, 1, 0); // إضافة الـ Form Panel إلى العمود الثاني

            this.Controls.Add(_mainLayout); // إضافة الـ TableLayoutPanel إلى الكنترول الرئيسي

            this.ResumeLayout(false);
        }

        // دالة تحميل البيانات غير المتزامنة
        private async Task LoadDataAsync()
        {
            try
            {
                using (var ctx = new SchedualAppModel())
                {
                    var list = await ctx.Lecturers.AsNoTracking().ToListAsync();
                    _bindingList = new BindingList<Lecturer>(list);
                    _dataGridView.DataSource = _bindingList;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading data: " + ex.ToString(), "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // دالة الحفظ غير المتزامنة
        private async Task BtnSave_ClickAsync()
        {
            if (!ValidateForm(out int maxWorkload)) return;

            try
            {
                using (var ctx = new SchedualAppModel())
                {
                    if (_selectedLecturerId == 0)
                    {
                        // إنشاء كيان جديد
                        var newLecturer = new Lecturer
                        {
                            FirstName = _txtFirstName.Text.Trim(),
                            LastName = _txtLastName.Text.Trim(),
                            AcademicRank = _txtAcademicRank.Text.Trim(),
                            MaxWorkload = maxWorkload,
                            IsActive = _chkIsActive.Checked
                        };
                        ctx.Lecturers.Add(newLecturer);
                    }
                    else
                    {
                        // تحديث كيان موجود
                        var existing = await ctx.Lecturers.FindAsync(_selectedLecturerId);
                        if (existing != null)
                        {
                            existing.FirstName = _txtFirstName.Text.Trim();
                            existing.LastName = _txtLastName.Text.Trim();
                            existing.AcademicRank = _txtAcademicRank.Text.Trim();
                            existing.MaxWorkload = maxWorkload;
                            existing.IsActive = _chkIsActive.Checked;
                            ctx.Entry(existing).State = EntityState.Modified;
                        }
                    }
                    await ctx.SaveChangesAsync();
                }
                await LoadDataAsync();
                ClearForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving data: " + ex.ToString(), "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // دالة الحذف غير المتزامنة
        private async Task BtnDelete_ClickAsync()
        {
            if (_selectedLecturerId == 0) return;

            if (MessageBox.Show("Are you sure you want to delete this lecturer?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                return;

            try
            {
                using (var ctx = new SchedualAppModel())
                {
                    var toDelete = await ctx.Lecturers.FindAsync(_selectedLecturerId);
                    if (toDelete != null)
                    {
                        ctx.Lecturers.Remove(toDelete);
                        await ctx.SaveChangesAsync();
                    }
                }
                await LoadDataAsync();
                ClearForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting data: " + ex.ToString(), "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // دالة اختيار الصف في DataGridView
        private void DataGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (_dataGridView.SelectedRows.Count == 0) return;
            var lecturer = _dataGridView.SelectedRows[0].DataBoundItem as Lecturer;
            if (lecturer != null)
            {
                _selectedLecturerId = lecturer.LecturerID;
                PopulateForm(lecturer);
            }
        }

        // دالة ملء نموذج الإدخال
        private void PopulateForm(Lecturer lecturer)
        {
            _txtFirstName.Text = lecturer.FirstName;
            _txtLastName.Text = lecturer.LastName;
            _txtAcademicRank.Text = lecturer.AcademicRank;
            _txtMaxWorkload.Text = lecturer.MaxWorkload.ToString();
            _chkIsActive.Checked = lecturer.IsActive;
        }

        // دالة زر "جديد"
        private void BtnNew_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        // دالة مسح نموذج الإدخال
        private void ClearForm()
        {
            _selectedLecturerId = 0;
            _txtFirstName.Text = string.Empty;
            _txtLastName.Text = string.Empty;
            _txtAcademicRank.Text = string.Empty;
            _txtMaxWorkload.Text = "12"; // القيمة الافتراضية
            _chkIsActive.Checked = true; // القيمة الافتراضية
            _dataGridView.ClearSelection();
        }

        // دالة التحقق من صحة الإدخال
        private bool ValidateForm(out int maxWorkload)
        {
            maxWorkload = 0;

            if (string.IsNullOrWhiteSpace(_txtFirstName.Text) || string.IsNullOrWhiteSpace(_txtLastName.Text))
            {
                MessageBox.Show("First Name and Last Name cannot be empty.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (!int.TryParse(_txtMaxWorkload.Text, out maxWorkload) || maxWorkload <= 0)
            {
                MessageBox.Show("Max Workload must be a positive number.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }
    }
}
