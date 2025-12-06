using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;

namespace SchedualApp
{
    public partial class LecturerManagementControl : UserControl
    {
        // Helper class for CheckedListBox items (Availability)
        public class AvailabilityItem
        {
            public DayOfWeek DayOfWeek { get; set; }
            public int TimeSlotDefinitionID { get; set; }
            public string DisplayName { get; set; }
        }

        // Helper class for CheckedListBox items (Courses)
        public class CourseItem
        {
            public int CourseID { get; set; }
            public string TeachingType { get; set; } // Internal value for DB: "Lecture" or "Practical"
            public string DisplayName { get; set; }
        }

        // Helper class for Arabic Day Names
        public static class ArabicDayNames
        {
            public static string GetArabicDayName(DayOfWeek day)
            {
                switch (day)
                {
                    case DayOfWeek.Saturday: return "السبت";
                    case DayOfWeek.Sunday: return "الأحد";
                    case DayOfWeek.Monday: return "الإثنين";
                    case DayOfWeek.Tuesday: return "الثلاثاء";
                    case DayOfWeek.Wednesday: return "الأربعاء";
                    case DayOfWeek.Thursday: return "الخميس";
                    default: return string.Empty;
                }
            }
        }

        private SchedualAppModel _context;
        private BindingList<Lecturer> _lecturers;
        private Lecturer _currentLecturer;

        // Internal lists for secondary data
        private BindingList<LecturerAvailability> _availabilities;
        private BindingList<CourseLecturer> _courseLecturers;


        public LecturerManagementControl()
        {
            InitializeComponent();

            _context = new SchedualAppModel();

            // Attach event handlers using the CORRECT control names (Private Fields)
            _lecturerGrid.SelectionChanged += LecturerGrid_SelectionChanged;
            _btnSave.Click += BtnSave_Click;
            _btnDelete.Click += BtnDelete_Click;
            _btnNew.Click += BtnNew_Click;

            ClearForm();
            LoadInitialDataAsync();
        }

        private void ClearForm()
        {
            _currentLecturer = null;

            _txtFirstName.Clear();
            _txtLastName.Clear();
            _txtAcademicRank.Clear();

            _btnDelete.Enabled = false;
            _btnSave.Text = "حفظ محاضر جديد";

            // Clear secondary data grids and list boxes
            _availabilityGrid.DataSource = null;
            _courseLecturerGrid.DataSource = null;

            // Clear CheckedListBoxes
            for (int i = 0; i < _availabilityListBox.Items.Count; i++)
            {
                _availabilityListBox.SetItemChecked(i, false);
            }

            for (int i = 0; i < _courseListBox.Items.Count; i++)
            {
                _courseListBox.SetItemChecked(i, false);
            }
        }

