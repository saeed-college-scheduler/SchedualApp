namespace SchedualApp
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Timetable
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Timetable()
        {
            ScheduleSlots = new HashSet<ScheduleSlot>();
        }

        public int TimetableID { get; set; }
        [Required]
        [StringLength(100)]
        public string TimetableName { get; set; }


        public int DepartmentID { get; set; }

        public int LevelID { get; set; }

        [Required]
        [StringLength(50)]
        public string Semester { get; set; }

        public DateTime CreationDate { get; set; }

        public bool IsApproved { get; set; }

        public virtual Department Department { get; set; }

        public virtual Level Level { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ScheduleSlot> ScheduleSlots { get; set; }
    }
}
