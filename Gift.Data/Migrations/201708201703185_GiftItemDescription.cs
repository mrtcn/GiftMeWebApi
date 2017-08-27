namespace Gift.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class GiftItemDescription : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.GiftItem", "Description", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.GiftItem", "Description");
        }
    }
}
