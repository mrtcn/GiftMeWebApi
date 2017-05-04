namespace Gift.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserEventUtils : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.EventUser", "UserId", "dbo.Event");
            DropForeignKey("dbo.EventUser", "EventId", "dbo.AspNetUsers");
            DropIndex("dbo.EventUser", new[] { "UserId" });
            DropIndex("dbo.EventUser", new[] { "EventId" });
            CreateTable(
                "dbo.UserEvent",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        EventId = c.Int(nullable: false),
                        CreatedBy = c.Int(nullable: false),
                        ModifiedBy = c.Int(),
                        UserType = c.Int(nullable: false),
                        Status = c.Int(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                        ModificationDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Event", t => t.EventId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.EventId);
            
            DropTable("dbo.EventUser");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.EventUser",
                c => new
                    {
                        UserId = c.Int(nullable: false),
                        EventId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.EventId });
            
            DropForeignKey("dbo.UserEvent", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.UserEvent", "EventId", "dbo.Event");
            DropIndex("dbo.UserEvent", new[] { "EventId" });
            DropIndex("dbo.UserEvent", new[] { "UserId" });
            DropTable("dbo.UserEvent");
            CreateIndex("dbo.EventUser", "EventId");
            CreateIndex("dbo.EventUser", "UserId");
            AddForeignKey("dbo.EventUser", "EventId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.EventUser", "UserId", "dbo.Event", "Id", cascadeDelete: true);
        }
    }
}
