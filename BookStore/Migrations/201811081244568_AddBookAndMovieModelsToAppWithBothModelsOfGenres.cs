namespace BookStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddBookAndMovieModelsToAppWithBothModelsOfGenres : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BookGenreModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.BookModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Pages = c.Short(nullable: false),
                        Language = c.String(),
                        BookGenreModelsId_Id = c.Int(),
                        MovieModelsId_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BookGenreModels", t => t.BookGenreModelsId_Id)
                .ForeignKey("dbo.MovieModels", t => t.MovieModelsId_Id)
                .Index(t => t.BookGenreModelsId_Id)
                .Index(t => t.MovieModelsId_Id);
            
            CreateTable(
                "dbo.MovieModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Director = c.String(),
                        ReleaseDate = c.DateTime(nullable: false),
                        RunningTime = c.Int(nullable: false),
                        Language = c.String(),
                        Country = c.String(),
                        MovieGenreModelsId_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.MovieGenreModels", t => t.MovieGenreModelsId_Id)
                .Index(t => t.MovieGenreModelsId_Id);
            
            CreateTable(
                "dbo.MovieGenreModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BookModels", "MovieModelsId_Id", "dbo.MovieModels");
            DropForeignKey("dbo.MovieModels", "MovieGenreModelsId_Id", "dbo.MovieGenreModels");
            DropForeignKey("dbo.BookModels", "BookGenreModelsId_Id", "dbo.BookGenreModels");
            DropIndex("dbo.MovieModels", new[] { "MovieGenreModelsId_Id" });
            DropIndex("dbo.BookModels", new[] { "MovieModelsId_Id" });
            DropIndex("dbo.BookModels", new[] { "BookGenreModelsId_Id" });
            DropTable("dbo.MovieGenreModels");
            DropTable("dbo.MovieModels");
            DropTable("dbo.BookModels");
            DropTable("dbo.BookGenreModels");
        }
    }
}
