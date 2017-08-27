namespace Gift.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class GiftItemBrand : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.GiftItem", "Brand", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.GiftItem", "Brand");
        }
    }
}
