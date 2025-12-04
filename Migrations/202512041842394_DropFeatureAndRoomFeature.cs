namespace SchedualApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DropFeatureAndRoomFeature : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.RoomFeatures", "FeatureID", "dbo.Features");
            DropForeignKey("dbo.RoomFeatures", "RoomID", "dbo.Rooms");
            DropIndex("dbo.RoomFeatures", new[] { "RoomID" });
            DropIndex("dbo.RoomFeatures", new[] { "FeatureID" });
            DropTable("dbo.RoomFeatures");
            DropTable("dbo.Features");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Features",
                c => new
                    {
                        FeatureID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.FeatureID);
            
            CreateTable(
                "dbo.RoomFeatures",
                c => new
                    {
                        RoomFeatureID = c.Int(nullable: false, identity: true),
                        RoomID = c.Int(nullable: false),
                        FeatureID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.RoomFeatureID);
            
            CreateIndex("dbo.RoomFeatures", "FeatureID");
            CreateIndex("dbo.RoomFeatures", "RoomID");
            AddForeignKey("dbo.RoomFeatures", "RoomID", "dbo.Rooms", "RoomID");
            AddForeignKey("dbo.RoomFeatures", "FeatureID", "dbo.Features", "FeatureID");
        }
    }
}
