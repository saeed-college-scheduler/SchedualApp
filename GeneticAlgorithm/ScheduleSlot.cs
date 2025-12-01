using System;

namespace SchedualApp.GeneticAlgorithm
{
    // يمثل حصة واحدة في الجدول الزمني
    public class ScheduleSlot
    {
        // بيانات المقرر المطلوب جدولته
        public int RequiredSlotID { get; set; } // معرف الفتحة المطلوبة (لربطها بـ RequiredSlots في DataManager)
        public int CourseID { get; set; }
        public int LecturerID { get; set; }
        public string SlotType { get; set; } // "Lecture" أو "Practical" (مأخوذ من TeachingType)

        // بيانات التعيين (التي تحددها الخوارزمية)
        public int DayOfWeek { get; set; } // 1=الأحد, 7=السبت (SQL format)
        public int TimeSlotDefinitionID { get; set; }
        public int RoomID { get; set; }

        public ScheduleSlot Clone()
        {
            return new ScheduleSlot
            {
                RequiredSlotID = this.RequiredSlotID,
                CourseID = this.CourseID,
                LecturerID = this.LecturerID,
                SlotType = this.SlotType,
                DayOfWeek = this.DayOfWeek,
                TimeSlotDefinitionID = this.TimeSlotDefinitionID,
                RoomID = this.RoomID
            };
        }
    }
}
