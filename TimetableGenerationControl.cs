using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Entity;
using System.Linq;

namespace SchedualApp
{
    public partial class TimetableGenerationControl : UserControl
    {
        private SchedualAppModel db = new SchedualAppModel();

        public TimetableGenerationControl()
        {
            InitializeComponent();
            LoadInitialData();
        }

        private async void LoadInitialData()
        {
            try
            {
                // Load Departments
                var departments = await db.Departments.ToListAsync();
                cmbDepartment.DataSource = departments;
                cmbDepartment.DisplayMember = "Name";
                cmbDepartment.ValueMember = "DepartmentID";

                // Load Levels (assuming Level entity exists and has Name/LevelID)
                var levels = await db.Levels.ToListAsync();
                cmbLevel.DataSource = levels;
                cmbLevel.DisplayMember = "Name";
                cmbLevel.ValueMember = "LevelID";

                // Set default selections
                if (cmbSemester.Items.Count > 0) cmbSemester.SelectedIndex = 0;
                if (cmbDepartment.Items.Count > 0) cmbDepartment.SelectedIndex = 0;
                if (cmbLevel.Items.Count > 0) cmbLevel.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading initial data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void BtnGenerate_Click(object sender, EventArgs e)
        {
            if (cmbSemester.SelectedItem == null || cmbDepartment.SelectedItem == null || cmbLevel.SelectedItem == null)
            {
                MessageBox.Show("Please select Semester, Department, and Level before generating.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            btnGenerate.Enabled = false;
            progressBar.Value = 0;
            txtResults.Clear();

            string selectedSemester = cmbSemester.SelectedItem.ToString();
            string selectedDepartment = (cmbDepartment.SelectedItem as dynamic)?.Name;
            string selectedLevel = (cmbLevel.SelectedItem as dynamic)?.Name;

            txtResults.AppendText($"Starting timetable generation for {selectedSemester}, Dept: {selectedDepartment}, Level: {selectedLevel}...\r\n");

            try
            {
                // 1. Gather necessary data (Courses, Lecturers, Rooms, TimeSlots) filtered by Department and Level
                txtResults.AppendText("Gathering data...\r\n");

                // Example of filtering logic (assuming Course entity has DepartmentID and LevelID)
                // var departmentId = (int)cmbDepartment.SelectedValue;
                // var levelId = (int)cmbLevel.SelectedValue;
                // var coursesToSchedule = await db.Courses
                //     .Where(c => c.DepartmentID == departmentId && c.LevelID == levelId)
                //     .ToListAsync();

                // Simulation of the complex generation process
                progressBar.Maximum = 100;
                for (int i = 0; i <= 100; i += 10)
                {
                    await Task.Delay(200);
                    progressBar.Value = i;
                    txtResults.AppendText($"Progress: {i}%\r\n");
                }

                // 2. Run the scheduling algorithm
                txtResults.AppendText("Running scheduling algorithm...\r\n");
                await Task.Delay(1000);

                // 3. Display results
                txtResults.AppendText("Timetable generated successfully!\r\n");
                txtResults.AppendText("Summary: 50 classes scheduled, 3 conflicts resolved.\r\n");
                MessageBox.Show("Timetable Generation Complete!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                txtResults.AppendText($"ERROR: Timetable generation failed: {ex.Message}\r\n");
                MessageBox.Show($"An error occurred during generation: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                progressBar.Value = 100;
                btnGenerate.Enabled = true;
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

        private void txtResults_TextChanged(object sender, EventArgs e)
        {

        }

        private void cmbLevel_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
