namespace Gift.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IUserInterface : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Event", "UserId", c => c.Int(nullable: false));
            AddColumn("dbo.EventComment", "UserId", c => c.Int(nullable: false));
            AddColumn("dbo.GiftItemComment", "UserId", c => c.Int(nullable: false));
            DropColumn("dbo.Event", "EventOwnerId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Event", "EventOwnerId", c => c.Int(nullable: false));
            DropColumn("dbo.GiftItemComment", "UserId");
            DropColumn("dbo.EventComment", "UserId");
            DropColumn("dbo.Event", "UserId");
        }
    }
}
