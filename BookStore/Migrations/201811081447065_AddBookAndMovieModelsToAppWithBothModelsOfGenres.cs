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
                        Author = c.String(),
                        ReleaseYear = c.Short(nullable: false),
                        Description = c.String(),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        BookGenreModels_Id = c.Int(),
                        MovieModels_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BookGenreModels", t => t.BookGenreModels_Id)
                .ForeignKey("dbo.MovieModels", t => t.MovieModels_Id)
                .Index(t => t.BookGenreModels_Id)
                .Index(t => t.MovieModels_Id);
            
            CreateTable(
                "dbo.MovieModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Director = c.String(),
                        ReleaseDate = c.DateTime(nullable: false),
                        RunningTime = c.Int(nullable: false),
                        MovieGenreModels_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.MovieGenreModels", t => t.MovieGenreModels_Id)
                .Index(t => t.MovieGenreModels_Id);
            
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
            DropForeignKey("dbo.BookModels", "MovieModels_Id", "dbo.MovieModels");
            DropForeignKey("dbo.MovieModels", "MovieGenreModels_Id", "dbo.MovieGenreModels");
            DropForeignKey("dbo.BookModels", "BookGenreModels_Id", "dbo.BookGenreModels");
            DropIndex("dbo.MovieModels", new[] { "MovieGenreModels_Id" });
            DropIndex("dbo.BookModels", new[] { "MovieModels_Id" });
            DropIndex("dbo.BookModels", new[] { "BookGenreModels_Id" });
            DropTable("dbo.MovieGenreModels");
            DropTable("dbo.MovieModels");
            DropTable("dbo.BookModels");
            DropTable("dbo.BookGenreModels");
        }
    }
}
