using GeneticSharp.Domain.Chromosomes;
using GeneticSharp.Domain.Fitnesses;
using System;
using System.Linq;
using System.Collections.Generic;

namespace SchedualApp.GeneticAlgorithm
{
    /// <summary>
    /// يمثل دالة اللياقة ويرث من IFitness.
    /// </summary>
    public class TimetableFitness : IFitness
    {
        private readonly DataManager _dataManager;
        
        // تعريف قيم العقوبات
        private const double HARD_CONSTRAINT_PENALTY = 1000.0;
        private const double SOFT_CONSTRAINT_PENALTY = 10.0;

        public TimetableFitness(DataManager dataManager)
        {
            _dataManager = dataManager;
        }

        /// <summary>
        /// تقييم لياقة الكروموسوم (الجدول الزمني)
        /// </summary>
        public double Evaluate(IChromosome chromosome)
        {
            var timetableChromosome = (TimetableChromosome)chromosome;
            var slots = timetableChromosome.Genes;
            
            // نبدأ بدرجة لياقة عالية (كلما كانت أعلى، كان الحل أفضل)
            double fitness = 10000.0;

            // 1. التحقق من القيود الصارمة (Hard Constraints)
            fitness -= CheckHardConstraints(slots);

            // 2. التحقق من القيود اللينة (Soft Constraints)
            fitness -= CheckSoftConstraints(slots);

            // ضمان أن اللياقة لا تقل عن الصفر
            return Math.Max(0, fitness);
        }

        /// <summary>
        /// التحقق من القيود الصارمة (تضارب الموارد)
        /// </summary>
        private double CheckHardConstraints(List<ScheduleSlot> slots)
        {
            double penalty = 0;

            for (int i = 0; i < slots.Count; i++)
            {
                var slot1 = slots[i];

                for (int j = i + 1; j < slots.Count; j++)
                {
                    var slot2 = slots[j];

                    // شرط التضارب الزمني: نفس اليوم ونفس الفترة الزمنية
                    if (slot1.DayOfWeek == slot2.DayOfWeek && slot1.TimeSlotDefinitionID == slot2.TimeSlotDefinitionID)
                    {
                        // تضارب المحاضرين (Lecturer Conflict)
                        if (slot1.LecturerID == slot2.LecturerID)
                        {
                            penalty += HARD_CONSTRAINT_PENALTY;
                        }

                        // تضارب القاعات (Room Conflict)
                        if (slot1.RoomID == slot2.RoomID)
                        {
                            penalty += HARD_CONSTRAINT_PENALTY;
                        }
                    }
                    
                    // قيد: المقرر يدرس لمستوى واحد فقط داخل القسم (تم فرضه في CoursLevel)
                    // يجب التحقق هنا من أن المقرر في ScheduleSlot يتوافق مع CoursLevel
                    // (هذا يتطلب منطق إضافي في DataManager)
                }
                
                // قيد: المحاضر مؤهل لتدريس المقرر (CoursLecturer)
                if (!_dataManager.IsLecturerQualified(slot1.LecturerID, slot1.CourseID, slot1.SlotType))
                {
                    penalty += HARD_CONSTRAINT_PENALTY;
                }
            }

            return penalty;
        }

        /// <summary>
        /// التحقق من القيود اللينة (تفضيلات المحاضرين، إلخ)
        /// </summary>
        private double CheckSoftConstraints(List<ScheduleSlot> slots)
        {
            double penalty = 0;

            foreach (var slot in slots)
            {
                // 1. قيد توافر المحاضرين (Lecturer Availability)
                if (!_dataManager.IsLecturerAvailable(slot.LecturerID, slot.DayOfWeek, slot.TimeSlotDefinitionID))
                {
                    penalty += SOFT_CONSTRAINT_PENALTY * 5; // عقوبة أعلى قليلاً
                }

                // 2. قيد سعة القاعة (Room Capacity)
                var course = _dataManager.GetCours(slot.CourseID);
                var room = _dataManager.GetRoom(slot.RoomID);
                
                // نفترض أن عدد الطلاب مرتبط بـ TimetableID، لكن لتبسيط الكود، نستخدم سعة القاعة
                // يجب أن تكون سعة القاعة أكبر من عدد الطلاب المسجلين في المقرر لهذا المستوى
                // (هذا يتطلب بيانات إضافية)
                // يجب أن تكون سعة القاعة أكبر من عدد الطلاب المسجلين في المقرر لهذا المستوى
                // (هذا يتطلب بيانات إضافية)
                // if (room.Capacity < course.ExpectedStudents) // تم حذف ExpectedStudents من Cours
                // {
                //     penalty += SOFT_CONSTRAINT_PENALTY * 2;
                // }
                {
                    penalty += SOFT_CONSTRAINT_PENALTY * 2;
                }
                
                // 3. قيد متطلبات القاعة (Room Features)
                if (!_dataManager.DoesRoomMeetCoursRequirements(slot.RoomID, slot.CourseID))
                {
                    penalty += SOFT_CONSTRAINT_PENALTY * 3;
                }
            }
            
            // 4. قيد تجنب الفراغات للمحاضرين (Lecturer Gaps)
            // يتطلب تجميع المحاضرات حسب المحاضر واليوم والتحقق من وجود فترات زمنية فارغة بينها
            
            return penalty;
        }
    }
}
