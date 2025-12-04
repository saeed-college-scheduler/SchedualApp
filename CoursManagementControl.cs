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
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void BtnSave_Click(object sender, EventArgs e)
        {
            // 1. تعريف متغير لاستقبال الرقم
            int lectureHours;
            int practicalHours;

            // 2. محاولة تحويل النص إلى رقم
            bool isNumber = int.TryParse(_txtLectureHours.Text, out lectureHours) && int.TryParse(_txtPracticalHours.Text, out practicalHours);

            // 3. التحقق من النتيجة
            if (!isNumber)
            {
                // الحالة: المستخدم أدخل نصاً أو رموزاً غير صالحة
                MessageBox.Show("عفواً، يجب إدخال قيمة رقمية فقط في خانة الساعات.", "خطأ في الإدخال");

                // إعادة التركيز على الحقل وتفريغه (اختياري)
                _txtLectureHours.Focus();
                _txtLectureHours.SelectAll();
                return; // إيقاف تنفيذ الكود
            }

            // 4. (اختياري) التحقق من أن الرقم ليس سالباً
            if (lectureHours < 0)
            {
                MessageBox.Show("لا يمكن أن تكون الساعات بالسالب.");
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
                MessageBox.Show("Error saving data: " + ex.ToString());
            }
        }

        private async void BtnDelete_Click(object sender, EventArgs e)
        {
            await DeleteDataInternalAsync();
        }

        private async Task DeleteDataInternalAsync()
        {
            if (_selectedCourseId == 0) return;

            if (MessageBox.Show("Confirm Delete?", "Delete", MessageBoxButtons.YesNo) == DialogResult.No) return;

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
                MessageBox.Show("Error deleting: " + ex.Message);
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
                MessageBox.Show("Required fields missing");
                return false;
            }
            int.TryParse(_txtLectureHours.Text, out lectureHours);
            int.TryParse(_txtPracticalHours.Text, out practicalHours);
            return true;
        }

        private void _formLayout_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}