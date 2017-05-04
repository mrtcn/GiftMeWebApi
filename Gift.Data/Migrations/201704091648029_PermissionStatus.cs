namespace Gift.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PermissionStatus : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Friend", "IsAccepted", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Friend", "IsAccepted", c => c.Boolean(nullable: false));
        }
    }
}
