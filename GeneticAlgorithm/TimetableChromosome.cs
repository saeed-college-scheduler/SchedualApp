using GeneticSharp.Domain.Chromosomes;
using GeneticSharp.Domain.Randomizations;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SchedualApp.GeneticAlgorithm
{
    public class TimetableChromosome : ChromosomeBase
    {
        private readonly DataManager _dataManager;
        public List<ScheduleSlot> GenesList { get; private set; }

        public TimetableChromosome(DataManager dataManager) : base(dataManager.RequiredSlots.Count)
        {
            _dataManager = dataManager;
            GenesList = new List<ScheduleSlot>();
            CreateGenes();
        }

        // يستخدم لعملية التزاوج (Crossover)
        private TimetableChromosome(DataManager dataManager, List<ScheduleSlot> genesList) : base(genesList.Count)
        {
            _dataManager = dataManager;
            GenesList = genesList;
            ReplaceGenes(0, genesList.Select(s => new Gene(s)).ToArray());
        }

        public override Gene GenerateGene(int index)
        {
            var requiredSlot = _dataManager.RequiredSlots[index];
            var slot = new ScheduleSlot
            {
                RequiredSlotID = requiredSlot.RequiredSlotID,
                CourseID = requiredSlot.CourseID,
                LecturerID = requiredSlot.LecturerID,
                SlotType = requiredSlot.SlotType
            };

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

            slot.DayOfWeek = randomDay;
            slot.TimeSlotDefinitionID = randomTimeSlot;
            slot.RoomID = randomRoom;

            GenesList.Add(slot);
            return new Gene(slot);
        }

        public override IChromosome CreateNew()
        {
            return new TimetableChromosome(_dataManager);
        }

        public override IChromosome Clone()
        {
            var clonedGenes = GetGenes().Select(g => ((ScheduleSlot)g.Value).Clone()).ToList();
            return new TimetableChromosome(_dataManager, clonedGenes);
        }

        // تم إضافة override لتصحيح خطأ CS0506
        protected  void ReplaceGenes(int startIndex, Gene[] genes)
        {
            base.ReplaceGenes(startIndex, genes);
            GenesList = genes.Select(g => (ScheduleSlot)g.Value).ToList();
        }
    }
}
