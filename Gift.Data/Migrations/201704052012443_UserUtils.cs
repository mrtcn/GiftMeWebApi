namespace Gift.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserUtils : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Event", "EventName", c => c.String());
            AlterColumn("dbo.EventComment", "CommentText", c => c.String());
            AlterColumn("dbo.GiftItem", "GiftItemName", c => c.String());
            AlterColumn("dbo.GiftItemComment", "CommentText", c => c.String());
            AlterColumn("dbo.GiftAutoComplete", "GiftAutoCompleteName", c => c.String());
            AlterColumn("dbo.EventType", "EventTypeName", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.EventType", "EventTypeName", c => c.Int(nullable: false));
            AlterColumn("dbo.GiftAutoComplete", "GiftAutoCompleteName", c => c.Int(nullable: false));
            AlterColumn("dbo.GiftItemComment", "CommentText", c => c.Int(nullable: false));
            AlterColumn("dbo.GiftItem", "GiftItemName", c => c.Int(nullable: false));
            AlterColumn("dbo.EventComment", "CommentText", c => c.Int(nullable: false));
            AlterColumn("dbo.Event", "EventName", c => c.Int(nullable: false));
        }
    }
}
