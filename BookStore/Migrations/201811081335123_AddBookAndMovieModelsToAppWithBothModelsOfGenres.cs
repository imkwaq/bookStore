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
                        Genre_Id = c.Int(),
                        RelatedMovie_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BookGenres", t => t.Genre_Id)
                .ForeignKey("dbo.Movies", t => t.RelatedMovie_Id)
                .Index(t => t.Genre_Id)
                .Index(t => t.RelatedMovie_Id);
            
            CreateTable(
                "dbo.Movies",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Director = c.String(),
                        ReleaseDate = c.DateTime(nullable: false),
                        RunningTime = c.Int(nullable: false),
                        Genre_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.MovieGenres", t => t.Genre_Id)
                .Index(t => t.Genre_Id);
            
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
            DropForeignKey("dbo.Books", "RelatedMovie_Id", "dbo.Movies");
            DropForeignKey("dbo.Movies", "Genre_Id", "dbo.MovieGenres");
            DropForeignKey("dbo.Books", "Genre_Id", "dbo.BookGenres");
            DropIndex("dbo.Movies", new[] { "Genre_Id" });
            DropIndex("dbo.Books", new[] { "RelatedMovie_Id" });
            DropIndex("dbo.Books", new[] { "Genre_Id" });
            DropTable("dbo.MovieGenres");
            DropTable("dbo.Movies");
            DropTable("dbo.Books");
            DropTable("dbo.BookGenres");
        }
    }
}
