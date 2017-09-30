namespace Gift.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddThumbnailProperties : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "ThumbnailPath", c => c.String());
            AddColumn("dbo.Event", "EventThumbnailPath", c => c.String());
            AddColumn("dbo.GiftItem", "GiftItemThumbnailPath", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.GiftItem", "GiftItemThumbnailPath");
            DropColumn("dbo.Event", "EventThumbnailPath");
            DropColumn("dbo.AspNetUsers", "ThumbnailPath");
        }
    }
}
