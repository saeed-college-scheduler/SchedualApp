using System;
using System.ComponentModel;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SchedualApp
{
    public partial class CoursManagementControl : UserControl
    {
        private BindingList<Cours> _bindingList;
        private int _selectedCourseId;
        private SchedualAppModel _db = new SchedualAppModel();

        public CoursManagementControl()
        {
            InitializeComponent();

            // --- إعدادات اللغة العربية ---
            this.RightToLeft = RightToLeft.Yes; // تفعيل الاتجاه من اليمين لليسار

            // ضبط اتجاه النصوص داخل مربعات الإدخال
            _txtCode.RightToLeft = RightToLeft.Yes;
            _txtTitle.RightToLeft = RightToLeft.Yes;
            _txtLectureHours.RightToLeft = RightToLeft.Yes;
            _txtPracticalHours.RightToLeft = RightToLeft.Yes;
            _chkIsPractical.RightToLeft = RightToLeft.Yes;

            this.Load += CoursManagementControl_Load;
        }

        private async void CoursManagementControl_Load(object sender, EventArgs e)
        {
            await LoadDataAsync();
        }

        private async Task LoadDataAsync()
        {
            try
            {
                var list = await _db.Courses.AsNoTracking().ToListAsync();
                _bindingList = new BindingList<Cours>(list);
                _dataGridView.DataSource = _bindingList;

                // --- تنسيق الأعمدة وإخفاء غير الضروري ---
                if (_dataGridView.Columns.Contains("CourseID")) _dataGridView.Columns["CourseID"].Visible = false;
                if (_dataGridView.Columns.Contains("Timetables")) _dataGridView.Columns["Timetables"].Visible = false;
                if (_dataGridView.Columns.Contains("DepartmentID")) _dataGridView.Columns["DepartmentID"].Visible = false;
                if (_dataGridView.Columns.Contains("Department")) _dataGridView.Columns["Department"].Visible = false;
                if (_dataGridView.Columns.Contains("CourseLecturers")) _dataGridView.Columns["CourseLecturers"].Visible = false;
                if (_dataGridView.Columns.Contains("CourseLevels")) _dataGridView.Columns["CourseLevels"].Visible = false;
                if (_dataGridView.Columns.Contains("ScheduleSlots")) _dataGridView.Columns["ScheduleSlots"].Visible = false;
                //if (_dataGridView.Columns.Contains("Department")) _dataGridView.Columns["Department"].Visible = false;
                //if (_dataGridView.Columns.Contains("Department")) _dataGridView.Columns["Department"].Visible = false;
                // تعريب الرؤوس
                if (_dataGridView.Columns.Contains("Code")) _dataGridView.Columns["Code"].HeaderText = "رمز المقرر";
                if (_dataGridView.Columns.Contains("Title")) _dataGridView.Columns["Title"].HeaderText = "اسم المقرر";
                if (_dataGridView.Columns.Contains("LectureHours")) _dataGridView.Columns["LectureHours"].HeaderText = "ساعات نظري";
                if (_dataGridView.Columns.Contains("PracticalHours")) _dataGridView.Columns["PracticalHours"].HeaderText = "ساعات عملي";
                if (_dataGridView.Columns.Contains("IsPractical")) _dataGridView.Columns["IsPractical"].HeaderText = "له عملي؟";
            }
            catch (Exception ex)
            {
                MessageBox.Show("خطأ في تحميل البيانات: " + ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void BtnSave_Click(object sender, EventArgs e)
        {
            int lectureHours=0;
            int practicalHours=0;

            // التحقق من الأرقام
            bool isNumber = int.TryParse(_txtLectureHours.Text, out lectureHours) && int.TryParse(_txtPracticalHours.Text, out practicalHours);

            if (!isNumber)
            {
                MessageBox.Show("عفواً، يجب إدخال قيمة رقمية فقط في خانة الساعات.", "خطأ في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                _txtLectureHours.Focus();
                _txtLectureHours.SelectAll();
                return;
            }

            if (lectureHours < 0 || practicalHours < 0)
            {
                MessageBox.Show("لا يمكن أن تكون الساعات بالسالب.", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            await SaveDataInternalAsync();
            MessageBox.Show("تم حفظ البيانات بنجاح", "نجاح", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private async Task SaveDataInternalAsync()
        {
            if (!ValidateForm(out int lectureHours, out int practicalHours)) return;

            try
            {
                if (_selectedCourseId == 0)
                {
                    var newCourse = new Cours
                    {
                        Code = _txtCode.Text.Trim(),
                        Title = _txtTitle.Text.Trim(),
                        LectureHours = lectureHours,
                        PracticalHours = practicalHours,
                        IsPractical = _chkIsPractical.Checked
                    };
                    _db.Courses.Add(newCourse);
                }
                else
                {
                    var existing = await _db.Courses.FindAsync(_selectedCourseId);
                    if (existing != null)
                    {
                        existing.Code = _txtCode.Text.Trim();
                        existing.Title = _txtTitle.Text.Trim();
                        existing.LectureHours = lectureHours;
                        existing.PracticalHours = practicalHours;
                        existing.IsPractical = _chkIsPractical.Checked;
                        _db.Entry(existing).State = EntityState.Modified;
                    }
                }
                await _db.SaveChangesAsync();
                await LoadDataAsync();
                ClearForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show("خطأ في الحفظ: " + ex.ToString(), "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void BtnDelete_Click(object sender, EventArgs e)
        {
            await DeleteDataInternalAsync();
        }

        private async Task DeleteDataInternalAsync()
        {
            if (_selectedCourseId == 0) return;

            if (MessageBox.Show("هل أنت متأكد من حذف هذا المقرر؟", "تأكيد الحذف", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;

            try
            {
                var toDelete = await _db.Courses.FindAsync(_selectedCourseId);
                if (toDelete != null)
                {
                    _db.Courses.Remove(toDelete);
                    await _db.SaveChangesAsync();
                }

                await LoadDataAsync();
                ClearForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show("خطأ في الحذف: " + ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

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

        private void PopulateForm(Cours course)
        {
            _txtCode.Text = course.Code;
            _txtTitle.Text = course.Title;
            _txtLectureHours.Text = course.LectureHours.ToString();
            _txtPracticalHours.Text = course.PracticalHours.ToString();
            _chkIsPractical.Checked = course.IsPractical;
        }

        private void BtnNew_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        private void ClearForm()
        {
            _selectedCourseId = 0;
            _txtCode.Text = "";
            _txtTitle.Text = "";
            _txtLectureHours.Text = "0";
            _txtPracticalHours.Text = "0";
            _chkIsPractical.Checked = false;
            _dataGridView.ClearSelection();
        }

        private bool ValidateForm(out int lectureHours, out int practicalHours)
        {
            lectureHours = 0;
            practicalHours = 0;
            if (string.IsNullOrWhiteSpace(_txtCode.Text) || string.IsNullOrWhiteSpace(_txtTitle.Text))
            {
                MessageBox.Show("يرجى تعبئة الحقول المطلوبة (الرمز والاسم).", "بيانات ناقصة", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            int.TryParse(_txtLectureHours.Text, out lectureHours);
            int.TryParse(_txtPracticalHours.Text, out practicalHours);
            return true;
        }

        // Event Handlers الفارغة (يمكنك حذفها إذا لم تكن مرتبطة)
        private void _formLayout_Paint(object sender, PaintEventArgs e) { }
        private void _txtPracticalHours_TextChanged(object sender, EventArgs e) { }
        private void _txtPracticalHours_TextChanged_1(object sender, EventArgs e) { }
        private void _buttonsPanel_Paint(object sender, PaintEventArgs e) { }
        private void _txtCode_TextChanged(object sender, EventArgs e) { }
    }
}