namespace BookStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddBookAndMovieModelsToAppWithBothModelsOfGenres : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BookGenres",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Books",
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
                .ForeignKey("dbo.BookGenres", t => t.BookGenreModelsId_Id)
                .ForeignKey("dbo.Movies", t => t.MovieModelsId_Id)
                .Index(t => t.BookGenreModelsId_Id)
                .Index(t => t.MovieModelsId_Id);
            
            CreateTable(
                "dbo.Movies",
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
                .ForeignKey("dbo.MovieGenres", t => t.MovieGenreModelsId_Id)
                .Index(t => t.MovieGenreModelsId_Id);
            
            CreateTable(
                "dbo.MovieGenres",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Books", "MovieModelsId_Id", "dbo.Movies");
            DropForeignKey("dbo.Movies", "MovieGenreModelsId_Id", "dbo.MovieGenres");
            DropForeignKey("dbo.Books", "BookGenreModelsId_Id", "dbo.BookGenres");
            DropIndex("dbo.Movies", new[] { "MovieGenreModelsId_Id" });
            DropIndex("dbo.Books", new[] { "MovieModelsId_Id" });
            DropIndex("dbo.Books", new[] { "BookGenreModelsId_Id" });
            DropTable("dbo.MovieGenres");
            DropTable("dbo.Movies");
            DropTable("dbo.Books");
            DropTable("dbo.BookGenres");
        }
    }
}
