using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SchedualApp
{
    public partial class CourseLevelManagementControl : UserControl
    {
        // يجب أن تكون هذه الكيانات موجودة في مشروعك
        // Department, Level, Course, CourseLevel
        private SchedualAppModel db = new SchedualAppModel();
        private SchedualApp.CourseLevel currentCourseLevel;

        public CourseLevelManagementControl()
        {
            InitializeComponent();
            // تعيين اتجاه النص ليتناسب مع اللغة العربية
            cmbDepartment.RightToLeft = RightToLeft.Yes;
            cmbLevel.RightToLeft = RightToLeft.Yes;
            cmbCourse.RightToLeft = RightToLeft.Yes;
        }

        private async void CourseLevelManagementControl_Load(object sender, EventArgs e)
        {
            await LoadInitialDataAsync();
            await LoadCourseLevelsAsync();
        }

        private async Task LoadInitialDataAsync()
        {
            try
            {
                // 1. تحميل الأقسام
                var departments = await db.Departments.ToListAsync();
                cmbDepartment.DataSource = departments;
                cmbDepartment.DisplayMember = "Name";
                cmbDepartment.ValueMember = "DepartmentID";

                // 2. تحميل المستويات
                // نفترض وجود كيان Level
                var levels = await db.Levels.ToListAsync();
                cmbLevel.DataSource = levels;
                cmbLevel.DisplayMember = "Name";
                cmbLevel.ValueMember = "LevelID";

                // 3. تحميل المواد
                // نفترض وجود كيان Course
                var courses = await db.Courses.ToListAsync();
                cmbCourse.DataSource = courses;
                cmbCourse.DisplayMember = "Title";
                cmbCourse.ValueMember = "CourseID";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"خطأ في تحميل البيانات الأولية: {ex.Message}\n{ex.InnerException?.Message}", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task LoadCourseLevelsAsync()
        {
            try
            {
                var courseLevels = await db.CourseLevels
                    .Include(cl => cl.Department)
                    .Include(cl => cl.Level)
                    .Include(cl => cl.Cours)
                    .ToListAsync();

                dataGridViewCourseLevels.DataSource = courseLevels;

                // إخفاء الأعمدة غير الضرورية
                if (dataGridViewCourseLevels.Columns.Contains("DepartmentID"))
                    dataGridViewCourseLevels.Columns["DepartmentID"].Visible = false;
                if (dataGridViewCourseLevels.Columns.Contains("LevelID"))
                    dataGridViewCourseLevels.Columns["LevelID"].Visible = false;
                if (dataGridViewCourseLevels.Columns.Contains("CourseID"))
                    dataGridViewCourseLevels.Columns["CourseID"].Visible = false;

                // تعيين أسماء الأعمدة باللغة العربية
                if (dataGridViewCourseLevels.Columns.Contains("Department"))
                    dataGridViewCourseLevels.Columns["Department"].HeaderText = "القسم";
                if (dataGridViewCourseLevels.Columns.Contains("Level"))
                    dataGridViewCourseLevels.Columns["Level"].HeaderText = "المستوى";
                if (dataGridViewCourseLevels.Columns.Contains("Course"))
                    dataGridViewCourseLevels.Columns["Course"].HeaderText = "المادة";

                ClearForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"خطأ في تحميل بيانات الروابط: {ex.Message}\n{ex.InnerException?.Message}", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearForm()
        {
            currentCourseLevel = null;
            cmbDepartment.SelectedIndex = -1;
            cmbLevel.SelectedIndex = -1;
            cmbCourse.SelectedIndex = -1;

            btnSave.Text = "حفظ جديد";
            btnDelete.Enabled = false;
        }

        private void BtnNew_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        private void DataGridViewCourseLevels_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridViewCourseLevels.SelectedRows.Count > 0)
            {
                var selectedRow = dataGridViewCourseLevels.SelectedRows[0];
                currentCourseLevel = selectedRow.DataBoundItem as SchedualApp.CourseLevel;

                if (currentCourseLevel != null)
                {
                    // تعيين القيم المختارة في القوائم المنسدلة
                    cmbDepartment.SelectedValue = currentCourseLevel.DepartmentID;
                    cmbLevel.SelectedValue = currentCourseLevel.LevelID;
                    cmbCourse.SelectedValue = currentCourseLevel.CourseID;

                    btnSave.Text = "تعديل";
                    btnDelete.Enabled = true;
                }
            }
            else
            {
                ClearForm();
            }
        }

        private async void BtnSave_ClickAsync(object sender, EventArgs e)
        {
            if (cmbDepartment.SelectedValue == null || cmbLevel.SelectedValue == null || cmbCourse.SelectedValue == null)
            {
                MessageBox.Show("الرجاء اختيار القسم والمستوى والمادة.", "بيانات ناقصة", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                int departmentId = (int)cmbDepartment.SelectedValue;
                int levelId = (int)cmbLevel.SelectedValue;
                int courseId = (int)cmbCourse.SelectedValue;

                // التحقق من عدم وجود رابط مكرر
                var existingLink = await db.CourseLevels
                    .FirstOrDefaultAsync(cl => cl.DepartmentID == departmentId && cl.LevelID == levelId && cl.CourseID == courseId);

                if (currentCourseLevel == null)
                {
                    // إضافة جديد
                    if (existingLink != null)
                    {
                        MessageBox.Show("هذا الرابط (القسم والمستوى والمادة) موجود بالفعل.", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    currentCourseLevel = new SchedualApp.CourseLevel
                    {
                        DepartmentID = departmentId,
                        LevelID = levelId,
                        CourseID = courseId
                    };
                    db.CourseLevels.Add(currentCourseLevel);
                    MessageBox.Show("تم إضافة الرابط بنجاح.", "نجاح", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    // تعديل موجود (يجب أن يكون التعديل نادراً هنا، لكن المنطق موجود)
                    if (existingLink != null && existingLink.CourseLevelID != currentCourseLevel.CourseLevelID)
                    {
                        MessageBox.Show("هذا الرابط (القسم والمستوى والمادة) موجود بالفعل.", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    currentCourseLevel.DepartmentID = departmentId;
                    currentCourseLevel.LevelID = levelId;
                    currentCourseLevel.CourseID = courseId;
                    db.Entry(currentCourseLevel).State = EntityState.Modified;
                    MessageBox.Show("تم تعديل الرابط بنجاح.", "نجاح", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                await db.SaveChangesAsync();
                await LoadCourseLevelsAsync(); // إعادة تحميل البيانات
            }
            catch (Exception ex)
            {
                MessageBox.Show($"خطأ في الحفظ: {ex.Message}\n{ex.InnerException?.Message}", "خطأ في قاعدة البيانات", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void BtnDelete_ClickAsync(object sender, EventArgs e)
        {
            if (currentCourseLevel == null) return;

            var result = MessageBox.Show("هل أنت متأكد من حذف هذا الرابط؟", "تأكيد الحذف", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    db.CourseLevels.Remove(currentCourseLevel);
                    await db.SaveChangesAsync();
                    MessageBox.Show("تم حذف الرابط بنجاح.", "نجاح", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    await LoadCourseLevelsAsync(); // إعادة تحميل البيانات
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"خطأ في الحذف: {ex.Message}\n{ex.InnerException?.Message}", "خطأ في قاعدة البيانات", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private void dataGridViewCourseLevels_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void cmbCourse_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void lblCourse_Click(object sender, EventArgs e)
        {

        }
    }
}
