using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.Entity;
using SchedualApp; // **تم التعديل**: من المحتمل أن تكون الكيانات في الـ namespace الرئيسي SchedualApp وليس SchedualApp.Models

namespace SchedualApp.GeneticAlgorithm
{
    // يمثل فتحة زمنية مطلوبة للجدولة (الجين)
    public class RequiredSlot
    {
        public int RequiredSlotID { get; set; }
        public int CourseID { get; set; }
        public int LecturerID { get; set; }
        public string SlotType { get; set; } // "Lecture" أو "Practical"
        public int DepartmentID { get; set; } // لتعارض الدفعة
        public int LevelID { get; set; } // لتعارض الدفعة
        public int DayOfWeek { get; set; } // Assigned Day
        public int TimeSlotDefinitionID { get; set; } // Assigned Time Slot
        public int RoomID { get; set; } // Assigned Room
    }

    public class DataManager
    {
        private readonly SchedualAppModel _context;

        // البيانات الثابتة
        public List<Lecturer> Lecturers { get; private set; }
        public List<Cours> Courses { get; private set; }
        public List<Room> Rooms { get; private set; }
        public List<TimeSlotDefinition> TimeSlotDefinitions { get; private set; }
        public List<LecturerAvailability> LecturerAvailabilities { get; private set; } // تم التصحيح

        // الكيانات الجديدة للعلاقات
        public List<CourseLevel> CourseLevels { get; private set; }
        public List<CourseLecturer> CourseLecturers { get; private set; }

        // البيانات المطلوبة للجدولة
        public List<RequiredSlot> RequiredSlots { get; private set; }

        // بيانات الجدولة العالمية (القائمة السوداء)
        public List<Timetable> ExistingTimetables { get; private set; }
        public HashSet<(int dayOfWeek, int timeSlotId, int resourceId)> GlobalBlacklist { get; private set; }

        public DataManager(SchedualAppModel context)
        {
            _context = context;
            RequiredSlots = new List<RequiredSlot>();
        }

        public async Task LoadDataAsync(int departmentId, int levelId)
        {
            // 1. تحميل البيانات الثابتة
            Lecturers = await _context.Lecturers.AsNoTracking().ToListAsync();
            Rooms = await _context.Rooms.AsNoTracking().ToListAsync();
            TimeSlotDefinitions = await _context.TimeSlotDefinitions.AsNoTracking().ToListAsync();
            LecturerAvailabilities = await _context.LecturerAvailabilities.AsNoTracking().ToListAsync();
            Courses = await _context.Courses.AsNoTracking().ToListAsync();

            // 2. تحديد المقررات المطلوبة باستخدام CourseLevel
            CourseLevels = await _context.CourseLevels
                .Where(cl => cl.DepartmentID == departmentId && cl.LevelID == levelId)
                .AsNoTracking().ToListAsync();

            var courseIds = CourseLevels.Select(cl => cl.CourseID).ToList();

            // 3. تحديد المحاضرين المسندين للمقررات المطلوبة باستخدام CourseLecturer
            CourseLecturers = await _context.CourseLecturers
                .Where(cl => courseIds.Contains(cl.CourseID))
                .AsNoTracking().ToListAsync();

            // 4. بناء القائمة السوداء العالمية
            ExistingTimetables = await _context.Timetables.Include(t => t.ScheduleSlots).AsNoTracking().ToListAsync();
            BuildGlobalBlacklist();

            // 5. توليد الفتحات المطلوبة للجدولة
            GenerateRequiredSlots(departmentId, levelId); // تم تمرير المعاملات
        }

        private void BuildGlobalBlacklist()
        {
            GlobalBlacklist = new HashSet<(int dayOfWeek, int timeSlotId, int resourceId)>();

            foreach (var timetable in ExistingTimetables)
            {
                foreach (var slot in timetable.ScheduleSlots)
                {
                    GlobalBlacklist.Add((slot.DayOfWeek, slot.TimeSlotDefinitionID, slot.LecturerID));
                    GlobalBlacklist.Add((slot.DayOfWeek, slot.TimeSlotDefinitionID, slot.RoomID));
                }
            }
        }

        private void GenerateRequiredSlots(int departmentId, int levelId) // تم إضافة المعاملات
        {
            RequiredSlots.Clear();
            int requiredSlotIdCounter = 1;

            foreach (var cl in CourseLecturers)
            {
                var course = GetCourse(cl.CourseID);
                if (course == null) continue;

                RequiredSlots.Add(new RequiredSlot
                {
                    RequiredSlotID = requiredSlotIdCounter++,
                    CourseID = cl.CourseID,
                    LecturerID = cl.LecturerID,
                    SlotType = cl.TeachingType,
                    DepartmentID = departmentId, // تم التصحيح
                    LevelID = levelId // تم التصحيح
                });
            }
        }

        // الدوال المساعدة
        public Cours GetCourse(int courseId) => Courses.FirstOrDefault(c => c.CourseID == courseId);
        public Room GetRoom(int roomId) => Rooms.FirstOrDefault(r => r.RoomID == roomId);
        public bool IsLecturerAvailable(int lecturerId, int dayOfWeek, int timeSlotId)
        {
            return LecturerAvailabilities.Any(a => a.LecturerID == lecturerId && a.DayOfWeek == dayOfWeek && a.TimeSlotDefinitionID == timeSlotId);
        }
        public bool IsGloballyBlacklisted(int dayOfWeek, int timeSlotId, int resourceId)
        {
            return GlobalBlacklist.Contains((dayOfWeek, timeSlotId, resourceId));
        }
        public bool IsLecturerQualified(int lecturerId, int courseId, string slotType)
        {
            return CourseLecturers.Any(cl => cl.LecturerID == lecturerId && cl.CourseID == courseId && cl.TeachingType == slotType);
        }
    }
}
