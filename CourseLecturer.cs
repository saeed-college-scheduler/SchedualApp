namespace SchedualApp
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class CourseLecturer
    {
        public int CourseLecturerID { get; set; }

        public int CourseID { get; set; }

        public int LecturerID { get; set; }

        [Required]
        [StringLength(50)]
        public string TeachingType { get; set; }

        public virtual Cours Cours { get; set; }

        public virtual Lecturer Lecturer { get; set; }
    }
}
