using GeneticSharp.Domain.Chromosomes;
using GeneticSharp.Domain.Fitnesses;
using System;
using System.Linq;
using System.Collections.Generic;

namespace SchedualApp.GeneticAlgorithm
{
    public class TimetableFitness : IFitness
    {
        private readonly DataManager _dataManager;

        // العقوبات المطلوبة
        private const double HARD_CONSTRAINT_PENALTY = 10000.0;
        private const double SOFT_CONSTRAINT_PENALTY = 100.0;

        public TimetableFitness(DataManager dataManager) => _dataManager = dataManager;

        public double Evaluate(IChromosome chromosome)
        {
            var timetable = (TimetableChromosome)chromosome;
            var slots = timetable.GenesList;
            double fitness = 1000000.0;

            // استدعاء الدوال المساعدة بشكل صحيح
            fitness -= CheckHardConstraints(slots);
            fitness -= CheckSoftConstraints(slots);

            return Math.Max(0, fitness);
        }

        // تم جعل الدوال private لتجنب خطأ CS0121
        private double CheckHardConstraints(List<ScheduleSlot> slots)
        {
            double penalty = 0;
            var groups = slots.GroupBy(s => new { s.DayOfWeek, s.TimeSlotDefinitionID });

            foreach (var group in groups)
            {
                // 1. تعارض قاعة
                penalty += (group.GroupBy(g => g.RoomID).Where(c => c.Count() > 1).Count() * HARD_CONSTRAINT_PENALTY);

                // 2. تعارض مدرس محلي
                penalty += (group.GroupBy(g => g.LecturerID).Where(c => c.Count() > 1).Count() * HARD_CONSTRAINT_PENALTY);
            }

            foreach (var slot in slots)
            {
                // 3. القائمة السوداء (Blacklist)
                if (_dataManager.IsGloballyBlacklisted(slot.DayOfWeek, slot.TimeSlotDefinitionID, slot.LecturerID) ||
                    _dataManager.IsGloballyBlacklisted(slot.DayOfWeek, slot.TimeSlotDefinitionID, slot.RoomID))
                {
                    penalty += HARD_CONSTRAINT_PENALTY;
                }
            }

            return penalty;
        }

        // تم جعل الدوال private لتجنب خطأ CS0121
        private double CheckSoftConstraints(List<ScheduleSlot> slots)
        {
            double penalty = 0;
            foreach (var slot in slots)
            {
                // 1. قيد توافر المحاضرين
                if (!_dataManager.IsLecturerAvailable(slot.LecturerID, slot.DayOfWeek, slot.TimeSlotDefinitionID))
                {
                    penalty += SOFT_CONSTRAINT_PENALTY * 5;
                }

                // 2. تعارض نوع القاعة
                var room = _dataManager.GetRoom(slot.RoomID);
                if (room != null)
                {
                    if (slot.SlotType == "Practical" && room.RoomType != "Lab")
                    {
                        penalty += SOFT_CONSTRAINT_PENALTY;
                    }
                    else if (slot.SlotType == "Lecture" && room.RoomType == "Lab")
                    {
                        penalty += SOFT_CONSTRAINT_PENALTY * 0.5;
                    }
                }
            }
            return penalty;
        }
    }
}
