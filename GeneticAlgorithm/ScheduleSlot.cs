using System;

namespace SchedualApp.GeneticAlgorithm
{
    /// <summary>
    /// يمثل الجين (المحاضرة). هذا الكلاس هو الكلاس المولد من قاعدة البيانات مع إضافة وظيفة Clone.
    /// </summary>
    public class ScheduleSlot
    {
        public int ScheduleSlotId { get; set; }
        public int TimetableID { get; set; }
        public int CourseID { get; set; }
        public int LecturerID { get; set; }
        public int RoomID { get; set; }
        public int DayOfWeek { get; set; } // 1=Sunday, 5=Thursday
        public int TimeSlotDefinitionID { get; set; }
        public string SlotType { get; set; } // Lecture or Practical

        /// <summary>
        /// استنساخ عميق (Deep Clone) لكائن ScheduleSlot.
        /// </summary>
        public ScheduleSlot Clone()
        {
            return (ScheduleSlot)this.MemberwiseClone();
        }
    }
}
