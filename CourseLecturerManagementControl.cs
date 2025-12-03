using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SchedualApp
{
    public partial class CourseLecturerManagementControl : UserControl
    {
        private SchedualAppModel db = new SchedualAppModel();
        private SchedualApp.CourseLecturer currentCourseLecturer;

        public CourseLecturerManagementControl()
        {
            InitializeComponent();
            //cmbCourse.RightToLeft = RightToLeft.Yes;
            //cmbLecturer.RightToLeft = RightToLeft.Yes;
            //cmbTeachingType.RightToLeft = RightToLeft.Yes;
        }

        private async void CourseLecturerManagementControl_Load(object sender, EventArgs e)
        {
            await LoadInitialDataAsync();
            await LoadCourseLecturersAsync();
        }

        private async Task LoadInitialDataAsync()
        {
            try
            {
                // 1. تحميل المواد (Courses)
                // نفترض أن اسم DbSet هو 'Courses'
                if (db.Courses != null)
                {
                   
                    var courses = await db.Courses.ToListAsync();
                    if (cmbCourse != null) // <--- الإضافة الجديدة
                    {
                        cmbCourse.DataSource = courses;
                        cmbCourse.DisplayMember = "Name";
                        cmbCourse.ValueMember = "CourseID";
                    }
                }
                else
                {
                    MessageBox.Show("DbSet 'Courses' is null.", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                // 2. تحميل المحاضرين (Lecturers)
                // نفترض أن اسم DbSet هو 'Lecturers'
                if (db.Lecturers != null)
                {
                    var lecturers = await db.Lecturers.ToListAsync();
                    if(cmbLecturer != null)
                    {

                    cmbLecturer.DataSource = lecturers;
                    cmbLecturer.DisplayMember = "Name";
                    cmbLecturer.ValueMember = "LecturerID";
                    }
                }
                // 3. نوع التدريس (TeachingType) يتم تعبئته في ملف Designer.cs
            }
            catch (Exception ex)
            {
                MessageBox.Show($"خطأ في تحميل البيانات الأولية: {ex.Message}\n{ex.InnerException?.Message}\n{ex.StackTrace}", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task LoadCourseLecturersAsync()
        {
            if (dataGridViewCourseLecturers == null)
            {
                MessageBox.Show("dataGridViewCourseLecturers is not initialized.", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; // <--- الإضافة الجديدة
            }
            try
            {
                // استخدام Select لعرض أسماء الكيانات بدلاً من الأرقام التعريفية
                var courseLecturersData = await db.CourseLecturers
                    .Include(cl => cl.Cours)    // <--- الإضافة الجديدة
                    .Include(cl => cl.Lecturer) // <--- الإضافة الجديدة
                    .Select(cl => new
                    {
                        cl.CourseLecturerID,
                        CourseName = cl.Cours.Title, // Assumes navigation property is 'Cours' as per user's model
                        LecturerName = cl.Lecturer.FirstName+" "+ cl.Lecturer.LastName,
                        cl.TeachingType,
                        cl.CourseID,
                        cl.LecturerID
                    })
                    .ToListAsync();

                dataGridViewCourseLecturers.DataSource = courseLecturersData;
                dataGridViewCourseLecturers.Refresh();
                dataGridViewCourseLecturers.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);

                // تعيين أسماء الأعمدة باللغة العربية
                if (dataGridViewCourseLecturers.Columns.Contains("CourseLecturerID"))
                    dataGridViewCourseLecturers.Columns["CourseLecturerID"].HeaderText = "الرقم";
                if (dataGridViewCourseLecturers.Columns.Contains("CourseName"))
                    dataGridViewCourseLecturers.Columns["CourseName"].HeaderText = "المادة";
                if (dataGridViewCourseLecturers.Columns.Contains("LecturerName"))
                    dataGridViewCourseLecturers.Columns["LecturerName"].HeaderText = "المحاضر";
                if (dataGridViewCourseLecturers.Columns.Contains("TeachingType"))
                    dataGridViewCourseLecturers.Columns["TeachingType"].HeaderText = "نوع التدريس";

                // إخفاء الأعمدة غير الضرورية (التي تحتوي على IDs)
                if (dataGridViewCourseLecturers.Columns.Contains("CourseID"))
                    dataGridViewCourseLecturers.Columns["CourseID"].Visible = false;
                if (dataGridViewCourseLecturers.Columns.Contains("LecturerID"))
                    dataGridViewCourseLecturers.Columns["LecturerID"].Visible = false;

                ClearForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"خطأ في تحميل بيانات روابط المحاضرين: {ex.Message}\n{ex.InnerException?.Message}\n{ex.StackTrace}", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearForm()
        {
            currentCourseLecturer = null;
            if (cmbCourse == null || cmbLecturer == null || cmbTeachingType == null || btnSave == null || btnDelete == null) return; // السطر 128 (تم إضافته)
            cmbCourse.SelectedIndex = -1;
            cmbLecturer.SelectedIndex = -1;
            cmbTeachingType.SelectedIndex = -1;
            btnSave.Text = "Save";
            btnDelete.Enabled = false;
        }

        private void BtnNew_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        private async void DataGridViewCourseLecturers_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridViewCourseLecturers == null) return;
            if (dataGridViewCourseLecturers.SelectedRows.Count > 0)
            {
                try
                {
                    var selectedRow = dataGridViewCourseLecturers.SelectedRows[0];
                    var selectedItem = selectedRow.DataBoundItem;

                    if (selectedItem != null)
                    {
                        // استخدام الانعكاس (Reflection) للحصول على قيم الخصائص
                        int courseLecturerId = (int)selectedItem.GetType().GetProperty("CourseLecturerID").GetValue(selectedItem, null);
                        currentCourseLecturer = await db.CourseLecturers.FindAsync(courseLecturerId);

                        if (currentCourseLecturer != null)
                        {
                            cmbCourse.SelectedValue = currentCourseLecturer.CourseID;
                            cmbLecturer.SelectedValue = currentCourseLecturer.LecturerID;
                            cmbTeachingType.SelectedItem = currentCourseLecturer.TeachingType;

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
            if (cmbCourse.SelectedValue == null || cmbLecturer.SelectedValue == null || cmbTeachingType.SelectedItem == null)
            {
                MessageBox.Show("الرجاء اختيار المادة والمحاضر ونوع التدريس.", "بيانات ناقصة", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                int courseId = (int)cmbCourse.SelectedValue;
                int lecturerId = (int)cmbLecturer.SelectedValue;
                string teachingType = cmbTeachingType.SelectedItem.ToString();

                if (currentCourseLecturer == null) // New record
                {
                    // التحقق من عدم وجود رابط مكرر
                    var existingLink = await db.CourseLecturers
                        .FirstOrDefaultAsync(cl => cl.CourseID == courseId && cl.LecturerID == lecturerId && cl.TeachingType == teachingType);

                    if (existingLink != null)
                    {
                        MessageBox.Show("هذا الرابط (المادة والمحاضر ونوع التدريس) موجود بالفعل.", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    var newCourseLecturer = new CourseLecturer
                    {
                        CourseID = courseId,
                        LecturerID = lecturerId,
                        TeachingType = teachingType
                    };
                    db.CourseLecturers.Add(newCourseLecturer);
                    MessageBox.Show("تم إضافة الرابط بنجاح.", "نجاح", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else // Update existing record
                {
                    // التحقق من عدم وجود رابط مكرر عند التعديل
                    var existingLink = await db.CourseLecturers
                        .FirstOrDefaultAsync(cl => cl.CourseID == courseId && cl.LecturerID == lecturerId && cl.TeachingType == teachingType && cl.CourseLecturerID != currentCourseLecturer.CourseLecturerID);

                    if (existingLink != null)
                    {
                        MessageBox.Show("هذا الرابط (المادة والمحاضر ونوع التدريس) موجود بالفعل.", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    currentCourseLecturer.CourseID = courseId;
                    currentCourseLecturer.LecturerID = lecturerId;
                    currentCourseLecturer.TeachingType = teachingType;
                    db.Entry(currentCourseLecturer).State = EntityState.Modified;
                    MessageBox.Show("تم تعديل الرابط بنجاح.", "نجاح", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                await db.SaveChangesAsync();
                await LoadCourseLecturersAsync(); // إعادة تحميل البيانات
            }
            catch (Exception ex)
            {
                MessageBox.Show($"خطأ في الحفظ: {ex.Message}\n{ex.InnerException?.Message}", "خطأ في قاعدة البيانات", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void BtnDelete_ClickAsync(object sender, EventArgs e)
        {
            if (currentCourseLecturer == null) return;

            var result = MessageBox.Show("هل أنت متأكد من حذف هذا الرابط؟", "تأكيد الحذف", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    db.CourseLecturers.Remove(currentCourseLecturer);
                    await db.SaveChangesAsync();
                    MessageBox.Show("تم حذف الرابط بنجاح.", "نجاح", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    await LoadCourseLecturersAsync(); // إعادة تحميل البيانات
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

        private void dataGridViewCourseLecturers_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
