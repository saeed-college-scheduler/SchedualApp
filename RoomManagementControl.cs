using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SchedualApp
{
    public partial class RoomManagementControl : UserControl
    {
        private SchedualAppModel db = new SchedualAppModel();
        private Room selectedRoom;

        // كلاس مساعد لربط نوع القاعة (الاسم بالعربي والقيمة بالانجليزي)
        public class RoomTypeItem
        {
            public string Text { get; set; }
            public string Value { get; set; }
        }

        public RoomManagementControl()
        {
            InitializeComponent();

            // --- إعدادات اللغة العربية ---
            this.RightToLeft = RightToLeft.Yes;
            txtRoomName.RightToLeft = RightToLeft.Yes;
            txtCapacity.RightToLeft = RightToLeft.Yes;
            cmbRoomType.RightToLeft = RightToLeft.Yes;

            // 1. إعداد قائمة أنواع القاعات (عربي للمستخدم، إنجليزي للداتابيس)
            var roomTypes = new List<RoomTypeItem>
            {
                new RoomTypeItem { Text = "قاعة محاضرات (نظري)", Value = "Lecture" },
                new RoomTypeItem { Text = "معمل (عملي)", Value = "Practical" } // أو "Lab" حسب ما تعتمده في الداتابيس
            };

            cmbRoomType.DataSource = roomTypes;
            cmbRoomType.DisplayMember = "Text";
            cmbRoomType.ValueMember = "Value";

            // 2. إعداد الأعمدة برمجياً لضمان التعريب
            dgvRooms.AutoGenerateColumns = false;
            dgvRooms.Columns.Clear();
            dgvRooms.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "RoomID", HeaderText = "ID", Visible = false });
            dgvRooms.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Name", HeaderText = "اسم القاعة" });
            dgvRooms.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Capacity", HeaderText = "السعة" });
            dgvRooms.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "RoomType", HeaderText = "النوع" });

            // 3. ربط الأحداث
            this.Load += async (s, e) => await LoadDataAsync();
            dgvRooms.SelectionChanged += dgvRooms_SelectionChanged;
            btnSave.Click += async (s, e) => await BtnSave_ClickAsync(s, e);
            btnDelete.Click += async (s, e) => await BtnDelete_ClickAsync(s, e);
            btnNew.Click += BtnNew_Click;
        }

        private async Task LoadDataAsync()
        {
            try
            {
                var rooms = await db.Rooms.AsNoTracking().ToListAsync();
                dgvRooms.DataSource = rooms;
                dgvRooms.ClearSelection();
                ClearForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"خطأ في تحميل البيانات: {ex.Message}", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvRooms_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvRooms.SelectedRows.Count > 0)
            {
                var selectedRow = dgvRooms.SelectedRows[0];
                selectedRoom = selectedRow.DataBoundItem as Room;
                PopulateForm(selectedRoom);
                btnDelete.Enabled = true;
                btnSave.Text = "تعديل";
            }
            else
            {
                selectedRoom = null;
                ClearForm();
                btnDelete.Enabled = false;
                btnSave.Text = "حفظ";
            }
        }

        private void PopulateForm(Room room)
        {
            if (room != null)
            {
                txtRoomName.Text = room.Name;
                txtCapacity.Text = room.Capacity.ToString();

                // تحديد العنصر في القائمة بناءً على القيمة المخزنة (Lecture/Practical)
                cmbRoomType.SelectedValue = room.RoomType;
            }
        }

        private void ClearForm()
        {
            txtRoomName.Clear();
            txtCapacity.Clear();
            selectedRoom = null;
            if (cmbRoomType.Items.Count > 0) cmbRoomType.SelectedIndex = 0;
            btnSave.Text = "حفظ";
            btnDelete.Enabled = false;
        }

        private void BtnNew_Click(object sender, EventArgs e)
        {
            ClearForm();
            dgvRooms.ClearSelection();
            txtRoomName.Focus();
        }

        private async Task BtnSave_ClickAsync(object sender, EventArgs e)
        {
            if (!ValidateInput()) return;

            try
            {
                if (selectedRoom == null)
                {
                    selectedRoom = new Room();
                    db.Rooms.Add(selectedRoom);
                }
                else
                {
                    // إعادة ربط الكائن بالسياق إذا كان مفصولاً (للتعديل)
                    db.Entry(selectedRoom).State = EntityState.Modified;
                }

                selectedRoom.Name = txtRoomName.Text;
                selectedRoom.Capacity = int.Parse(txtCapacity.Text);

                // حفظ القيمة الإنجليزية (Lecture/Practical)
                selectedRoom.RoomType = cmbRoomType.SelectedValue.ToString();

                await db.SaveChangesAsync();
                await LoadDataAsync();

                MessageBox.Show("تم حفظ بيانات القاعة بنجاح.", "نجاح", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"حدث خطأ أثناء الحفظ: {ex.Message}", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task BtnDelete_ClickAsync(object sender, EventArgs e)
        {
            if (selectedRoom == null) return;

            var result = MessageBox.Show($"هل أنت متأكد من حذف القاعة: {selectedRoom.Name}؟", "تأكيد الحذف", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                try
                {
                    // يجب جلب الكائن من السياق الحالي لحذفه إذا كان مفصولاً
                    var roomToDelete = await db.Rooms.FindAsync(selectedRoom.RoomID);
                    if (roomToDelete != null)
                    {
                        db.Rooms.Remove(roomToDelete);
                        await db.SaveChangesAsync();
                        await LoadDataAsync();
                        MessageBox.Show("تم حذف القاعة بنجاح.", "نجاح", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            if (string.IsNullOrWhiteSpace(txtRoomName.Text) || string.IsNullOrWhiteSpace(txtCapacity.Text) || cmbRoomType.SelectedValue == null)
            {
                MessageBox.Show("يرجى تعبئة جميع الحقول المطلوبة.", "بيانات ناقصة", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (!int.TryParse(txtCapacity.Text, out int cap) || cap <= 0)
            {
                MessageBox.Show("السعة يجب أن تكون رقماً صحيحاً أكبر من صفر.", "خطأ في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

        // Event Handlers غير المستخدمة
        private void cmbRoomType_SelectedIndexChanged(object sender, EventArgs e) { }
        private void panelForm_Paint(object sender, PaintEventArgs e) { }
    }
}