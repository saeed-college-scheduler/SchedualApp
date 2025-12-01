using DocumentFormat.OpenXml.Drawing;
using GeneticSharp.Domain;
using GeneticSharp.Domain.Chromosomes;
using GeneticSharp.Domain.Crossovers;
using GeneticSharp.Domain.Mutations;
using GeneticSharp.Domain.Populations;
using GeneticSharp.Domain.Selections;
using GeneticSharp.Domain.Terminations;
using SchedualApp.GeneticAlgorithm;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SchedualApp
{
    public partial class TimetableGenerationControl : UserControl
    {
        private SchedualAppModel _context;
        private DataManager _dataManager;

        // هيكل لعرض الجداول المحفوظة
        private class TimetableArchiveItem
        {
            public int TimetableID { get; set; }
            public string Name { get; set; }
        }

        public TimetableGenerationControl()
        {
            InitializeComponent();
            _context = new SchedualAppModel();
            this.Load += TimetableGenerationControl_Load;
        }

        private async void TimetableGenerationControl_Load(object sender, EventArgs e)
        {
            await LoadInitialDataAsync();
            await LoadArchivedTimetablesAsync();
        }

        private async Task LoadInitialDataAsync()
        {
            // تحميل بيانات الأقسام والمستويات لملء القوائم المنسدلة
            var departments = await _context.Departments.AsNoTracking().ToListAsync();
            cboDepartment.DataSource = departments;
            cboDepartment.DisplayMember = "Name";
            cboDepartment.ValueMember = "DepartmentID";

            var levels = await _context.Levels.AsNoTracking().ToListAsync();
            cboLevel.DataSource = levels;
            cboLevel.DisplayMember = "Name";
            cboLevel.ValueMember = "LevelID";
        }

        private async Task LoadArchivedTimetablesAsync()
        {
            var timetables = await _context.Timetables.AsNoTracking().ToListAsync();
            // تم تصحيح استخدام TimetableName
            var archiveItems = timetables.Select(t => new TimetableArchiveItem { TimetableID = t.TimetableID, Name = t.TimetableName }).ToList();

            // ربط قائمة الجداول المحفوظة
            lstArchivedTimetables.DataSource = archiveItems;
            lstArchivedTimetables.DisplayMember = "Name";
            lstArchivedTimetables.ValueMember = "TimetableID";
        }

        private async void btnGenerate_Click(object sender, EventArgs e)
        {
            if (cboDepartment.SelectedValue == null || cboLevel.SelectedValue == null || string.IsNullOrWhiteSpace(txtTimetableName.Text))
            {
                MessageBox.Show("الرجاء اختيار القسم والمستوى وإدخال اسم للجدول.", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int departmentId = (int)cboDepartment.SelectedValue;
            int levelId = (int)cboLevel.SelectedValue;
            string timetableName = txtTimetableName.Text.Trim();

            // 1. تهيئة مدير البيانات
            _dataManager = new DataManager(_context);
            await _dataManager.LoadDataAsync(departmentId, levelId);

            if (!_dataManager.RequiredSlots.Any())
            {
                lblStatus.Text = "لا توجد مقررات/حصص مطلوبة للجدولة لهذا القسم والمستوى.";
                lblStatus.ForeColor = Color.Red;
                return;
            }

            // 2. تهيئة الخوارزمية الجينية
            var chromosome = new TimetableChromosome(_dataManager);
            var fitness = new TimetableFitness(_dataManager);
            var population = new Population(50, 100, chromosome); // حجم السكان: 50-100

            // تهيئة الخوارزمية الجينية (تم تصحيح استخدام الكلاس)
            var ga = new GeneticSharp.Domain.GeneticAlgorithm(
                population,
                fitness,
                new TournamentSelection(),
                new UniformCrossover(),
                new UniformMutation(true)
            );

            ga.Termination = new FitnessStagnationTermination(100); // التوقف بعد 100 جيل بدون تحسن
            ga.GenerationRan += (s, args) =>
            {
                // تحديث شريط التقدم والحالة
                var bestFitness = ga.BestChromosome.Fitness.Value;
                var currentGeneration = ga.GenerationsNumber;

                // استخدام Invoke لتحديث الواجهة من Thread آخر
                this.Invoke((MethodInvoker)delegate
                {
                    lblStatus.Text = $"الجيل: {currentGeneration}. أفضل لياقة: {bestFitness:N2}";
                    progressBar.Value = Math.Min(progressBar.Maximum, currentGeneration);
                    Application.DoEvents(); // تحديث الواجهة
                });
            };

            // 3. تشغيل الخوارزمية بشكل غير متزامن
            lblStatus.Text = "جاري توليد الجدول الزمني... يرجى الانتظار.";
            progressBar.Maximum = 500; // عدد الأجيال الأقصى
            progressBar.Value = 0;
            btnGenerate.Enabled = false;

            await Task.Run(() => ga.Start());

            btnGenerate.Enabled = true;

            // 4. عرض النتائج وحفظها
            var bestTimetable = (TimetableChromosome)ga.BestChromosome;
            DisplaySchedule(bestTimetable);

            if (bestTimetable.Fitness.Value > 900000) // افتراض أن 900000 تعني جدولاً جيداً جداً
            {
                lblStatus.Text = $"تم إنشاء الجدول بنجاح. اللياقة: {bestTimetable.Fitness.Value:N2}";
                lblStatus.ForeColor = Color.Green;
                await SaveTimetableAsync(bestTimetable, timetableName, departmentId, levelId);
            }
            else
            {
                lblStatus.Text = $"تم إنشاء الجدول، لكنه يحتوي على تعارضات. اللياقة: {bestTimetable.Fitness.Value:N2}";
                lblStatus.ForeColor = Color.Orange;
            }
        }

        private async Task SaveTimetableAsync(TimetableChromosome timetable, string name, int departmentId, int levelId)
        {
            var newTimetable = new Timetable
            {
                TimetableName = name,
                DepartmentID = departmentId,
                LevelID = levelId,

                // تأكد من تعيين قيمة لـ Semester
                // استخدم القيمة الثابتة "الترم السادس" مؤقتاً أو قم بتمريرها من واجهة المستخدم
                Semester = "الترم السادس",

                CreationDate = DateTime.Now,
                IsApproved = false,
                ScheduleSlots = timetable.GenesList.Select(s => new ScheduleSlot
                {
                    CourseID = ((RequiredSlot)s.Value).CourseID,
                    LecturerID = ((RequiredSlot)s.Value).LecturerID,
                    DayOfWeek = ((RequiredSlot)s.Value).DayOfWeek,
                    TimeSlotDefinitionID = ((RequiredSlot)s.Value).TimeSlotDefinitionID,
                    RoomID = ((RequiredSlot)s.Value).RoomID,
                    SlotType = ((RequiredSlot)s.Value).SlotType

                    // إذا كان لديك خاصية SlotType مطلوبة في ScheduleSlot، يجب تعيينها هنا
                    // SlotType = "Lecture", // مثال

                }).ToList()
            };
            // ...

            _context.Timetables.Add(newTimetable);
            await _context.SaveChangesAsync();


            await LoadArchivedTimetablesAsync();
            MessageBox.Show("تم حفظ الجدول بنجاح.", "نجاح", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private async void BtnDelete_Click(object sender, EventArgs e)
        {
            if (lstArchivedTimetables.SelectedValue != null)
            {
                int timetableId = (int)lstArchivedTimetables.SelectedValue;
                var result = MessageBox.Show($"هل أنت متأكد من حذف الجدول رقم {timetableId}؟ سيتم استعادة الموارد.", "تأكيد الحذف", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    // Implementation needed: Delete Timetable and its ScheduleSlots from DB
                    // This action RESTORES the resources (Lecturers/Rooms) globally.

                    lblStatus.Text = $"تم حذف الجدول رقم {timetableId}. جاري تحديث القائمة...";
                    // مثال على الحذف (يجب استبداله بمنطق Entity Framework الفعلي)
                    await LoadArchivedTimetablesAsync();
                    MessageBox.Show("تم حذف الجدول بنجاح.", "نجاح", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
        private void DisplaySchedule(TimetableChromosome timetable)
        {
            // 1. الحصول على جميع الفترات الزمنية المتاحة (الأعمدة)
            var timeSlots = _dataManager.TimeSlotDefinitions.OrderBy(ts => ts.SlotNumber).ToList();

            // 2. تحديد الأيام (الصفوف) - نستخدم الترتيب المخصص الذي ناقشناه سابقاً
            var customDayOrder = new System.DayOfWeek[]
            {
        System.DayOfWeek.Saturday,
        System.DayOfWeek.Sunday,
        System.DayOfWeek.Monday,
        System.DayOfWeek.Tuesday,
        System.DayOfWeek.Wednesday,
        System.DayOfWeek.Thursday
                // تم استثناء الجمعة
            };

            // 3. إنشاء DataTable بالهيكل الجديد (اليوم + الفترات الزمنية)
            var dt = new DataTable();
            dt.Columns.Add("اليوم"); // العمود الأول: اليوم

            // إضافة أعمدة الفترات الزمنية
            foreach (var slot in timeSlots)
            {
                dt.Columns.Add($"{slot.StartTime.ToString(@"hh\:mm")} - {slot.EndTime.ToString(@"hh\:mm")}");
            }

            // 4. تعبئة البيانات
            var slotsByDayAndTime = timetable.GenesList
                .GroupBy(s => new { Day = ((RequiredSlot)s.Value).DayOfWeek, TimeSlot = ((RequiredSlot)s.Value).TimeSlotDefinitionID })
                .ToDictionary(g => g.Key, g => g.ToList());

            foreach (var day in customDayOrder)
            {
                // إنشاء صف جديد يبدأ باسم اليوم
                var row = dt.NewRow();
                row["اليوم"] = day.ToString();

                // تعبئة خلايا الصف بناءً على الفترات الزمنية
                for (int i = 0; i < timeSlots.Count; i++)
                {
                    var timeSlot = timeSlots[i];

                    // البحث عن ScheduleSlot المطابق لليوم والفترة
                    var key = new { Day = (int)day, TimeSlot = timeSlot.TimeSlotDefinitionID };

                    // يجب أن نستخدم DayOfWeek كـ int في المفتاح
                    // ملاحظة: إذا كان DayOfWeek في ScheduleSlot يبدأ من 1 (الأحد) بدلاً من 0 (الأحد)، يجب تعديل المفتاح
                    // بناءً على الكود السابق (slot.DayOfWeek - 1) في السطر 231، نفترض أن s.DayOfWeek هو int يبدأ من 1
                    var adjustedKey = new { Day = (int)day + 1, TimeSlot = timeSlot.TimeSlotDefinitionID };

                    if (slotsByDayAndTime.ContainsKey(adjustedKey))
                    {
                        var scheduleSlots = slotsByDayAndTime[adjustedKey];

                        // تجميع بيانات SchedualSlot في متغير سترنج واحد
                        var cellContent = new System.Text.StringBuilder();
                        foreach (var slotData in scheduleSlots)
                        {
                            var course = _dataManager.GetCourse(((RequiredSlot)slotData.Value).CourseID);
                            var lecturer = _context.Lecturers.FirstOrDefault(l => l.LecturerID == ((RequiredSlot)slotData.Value).LecturerID);
                            var room = _dataManager.GetRoom(((RequiredSlot)slotData.Value).RoomID);

                            cellContent.AppendLine($"{course?.Title} ({((RequiredSlot)slotData.Value).SlotType})");
                            cellContent.AppendLine($"المحاضر: {lecturer?.FirstName} {lecturer?.LastName}");
                            cellContent.AppendLine($"القاعة: {room?.Name}");
                            cellContent.AppendLine("---");
                        }

                        // إزالة آخر "---"
                        if (cellContent.Length > 0)
                        {
                            cellContent.Length -= 4;
                        }

                        row[timeSlot.StartTime.ToString(@"hh\:mm") + " - " + timeSlot.EndTime.ToString(@"hh\:mm")] = cellContent.ToString();
                    }
                    else
                    {
                        row[timeSlot.StartTime.ToString(@"hh\:mm") + " - " + timeSlot.EndTime.ToString(@"hh\:mm")] = string.Empty;
                    }
                }

                dt.Rows.Add(row);
            }

            dgvTimetable.DataSource = dt;
            dgvTimetable.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
        }


        private void dgvTimetable_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        //private void btnGenerate_Click(object sender, EventArgs e)
        //{

        //}
    }
}
