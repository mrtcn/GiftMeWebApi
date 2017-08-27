namespace Gift.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FavoriteEvent : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FavoriteEvent",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EventId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                        CreatedBy = c.Int(nullable: false),
                        ModifiedBy = c.Int(),
                        UserType = c.Int(nullable: false),
                        Status = c.Int(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                        ModificationDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Event", t => t.EventId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.EventId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FavoriteEvent", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.FavoriteEvent", "EventId", "dbo.Event");
            DropIndex("dbo.FavoriteEvent", new[] { "UserId" });
            DropIndex("dbo.FavoriteEvent", new[] { "EventId" });
            DropTable("dbo.FavoriteEvent");
        }
    }
}
