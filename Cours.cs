namespace SchedualApp
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Courses")]
    public partial class Cours
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Cours()
        {
            CourseLecturers = new HashSet<CourseLecturer>();
            CourseLevels = new HashSet<CourseLevel>();
            ScheduleSlots = new HashSet<ScheduleSlot>();
        }

        [Key]
        public int CourseID { get; set; }

        [Required]
        [StringLength(10)]
        public string Code { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        public bool IsPractical { get; set; }

        public int LectureHours { get; set; }

        public int PracticalHours { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CourseLecturer> CourseLecturers { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CourseLevel> CourseLevels { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ScheduleSlot> ScheduleSlots { get; set; }
    }
}