        private async Task LoadInitialDataAsync()
        {
            try
            {
                // 1. Load Lecturers
                await _context.Lecturers.LoadAsync();
                _lecturers = _context.Lecturers.Local.ToBindingList();


                // Rebind the grid to the anonymous object list
                _lecturerGrid.DataSource = _lecturers.Select(l => new
                {
                    l.LecturerID,
                    FullName = $"{l.FirstName} {l.LastName}",
                    l.AcademicRank
                }).ToList();
                _lecturerGrid.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
                if (_lecturerGrid.Columns.Contains("LecturerID")) _lecturerGrid.Columns["LecturerID"].HeaderText = "معرف المحاضر";
                if (_lecturerGrid.Columns.Contains("FullName")) _lecturerGrid.Columns["FullName"].HeaderText = "الاسم";
                if (_lecturerGrid.Columns.Contains("AcademicRank")) _lecturerGrid.Columns["AcademicRank"].HeaderText = "الدرجة الأكاديمية";
                //if (_lecturerGrid.Columns.Contains("FullName")) _lecturerGrid.Columns["FullName"].HeaderText = "الاسم";

                // 2. Load Availability ListBox Data (Days and Time Slots)
                // Filter out Friday (5) and Saturday (6)
                var days = Enum.GetValues(typeof(DayOfWeek)).Cast<DayOfWeek>().Where(d => d != DayOfWeek.Friday).OrderBy(d => (d == DayOfWeek.Saturday) ? 0 : (int)d + 1); ;
                var timeSlots = await _context.TimeSlotDefinitions.OrderBy(ts => ts.SlotNumber).ToListAsync();
                var availabilityItems = new List<AvailabilityItem>();

                foreach (var day in days)
                {
                    foreach (var slot in timeSlots)
                    {
                        availabilityItems.Add(new AvailabilityItem
                        {
                            DayOfWeek = day,
                            TimeSlotDefinitionID = slot.TimeSlotDefinitionID,
                            DisplayName = $"{ArabicDayNames.GetArabicDayName(day)} - {slot.StartTime.ToString(@"hh\:mm")} - {slot.EndTime.ToString(@"hh\:mm")}"
                        });
                    }
                }

                _availabilityListBox.DataSource = availabilityItems;
                _availabilityListBox.DisplayMember = "DisplayName";

                // 3. Load Course ListBox Data (Courses and Teaching Types)
                var courses = await _context.Courses.ToListAsync();
                var courseItems = new List<CourseItem>();

                foreach (var course in courses)
                {
                    // 1. Always add "نظري" (Theoretical)
                    courseItems.Add(new CourseItem
                    {
                        CourseID = course.CourseID,
                        TeachingType = "Lecture", // Correct value based on SQL DDL
                        DisplayName = $"{course.Title} - نظري" // القيمة المعروضة
                    });

                    // 2. Add "عملي" (Practical) only if IsPractical is true
                    if (course.IsPractical)
                    {
                        courseItems.Add(new CourseItem
                        {
                            CourseID = course.CourseID,
                            TeachingType = "Practical", // Correct value based on SQL DDL
                            DisplayName = $"{course.Title} - عملي" // القيمة المعروضة
                        });
                    }
                }

                _courseListBox.DataSource = courseItems;
                _courseListBox.DisplayMember = "DisplayName";

                // Select the first lecturer if available
                if (_lecturerGrid.Rows.Count > 0)
                {
                    _lecturerGrid.Rows[0].Selected = true;
                    LecturerGrid_SelectionChanged(null, null);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading initial data: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LecturerGrid_SelectionChanged(object sender, EventArgs e)
        {
            if (_lecturerGrid.SelectedRows.Count > 0)
            {
                // Use reflection to get the LecturerID from the anonymous object
                var selectedItem = _lecturerGrid.SelectedRows[0].DataBoundItem;

                if (selectedItem == null) return;

                // Get the LecturerID using reflection
                PropertyInfo prop = selectedItem.GetType().GetProperty("LecturerID");
                if (prop == null) return;

                int lecturerId = (int)prop.GetValue(selectedItem, null);

                // Find the actual Lecturer object from the local list
                _currentLecturer = _lecturers.FirstOrDefault(l => l.LecturerID == lecturerId);

                if (_currentLecturer != null)
                {
                    DisplayLecturerData();
                    LoadSecondaryDataAsync(_currentLecturer.LecturerID);
                }
            }
        }

        private void DisplayLecturerData()
        {
            if (_currentLecturer != null)
            {
                _txtFirstName.Text = _currentLecturer.FirstName;
                _txtLastName.Text = _currentLecturer.LastName;
                _txtAcademicRank.Text = _currentLecturer.AcademicRank;

                _btnDelete.Enabled = true;
                _btnSave.Text = "تحديث";
            }
        }

        private async Task LoadSecondaryDataAsync(int lecturerId)
        {
            // Use a new context to avoid the "second operation started on this context" error
            using (var tempContext = new SchedualAppModel())
            {
                try
                {
                    // 1. Load Lecturer Availability
                    var availabilities = await tempContext.LecturerAvailabilities
                        .Where(la => la.LecturerID == lecturerId)
                        .Include(la => la.TimeSlotDefinition)
                        .ToListAsync();

                    _availabilities = new BindingList<LecturerAvailability>(availabilities);

                    _availabilityGrid.DataSource = _availabilities.Select(la => new
                    {
                        la.LecturerAvailabilityID,
                        Day = ArabicDayNames.GetArabicDayName((DayOfWeek)(la.DayOfWeek - 1)),
                        TimeSlot = $"{la.TimeSlotDefinition.StartTime.ToString(@"hh\:mm")} - {la.TimeSlotDefinition.EndTime.ToString(@"hh\:mm")}"
                    }).ToList();
                    _availabilityGrid.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
                    //اخفاء عمود LecturerAvailabilityID
                    if (_availabilityGrid.Columns.Contains("LecturerAvailabilityID")) _availabilityGrid.Columns["LecturerAvailabilityID"].Visible = false;
                    //تغيير اسماء الاعمدة بالعربي
                    if (_availabilityGrid.Columns.Contains("Day")) _availabilityGrid.Columns["Day"].HeaderText = "اليوم";
                    if (_availabilityGrid.Columns.Contains("TimeSlot")) _availabilityGrid.Columns["TimeSlot"].HeaderText = "الفترة";

                    // Check the corresponding items in the Availability CheckedListBox
                    for (int i = 0; i < _availabilityListBox.Items.Count; i++)
                    {
                        var item = _availabilityListBox.Items[i] as AvailabilityItem;
                        bool isChecked = availabilities.Any(la =>
                            // FIX: Use la.DayOfWeek - 1 for comparison with C# DayOfWeek enum
                            (DayOfWeek)(la.DayOfWeek - 1) == item.DayOfWeek &&
                            la.TimeSlotDefinitionID == item.TimeSlotDefinitionID);
                        _availabilityListBox.SetItemChecked(i, isChecked);
                    }

                    // 2. Load Course Lecturer
                    var courseLecturers = await tempContext.CourseLecturers
                        .Where(cl => cl.LecturerID == lecturerId)
                        .Include(cl => cl.Cours)
                        .ToListAsync();

                    _courseLecturers = new BindingList<CourseLecturer>(courseLecturers);

                    _courseLecturerGrid.DataSource = _courseLecturers.Select(cl => new
                    {
                        cl.CourseLecturerID,
                        Course = cl.Cours.Title, // Display Name
                        cl.TeachingType
                    }).ToList();
                    _courseLecturerGrid.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
                    //اخفاء عمود LecturerAvailabilityID
                    if (_courseLecturerGrid.Columns.Contains("CourseLecturerID")) _courseLecturerGrid.Columns["CourseLecturerID"].Visible = false;
                    //تغيير اسماء الاعمدة بالعربي
                    if (_courseLecturerGrid.Columns.Contains("Course")) _courseLecturerGrid.Columns["Course"].HeaderText = "المقرر";
                    if (_courseLecturerGrid.Columns.Contains("TeachingType")) _courseLecturerGrid.Columns["TeachingType"].HeaderText = "نوع التدريس";


                    // Check the corresponding items in the Course CheckedListBox
                    for (int i = 0; i < _courseListBox.Items.Count; i++)
                    {
                        var item = _courseListBox.Items[i] as CourseItem;
                        bool isChecked = courseLecturers.Any(cl =>
                            cl.CourseID == item.CourseID &&
                            // Use robust comparison for case sensitivity
                            string.Equals(cl.TeachingType, item.TeachingType, StringComparison.OrdinalIgnoreCase));
                        _courseListBox.SetItemChecked(i, isChecked);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error loading secondary data: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void BtnNew_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        private async void BtnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateLecturerData()) return;

            try
            {
                // 1. Save/Update Lecturer Data
                bool isNewLecturer = false;
                if (_currentLecturer == null)
                {
                    // New Lecturer
                    _currentLecturer = new Lecturer();
                    // Set properties immediately for validation
                    _currentLecturer.FirstName = _txtFirstName.Text;
                    _currentLecturer.LastName = _txtLastName.Text;
                    _currentLecturer.AcademicRank = _txtAcademicRank.Text;

                    _context.Lecturers.Add(_currentLecturer);
                    _lecturers.Add(_currentLecturer);
                    isNewLecturer = true;
                }

                // Update properties for existing lecturer (or re-update for new lecturer)
                _currentLecturer.FirstName = _txtFirstName.Text;
                _currentLecturer.LastName = _txtLastName.Text;
                _currentLecturer.AcademicRank = _txtAcademicRank.Text;

                // Save changes to get the LecturerID for new lecturers
                await _context.SaveChangesAsync();

                // 2. Handle Lecturer Availability (Batch Update)
                // Get current saved availabilities
                var existingAvailabilities = await _context.LecturerAvailabilities
                    .Where(la => la.LecturerID == _currentLecturer.LecturerID)
                    .ToListAsync();

                // Get selected availabilities from the CheckedListBox
                var selectedAvailabilityItems = _availabilityListBox.CheckedItems.Cast<AvailabilityItem>().ToList();

                // Items to Add
                var toAddAvailabilities = selectedAvailabilityItems
                    .Where(selected => !existingAvailabilities.Any(existing =>
                        // FIX: Use la.DayOfWeek - 1 for comparison with C# DayOfWeek enum
                        (DayOfWeek)(existing.DayOfWeek - 1) == selected.DayOfWeek &&
                        existing.TimeSlotDefinitionID == selected.TimeSlotDefinitionID))
                    .Select(selected => new LecturerAvailability
                    {
                        LecturerID = _currentLecturer.LecturerID,
                        // FIX: Add +1 to C# DayOfWeek enum value for SQL Server 1-indexed DayOfWeek
                        DayOfWeek = (int)selected.DayOfWeek + 1,
                        TimeSlotDefinitionID = selected.TimeSlotDefinitionID
                    }).ToList();

                // Items to Remove
                var toRemoveAvailabilities = existingAvailabilities
                    .Where(existing => !selectedAvailabilityItems.Any(selected =>
                        // FIX: Use la.DayOfWeek - 1 for comparison with C# DayOfWeek enum
                        (DayOfWeek)(existing.DayOfWeek - 1) == selected.DayOfWeek &&
                        existing.TimeSlotDefinitionID == selected.TimeSlotDefinitionID))
                    .ToList();

                _context.LecturerAvailabilities.AddRange(toAddAvailabilities);
                // Explicitly mark for deletion to avoid foreign key issues
                foreach (var item in toRemoveAvailabilities)
                {
                    _context.Entry(item).State = EntityState.Deleted;
                }


                // 3. Handle Course Lecturer (Batch Update)
                // Get current saved course assignments
                var existingCourseLecturers = await _context.CourseLecturers
                    .Where(cl => cl.LecturerID == _currentLecturer.LecturerID)
                    .ToListAsync();

                // Get selected course assignments from the CheckedListBox
                var selectedCourseItems = _courseListBox.CheckedItems.Cast<CourseItem>().ToList();

                // Items to Add
                var toAddCourseLecturers = selectedCourseItems
                    .Where(selected => !existingCourseLecturers.Any(existing =>
                        existing.CourseID == selected.CourseID &&
                        // Use robust comparison for case sensitivity
                        string.Equals(existing.TeachingType, selected.TeachingType, StringComparison.OrdinalIgnoreCase)))
                    .Select(selected => new CourseLecturer
                    {
                        LecturerID = _currentLecturer.LecturerID,
                        CourseID = selected.CourseID,
                        TeachingType = selected.TeachingType // Will be "Lecture" or "Practical"
                    }).ToList();

                // Items to Remove
                var toRemoveCourseLecturers = existingCourseLecturers
                    .Where(existing => !selectedCourseItems.Any(selected =>
                        existing.CourseID == selected.CourseID &&
                        // Use robust comparison for case sensitivity
                        string.Equals(existing.TeachingType, selected.TeachingType, StringComparison.OrdinalIgnoreCase)))
                    .ToList();

                _context.CourseLecturers.AddRange(toAddCourseLecturers);
                // Explicitly mark for deletion to avoid foreign key issues
                foreach (var item in toRemoveCourseLecturers)
                {
                    _context.Entry(item).State = EntityState.Deleted;
                }

                // 4. Final Save (Transactional)
                await _context.SaveChangesAsync();

                // 5. Update UI
                MessageBox.Show("تم حفظ بيانات المحاضر والأوقات والمقررات بنجاح.", "نجاح", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Reload secondary data to update the grids and the CheckedListBoxes state
                await LoadSecondaryDataAsync(_currentLecturer.LecturerID);

                // Rebind the grid to update the anonymous object list (in case of new lecturer)
                _lecturerGrid.DataSource = _lecturers.Select(l => new
                {
                    l.LecturerID,
                    FullName = $"{l.FirstName} {l.LastName}",
                    l.AcademicRank
                }).ToList();
                _lecturerGrid.Refresh();
                DisplayLecturerData(); // Update form buttons
            }
            catch (DbEntityValidationException ex)
            {
                var errorMessages = ex.EntityValidationErrors
                    .SelectMany(x => x.ValidationErrors)
                    .Select(x => x.PropertyName + ": " + x.ErrorMessage);

                var fullErrorMessage = string.Join("\n", errorMessages);
                var caption = "Database Validation Error";
                MessageBox.Show($"Validation failed for the following entities:\n{fullErrorMessage}", caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (System.Data.Entity.Infrastructure.DbUpdateException ex)
            {
                // Handle database update errors (including CHECK constraint violations)
                var innerException = ex.InnerException?.InnerException?.Message ?? ex.Message;
                MessageBox.Show($"Error saving lecturer and related data: {innerException}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An unexpected error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidateLecturerData()
        {
            if (string.IsNullOrWhiteSpace(_txtFirstName.Text) || string.IsNullOrWhiteSpace(_txtLastName.Text))
            {
                MessageBox.Show("يجب إدخال الاسم الأول واسم العائلة للمحاضر.", "خطأ في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

           

            return true;
        }

        private async void BtnDelete_Click(object sender, EventArgs e)
        {
            if (_currentLecturer == null) return;

            var result = MessageBox.Show($"هل أنت متأكد من حذف المحاضر {_currentLecturer.FirstName} {_currentLecturer.LastName}؟ سيتم حذف جميع الأوقات والمقررات المرتبطة به.", "تأكيد الحذف", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                try
                {
                    // 1. Delete related entities to prevent foreign key constraint violations
                    var availabilitiesToDelete = await _context.LecturerAvailabilities
                        .Where(la => la.LecturerID == _currentLecturer.LecturerID)
                        .ToListAsync();
                    _context.LecturerAvailabilities.RemoveRange(availabilitiesToDelete);

                    var courseLecturersToDelete = await _context.CourseLecturers
                        .Where(cl => cl.LecturerID == _currentLecturer.LecturerID)
                        .ToListAsync();
                    _context.CourseLecturers.RemoveRange(courseLecturersToDelete);

                    // 2. Delete the Lecturer
                    _context.Lecturers.Remove(_currentLecturer);
                    _lecturers.Remove(_currentLecturer);

                    // 3. Save changes
                    await _context.SaveChangesAsync();

                    MessageBox.Show("تم حذف المحاضر بنجاح.", "نجاح", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // FIX: Rebind the grid and select the first item to refresh the UI
                    _lecturerGrid.DataSource = _lecturers.Select(l => new
                    {
                        l.LecturerID,
                        FullName = $"{l.FirstName} {l.LastName}",
                        l.AcademicRank
                    }).ToList();

                    if (_lecturerGrid.Rows.Count > 0)
                    {
                        _lecturerGrid.Rows[0].Selected = true;
                        LecturerGrid_SelectionChanged(null, null);
                    }
                    else
                    {
                        ClearForm(); // FIX: Clear form if no lecturers remain
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting lecturer: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void _availabilityListBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void _availabilityGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void _courseListBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void _dataGridsLayout_Paint(object sender, PaintEventArgs e)
        {

        }

        private void _mainLayout_Paint(object sender, PaintEventArgs e)
        {

        }

        private void _lecturerFormPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void _btnDelete_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
