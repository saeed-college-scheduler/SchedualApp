namespace SchedualApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveIsActiveAndMaxWorkFromLecturere : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Lecturers", "MaxWorkload");
            DropColumn("dbo.Lecturers", "IsActive");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Lecturers", "IsActive", c => c.Boolean(nullable: false));
            AddColumn("dbo.Lecturers", "MaxWorkload", c => c.Int(nullable: false));
        }
    }
}
