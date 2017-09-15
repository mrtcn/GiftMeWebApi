namespace Gift.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class GiftItemImagePath : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.GiftItem", "GiftItemImagePath", c => c.String());
            DropColumn("dbo.GiftItem", "GiftImagePath");
        }
        
        public override void Down()
        {
            AddColumn("dbo.GiftItem", "GiftImagePath", c => c.String());
            DropColumn("dbo.GiftItem", "GiftItemImagePath");
        }
    }
}
