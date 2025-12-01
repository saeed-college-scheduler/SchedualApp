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

        // **إصلاح الخطأ CS0534**: يجب تطبيق هذه الدالة لتتوافق مع متطلبات ChromosomeBase
        public override Gene GenerateGene(int index)
        {
            var random = RandomizationProvider.Current;
            var requiredSlot = RequiredSlots[index];

            // 1. اختيار يوم عشوائي
            int[] availableDays = { 6, 0, 1, 2, 3, 4 };
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
