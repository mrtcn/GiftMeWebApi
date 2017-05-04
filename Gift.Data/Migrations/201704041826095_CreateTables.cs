namespace Gift.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateTables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Event",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EventName = c.Int(nullable: false),
                        EventTypeId = c.Int(nullable: false),
                        EventOwnerId = c.Int(nullable: false),
                        EventDate = c.DateTime(),
                        Permission = c.Int(nullable: false),
                        CreatedBy = c.Int(nullable: false),
                        ModifiedBy = c.Int(),
                        UserType = c.Int(nullable: false),
                        Status = c.Int(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                        ModificationDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.EventComment",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CommentText = c.Int(nullable: false),
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
                .Index(t => t.EventId);
            
            CreateTable(
                "dbo.GiftItem",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        GiftItemName = c.Int(nullable: false),
                        EventId = c.Int(),
                        UserId = c.Int(nullable: false),
                        IsBought = c.Boolean(nullable: false),
                        CreatedBy = c.Int(nullable: false),
                        ModifiedBy = c.Int(),
                        UserType = c.Int(nullable: false),
                        Status = c.Int(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                        ModificationDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Event", t => t.EventId)
                .Index(t => t.EventId);
            
            CreateTable(
                "dbo.GiftItemComment",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CommentText = c.Int(nullable: false),
                        GiftItemId = c.Int(nullable: false),
                        CreatedBy = c.Int(nullable: false),
                        ModifiedBy = c.Int(),
                        UserType = c.Int(nullable: false),
                        Status = c.Int(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                        ModificationDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.GiftItem", t => t.GiftItemId, cascadeDelete: true)
                .Index(t => t.GiftItemId);
            
            CreateTable(
                "dbo.Friend",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(),
                        FriendId = c.Int(),
                        IsAccepted = c.Boolean(nullable: false),
                        CreatedBy = c.Int(nullable: false),
                        ModifiedBy = c.Int(),
                        UserType = c.Int(nullable: false),
                        Status = c.Int(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                        ModificationDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .ForeignKey("dbo.AspNetUsers", t => t.FriendId)
                .Index(t => t.UserId)
                .Index(t => t.FriendId);
            
            CreateTable(
                "dbo.GiftAutoComplete",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        GiftAutoCompleteName = c.Int(nullable: false),
                        CreatedBy = c.Int(nullable: false),
                        ModifiedBy = c.Int(),
                        UserType = c.Int(nullable: false),
                        Status = c.Int(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                        ModificationDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.EventType",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EventTypeName = c.Int(nullable: false),
                        CreatedBy = c.Int(nullable: false),
                        ModifiedBy = c.Int(),
                        UserType = c.Int(nullable: false),
                        Status = c.Int(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                        ModificationDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.EventUser",
                c => new
                    {
                        UserId = c.Int(nullable: false),
                        EventId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.EventId })
                .ForeignKey("dbo.Event", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.EventId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.EventId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Friend", "FriendId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Friend", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.EventUser", "EventId", "dbo.AspNetUsers");
            DropForeignKey("dbo.EventUser", "UserId", "dbo.Event");
            DropForeignKey("dbo.GiftItemComment", "GiftItemId", "dbo.GiftItem");
            DropForeignKey("dbo.GiftItem", "EventId", "dbo.Event");
            DropForeignKey("dbo.EventComment", "EventId", "dbo.Event");
            DropIndex("dbo.EventUser", new[] { "EventId" });
            DropIndex("dbo.EventUser", new[] { "UserId" });
            DropIndex("dbo.Friend", new[] { "FriendId" });
            DropIndex("dbo.Friend", new[] { "UserId" });
            DropIndex("dbo.GiftItemComment", new[] { "GiftItemId" });
            DropIndex("dbo.GiftItem", new[] { "EventId" });
            DropIndex("dbo.EventComment", new[] { "EventId" });
            DropTable("dbo.EventUser");
            DropTable("dbo.EventType");
            DropTable("dbo.GiftAutoComplete");
            DropTable("dbo.Friend");
            DropTable("dbo.GiftItemComment");
            DropTable("dbo.GiftItem");
            DropTable("dbo.EventComment");
            DropTable("dbo.Event");
        }
    }
}
