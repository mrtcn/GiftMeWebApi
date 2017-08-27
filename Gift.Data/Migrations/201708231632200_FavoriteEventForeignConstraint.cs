namespace Gift.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FavoriteEventForeignConstraint : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.FavoriteEvent", "UserId", "dbo.AspNetUsers");
            AddForeignKey("dbo.FavoriteEvent", "UserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FavoriteEvent", "UserId", "dbo.AspNetUsers");
            AddForeignKey("dbo.FavoriteEvent", "UserId", "dbo.AspNetUsers", "Id");
        }
    }
}
