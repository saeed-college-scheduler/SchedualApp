using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace SchedualApp
{
    public partial class SchedualAppModel : DbContext
    {
        public SchedualAppModel()
            : base("name=SchedualAppModel")
        {
        }

        public virtual DbSet<CourseLecturer> CourseLecturers { get; set; }
        public virtual DbSet<CourseLevel> CourseLevels { get; set; }
        public virtual DbSet<Cours> Courses { get; set; }
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<Feature> Features { get; set; }
        public virtual DbSet<LecturerAvailability> LecturerAvailabilities { get; set; }
        public virtual DbSet<Lecturer> Lecturers { get; set; }
        public virtual DbSet<Level> Levels { get; set; }
        public virtual DbSet<RoomFeature> RoomFeatures { get; set; }
        public virtual DbSet<Room> Rooms { get; set; }
        public virtual DbSet<ScheduleSlot> ScheduleSlots { get; set; }
        //public DbSet<TimeSlot> TimeSlots { get; set; }
        public virtual DbSet<TimeSlotDefinition> TimeSlotDefinitions { get; set; }
        public virtual DbSet<Timetable> Timetables { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cours>()
                .HasMany(e => e.CourseLecturers)
                .WithRequired(e => e.Cours)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Cours>()
                .HasMany(e => e.CourseLevels)
                .WithRequired(e => e.Cours)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Cours>()
                .HasMany(e => e.ScheduleSlots)
                .WithRequired(e => e.Cours)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Department>()
                .HasMany(e => e.CourseLevels)
                .WithRequired(e => e.Department)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Department>()
                .HasMany(e => e.Timetables)
                .WithRequired(e => e.Department)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Feature>()
                .HasMany(e => e.RoomFeatures)
                .WithRequired(e => e.Feature)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Lecturer>()
                .HasMany(e => e.CourseLecturers)
                .WithRequired(e => e.Lecturer)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Lecturer>()
                .HasMany(e => e.LecturerAvailabilities)
                .WithRequired(e => e.Lecturer)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Lecturer>()
                .HasMany(e => e.ScheduleSlots)
                .WithRequired(e => e.Lecturer)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Level>()
                .HasMany(e => e.CourseLevels)
                .WithRequired(e => e.Level)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Level>()
                .HasMany(e => e.Timetables)
                .WithRequired(e => e.Level)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Room>()
                .HasMany(e => e.RoomFeatures)
                .WithRequired(e => e.Room)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Room>()
                .HasMany(e => e.ScheduleSlots)
                .WithRequired(e => e.Room)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TimeSlotDefinition>()
                .HasMany(e => e.LecturerAvailabilities)
                .WithRequired(e => e.TimeSlotDefinition)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TimeSlotDefinition>()
                .HasMany(e => e.ScheduleSlots)
                .WithRequired(e => e.TimeSlotDefinition)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Timetable>()
                .HasMany(e => e.ScheduleSlots)
                .WithRequired(e => e.Timetable)
                .WillCascadeOnDelete(false);
        }
    }
}
