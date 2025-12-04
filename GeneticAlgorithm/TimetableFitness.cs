using System;
using System.Collections.Generic;
using System.Linq;
using GeneticSharp.Domain.Chromosomes;
using GeneticSharp.Domain.Fitnesses;
using SchedualApp.GeneticAlgorithm; // تمت الإضافة

namespace SchedualApp.GeneticAlgorithm
{
    public class TimetableFitness : IFitness
    {
        private readonly DataManager _dataManager;

        public TimetableFitness(DataManager dataManager)
        {
            _dataManager = dataManager;
        }

        public double Evaluate(IChromosome chromosome)
        {
            var timetable = (TimetableChromosome)chromosome;
            // **إصلاح الخطأ CS1503**: استخدام GetGenes() للحصول على قائمة الجينات الرسمية
            var slots = timetable.GetGenes();
            double fitness = 1000000.0; // قيمة لياقة ابتدائية عالية

            // استدعاء الدوال المساعدة بشكل صحيح
            fitness -= CheckHardConstraints(slots);
            fitness -= CheckSoftConstraints(slots);

            return Math.Max(0, fitness);
        }

        private double CheckHardConstraints(IList<Gene> slots)
        {
            double penalty = 0;
            const double HARD_PENALTY = 500000; // عقوبة قاسية جداً

            // **الخطوة الحاسمة:** تحويل قائمة الجينات إلى قائمة من RequiredSlot
            // هذا يحل مشكلة الوصول إلى الخصائص
            var requiredSlots = slots.Select(g => (RequiredSlot)g.Value).ToList();

            // 1. التحقق من تعارض الدفعة (نفس الدفعة في نفس الوقت)
            var studentGroupConflicts = requiredSlots
                // تجميع الجينات حسب اليوم والفترة الزمنية
                .GroupBy(s => new { s.DayOfWeek, s.TimeSlotDefinitionID })
                // داخل كل فترة زمنية، تجميع الجينات حسب الدفعة (القسم والمستوى)
                .SelectMany(g => g.GroupBy(s => new { s.DepartmentID, s.LevelID }))
                // تحديد المجموعات التي تحتوي على أكثر من جين واحد (تعارض)
                .Where(g => g.Count() > 1)
                .Sum(g => g.Count() - 1);

            penalty += studentGroupConflicts * HARD_PENALTY;

            // 2. التحقق من توافر المحاضرين (Lecturer Availability)
            var unavailableLecturerSlots = requiredSlots
                .Where(s => !_dataManager.LecturerAvailabilities.Any(la =>
                    la.LecturerID == s.LecturerID &&
                    la.DayOfWeek == s.DayOfWeek &&
                    la.TimeSlotDefinitionID == s.TimeSlotDefinitionID))
                .Count();

            penalty += unavailableLecturerSlots * HARD_PENALTY;

            // 3. تعارض القاعات (Room Conflict)
            var roomConflicts = requiredSlots
                .GroupBy(s => new { s.DayOfWeek, s.TimeSlotDefinitionID, s.RoomID })
                .Where(g => g.Count() > 1)
                .Sum(g => g.Count() - 1);

            penalty += roomConflicts * HARD_PENALTY;

            // 4. تعارض المحاضرين (Lecturer Conflict - محاضر واحد في مكانين)
            var lecturerConflicts = requiredSlots
                .GroupBy(s => new { s.DayOfWeek, s.TimeSlotDefinitionID, s.LecturerID })
                .Where(g => g.Count() > 1)
                .Sum(g => g.Count() - 1);

            penalty += lecturerConflicts * HARD_PENALTY;

            // 5. القائمة السوداء العالمية (Global Blacklist)
            var blacklistConflicts = requiredSlots
                .Where(s => _dataManager.IsGloballyBlacklisted(s.DayOfWeek, s.TimeSlotDefinitionID, s.LecturerID) ||
                            _dataManager.IsGloballyBlacklisted(s.DayOfWeek, s.TimeSlotDefinitionID, s.RoomID))
                .Count();

            penalty += blacklistConflicts * HARD_PENALTY;

            return penalty;
        }

        private double CheckSoftConstraints(IList<Gene> slots)
        {
            double penalty = 0;
            const double SOFT_PENALTY = 1000; // عقوبة مرنة

            // **الخطوة الحاسمة:** تحويل قائمة الجينات إلى قائمة من RequiredSlot
            var requiredSlots = slots.Select(g => (RequiredSlot)g.Value).ToList();

            // 1. تجميع المحاضرات للمحاضر (Lecturer Lecture Spreading)
            var lecturerDaySlots = requiredSlots
                .GroupBy(s => new { s.LecturerID, s.DayOfWeek })
                .Where(g => g.Count() > 1);

            foreach (var group in lecturerDaySlots)
            {
                var orderedSlots = group.OrderBy(s => s.TimeSlotDefinitionID).ToList();
                for (int i = 0; i < orderedSlots.Count - 1; i++)
                {
                    // عقوبة على كل فجوة بين المحاضرات
                    if (orderedSlots[i + 1].TimeSlotDefinitionID - orderedSlots[i].TimeSlotDefinitionID > 1)
                    {
                        penalty += SOFT_PENALTY * 0.5;
                    }
                }
            }

            // 2. تجميع المحاضرات للدفعة (Student Group Spreading)
            var studentDaySlots = requiredSlots
                .GroupBy(s => new { s.DepartmentID, s.LevelID, s.DayOfWeek })
                .Where(g => g.Count() > 1);

            foreach (var group in studentDaySlots)
            {
                var orderedSlots = group.OrderBy(s => s.TimeSlotDefinitionID).ToList();
                for (int i = 0; i < orderedSlots.Count - 1; i++)
                {
                    // عقوبة على كل فجوة بين المحاضرات
                    if (orderedSlots[i + 1].TimeSlotDefinitionID - orderedSlots[i].TimeSlotDefinitionID > 1)
                    {
                        penalty += SOFT_PENALTY * 0.5;
                    }
                }
            }

            return penalty;
        }
    }
}
