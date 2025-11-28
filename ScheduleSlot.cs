namespace SchedualApp
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ScheduleSlot
    {
        public int ScheduleSlotID { get; set; }

        public int TimetableID { get; set; }

        public int CourseID { get; set; }

        public int LecturerID { get; set; }

        public int RoomID { get; set; }

        public int DayOfWeek { get; set; }

        public int TimeSlotDefinitionID { get; set; }

        [Required]
        [StringLength(50)]
        public string SlotType { get; set; }

        public virtual Cours Cours { get; set; }

        public virtual Lecturer Lecturer { get; set; }

        public virtual Room Room { get; set; }

        public virtual TimeSlotDefinition TimeSlotDefinition { get; set; }

        public virtual Timetable Timetable { get; set; }
    }
}
