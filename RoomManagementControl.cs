using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Entity;

namespace SchedualApp
{
    public partial class RoomManagementControl : UserControl
    {
        private SchedualAppModel db = new SchedualAppModel();
        private Room selectedRoom;

        public RoomManagementControl()
        {
            InitializeComponent();

            // 1. Initial data load - يجب أن يتم استدعاؤه بعد InitializeComponent()
            LoadDataAsync();

            // 2. Set up DataGridView columns and event handlers
            dgvRooms.AutoGenerateColumns = false;
            // تم إضافة عمود RoomType
            dgvRooms.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "RoomID", HeaderText = "ID", Visible = false });
            dgvRooms.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Name", HeaderText = "Room Name" });
            dgvRooms.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Capacity", HeaderText = "Capacity" });
            dgvRooms.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "RoomType", HeaderText = "Type" });

            dgvRooms.SelectionChanged += dgvRooms_SelectionChanged;
            btnSave.Click += async (s, e) => await BtnSave_ClickAsync(s, e);
            btnDelete.Click += async (s, e) => await BtnDelete_ClickAsync(s, e);
            btnNew.Click += BtnNew_Click;

            // تهيئة ComboBox
            // التأكد من وجود عناصر قبل تعيين SelectedIndex
            if (cmbRoomType.Items.Count > 0)
            {
                cmbRoomType.SelectedIndex = 0;
            }
        }

        private async Task LoadDataAsync()
        {
            try
            {
                if (dgvRooms == null)
                {
                    MessageBox.Show("Error: DataGridView (dgvRooms) is not initialized.", "Initialization Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var rooms = await db.Rooms.ToListAsync();
                dgvRooms.DataSource = rooms;
                dgvRooms.ClearSelection();
                ClearForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading data: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            }
            else
            {
                selectedRoom = null;
                ClearForm();
                btnDelete.Enabled = false;
            }
        }

        private void PopulateForm(Room room)
        {
            if (room != null)
            {
                txtRoomName.Text = room.Name;
                txtCapacity.Text = room.Capacity.ToString();
                // تعيين قيمة ComboBox
                cmbRoomType.SelectedItem = room.RoomType;
            }
        }

        private void ClearForm()
        {
            txtRoomName.Clear();
            txtCapacity.Clear();
            selectedRoom = null;
            // تعيين قيمة افتراضية للـ ComboBox
            if (cmbRoomType.Items.Count > 0)
            {
                cmbRoomType.SelectedIndex = 0;
            }
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
                    // New Room
                    selectedRoom = new Room();
                    db.Rooms.Add(selectedRoom);
                }

                // Update properties
                selectedRoom.Name = txtRoomName.Text;
                selectedRoom.Capacity = int.Parse(txtCapacity.Text);
                // تعيين قيمة RoomType من ComboBox
                selectedRoom.RoomType = cmbRoomType.SelectedItem.ToString();

                // Save changes to the database
                await db.SaveChangesAsync();

                // Reload data to update the DataGridView
                await LoadDataAsync();

                MessageBox.Show("Room saved successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException ex)
            {
                // هذا الجزء يعالج أخطاء التحقق من الكيان (Validation Errors)
                var errorMessages = ex.EntityValidationErrors
                    .SelectMany(x => x.ValidationErrors)
                    .Select(x => x.PropertyName + ": " + x.ErrorMessage);

                var fullErrorMessage = string.Join(Environment.NewLine, errorMessages);
                var exceptionMessage = $"Validation failed for one or more entities. Errors: {fullErrorMessage}";

                MessageBox.Show(exceptionMessage, "Error saving data (Validation)", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (System.Data.Entity.Infrastructure.DbUpdateException ex)
            {
                // **السطر المصحح: عرض الاستثناء الداخلي من قاعدة البيانات**
                string innerMessage = ex.InnerException != null && ex.InnerException.InnerException != null
                                    ? ex.InnerException.InnerException.Message
                                    : ex.Message;

                MessageBox.Show($"Error saving data (Database Update): {innerMessage}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task BtnDelete_ClickAsync(object sender, EventArgs e)
        {
            if (selectedRoom == null) return;

            var result = MessageBox.Show($"Are you sure you want to delete Room: {selectedRoom.Name}?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                try
                {
                    db.Rooms.Remove(selectedRoom);
                    await db.SaveChangesAsync();
                    await LoadDataAsync();
                    MessageBox.Show("Room deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(txtRoomName.Text) || string.IsNullOrWhiteSpace(txtCapacity.Text) || cmbRoomType.SelectedItem == null)
            {
                MessageBox.Show("All fields are required (including Room Type).", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            int capacity;
            if (!int.TryParse(txtCapacity.Text, out capacity))
            {
                MessageBox.Show("Capacity must be a valid integer.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        // Dispose method to clean up the DbContext
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private void cmbRoomType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
