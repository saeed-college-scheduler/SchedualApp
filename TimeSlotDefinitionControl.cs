using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Entity;

namespace SchedualApp
{
    public partial class TimeSlotDefinitionControl : UserControl
    {
        private SchedualAppModel db = new SchedualAppModel();
        private TimeSlotDefinition selectedTimeSlotDefinition;

        public TimeSlotDefinitionControl()
        {
            InitializeComponent();

            // --- إعدادات اللغة العربية ---
            this.RightToLeft = RightToLeft.Yes;
            txtSlotNumber.RightToLeft = RightToLeft.Yes;
            txtStartTime.RightToLeft = RightToLeft.Yes;
            txtEndTime.RightToLeft = RightToLeft.Yes;

            // إعداد أعمدة الجدول
            dgvTimeSlots.AutoGenerateColumns = false;
            dgvTimeSlots.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "TimeSlotDefinitionID", HeaderText = "ID", Visible = false });
            dgvTimeSlots.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "SlotNumber", HeaderText = "رقم الفترة" });
            dgvTimeSlots.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "StartTime", HeaderText = "وقت البدء" });
            dgvTimeSlots.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "EndTime", HeaderText = "وقت النهاية" });

            // تنسيق عرض الوقت في الأعمدة (اختياري ليكون HH:mm فقط)
            dgvTimeSlots.Columns[2].DefaultCellStyle.Format = @"hh\:mm";
            dgvTimeSlots.Columns[3].DefaultCellStyle.Format = @"hh\:mm";

            dgvTimeSlots.SelectionChanged += dgvTimeSlots_SelectionChanged;
            btnSave.Click += async (s, e) => await BtnSave_ClickAsync(s, e);
            btnDelete.Click += async (s, e) => await BtnDelete_ClickAsync(s, e);
            btnNew.Click += BtnNew_Click;

            // تحميل البيانات
            LoadDataAsync();
        }

        private async Task LoadDataAsync()
        {
            try
            {
                var timeSlots = await db.TimeSlotDefinitions.AsNoTracking().OrderBy(t => t.SlotNumber).ToListAsync();
                dgvTimeSlots.DataSource = timeSlots;
                dgvTimeSlots.ClearSelection();
                ClearForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"خطأ في تحميل البيانات: {ex.Message}", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvTimeSlots_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvTimeSlots.SelectedRows.Count > 0)
            {
                var selectedRow = dgvTimeSlots.SelectedRows[0];
                selectedTimeSlotDefinition = selectedRow.DataBoundItem as TimeSlotDefinition;
                PopulateForm(selectedTimeSlotDefinition);
                btnDelete.Enabled = true;
                btnSave.Text = "تعديل";
            }
            else
            {
                selectedTimeSlotDefinition = null;
                ClearForm();
                btnDelete.Enabled = false;
                btnSave.Text = "حفظ";
            }
        }

        private void PopulateForm(TimeSlotDefinition timeSlotDefinition)
        {
            if (timeSlotDefinition != null)
            {
                txtSlotNumber.Text = timeSlotDefinition.SlotNumber.ToString();
                // عرض الوقت بتنسيق HH:mm
                txtStartTime.Text = timeSlotDefinition.StartTime.ToString(@"hh\:mm");
                txtEndTime.Text = timeSlotDefinition.EndTime.ToString(@"hh\:mm");
            }
        }

        private void ClearForm()
        {
            txtSlotNumber.Clear();
            txtStartTime.Clear();
            txtEndTime.Clear();
            selectedTimeSlotDefinition = null;
            btnSave.Text = "حفظ";
            btnDelete.Enabled = false;
        }

        private void BtnNew_Click(object sender, EventArgs e)
        {
            ClearForm();
            dgvTimeSlots.ClearSelection();
            txtSlotNumber.Focus();
        }

        private async Task BtnSave_ClickAsync(object sender, EventArgs e)
        {
            if (!ValidateInput()) return;

            try
            {
                TimeSpan startTime, endTime;
                // محاولة تحليل الوقت المدخل
                if (!TimeSpan.TryParse(txtStartTime.Text, out startTime) || !TimeSpan.TryParse(txtEndTime.Text, out endTime))
                {
                    MessageBox.Show("صيغة الوقت غير صحيحة. يرجى استخدام الصيغة HH:MM (مثال 08:00).", "خطأ في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (selectedTimeSlotDefinition == null)
                {
                    // جديد
                    selectedTimeSlotDefinition = new TimeSlotDefinition();
                    db.TimeSlotDefinitions.Add(selectedTimeSlotDefinition);
                }
                else
                {
                    // تعديل
                    db.Entry(selectedTimeSlotDefinition).State = EntityState.Modified;
                }

                selectedTimeSlotDefinition.SlotNumber = int.Parse(txtSlotNumber.Text);
                selectedTimeSlotDefinition.StartTime = startTime;
                selectedTimeSlotDefinition.EndTime = endTime;

                await db.SaveChangesAsync();
                await LoadDataAsync();

                MessageBox.Show("تم حفظ بيانات الفترة الزمنية بنجاح.", "نجاح", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"خطأ في الحفظ: {ex.Message}", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task BtnDelete_ClickAsync(object sender, EventArgs e)
        {
            if (selectedTimeSlotDefinition == null) return;

            var result = MessageBox.Show($"هل أنت متأكد من حذف الفترة رقم: {selectedTimeSlotDefinition.SlotNumber}؟", "تأكيد الحذف", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                try
                {
                    // إعادة جلب الكائن للحذف الآمن
                    var itemToDelete = await db.TimeSlotDefinitions.FindAsync(selectedTimeSlotDefinition.TimeSlotDefinitionID);
                    if (itemToDelete != null)
                    {
                        db.TimeSlotDefinitions.Remove(itemToDelete);
                        await db.SaveChangesAsync();
                        await LoadDataAsync();
                        MessageBox.Show("تم الحذف بنجاح.", "نجاح", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"خطأ في الحذف: {ex.Message}", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(txtSlotNumber.Text) || string.IsNullOrWhiteSpace(txtStartTime.Text) || string.IsNullOrWhiteSpace(txtEndTime.Text))
            {
                MessageBox.Show("يرجى تعبئة جميع الحقول.", "بيانات ناقصة", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (!int.TryParse(txtSlotNumber.Text, out _))
            {
                MessageBox.Show("رقم الفترة يجب أن يكون رقماً صحيحاً.", "خطأ في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private void TimeSlotDefinitionControl_Load(object sender, EventArgs e) { }
        private void panelForm_Paint(object sender, PaintEventArgs e) { }
    }
}