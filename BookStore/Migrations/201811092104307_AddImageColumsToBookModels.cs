namespace BookStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddImageColumsToBookModels : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BookModels", "ImageData", c => c.Binary());
            AddColumn("dbo.BookModels", "ImageMimeType", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.BookModels", "ImageMimeType");
            DropColumn("dbo.BookModels", "ImageData");
        }
    }
}
