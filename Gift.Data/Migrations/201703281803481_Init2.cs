namespace Gift.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ActorMovie", "ActorId", "dbo.Actor");
            DropForeignKey("dbo.MovieCulture", "BaseEntityId", "dbo.Movie");
            DropForeignKey("dbo.MovieCulture", "CultureId", "dbo.Culture");
            DropForeignKey("dbo.GenreCulture", "BaseEntityId", "dbo.Genre");
            DropForeignKey("dbo.GenreCulture", "CultureId", "dbo.Culture");
            DropForeignKey("dbo.GenreMovie", "GenreId", "dbo.Genre");
            DropForeignKey("dbo.GenreMovie", "MovieId", "dbo.Movie");
            DropForeignKey("dbo.ActorMovie", "MovieId", "dbo.Movie");
            DropForeignKey("dbo.ConnectionPath", "BaseActorId", "dbo.Actor");
            DropIndex("dbo.ActorMovie", new[] { "MovieId" });
            DropIndex("dbo.ActorMovie", new[] { "ActorId" });
            DropIndex("dbo.MovieCulture", new[] { "BaseEntityId" });
            DropIndex("dbo.MovieCulture", new[] { "CultureId" });
            DropIndex("dbo.GenreMovie", new[] { "MovieId" });
            DropIndex("dbo.GenreMovie", new[] { "GenreId" });
            DropIndex("dbo.GenreCulture", new[] { "BaseEntityId" });
            DropIndex("dbo.GenreCulture", new[] { "CultureId" });
            DropIndex("dbo.ConnectionPath", new[] { "BaseActorId" });
            DropTable("dbo.Actor");
            DropTable("dbo.ActorMovie");
            DropTable("dbo.Movie");
            DropTable("dbo.MovieCulture");
            DropTable("dbo.GenreMovie");
            DropTable("dbo.Genre");
            DropTable("dbo.GenreCulture");
            DropTable("dbo.ConnectionPath");
            DropTable("dbo.MovieBranch");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.MovieBranch",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        CreatedBy = c.Int(nullable: false),
                        ModifiedBy = c.Int(),
                        UserType = c.Int(nullable: false),
                        Status = c.Int(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                        ModificationDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ConnectionPath",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BaseActorId = c.Int(nullable: false),
                        DestinationActorId = c.Int(nullable: false),
                        ActorPath = c.String(),
                        MoviePath = c.String(),
                        MaxBranchLevel = c.Int(nullable: false),
                        CreatedBy = c.Int(nullable: false),
                        ModifiedBy = c.Int(),
                        UserType = c.Int(nullable: false),
                        Status = c.Int(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                        ModificationDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.GenreCulture",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BaseEntityId = c.Int(nullable: false),
                        CultureId = c.Int(nullable: false),
                        Name = c.String(),
                        CreatedBy = c.Int(nullable: false),
                        ModifiedBy = c.Int(),
                        UserType = c.Int(nullable: false),
                        Status = c.Int(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                        ModificationDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Genre",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CreatedBy = c.Int(nullable: false),
                        ModifiedBy = c.Int(),
                        UserType = c.Int(nullable: false),
                        Status = c.Int(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                        ModificationDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.GenreMovie",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MovieId = c.Int(nullable: false),
                        GenreId = c.Int(nullable: false),
                        CreatedBy = c.Int(nullable: false),
                        ModifiedBy = c.Int(),
                        UserType = c.Int(nullable: false),
                        Status = c.Int(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                        ModificationDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MovieCulture",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BaseEntityId = c.Int(nullable: false),
                        CultureId = c.Int(nullable: false),
                        Title = c.String(),
                        Country = c.String(),
                        CreatedBy = c.Int(nullable: false),
                        ModifiedBy = c.Int(),
                        UserType = c.Int(nullable: false),
                        Status = c.Int(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                        ModificationDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Movie",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OriginalTitle = c.String(),
                        Director = c.String(),
                        Rating = c.Double(nullable: false),
                        NumberOfVotes = c.Int(nullable: false),
                        Year = c.DateTime(),
                        MovieType = c.Int(nullable: false),
                        CreatedBy = c.Int(nullable: false),
                        ModifiedBy = c.Int(),
                        UserType = c.Int(nullable: false),
                        Status = c.Int(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                        ModificationDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ActorMovie",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MovieId = c.Int(nullable: false),
                        ActorId = c.Int(nullable: false),
                        CharacterName = c.String(),
                        IsStar = c.Boolean(nullable: false),
                        CreatedBy = c.Int(nullable: false),
                        ModifiedBy = c.Int(),
                        UserType = c.Int(nullable: false),
                        Status = c.Int(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                        ModificationDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Actor",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        CreatedBy = c.Int(nullable: false),
                        ModifiedBy = c.Int(),
                        UserType = c.Int(nullable: false),
                        Status = c.Int(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                        ModificationDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.ConnectionPath", "BaseActorId");
            CreateIndex("dbo.GenreCulture", "CultureId");
            CreateIndex("dbo.GenreCulture", "BaseEntityId");
            CreateIndex("dbo.GenreMovie", "GenreId");
            CreateIndex("dbo.GenreMovie", "MovieId");
            CreateIndex("dbo.MovieCulture", "CultureId");
            CreateIndex("dbo.MovieCulture", "BaseEntityId");
            CreateIndex("dbo.ActorMovie", "ActorId");
            CreateIndex("dbo.ActorMovie", "MovieId");
            AddForeignKey("dbo.ConnectionPath", "BaseActorId", "dbo.Actor", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ActorMovie", "MovieId", "dbo.Movie", "Id", cascadeDelete: true);
            AddForeignKey("dbo.GenreMovie", "MovieId", "dbo.Movie", "Id", cascadeDelete: true);
            AddForeignKey("dbo.GenreMovie", "GenreId", "dbo.Genre", "Id", cascadeDelete: true);
            AddForeignKey("dbo.GenreCulture", "CultureId", "dbo.Culture", "Id", cascadeDelete: true);
            AddForeignKey("dbo.GenreCulture", "BaseEntityId", "dbo.Genre", "Id", cascadeDelete: true);
            AddForeignKey("dbo.MovieCulture", "CultureId", "dbo.Culture", "Id", cascadeDelete: true);
            AddForeignKey("dbo.MovieCulture", "BaseEntityId", "dbo.Movie", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ActorMovie", "ActorId", "dbo.Actor", "Id", cascadeDelete: true);
        }
    }
}
