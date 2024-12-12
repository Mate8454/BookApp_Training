namespace BookApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateBookTitleName : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Books", "BookTitle", c => c.String());
            DropColumn("dbo.Books", "BookTitile");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Books", "BookTitile", c => c.String());
            DropColumn("dbo.Books", "BookTitle");
        }
    }
}
