namespace SchedualApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateDataBase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CourseLecturers",
                c => new
                    {
                        CourseLecturerID = c.Int(nullable: false, identity: true),
                        CourseID = c.Int(nullable: false),
                        LecturerID = c.Int(nullable: false),
                        TeachingType = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.CourseLecturerID)
                .ForeignKey("dbo.Courses", t => t.CourseID)
                .ForeignKey("dbo.Lecturers", t => t.LecturerID)
                .Index(t => t.CourseID)
                .Index(t => t.LecturerID);
            
            CreateTable(
                "dbo.Courses",
                c => new
                    {
                        CourseID = c.Int(nullable: false, identity: true),
                        Code = c.String(nullable: false, maxLength: 10),
                        Title = c.String(nullable: false, maxLength: 100),
                        IsPractical = c.Boolean(nullable: false),
                        LectureHours = c.Int(nullable: false),
                        PracticalHours = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CourseID);
            
            CreateTable(
                "dbo.CourseLevels",
                c => new
                    {
                        CourseLevelID = c.Int(nullable: false, identity: true),
                        CourseID = c.Int(nullable: false),
                        DepartmentID = c.Int(nullable: false),
                        LevelID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CourseLevelID)
                .ForeignKey("dbo.Departments", t => t.DepartmentID)
                .ForeignKey("dbo.Levels", t => t.LevelID)
                .ForeignKey("dbo.Courses", t => t.CourseID)
                .Index(t => t.CourseID)
                .Index(t => t.DepartmentID)
                .Index(t => t.LevelID);
            
            CreateTable(
                "dbo.Departments",
                c => new
                    {
                        DepartmentID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.DepartmentID);
            
            CreateTable(
                "dbo.Timetables",
                c => new
                    {
                        TimetableID = c.Int(nullable: false, identity: true),
                        TimetableName = c.String(nullable: false, maxLength: 100),
                        DepartmentID = c.Int(nullable: false),
                        LevelID = c.Int(nullable: false),
                        Semester = c.String(nullable: false, maxLength: 50),
                        CreationDate = c.DateTime(nullable: false),
                        IsApproved = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.TimetableID)
                .ForeignKey("dbo.Levels", t => t.LevelID)
                .ForeignKey("dbo.Departments", t => t.DepartmentID)
                .Index(t => t.DepartmentID)
                .Index(t => t.LevelID);
            
            CreateTable(
                "dbo.Levels",
                c => new
                    {
                        LevelID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.LevelID);
            
            CreateTable(
                "dbo.ScheduleSlots",
                c => new
                    {
                        ScheduleSlotID = c.Int(nullable: false, identity: true),
                        TimetableID = c.Int(nullable: false),
                        CourseID = c.Int(nullable: false),
                        LecturerID = c.Int(nullable: false),
                        RoomID = c.Int(nullable: false),
                        DayOfWeek = c.Int(nullable: false),
                        TimeSlotDefinitionID = c.Int(nullable: false),
                        SlotType = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.ScheduleSlotID)
                .ForeignKey("dbo.TimeSlotDefinitions", t => t.TimeSlotDefinitionID)
                .ForeignKey("dbo.Lecturers", t => t.LecturerID)
                .ForeignKey("dbo.Rooms", t => t.RoomID)
                .ForeignKey("dbo.Timetables", t => t.TimetableID)
                .ForeignKey("dbo.Courses", t => t.CourseID)
                .Index(t => t.TimetableID)
                .Index(t => t.CourseID)
                .Index(t => t.LecturerID)
                .Index(t => t.RoomID)
                .Index(t => t.TimeSlotDefinitionID);
            
            CreateTable(
                "dbo.Lecturers",
                c => new
                    {
                        LecturerID = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, maxLength: 50),
                        LastName = c.String(nullable: false, maxLength: 50),
                        AcademicRank = c.String(maxLength: 50),
                        MaxWorkload = c.Int(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.LecturerID);
            
            CreateTable(
                "dbo.LecturerAvailabilities",
                c => new
                    {
                        LecturerAvailabilityID = c.Int(nullable: false, identity: true),
                        LecturerID = c.Int(nullable: false),
                        DayOfWeek = c.Int(nullable: false),
                        TimeSlotDefinitionID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.LecturerAvailabilityID)
                .ForeignKey("dbo.TimeSlotDefinitions", t => t.TimeSlotDefinitionID)
                .ForeignKey("dbo.Lecturers", t => t.LecturerID)
                .Index(t => t.LecturerID)
                .Index(t => t.TimeSlotDefinitionID);
            
            CreateTable(
                "dbo.TimeSlotDefinitions",
                c => new
                    {
                        TimeSlotDefinitionID = c.Int(nullable: false, identity: true),
                        SlotNumber = c.Int(nullable: false),
                        StartTime = c.Time(nullable: false, precision: 7),
                        EndTime = c.Time(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => t.TimeSlotDefinitionID);
            
            CreateTable(
                "dbo.Rooms",
                c => new
                    {
                        RoomID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Capacity = c.Int(nullable: false),
                        RoomType = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.RoomID);
            
            CreateTable(
                "dbo.RoomFeatures",
                c => new
                    {
                        RoomFeatureID = c.Int(nullable: false, identity: true),
                        RoomID = c.Int(nullable: false),
                        FeatureID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.RoomFeatureID)
                .ForeignKey("dbo.Features", t => t.FeatureID)
                .ForeignKey("dbo.Rooms", t => t.RoomID)
                .Index(t => t.RoomID)
                .Index(t => t.FeatureID);
            
            CreateTable(
                "dbo.Features",
                c => new
                    {
                        FeatureID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.FeatureID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ScheduleSlots", "CourseID", "dbo.Courses");
            DropForeignKey("dbo.CourseLevels", "CourseID", "dbo.Courses");
            DropForeignKey("dbo.Timetables", "DepartmentID", "dbo.Departments");
            DropForeignKey("dbo.ScheduleSlots", "TimetableID", "dbo.Timetables");
            DropForeignKey("dbo.ScheduleSlots", "RoomID", "dbo.Rooms");
            DropForeignKey("dbo.RoomFeatures", "RoomID", "dbo.Rooms");
            DropForeignKey("dbo.RoomFeatures", "FeatureID", "dbo.Features");
            DropForeignKey("dbo.ScheduleSlots", "LecturerID", "dbo.Lecturers");
            DropForeignKey("dbo.LecturerAvailabilities", "LecturerID", "dbo.Lecturers");
            DropForeignKey("dbo.ScheduleSlots", "TimeSlotDefinitionID", "dbo.TimeSlotDefinitions");
            DropForeignKey("dbo.LecturerAvailabilities", "TimeSlotDefinitionID", "dbo.TimeSlotDefinitions");
            DropForeignKey("dbo.CourseLecturers", "LecturerID", "dbo.Lecturers");
            DropForeignKey("dbo.Timetables", "LevelID", "dbo.Levels");
            DropForeignKey("dbo.CourseLevels", "LevelID", "dbo.Levels");
            DropForeignKey("dbo.CourseLevels", "DepartmentID", "dbo.Departments");
            DropForeignKey("dbo.CourseLecturers", "CourseID", "dbo.Courses");
            DropIndex("dbo.RoomFeatures", new[] { "FeatureID" });
            DropIndex("dbo.RoomFeatures", new[] { "RoomID" });
            DropIndex("dbo.LecturerAvailabilities", new[] { "TimeSlotDefinitionID" });
            DropIndex("dbo.LecturerAvailabilities", new[] { "LecturerID" });
            DropIndex("dbo.ScheduleSlots", new[] { "TimeSlotDefinitionID" });
            DropIndex("dbo.ScheduleSlots", new[] { "RoomID" });
            DropIndex("dbo.ScheduleSlots", new[] { "LecturerID" });
            DropIndex("dbo.ScheduleSlots", new[] { "CourseID" });
            DropIndex("dbo.ScheduleSlots", new[] { "TimetableID" });
            DropIndex("dbo.Timetables", new[] { "LevelID" });
            DropIndex("dbo.Timetables", new[] { "DepartmentID" });
            DropIndex("dbo.CourseLevels", new[] { "LevelID" });
            DropIndex("dbo.CourseLevels", new[] { "DepartmentID" });
            DropIndex("dbo.CourseLevels", new[] { "CourseID" });
            DropIndex("dbo.CourseLecturers", new[] { "LecturerID" });
            DropIndex("dbo.CourseLecturers", new[] { "CourseID" });
            DropTable("dbo.Features");
            DropTable("dbo.RoomFeatures");
            DropTable("dbo.Rooms");
            DropTable("dbo.TimeSlotDefinitions");
            DropTable("dbo.LecturerAvailabilities");
            DropTable("dbo.Lecturers");
            DropTable("dbo.ScheduleSlots");
            DropTable("dbo.Levels");
            DropTable("dbo.Timetables");
            DropTable("dbo.Departments");
            DropTable("dbo.CourseLevels");
            DropTable("dbo.Courses");
            DropTable("dbo.CourseLecturers");
        }
    }
}
