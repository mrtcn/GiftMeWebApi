namespace Gift.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.Int(nullable: false),
                        RoleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, maxLength: 255),
                        LastName = c.String(nullable: false, maxLength: 255),
                        ImagePath = c.String(),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
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
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Actor", t => t.ActorId, cascadeDelete: true)
                .ForeignKey("dbo.Movie", t => t.MovieId, cascadeDelete: true)
                .Index(t => t.MovieId)
                .Index(t => t.ActorId);
            
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
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Movie", t => t.BaseEntityId, cascadeDelete: true)
                .ForeignKey("dbo.Culture", t => t.CultureId, cascadeDelete: true)
                .Index(t => t.BaseEntityId)
                .Index(t => t.CultureId);
            
            CreateTable(
                "dbo.Culture",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 255),
                        CultureCode = c.String(maxLength: 10),
                        ShortName = c.String(maxLength: 5),
                        FlagImagePath = c.String(),
                        Url = c.String(maxLength: 50),
                        IsDefault = c.Boolean(nullable: false),
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
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Genre", t => t.GenreId, cascadeDelete: true)
                .ForeignKey("dbo.Movie", t => t.MovieId, cascadeDelete: true)
                .Index(t => t.MovieId)
                .Index(t => t.GenreId);
            
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
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Genre", t => t.BaseEntityId, cascadeDelete: true)
                .ForeignKey("dbo.Culture", t => t.CultureId, cascadeDelete: true)
                .Index(t => t.BaseEntityId)
                .Index(t => t.CultureId);
            
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
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Actor", t => t.BaseActorId, cascadeDelete: true)
                .Index(t => t.BaseActorId);
            
            CreateTable(
                "dbo.City",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IsOverpopulated = c.Boolean(nullable: false),
                        CreatedBy = c.Int(nullable: false),
                        ModifiedBy = c.Int(),
                        UserType = c.Int(nullable: false),
                        Status = c.Int(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                        ModificationDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CityCulture",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BaseEntityId = c.Int(nullable: false),
                        Name = c.String(),
                        CreatedBy = c.Int(nullable: false),
                        ModifiedBy = c.Int(),
                        UserType = c.Int(nullable: false),
                        CultureId = c.Int(nullable: false),
                        Status = c.Int(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                        ModificationDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.City", t => t.BaseEntityId, cascadeDelete: true)
                .ForeignKey("dbo.Culture", t => t.CultureId, cascadeDelete: true)
                .Index(t => t.BaseEntityId)
                .Index(t => t.CultureId);
            
            CreateTable(
                "dbo.District",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CityId = c.Int(nullable: false),
                        Name = c.String(maxLength: 255),
                        IsOverpopulated = c.Boolean(nullable: false),
                        Status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.City", t => t.CityId, cascadeDelete: true)
                .Index(t => t.CityId);
            
            CreateTable(
                "dbo.Country",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 255),
                        IsOverpopulated = c.Boolean(nullable: false),
                        Status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CustomRoute",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Url = c.String(maxLength: 255),
                        SeoTitle = c.String(maxLength: 255),
                        MetaKeyword = c.String(maxLength: 500),
                        MetaDescription = c.String(maxLength: 500),
                        ContentId = c.Int(nullable: false),
                        IsAutoGenerated = c.Boolean(nullable: false),
                        HttpStatusCode = c.Int(nullable: false),
                        PredefinedPage = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
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
                "dbo.Module",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ModuleName = c.String(maxLength: 255),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ModulePermission",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ModuleId = c.Int(nullable: false),
                        UserId = c.Int(),
                        RoleId = c.Int(),
                        Permission = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Module", t => t.ModuleId, cascadeDelete: true)
                .Index(t => t.ModuleId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ModulePermission", "ModuleId", "dbo.Module");
            DropForeignKey("dbo.District", "CityId", "dbo.City");
            DropForeignKey("dbo.CityCulture", "CultureId", "dbo.Culture");
            DropForeignKey("dbo.CityCulture", "BaseEntityId", "dbo.City");
            DropForeignKey("dbo.ConnectionPath", "BaseActorId", "dbo.Actor");
            DropForeignKey("dbo.ActorMovie", "MovieId", "dbo.Movie");
            DropForeignKey("dbo.GenreMovie", "MovieId", "dbo.Movie");
            DropForeignKey("dbo.GenreMovie", "GenreId", "dbo.Genre");
            DropForeignKey("dbo.GenreCulture", "CultureId", "dbo.Culture");
            DropForeignKey("dbo.GenreCulture", "BaseEntityId", "dbo.Genre");
            DropForeignKey("dbo.MovieCulture", "CultureId", "dbo.Culture");
            DropForeignKey("dbo.MovieCulture", "BaseEntityId", "dbo.Movie");
            DropForeignKey("dbo.ActorMovie", "ActorId", "dbo.Actor");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropIndex("dbo.ModulePermission", new[] { "ModuleId" });
            DropIndex("dbo.District", new[] { "CityId" });
            DropIndex("dbo.CityCulture", new[] { "CultureId" });
            DropIndex("dbo.CityCulture", new[] { "BaseEntityId" });
            DropIndex("dbo.ConnectionPath", new[] { "BaseActorId" });
            DropIndex("dbo.GenreCulture", new[] { "CultureId" });
            DropIndex("dbo.GenreCulture", new[] { "BaseEntityId" });
            DropIndex("dbo.GenreMovie", new[] { "GenreId" });
            DropIndex("dbo.GenreMovie", new[] { "MovieId" });
            DropIndex("dbo.MovieCulture", new[] { "CultureId" });
            DropIndex("dbo.MovieCulture", new[] { "BaseEntityId" });
            DropIndex("dbo.ActorMovie", new[] { "ActorId" });
            DropIndex("dbo.ActorMovie", new[] { "MovieId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropTable("dbo.ModulePermission");
            DropTable("dbo.Module");
            DropTable("dbo.MovieBranch");
            DropTable("dbo.CustomRoute");
            DropTable("dbo.Country");
            DropTable("dbo.District");
            DropTable("dbo.CityCulture");
            DropTable("dbo.City");
            DropTable("dbo.ConnectionPath");
            DropTable("dbo.GenreCulture");
            DropTable("dbo.Genre");
            DropTable("dbo.GenreMovie");
            DropTable("dbo.Culture");
            DropTable("dbo.MovieCulture");
            DropTable("dbo.Movie");
            DropTable("dbo.ActorMovie");
            DropTable("dbo.Actor");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
        }
    }
}
