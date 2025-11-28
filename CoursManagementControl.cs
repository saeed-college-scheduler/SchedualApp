using System;
using System.ComponentModel;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SchedualApp
{
    public class CoursManagementControl : UserControl
    {
        private TableLayoutPanel _mainLayout;
        private DataGridView _dataGridView;
        private Panel _formPanel;
        private TextBox _txtCode;
        private TextBox _txtTitle;
        private TextBox _txtLectureHours;
        private TextBox _txtPracticalHours;
        private CheckBox _chkIsPractical; // تمت إضافة مربع الاختيار
        private Button _btnNew;
        private Button _btnSave;
        private Button _btnDelete;
        private BindingList<Cours> _bindingList;
        private int _selectedCourseId;

        public CoursManagementControl()
        {
            InitializeComponent();
            this.Load += CoursManagementControl_Load;
        }

        private async void CoursManagementControl_Load(object sender, EventArgs e)
        {
            await LoadDataAsync();
        }

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

            // Define columns
            _dataGridView.Columns.AddRange(new DataGridViewColumn[] {
                new DataGridViewTextBoxColumn { DataPropertyName = "CourseID", HeaderText = "ID", ReadOnly = true, Width = 50 },
                new DataGridViewTextBoxColumn { DataPropertyName = "Code", HeaderText = "Code", ReadOnly = true, Width = 80 },
                new DataGridViewTextBoxColumn { DataPropertyName = "Title", HeaderText = "Title", ReadOnly = true, AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill },
                new DataGridViewTextBoxColumn { DataPropertyName = "LectureHours", HeaderText = "Lec Hrs", ReadOnly = true, Width = 60 },
                new DataGridViewTextBoxColumn { DataPropertyName = "PracticalHours", HeaderText = "Prac Hrs", ReadOnly = true, Width = 60 },
                new DataGridViewCheckBoxColumn { DataPropertyName = "IsPractical", HeaderText = "Practical", ReadOnly = true, Width = 60 } // تم إضافة عمود IsPractical
            });

            _mainLayout.Controls.Add(_dataGridView, 0, 0); // إضافة الـ DataGridView إلى العمود الأول

            // Right panel - form (Column 1)
            _formPanel = new Panel();
            _formPanel.Dock = DockStyle.Fill;
            _formPanel.BackColor = SystemColors.Control;

            var table = new TableLayoutPanel();
            table.Dock = DockStyle.Fill;
            table.ColumnCount = 2;
            table.RowCount = 7; // زيادة عدد الصفوف لـ IsPractical
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 35F));
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 65F));
            for (int i = 0; i < 6; i++) table.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            table.RowStyles.Add(new RowStyle(SizeType.Percent, 100F)); // الصف الأخير مرن
            table.Padding = new Padding(10);

            // Row 0: Code
            table.Controls.Add(new Label { Text = "Code:", TextAlign = ContentAlignment.MiddleLeft, Dock = DockStyle.Fill }, 0, 0);
            _txtCode = new TextBox { Dock = DockStyle.Fill, Font = new Font("Segoe UI", 9F) };
            table.Controls.Add(_txtCode, 1, 0);

            // Row 1: Title
            table.Controls.Add(new Label { Text = "Title:", TextAlign = ContentAlignment.MiddleLeft, Dock = DockStyle.Fill }, 0, 1);
            _txtTitle = new TextBox { Dock = DockStyle.Fill, Font = new Font("Segoe UI", 9F) };
            table.Controls.Add(_txtTitle, 1, 1);

            // Row 2: Lecture Hours
            table.Controls.Add(new Label { Text = "Lecture Hours:", TextAlign = ContentAlignment.MiddleLeft, Dock = DockStyle.Fill }, 0, 2);
            _txtLectureHours = new TextBox { Dock = DockStyle.Fill, Font = new Font("Segoe UI", 9F) };
            table.Controls.Add(_txtLectureHours, 1, 2);

            // Row 3: Practical Hours
            table.Controls.Add(new Label { Text = "Practical Hours:", TextAlign = ContentAlignment.MiddleLeft, Dock = DockStyle.Fill }, 0, 3);
            _txtPracticalHours = new TextBox { Dock = DockStyle.Fill, Font = new Font("Segoe UI", 9F) };
            table.Controls.Add(_txtPracticalHours, 1, 3);

            // Row 4: Is Practical (مربع الاختيار)
            table.Controls.Add(new Label { Text = "Is Practical:", TextAlign = ContentAlignment.MiddleLeft, Dock = DockStyle.Fill }, 0, 4);
            _chkIsPractical = new CheckBox { Dock = DockStyle.Fill, Text = "Check if practical course", AutoSize = true };
            table.Controls.Add(_chkIsPractical, 1, 4);

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
            table.Controls.Add(buttonsPanel, 0, 5);
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
                    var list = await ctx.Courses.AsNoTracking().ToListAsync();
                    _bindingList = new BindingList<Cours>(list);
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
            if (!ValidateForm(out int lectureHours, out int practicalHours)) return;

            try
            {
                using (var ctx = new SchedualAppModel())
                {
                    if (_selectedCourseId == 0)
                    {
                        var newCourse = new Cours
                        {
                            Code = _txtCode.Text.Trim(),
                            Title = _txtTitle.Text.Trim(),
                            LectureHours = lectureHours,
                            PracticalHours = practicalHours,
                            IsPractical = _chkIsPractical.Checked // قراءة قيمة مربع الاختيار
                        };
                        ctx.Courses.Add(newCourse);
                    }
                    else
                    {
                        var existing = await ctx.Courses.FindAsync(_selectedCourseId);
                        if (existing != null)
                        {
                            existing.Code = _txtCode.Text.Trim();
                            existing.Title = _txtTitle.Text.Trim();
                            existing.LectureHours = lectureHours;
                            existing.PracticalHours = practicalHours;
                            existing.IsPractical = _chkIsPractical.Checked; // تحديث قيمة مربع الاختيار
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
            if (_selectedCourseId == 0) return;

            if (MessageBox.Show("Are you sure you want to delete this course?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                return;

            try
            {
                using (var ctx = new SchedualAppModel())
                {
                    var toDelete = await ctx.Courses.FindAsync(_selectedCourseId);
                    if (toDelete != null)
                    {
                        ctx.Courses.Remove(toDelete);
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
            var course = _dataGridView.SelectedRows[0].DataBoundItem as Cours;
            if (course != null)
            {
                _selectedCourseId = course.CourseID;
                PopulateForm(course);
            }
        }

        // دالة ملء نموذج الإدخال
        private void PopulateForm(Cours course)
        {
            _txtCode.Text = course.Code;
            _txtTitle.Text = course.Title;
            _txtLectureHours.Text = course.LectureHours.ToString();
            _txtPracticalHours.Text = course.PracticalHours.ToString();
            _chkIsPractical.Checked = course.IsPractical; // تعيين قيمة مربع الاختيار
        }

        // دالة زر "جديد"
        private void BtnNew_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        // دالة مسح نموذج الإدخال
        private void ClearForm()
        {
            _selectedCourseId = 0;
            _txtCode.Text = string.Empty;
            _txtTitle.Text = string.Empty;
            _txtLectureHours.Text = "0";
            _txtPracticalHours.Text = "0";
            _chkIsPractical.Checked = false; // إعادة تعيين مربع الاختيار
            _dataGridView.ClearSelection();
        }

        // دالة التحقق من صحة الإدخال
        private bool ValidateForm(out int lectureHours, out int practicalHours)
        {
            lectureHours = 0;
            practicalHours = 0;

            if (string.IsNullOrWhiteSpace(_txtCode.Text) || string.IsNullOrWhiteSpace(_txtTitle.Text))
            {
                MessageBox.Show("Code and Title cannot be empty.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (!int.TryParse(_txtLectureHours.Text, out lectureHours) || lectureHours < 0)
            {
                MessageBox.Show("Lecture Hours must be a non-negative number.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (!int.TryParse(_txtPracticalHours.Text, out practicalHours) || practicalHours < 0)
            {
                MessageBox.Show("Practical Hours must be a non-negative number.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }
    }
}
