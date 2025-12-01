using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SchedualApp
{
    public partial class CourseLevelManagementControl : UserControl
    {
        private SchedualAppModel db = new SchedualAppModel();
        private SchedualApp.CourseLevel currentCourseLevel;

        public CourseLevelManagementControl()
        {
            InitializeComponent();
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
                var departments = await db.Departments.ToListAsync();
                cmbDepartment.DataSource = departments;
                cmbDepartment.DisplayMember = "Name";
                cmbDepartment.ValueMember = "DepartmentID";

                var levels = await db.Levels.ToListAsync();
                cmbLevel.DataSource = levels;
                cmbLevel.DisplayMember = "Name";
                cmbLevel.ValueMember = "LevelID";

                // Corrected based on error CS1061: The entity set is likely plural 'Courses' in the DbContext
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
                // Corrected based on error CS1061: The navigation property is 'Course', not 'Cours'
                var courseLevelsData = await db.CourseLevels
                    .Select(cl => new
                    {
                        cl.CourseLevelID,
                        CourseName = cl.Cours.Title, // Assumes navigation property is 'Course'
                        DepartmentName = cl.Department.Name,
                        LevelName = cl.Level.Name,
                        cl.DepartmentID,
                        cl.LevelID,
                        cl.CourseID
                    })
                    .ToListAsync();

                dataGridViewCourseLevels.DataSource = courseLevelsData;

                if (dataGridViewCourseLevels.Columns.Contains("CourseLevelID"))
                    dataGridViewCourseLevels.Columns["CourseLevelID"].HeaderText = "الرقم";
                if (dataGridViewCourseLevels.Columns.Contains("CourseName"))
                    dataGridViewCourseLevels.Columns["CourseName"].HeaderText = "المادة";
                if (dataGridViewCourseLevels.Columns.Contains("DepartmentName"))
                    dataGridViewCourseLevels.Columns["DepartmentName"].HeaderText = "القسم";
                if (dataGridViewCourseLevels.Columns.Contains("LevelName"))
                    dataGridViewCourseLevels.Columns["LevelName"].HeaderText = "المستوى";

                if (dataGridViewCourseLevels.Columns.Contains("DepartmentID"))
                    dataGridViewCourseLevels.Columns["DepartmentID"].Visible = false;
                if (dataGridViewCourseLevels.Columns.Contains("LevelID"))
                    dataGridViewCourseLevels.Columns["LevelID"].Visible = false;
                if (dataGridViewCourseLevels.Columns.Contains("CourseID"))
                    dataGridViewCourseLevels.Columns["CourseID"].Visible = false;

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

        private async void DataGridViewCourseLevels_SelectionChanged(object sender, EventArgs e)
        {

            if (dataGridViewCourseLevels.SelectedRows.Count > 0)
            {
                try
                {
                    var selectedRow = dataGridViewCourseLevels.SelectedRows[0];
                    var selectedItem = selectedRow.DataBoundItem;
                    if (selectedItem != null)
                    {
                        int courseLevelId = (int)selectedItem.GetType().GetProperty("CourseLevelID").GetValue(selectedItem, null);
                        currentCourseLevel = await db.CourseLevels.FindAsync(courseLevelId);

                        if (currentCourseLevel != null)
                        {
                            cmbDepartment.SelectedValue = currentCourseLevel.DepartmentID;
                            cmbLevel.SelectedValue = currentCourseLevel.LevelID;
                            cmbCourse.SelectedValue = currentCourseLevel.CourseID;

                            btnSave.Text = "تعديل";
                            btnDelete.Enabled = true;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"خطأ في تحديد الصف: {ex.Message}", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

                if (currentCourseLevel == null) // New record
                {
                    var existingLink = await db.CourseLevels.FirstOrDefaultAsync(cl => cl.DepartmentID == departmentId && cl.LevelID == levelId && cl.CourseID == courseId);
                    if (existingLink != null)
                    {
                        MessageBox.Show("هذا الرابط (القسم والمستوى والمادة) موجود بالفعل.", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    var newCourseLevel = new CourseLevel
                    {
                        DepartmentID = departmentId,
                        LevelID = levelId,
                        CourseID = courseId
                    };
                    db.CourseLevels.Add(newCourseLevel);
                    MessageBox.Show("تم إضافة الرابط بنجاح.", "نجاح", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else // Update existing record
                {
                    db.Entry(currentCourseLevel).State = EntityState.Modified;
                    currentCourseLevel.DepartmentID = departmentId;
                    currentCourseLevel.LevelID = levelId;
                    currentCourseLevel.CourseID = courseId;
                    MessageBox.Show("تم تعديل الرابط بنجاح.", "نجاح", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                await db.SaveChangesAsync();
                await LoadCourseLevelsAsync();
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
                    await LoadCourseLevelsAsync();
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
    }
}
