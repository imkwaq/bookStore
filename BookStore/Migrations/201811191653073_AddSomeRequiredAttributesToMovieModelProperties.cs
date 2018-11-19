namespace BookStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSomeRequiredAttributesToMovieModelProperties : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.MovieModels", "MovieGenreModels_Id", "dbo.MovieGenreModels");
            DropIndex("dbo.MovieModels", new[] { "MovieGenreModels_Id" });
            AddColumn("dbo.MovieModels", "ImageData", c => c.Binary());
            AddColumn("dbo.MovieModels", "ImageMimeType", c => c.String());
            AlterColumn("dbo.MovieModels", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.MovieModels", "Director", c => c.String(nullable: false));
            AlterColumn("dbo.MovieModels", "MovieGenreModels_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.MovieModels", "MovieGenreModels_Id");
            AddForeignKey("dbo.MovieModels", "MovieGenreModels_Id", "dbo.MovieGenreModels", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MovieModels", "MovieGenreModels_Id", "dbo.MovieGenreModels");
            DropIndex("dbo.MovieModels", new[] { "MovieGenreModels_Id" });
            AlterColumn("dbo.MovieModels", "MovieGenreModels_Id", c => c.Int());
            AlterColumn("dbo.MovieModels", "Director", c => c.String());
            AlterColumn("dbo.MovieModels", "Name", c => c.String());
            DropColumn("dbo.MovieModels", "ImageMimeType");
            DropColumn("dbo.MovieModels", "ImageData");
            CreateIndex("dbo.MovieModels", "MovieGenreModels_Id");
            AddForeignKey("dbo.MovieModels", "MovieGenreModels_Id", "dbo.MovieGenreModels", "Id");
        }
    }
}
