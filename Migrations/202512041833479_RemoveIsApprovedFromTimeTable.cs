namespace SchedualApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveIsApprovedFromTimeTable : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Timetables", "IsApproved");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Timetables", "IsApproved", c => c.Boolean(nullable: false));
        }
    }
}
