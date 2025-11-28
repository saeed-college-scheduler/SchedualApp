namespace SchedualApp
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class TimeSlotDefinition
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TimeSlotDefinition()
        {
            LecturerAvailabilities = new HashSet<LecturerAvailability>();
            ScheduleSlots = new HashSet<ScheduleSlot>();
        }

        public int TimeSlotDefinitionID { get; set; }

        public int SlotNumber { get; set; }

        public TimeSpan StartTime { get; set; }

        public TimeSpan EndTime { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LecturerAvailability> LecturerAvailabilities { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ScheduleSlot> ScheduleSlots { get; set; }
    }
}
