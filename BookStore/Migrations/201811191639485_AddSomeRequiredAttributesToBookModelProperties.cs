namespace BookStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSomeRequiredAttributesToBookModelProperties : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.BookModels", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.BookModels", "Author", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.BookModels", "Author", c => c.String());
            AlterColumn("dbo.BookModels", "Name", c => c.String());
        }
    }
}
