using System;
using System.Collections.Generic;
using System.Linq;
using GeneticSharp.Domain.Chromosomes;
using GeneticSharp.Domain.Randomizations;
using SchedualApp.GeneticAlgorithm; // تمت الإضافة

namespace SchedualApp.GeneticAlgorithm
{
    public class TimetableChromosome : ChromosomeBase
    {
        private readonly DataManager _dataManager;
        public List<RequiredSlot> RequiredSlots { get; private set; }
        // تم تغيير نوع GenesList إلى IList<Gene> ليتوافق مع متطلبات GeneticSharp
        public IList<Gene> GenesList { get; private set; }

        public TimetableChromosome(DataManager dataManager) : base(dataManager.RequiredSlots.Count)
        {
            _dataManager = dataManager;
            RequiredSlots = dataManager.RequiredSlots;
            GenesList = new List<Gene>();
            CreateGenes();
        }

        // منشئ النسخ
        public TimetableChromosome(DataManager dataManager, IList<Gene> genes) : base(genes.Count)
        {
            _dataManager = dataManager;
            RequiredSlots = dataManager.RequiredSlots;
            GenesList = new List<Gene>();
            ReplaceGenes(genes);
        }

        protected override void CreateGenes()
        {
            var random = RandomizationProvider.Current;
            GenesList.Clear();

            for (int i = 0; i < Length; i++)
            {
                var requiredSlot = RequiredSlots[i];

                // 1. اختيار يوم عشوائي (السبت = 6، الأحد = 0، ...، الخميس = 4). الجمعة (5) مستبعد.
                // أيام الأسبوع في C# تبدأ من الأحد (0)
                // نحن نريد السبت (6) إلى الخميس (4)
                // الأيام المتاحة: 0, 1, 2, 3, 4, 6
                int[] availableDays = { 6, 0, 1, 2, 3, 4 }; // السبت، الأحد، الإثنين، الثلاثاء، الأربعاء، الخميس
                int dayIndex = random.GetInt(0, availableDays.Length);
                requiredSlot.DayOfWeek = availableDays[dayIndex];

                // 2. اختيار فترة زمنية عشوائية
                var timeSlots = _dataManager.TimeSlotDefinitions;
                int timeSlotIndex = random.GetInt(0, timeSlots.Count);
                requiredSlot.TimeSlotDefinitionID = timeSlots[timeSlotIndex].TimeSlotDefinitionID;

                // 3. اختيار قاعة عشوائية
                var rooms = _dataManager.Rooms;
                int roomIndex = random.GetInt(0, rooms.Count);
                requiredSlot.RoomID = rooms[roomIndex].RoomID;

                // إضافة الجين (الـ RequiredSlot نفسه)
                ReplaceGene(i, new Gene(requiredSlot));
                GenesList.Add(new Gene(requiredSlot)); // إضافة الجين إلى القائمة الجديدة
            }
        }

            // تعيين عشوائي للوقت والقاعة
            var allowedDays = new int[] { 1, 2, 3, 4, 5, 7 };

            // 2. اختر مؤشراً عشوائياً من المصفوفة
            var randomIndex = RandomizationProvider.Current.GetInt(0, allowedDays.Length);

            // 3. احصل على اليوم الفعلي
            var randomDay = allowedDays[randomIndex];
            var timeSlotIds = _dataManager.TimeSlotDefinitions.Select(t => t.TimeSlotDefinitionID).ToList();
            var randomTimeSlot = timeSlotIds[RandomizationProvider.Current.GetInt(0, timeSlotIds.Count)];

            //var roomIds = _dataManager.Rooms.Select(r => r.RoomID).ToList();
            //var randomRoom = roomIds[RandomizationProvider.Current.GetInt(0, roomIds.Count)];
            // 1. تحديد نوع القاعة المطلوب بناءً على نوع الحصة
            // إذا كانت الحصة "Practical" نبحث عن "Lab"، وإلا نبحث عن "Lecture"
            string targetRoomType = (slot.SlotType == "Practical") ? "Practical" : "Lecture";

            // 2. فلترة القاعات المناسبة فقط
            var suitableRoomIds = _dataManager.Rooms
                .Where(r => r.RoomType == targetRoomType)
                .Select(r => r.RoomID)
                .ToList();

            // أمان: إذا لم نجد قاعات مناسبة (مثلاً لا يوجد معامل)، نستخدم أي قاعة لتجنب توقف البرنامج
            // ولكن الأفضل أن يكون لديك ما يكفي من القاعات
            if (!suitableRoomIds.Any())
            {
                suitableRoomIds = _dataManager.Rooms.Select(r => r.RoomID).ToList();
            }

            // 3. الاختيار العشوائي من القائمة المفلترة
            var randomRoom = suitableRoomIds[RandomizationProvider.Current.GetInt(0, suitableRoomIds.Count)];

            // 3. اختيار قاعة عشوائية
            var rooms = _dataManager.Rooms;
            int roomIndex = random.GetInt(0, rooms.Count);
            requiredSlot.RoomID = rooms[roomIndex].RoomID;

            return new Gene(requiredSlot);
        }

        public override IChromosome CreateNew()
        {
            return new TimetableChromosome(_dataManager);
        }

        public override IChromosome Clone()
        {
            return new TimetableChromosome(_dataManager, GetGenes());
        }

        // لتسهيل الوصول إلى الجينات كـ RequiredSlot بعد التعديل
        public void ReplaceGenes(IList<Gene> genes)
        {
            GenesList.Clear();
            for (int i = 0; i < genes.Count; i++)
            {
                ReplaceGene(i, genes[i]);
                GenesList.Add(genes[i]);
            }
        }
    }
}
