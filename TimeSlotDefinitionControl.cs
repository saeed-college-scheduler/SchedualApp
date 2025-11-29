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

            // Set up DataGridView columns and event handlers
            dgvTimeSlots.AutoGenerateColumns = false;
            dgvTimeSlots.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "TimeSlotDefinitionID", HeaderText = "ID", Visible = false });
            dgvTimeSlots.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "SlotNumber", HeaderText = "Slot Number" });
            dgvTimeSlots.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "StartTime", HeaderText = "Start Time" });
            dgvTimeSlots.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "EndTime", HeaderText = "End Time" });

            dgvTimeSlots.SelectionChanged += dgvTimeSlots_SelectionChanged;
            btnSave.Click += async (s, e) => await BtnSave_ClickAsync(s, e);
            btnDelete.Click += async (s, e) => await BtnDelete_ClickAsync(s, e);
            btnNew.Click += BtnNew_Click;

            // Initial data load
            LoadDataAsync();
        }

        private async Task LoadDataAsync()
        {
            try
            {
                var timeSlots = await db.TimeSlotDefinitions.ToListAsync();
                dgvTimeSlots.DataSource = timeSlots;
                dgvTimeSlots.ClearSelection();
                ClearForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            }
            else
            {
                selectedTimeSlotDefinition = null;
                ClearForm();
                btnDelete.Enabled = false;
            }
        }

        private void PopulateForm(TimeSlotDefinition timeSlotDefinition)
        {
            if (timeSlotDefinition != null)
            {
                // Assuming txtSlotNumber is a TextBox for the integer SlotNumber
                txtSlotNumber.Text = timeSlotDefinition.SlotNumber.ToString();

                // Assuming we use MaskedTextBox or similar for TimeSpan input, 
                // or just a simple TextBox for demonstration purposes.
                // For TimeSpan, a simple string representation is used here.
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
                if (!TimeSpan.TryParse(txtStartTime.Text, out startTime) || !TimeSpan.TryParse(txtEndTime.Text, out endTime))
                {
                    MessageBox.Show("Invalid time format. Please use HH:MM.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (selectedTimeSlotDefinition == null)
                {
                    // New TimeSlotDefinition
                    selectedTimeSlotDefinition = new TimeSlotDefinition();
                    db.TimeSlotDefinitions.Add(selectedTimeSlotDefinition);
                }

                // Update properties
                selectedTimeSlotDefinition.SlotNumber = int.Parse(txtSlotNumber.Text);
                selectedTimeSlotDefinition.StartTime = startTime;
                selectedTimeSlotDefinition.EndTime = endTime;

                // Save changes to the database
                await db.SaveChangesAsync();

                // Reload data to update the DataGridView
                await LoadDataAsync();

                MessageBox.Show("Time Slot Definition saved successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task BtnDelete_ClickAsync(object sender, EventArgs e)
        {
            if (selectedTimeSlotDefinition == null) return;

            var result = MessageBox.Show($"Are you sure you want to delete Time Slot Definition ID: {selectedTimeSlotDefinition.TimeSlotDefinitionID}?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                try
                {
                    db.TimeSlotDefinitions.Remove(selectedTimeSlotDefinition);
                    await db.SaveChangesAsync();
                    await LoadDataAsync();
                    MessageBox.Show("Time Slot Definition deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(txtSlotNumber.Text) || string.IsNullOrWhiteSpace(txtStartTime.Text) || string.IsNullOrWhiteSpace(txtEndTime.Text))
            {
                MessageBox.Show("All fields are required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            int slotNumber;
            if (!int.TryParse(txtSlotNumber.Text, out slotNumber))
            {
                MessageBox.Show("Slot Number must be a valid integer.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

        private void TimeSlotDefinitionControl_Load(object sender, EventArgs e)
        {

        }

        private void panelForm_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
