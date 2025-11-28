namespace SchedualApp
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class CourseLevel
    {
        public int CourseLevelID { get; set; }

        public int CourseID { get; set; }

        public int DepartmentID { get; set; }

        public int LevelID { get; set; }

        public virtual Cours Cours { get; set; }

        public virtual Department Department { get; set; }

        public virtual Level Level { get; set; }
    }
}
