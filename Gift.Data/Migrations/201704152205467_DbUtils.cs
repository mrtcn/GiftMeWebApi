namespace Gift.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DbUtils : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Friend", "FriendshipStatus", c => c.Int(nullable: false));
            DropColumn("dbo.Friend", "IsAccepted");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Friend", "IsAccepted", c => c.Int(nullable: false));
            DropColumn("dbo.Friend", "FriendshipStatus");
        }
    }
}
