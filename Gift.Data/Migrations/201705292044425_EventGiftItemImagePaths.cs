namespace Gift.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EventGiftItemImagePaths : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Event", "EventImagePath", c => c.String());
            AddColumn("dbo.GiftItem", "GiftImagePath", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.GiftItem", "GiftImagePath");
            DropColumn("dbo.Event", "EventImagePath");
        }
    }
}
