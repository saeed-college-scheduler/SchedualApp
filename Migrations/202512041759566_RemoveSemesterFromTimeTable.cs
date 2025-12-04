namespace SchedualApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveSemesterFromTimeTable : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Timetables", "Semester");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Timetables", "Semester", c => c.String(nullable: true, maxLength: 50));
        }
    }
}
