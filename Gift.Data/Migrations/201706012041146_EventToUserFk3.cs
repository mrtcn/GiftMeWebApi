namespace Gift.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EventToUserFk3 : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Event", "UserId");
            AddForeignKey("dbo.Event", "UserId", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Event", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.Event", new[] { "UserId" });
        }
    }
}
