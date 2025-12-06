using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SchedualApp
{
    public partial class DepartmentManagementControl : UserControl
    {
        private SchedualAppModel db = new SchedualAppModel();
        private SchedualApp.Department currentDepartment;

        public DepartmentManagementControl()
        {
            InitializeComponent();

            // --- إعدادات اللغة العربية ---
            this.RightToLeft = RightToLeft.Yes; // جعل الواجهة من اليمين لليسار
            txtDepartmentName.RightToLeft = RightToLeft.Yes;
            txtDepartmentDescription.RightToLeft = RightToLeft.Yes;
        }

        private async void DepartmentManagementControl_Load(object sender, EventArgs e)
        {
            await LoadDataAsync();
        }

        private async Task LoadDataAsync()
        {
            try
            {
                var departments = await db.Departments.ToListAsync();
                dataGridViewDepartments.DataSource = departments;

                // إخفاء الأعمدة غير الضرورية
                if (dataGridViewDepartments.Columns.Contains("DepartmentID"))
                    dataGridViewDepartments.Columns["DepartmentID"].Visible = false;
                if (dataGridViewDepartments.Columns.Contains("Courses"))
                    dataGridViewDepartments.Columns["Courses"].Visible = false;
                if (dataGridViewDepartments.Columns.Contains("CourseLevels"))
                    dataGridViewDepartments.Columns["CourseLevels"].Visible = false;
                if (dataGridViewDepartments.Columns.Contains("Timetables"))
                    dataGridViewDepartments.Columns["Timetables"].Visible = false;

                // تعيين أسماء الأعمدة باللغة العربية (تأكيد إضافي للكود الموجود في المصمم)
                if (dataGridViewDepartments.Columns.Contains("Name"))
                    dataGridViewDepartments.Columns["Name"].HeaderText = "اسم القسم";
                if (dataGridViewDepartments.Columns.Contains("Description"))
                    dataGridViewDepartments.Columns["Description"].HeaderText = "وصف القسم";

                ClearForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"خطأ في تحميل البيانات: {ex.Message}\n{ex.InnerException?.Message}", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearForm()
        {
            currentDepartment = null;
            txtDepartmentName.Clear();
            txtDepartmentDescription.Clear();

            btnDelete.Enabled = false;
            btnSave.Text = "حفظ"; // تم التعريب
        }

        private void BtnNew_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        private void DataGridViewDepartments_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridViewDepartments.SelectedRows.Count > 0)
            {
                var selectedRow = dataGridViewDepartments.SelectedRows[0];
                currentDepartment = selectedRow.DataBoundItem as Department;

                if (currentDepartment != null)
                {
                    txtDepartmentName.Text = currentDepartment.Name;
                    txtDepartmentDescription.Text = currentDepartment.Description; // تصحيح: عرض الوصف عند الاختيار
                    btnDelete.Enabled = true;
                    btnSave.Text = "تعديل";
                }
            }
            else
            {
                ClearForm();
            }
        }

        private async void BtnSave_ClickAsync(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtDepartmentName.Text))
            {
                MessageBox.Show("الرجاء إدخال اسم القسم.", "بيانات ناقصة", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                if (currentDepartment == null)
                {
                    // إضافة جديد
                    currentDepartment = new SchedualApp.Department
                    {
                        Name = txtDepartmentName.Text,
                        Description = txtDepartmentDescription.Text
                    };
                    db.Departments.Add(currentDepartment);
                    MessageBox.Show("تم إضافة القسم بنجاح.", "نجاح", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    // تعديل موجود
                    currentDepartment.Name = txtDepartmentName.Text;
                    currentDepartment.Description = txtDepartmentDescription.Text;
                    db.Entry(currentDepartment).State = EntityState.Modified;
                    MessageBox.Show("تم تعديل القسم بنجاح.", "نجاح", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                await db.SaveChangesAsync();
                await LoadDataAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"خطأ في الحفظ: {ex.Message}\n{ex.InnerException?.Message}", "خطأ في قاعدة البيانات", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void BtnDelete_ClickAsync(object sender, EventArgs e)
        {
            if (currentDepartment == null) return;

            var result = MessageBox.Show($"هل أنت متأكد من حذف القسم: {currentDepartment.Name}؟", "تأكيد الحذف", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    db.Departments.Remove(currentDepartment);
                    await db.SaveChangesAsync();
                    MessageBox.Show("تم حذف القسم بنجاح.", "نجاح", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    await LoadDataAsync();
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

        // الدوال الفارغة يمكن تركها أو حذفها
        private void lblDepartmentName_Click(object sender, EventArgs e) { }
        private void dataGridViewDepartments_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
        private void txtDepartmentName_TextChanged(object sender, EventArgs e) { }

        private void panelForm_Paint(object sender, PaintEventArgs e)
        {

        }

        private void lblDepartmentDescription_Click(object sender, EventArgs e)
        {

        }
    }
}