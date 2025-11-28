namespace SchedualApp
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class LecturerAvailability
    {
        public int LecturerAvailabilityID { get; set; }

        public int LecturerID { get; set; }

        public int DayOfWeek { get; set; }

        public int TimeSlotDefinitionID { get; set; }

        public virtual Lecturer Lecturer { get; set; }

        public virtual TimeSlotDefinition TimeSlotDefinition { get; set; }
    }
}
