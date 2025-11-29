using System;
using System.ComponentModel;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SchedualApp
{
    public partial class LecturerManagementControl : UserControl
    {
        // المتغيرات الخاصة بمنطق العمل فقط
        private BindingList<Lecturer> _bindingList;
        private int _selectedLecturerId;

        public LecturerManagementControl()
        {
            // هذا هو استدعاء الدالة التي تنشئ الواجهة برمجياً من ملف Designer.cs
            InitializeComponent();
            this.Load += LecturerManagementControl_Load;
        }

        private async void LecturerManagementControl_Load(object sender, EventArgs e)
        {
            await LoadDataAsync();
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

                    // فحص null check لمنع NullReferenceException
                    if (_dataGridView != null)
                    {
                        _dataGridView.DataSource = _bindingList;
                        // يمكن إضافة تنسيق DataGridView هنا
                        _dataGridView.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading data: " + ex.ToString(), "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // دالة الحفظ غير المتزامنة
        private async void BtnSave_Click(object sender, EventArgs e)
        {
            await BtnSave_ClickAsync();
        }

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
        private async void BtnDelete_Click(object sender, EventArgs e)
        {
            await BtnDelete_ClickAsync();
        }

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
            // فحص null check لمنع NullReferenceException
            if (_dataGridView == null || _dataGridView.SelectedRows.Count == 0) return;

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

            // فحص null check لمنع NullReferenceException
            if (_dataGridView != null)
            {
                _dataGridView.ClearSelection();
            }
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
