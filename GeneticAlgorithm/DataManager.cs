using System.Collections.Generic;
using System.Linq;
using SchedualApp; // افتراض أن الكلاسات المولدة موجودة في هذا الـ Namespace

namespace SchedualApp.GeneticAlgorithm
{
    /// <summary>
    /// مسؤول عن تحميل جميع البيانات الثابتة والقيود من قاعدة البيانات.
    /// </summary>
    public class DataManager
    {
        // الموارد الثابتة
        public List<Cours> Courss { get; private set; }
        public List<Lecturer> Lecturers { get; private set; }
        public List<Room> Rooms { get; private set; }
        public List<TimeSlotDefinition> TimeSlotDefinitions { get; private set; }
        
        // هياكل البيانات المساعدة للبحث السريع (O(1))
        private Dictionary<int, Cours> _courseLookup;
        private Dictionary<int, Lecturer> _lecturerLookup;
        private Dictionary<int, Room> _roomLookup;
        private HashSet<(int courseId, int lecturerId, string slotType)> _qualifiedLecturers;
        private Dictionary<(int lecturerId, int dayOfWeek, int timeSlotId), bool> _lecturerAvailabilityLookup;
        
        // القيود
        public List<CourseLevel> CourseLevels { get; private set; }
        public List<CourseLecturer> CourseLecturers { get; private set; }
        public List<LecturerAvailability> LecturerAvailabilities { get; private set; }
        public List<RoomFeature> RoomFeatures { get; private set; }
        
        // متطلبات الجدولة
        public List<RequiredSlot> RequiredSlots { get; private set; }
        public int RequiredSlotsCount => RequiredSlots?.Count ?? 0;

        public DataManager(int departmentId, int levelId)
        {
            // هنا يتم تحميل البيانات من قاعدة البيانات باستخدام Entity Framework
            // لتبسيط المثال، سنفترض أن هذه الدوال تقوم بتحميل البيانات
            LoadStaticData();
            LoadConstraints();
            BuildLookups();
            GenerateRequiredSlots(departmentId, levelId);
        }

        private void LoadStaticData()
        {
            // يجب استبدال هذا بمنطق تحميل EF الفعلي
            // مثال: using (var context = new SchedualAppModel()) { Courss = context.Courss.ToList(); }
            
            // بيانات وهمية (Mock Data)
            Courss = new List<Cours> { new Cours { CourseID = 1, Code = "CS101", Title = "Intro to CS", LectureHours = 3, PracticalHours = 1 } };
            Lecturers = new List<Lecturer> { new Lecturer { LecturerID = 1, FirstName = "Dr.", LastName = "Ahmed" } };
            Rooms = new List<Room> { new Room { RoomID = 1, Name = "L101", Capacity = 60 } };
            TimeSlotDefinitions = new List<TimeSlotDefinition> { new TimeSlotDefinition { TimeSlotDefinitionID = 1, SlotNumber = 1 } };
        }

        private void LoadConstraints()
        {
            // يجب استبدال هذا بمنطق تحميل EF الفعلي
            CourseLevels = new List<CourseLevel> { new CourseLevel { CourseID = 1, DepartmentID = 1, LevelID = 1 } };
            CourseLecturers = new List<CourseLecturer> { new CourseLecturer { CourseID = 1, LecturerID = 1, TeachingType = "Lecture" } };
            LecturerAvailabilities = new List<LecturerAvailability>(); // افترض أنهم متاحون افتراضياً
            RoomFeatures = new List<RoomFeature>(); // افترض عدم وجود متطلبات خاصة
        }

        private void BuildLookups()
        {
            // بناء القواميس للبحث السريع عن الموارد
            _courseLookup = Courss.ToDictionary(c => c.CourseID);
            _lecturerLookup = Lecturers.ToDictionary(l => l.LecturerID);
            _roomLookup = Rooms.ToDictionary(r => r.RoomID);

            // بناء مجموعة الهاش للتحقق من تأهيل المحاضرين (O(1))
            _qualifiedLecturers = new HashSet<(int courseId, int lecturerId, string slotType)>(
                CourseLecturers.Select(cl => (cl.CourseID, cl.LecturerID, cl.TeachingType))
            );

            // بناء قاموس لتوافر المحاضرين (O(1))
            _lecturerAvailabilityLookup = LecturerAvailabilities
                .ToDictionary(
                    la => (la.LecturerID, la.DayOfWeek, la.TimeSlotDefinitionID),
                    la => true // LecturerAvailability has no IsAvailable in generated type; assume presence means available
                );
        }

        private void GenerateRequiredSlots(int departmentId, int levelId)
        {
            // توليد قائمة الفتحات المطلوبة بناءً على المقررات المخصصة لهذا القسم والمستوى
            RequiredSlots = new List<RequiredSlot>();
            
            var relevantCourseIds = Courss.Where(c => CourseLevels.Any(cl => cl.CourseID == c.CourseID && cl.DepartmentID == departmentId && cl.LevelID == levelId)).Select(c=>c.CourseID).ToHashSet();

            var relevantCourss = Courss.Where(c => relevantCourseIds.Contains(c.CourseID)).ToList();

            int totalSlots = relevantCourss.Sum(c => c.LectureHours + c.PracticalHours);
            try { RequiredSlots.Capacity = totalSlots; } catch { }

            foreach (var course in relevantCourss)
            {
                for (int i = 0; i < course.LectureHours; i++)
                {
                    RequiredSlots.Add(new RequiredSlot { CourseID = course.CourseID, SlotType = "Lecture" });
                }
                for (int i = 0; i < course.PracticalHours; i++)
                {
                    RequiredSlots.Add(new RequiredSlot { CourseID = course.CourseID, SlotType = "Practical" });
                }
            }
        }
        
        // دوال مساعدة للتحقق من القيود (تستخدم في TimetableFitness)
        
        public bool IsLecturerQualified(int lecturerId, int courseId, string slotType)
        {
            return _qualifiedLecturers.Contains((courseId, lecturerId, slotType));
        }

        public bool IsLecturerAvailable(int lecturerId, int dayOfWeek, int timeSlotDefinitionId)
        {
            if (_lecturerAvailabilityLookup.TryGetValue((lecturerId, dayOfWeek, timeSlotDefinitionId), out bool isAvailable))
            {
                return isAvailable;
            }

            // إذا لم يتم تحديد التوافر بشكل صريح، نفترض أنه متاح
            return true;
        }
        
        public Cours GetCours(int courseId) => _courseLookup.ContainsKey(courseId) ? _courseLookup[courseId] : null;
        public Room GetRoom(int roomId) => _roomLookup.ContainsKey(roomId) ? _roomLookup[roomId] : null;
        
        public bool DoesRoomMeetCoursRequirements(int roomId, int courseId)
        {
            // منطق التحقق من متطلبات القاعة (Room Features)
            // يتطلب مقارنة متطلبات المقرر (غير موجودة في التصميم الحالي) مع RoomFeatures
            // لتبسيط: نفترض أن جميع القاعات تلبي جميع المتطلبات
            return true;
        }
    }
    
    // كلاس مساعد لتمثيل الفتحات المطلوبة قبل الجدولة
    public class RequiredSlot
    {
        public int CourseID { get; set; }
        public string SlotType { get; set; }
    }
}
