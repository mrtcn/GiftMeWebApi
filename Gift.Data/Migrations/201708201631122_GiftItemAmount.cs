namespace Gift.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class GiftItemAmount : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.GiftItem", "Amount", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.GiftItem", "Amount");
        }
    }
}
