using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using Excel = DocumentFormat.OpenXml.Spreadsheet; // هذا هو الحل السحري
//using GeneticSharp.Domain;
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

        //private async Task LoadArchivedTimetablesAsync()
        //{
        //    var timetables = await _context.Timetables.AsNoTracking().ToListAsync();
        //    // تم تصحيح استخدام TimetableName
        //    var archiveItems = timetables.Select(t => new TimetableArchiveItem { TimetableID = t.TimetableID, Name = t.TimetableName }).ToList();

        //    // ربط قائمة الجداول المحفوظة
        //    lstArchivedTimetables.DataSource = archiveItems;
        //    lstArchivedTimetables.DisplayMember = "Name";
        //    lstArchivedTimetables.ValueMember = "TimetableID";
        //}
        private async Task LoadArchivedTimetablesAsync()
        {
            var timetables = await _context.Timetables.AsNoTracking().ToListAsync();
            var archiveItems = timetables.Select(t => new TimetableArchiveItem { TimetableID = t.TimetableID, Name = t.TimetableName }).ToList();

            // 1. أوقف حدث التغيير مؤقتاً لمنع تشغيل كود العرض
            lstArchivedTimetables.SelectedIndexChanged -= lstArchivedTimetables_SelectedIndexChanged;

            // 2. اربط البيانات
            lstArchivedTimetables.DataSource = archiveItems;
            lstArchivedTimetables.DisplayMember = "Name";
            lstArchivedTimetables.ValueMember = "TimetableID";

            // 3. إلغاء تحديد أي عنصر (لكي لا يظهر أي جدول)
            lstArchivedTimetables.SelectedIndex = -1;

            // 4. أعد تفعيل الحدث مرة أخرى ليتمكن المستخدم من الاختيار لاحقاً
            lstArchivedTimetables.SelectedIndexChanged += lstArchivedTimetables_SelectedIndexChanged;
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
                MessageBox.Show("لا توجد مقررات/محاضرين مطلوبة للجدولة لهذا القسم والترم.", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Warning);


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
            progressBar.Maximum = 250; // عدد الأجيال الأقصى
            progressBar.Value = 0;
            btnGenerate.Enabled = false;

            await Task.Run(() => ga.Start());

            btnGenerate.Enabled = true;

            // 4. عرض النتائج وحفظها
            var bestTimetable = (TimetableChromosome)ga.BestChromosome;
            //DisplayArchivedSchedule((Timetable)ga.BestChromosome);

            if (bestTimetable.Fitness.Value > 900000) // افتراض أن 900000 تعني جدولاً جيداً جداً
            {
                lblStatus.Text = $"تم إنشاء الجدول بنجاح. اللياقة: {bestTimetable.Fitness.Value:N2}";
                lblStatus.ForeColor = System.Drawing.Color.Green;
                await SaveTimetableAsync(bestTimetable, timetableName, departmentId, levelId);
            }
            else
            {
                lblStatus.Text = $"تم إنشاء الجدول، لكنه يحتوي على تعارضات. اللياقة: {bestTimetable.Fitness.Value:N2}";
                lblStatus.ForeColor = System.Drawing.Color.Orange;
            }
        }

        private async Task SaveTimetableAsync(TimetableChromosome timetable, string name, int departmentId, int levelId)
        {
                // Implementation needed: Save Timetable and its ScheduleSlots to DB
                // This action CONSUMES the resources (Lecturers/Rooms) globally.
            var newTimetable = new Timetable
            {
                TimetableName = name,
                DepartmentID = departmentId,
                LevelID = levelId,

                // تأكد من تعيين قيمة لـ Semester
                // استخدم القيمة الثابتة "الترم السادس" مؤقتاً أو قم بتمريرها من واجهة المستخدم
                //Semester = "الترم السادس",

                CreationDate = DateTime.Now,
                ScheduleSlots = timetable.GenesList.Select(s => new ScheduleSlot
                {
                    CourseID = s.CourseID,
                    LecturerID = s.LecturerID,
                    DayOfWeek = s.DayOfWeek,
                    TimeSlotDefinitionID = s.TimeSlotDefinitionID,
                    RoomID = s.RoomID,
                    SlotType = s.SlotType

                    // إذا كان لديك خاصية SlotType مطلوبة في ScheduleSlot، يجب تعيينها هنا
                    // SlotType = "Lecture", // مثال

                }).ToList()
            };
            // ...

            _context.Timetables.Add(newTimetable);
            await _context.SaveChangesAsync();
            await DisplayArchivedSchedule(newTimetable);
            

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
                 
                    var timetableToDelete = await _context.Timetables.Include(t => t.ScheduleSlots).FirstOrDefaultAsync(t => t.TimetableID == timetableId);
                    if (timetableToDelete != null)
                    {
                        _context.ScheduleSlots.RemoveRange(timetableToDelete.ScheduleSlots);
                        _context.Timetables.Remove(timetableToDelete);
                        await _context.SaveChangesAsync();
                    }
                  

                    await LoadArchivedTimetablesAsync();
                    MessageBox.Show("تم حذف الجدول بنجاح.", "نجاح", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
        private async void lstArchivedTimetables_SelectedIndexChanged(object sender, EventArgs e)
        {
            // التأكد من أن المستخدم اختار عنصراً
            if (lstArchivedTimetables.SelectedItem == null) return;

            // 1. التحويل الصحيح إلى النوع الموجود فعلياً في القائمة
            var selectedItem = lstArchivedTimetables.SelectedItem as TimetableArchiveItem;

            if (selectedItem != null)
            {
                try
                {
                    lblStatus.Text = "جاري تحميل الجدول...";

                    // 2. جلب الجدول الكامل من قاعدة البيانات باستخدام الـ ID
                    var savedTimetable = await _context.Timetables
                                                .Include(t => t.ScheduleSlots) // ضروري لجلب الحصص
                                                .AsNoTracking()
                                                .FirstOrDefaultAsync(t => t.TimetableID == selectedItem.TimetableID);

                    if (savedTimetable != null)
                    {
                        // 3. تحديث DataManager ليعرف أسماء المواد والقاعات الخاصة بهذا القسم والمستوى
                        _dataManager = new DataManager(_context);
                        await _dataManager.LoadDataAsync(savedTimetable.DepartmentID, savedTimetable.LevelID);

                        // 4. استدعاء دالة العرض الجديدة (انظر الخطوة 2)
                        await DisplayArchivedSchedule(savedTimetable);

                        lblStatus.Text = $"تم عرض الجدول: {savedTimetable.TimetableName}";
                        lblStatus.ForeColor = System.Drawing.Color.Blue;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("حدث خطأ أثناء تحميل الجدول: " + ex.Message);
                }
            }
        }
        private async Task DisplayArchivedSchedule(Timetable dbTimetable)
        {
            // 1. إعداد الأعمدة (الوقت)
            var timeSlots = await _context.TimeSlotDefinitions.AsNoTracking().OrderBy(ts => ts.SlotNumber).ToListAsync();

            // 2. إعداد الصفوف (الأيام)
            var customDayOrder = new System.DayOfWeek[]
            {
        System.DayOfWeek.Saturday, System.DayOfWeek.Sunday, System.DayOfWeek.Monday,
        System.DayOfWeek.Tuesday, System.DayOfWeek.Wednesday, System.DayOfWeek.Thursday
            };

            // 3. تجهيز الجدول للبيانات
            var dt = new DataTable();
            dt.Columns.Add("اليوم");
            foreach (var slot in timeSlots)
            {
                dt.Columns.Add($"{slot.StartTime.ToString(@"hh\:mm")} - {slot.EndTime.ToString(@"hh\:mm")}");
            }

            // 4. تعبئة البيانات
            foreach (var day in customDayOrder)
            {
                var row = dt.NewRow();
                row["اليوم"] = day.ToString();

                for (int i = 0; i < timeSlots.Count; i++)
                {
                    var timeSlot = timeSlots[i];
                    string colName = $"{timeSlot.StartTime.ToString(@"hh\:mm")} - {timeSlot.EndTime.ToString(@"hh\:mm")}";

                    // البحث عن الحصة في البيانات القادمة من قاعدة البيانات
                    // ملاحظة: تأكد من طريقة تخزين DayOfWeek في الداتابيس (هل يبدأ من 0 أم 1؟)
                    // الكود هنا يفترض التوافق المباشر
                    var slotsInCell = dbTimetable.ScheduleSlots
                        .Where(s => s.DayOfWeek == (int)day && s.TimeSlotDefinitionID == timeSlot.TimeSlotDefinitionID)
                        .ToList();

                    if (slotsInCell.Any())
                    {
                        var cellContent = new System.Text.StringBuilder();
                        foreach (var slot in slotsInCell)
                        {
                            var course = _dataManager.GetCourse(slot.CourseID);
                            var lecturer = _context.Lecturers.Find(slot.LecturerID);
                            var room = _dataManager.GetRoom(slot.RoomID);

                            cellContent.AppendLine($"{course?.Title ?? "مادة"} ({slot.SlotType})");
                            cellContent.AppendLine($"{lecturer?.FirstName} {lecturer?.LastName}");
                            cellContent.AppendLine($"قاعة: {room?.Name}");
                            cellContent.AppendLine("-");
                        }
                        // حذف آخر شرطة
                        if (cellContent.Length > 0) cellContent.Length -= 3;

                        row[colName] = cellContent.ToString();
                    }
                    else
                    {
                        row[colName] = "";
                    }
                }
                dt.Rows.Add(row);
            }

            // عرض الجدول
            dgvTimetable.DataSource = dt;
            dgvTimetable.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
        }

        private void dgvTimetable_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void progressBar_Click(object sender, EventArgs e)
        {

        }
        private void CreateExcelFile(string filePath)
        {
            using (SpreadsheetDocument package = SpreadsheetDocument.Create(filePath, SpreadsheetDocumentType.Workbook))
            {
                // 1. إنشاء الهيكل الأساسي
                WorkbookPart workbookPart = package.AddWorkbookPart();
                workbookPart.Workbook = new Excel.Workbook();

                WorksheetPart worksheetPart = workbookPart.AddNewPart<WorksheetPart>();
                worksheetPart.Worksheet = new Excel.Worksheet(new Excel.SheetData());

                // 2. إضافة الأنماط
                WorkbookStylesPart stylesPart = workbookPart.AddNewPart<WorkbookStylesPart>();
                stylesPart.Stylesheet = GenerateStyleSheet();
                stylesPart.Stylesheet.Save();

                Excel.Sheets sheets = package.WorkbookPart.Workbook.AppendChild(new Excel.Sheets());
                Excel.Sheet sheet = new Excel.Sheet()
                {
                    Id = package.WorkbookPart.GetIdOfPart(worksheetPart),
                    SheetId = 1,
                    Name = "الجدول الدراسي"
                };
                sheets.Append(sheet);

                Excel.SheetData sheetData = worksheetPart.Worksheet.GetFirstChild<Excel.SheetData>();

                // 3. إضافة العناوين
                Excel.Row headerRow = new Excel.Row();
                foreach (DataGridViewColumn column in dgvTimetable.Columns)
                {
                    Excel.Cell cell = new Excel.Cell();
                    cell.DataType = CellValues.String;
                    cell.CellValue = new Excel.CellValue(column.HeaderText);
                    headerRow.AppendChild(cell);
                }
                sheetData.AppendChild(headerRow);

                // 4. إضافة البيانات
                foreach (DataGridViewRow dgvRow in dgvTimetable.Rows)
                {
                    if (!dgvRow.IsNewRow)
                    {
                        Excel.Row newRow = new Excel.Row();
                        foreach (DataGridViewCell dgvCell in dgvRow.Cells)
                        {
                            Excel.Cell cell = new Excel.Cell();
                            cell.DataType = CellValues.String;

                            string cellText = dgvCell.Value != null ? dgvCell.Value.ToString() : "";
                            cell.CellValue = new Excel.CellValue(cellText);

                            // تفعيل التفاف النص للأسطر المتعددة
                            if (cellText.Contains("\n") || cellText.Contains("\r"))
                            {
                                cell.StyleIndex = 1;
                            }

                            newRow.AppendChild(cell);
                        }
                        sheetData.AppendChild(newRow);
                    }
                }
                workbookPart.Workbook.Save();
            }
        }

        // دالة الأنماط المصححة باستخدام الاسم المستعار Excel
        private Excel.Stylesheet GenerateStyleSheet()
        {
            return new Excel.Stylesheet(
                new Excel.Fonts(
                    new Excel.Font( // Index 0 - Default
                        new Excel.FontSize() { Val = 11 },
                        new Excel.Color() { Rgb = new HexBinaryValue() { Value = "000000" } },
                        new Excel.FontName() { Val = "Calibri" })
                ),
                new Excel.Fills(
                    new Excel.Fill(new Excel.PatternFill() { PatternType = Excel.PatternValues.None }), // Index 0
                    new Excel.Fill(new Excel.PatternFill() { PatternType = Excel.PatternValues.Gray125 }) // Index 1
                ),
                new Excel.Borders(
                    new Excel.Border( // Index 0
                        new Excel.LeftBorder(),
                        new Excel.RightBorder(),
                        new Excel.TopBorder(),
                        new Excel.BottomBorder(),
                        new Excel.DiagonalBorder())
                ),
                new Excel.CellFormats(
                    new Excel.CellFormat() { FontId = 0, FillId = 0, BorderId = 0 }, // Index 0
                    new Excel.CellFormat() // Index 1 - Wrap Text
                    {
                        FontId = 0,
                        FillId = 0,
                        BorderId = 0,
                        ApplyAlignment = true,
                        Alignment = new Excel.Alignment() { WrapText = true, Vertical = Excel.VerticalAlignmentValues.Center }
                    }
                )
            );
        }
        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            // التأكد من وجود بيانات
            if (dgvTimetable.Rows.Count == 0)
            {
                MessageBox.Show("لا توجد بيانات لتصديرها.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Excel Files (*.xlsx)|*.xlsx";
            sfd.FileName = "الجدول الدراسي.xlsx";
            sfd.Title = "حفظ الجدول كملف إكسل";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    CreateExcelFile(sfd.FileName);
                    MessageBox.Show("تم حفظ ملف الإكسل بنجاح!", "نجاح", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (System.IO.IOException)
                {
                    MessageBox.Show("الملف مفتوح حالياً في برنامج Excel.\nالرجاء إغلاق الملف ثم المحاولة مرة أخرى.",
                                    "خطأ في الحفظ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("حدث خطأ غير متوقع: " + ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
