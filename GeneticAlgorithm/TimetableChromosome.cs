using GeneticSharp.Domain.Chromosomes;
using GeneticSharp.Domain.Randomizations;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SchedualApp.GeneticAlgorithm
{
    /// <summary>
    /// يمثل الكروموسوم (الجدول الزمني) ويرث من IChromosome.
    /// </summary>
    public class TimetableChromosome : ChromosomeBase
    {
        private readonly DataManager _dataManager;
        private readonly int _timetableId;
        
        // Genes: تمثل قائمة المحاضرات (ScheduleSlot) التي تشكل الجدول الزمني
        public List<ScheduleSlot> Genes { get; set; }

        /// <summary>
        /// Constructor for creating a new chromosome.
        /// </summary>
        /// <param name="dataManager">مدير البيانات لتحميل الموارد.</param>
        /// <param name="timetableId">معرف الجدول الزمني الذي يتم توليده.</param>
        public TimetableChromosome(DataManager dataManager, int timetableId) : base(dataManager.RequiredSlots.Count)
        {
            _dataManager = dataManager;
            _timetableId = timetableId;
            Genes = new List<ScheduleSlot>(dataManager.RequiredSlots.Count);
            
            // يجب أن يكون عدد الجينات مساوياً لعدد الفتحات المطلوبة
            CreateGenes();
        }

        /// <summary>
        /// Constructor for cloning.
        /// </summary>
        private TimetableChromosome(DataManager dataManager, int timetableId, List<ScheduleSlot> genes) : base(dataManager.RequiredSlots.Count)
        {
            _dataManager = dataManager;
            _timetableId = timetableId;
            Genes = genes.Select(s => s.Clone()).ToList(); // Deep copy of ScheduleSlots

            // Replace internal genes array starting at 0
            var geneArray = Genes.Select(g => new Gene(g)).ToArray();
            ReplaceGenes(0, geneArray);
        }

        /// <summary>
        /// إنشاء الجينات (مطلوب من ChromosomeBase)
        /// </summary>
        public override Gene GenerateGene(int geneIndex)
        {
            var requiredSlots = _dataManager.RequiredSlots;
            var random = RandomizationProvider.Current;

            var requiredSlot = requiredSlots[geneIndex];

            // اختيار مورد عشوائي لكل فتحة مطلوبة
            var randomLecturer = _dataManager.Lecturers.ElementAt(random.GetInt(0, Math.Max(1, _dataManager.Lecturers.Count)));
            var randomRoom = _dataManager.Rooms.ElementAt(random.GetInt(0, Math.Max(1, _dataManager.Rooms.Count)));
            var randomDay = random.GetInt(1, 7); // 1-6 inclusive? use 1..6 or 1..7; keep 1..6 originally used 1..6 (end-exclusive), adjust to 7
            var randomTimeSlot = _dataManager.TimeSlotDefinitions.ElementAt(random.GetInt(0, Math.Max(1, _dataManager.TimeSlotDefinitions.Count)));

            var newSlot = new ScheduleSlot
            {
                TimetableID = _timetableId,
                CourseID = requiredSlot.CourseID,
                LecturerID = randomLecturer.LecturerID,
                RoomID = randomRoom.RoomID,
                DayOfWeek = randomDay,
                TimeSlotDefinitionID = randomTimeSlot.TimeSlotDefinitionID,
                SlotType = requiredSlot.SlotType // Lecture or Practical
            };

            // Ensure Genes list mirrors the chromosome genes
            if (Genes.Count > geneIndex)
            {
                Genes[geneIndex] = newSlot;
            }
            else
            {
                // pad with nulls if necessary
                while (Genes.Count < geneIndex) Genes.Add(new ScheduleSlot());
                Genes.Add(newSlot);
            }

            return new Gene(newSlot);
        }

        /// <summary>
        /// إنشاء نسخة جديدة من الكروموسوم (مطلوب من IChromosome)
        /// </summary>
        public override IChromosome CreateNew()
        {
            return new TimetableChromosome(_dataManager, _timetableId);
        }

        /// <summary>
        /// استنساخ الكروموسوم (مطلوب من IChromosome)
        /// </summary>
        public override IChromosome Clone()
        {
            // يجب أن يتم استنساخ الجينات أيضاً (Deep Clone)
            var clonedGenes = Genes.Select(s => s.Clone()).ToList();
            return new TimetableChromosome(_dataManager, _timetableId, clonedGenes);
        }
        
        // يجب أن يتم تطبيق منطق التزاوج والطفرة على هذا الكلاس
        // لكن GeneticSharp يتعامل معها عبر كلاسات Crossover و Mutation منفصلة.
    }
}
