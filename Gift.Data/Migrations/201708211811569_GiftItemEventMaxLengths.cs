namespace Gift.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class GiftItemEventMaxLengths : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Event", "EventName", c => c.String(maxLength: 40));
            AlterColumn("dbo.GiftItem", "GiftItemName", c => c.String(maxLength: 40));
            AlterColumn("dbo.GiftItem", "Brand", c => c.String(maxLength: 60));
            AlterColumn("dbo.GiftItem", "Description", c => c.String(maxLength: 140));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.GiftItem", "Description", c => c.String());
            AlterColumn("dbo.GiftItem", "Brand", c => c.String());
            AlterColumn("dbo.GiftItem", "GiftItemName", c => c.String());
            AlterColumn("dbo.Event", "EventName", c => c.String());
        }
    }
}
