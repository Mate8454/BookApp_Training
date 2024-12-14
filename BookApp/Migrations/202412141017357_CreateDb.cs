namespace BookApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateDb : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Books",
                c => new
                    {
                        BookId = c.Int(nullable: false, identity: true),
                        BookTitle = c.String(),
                        Author = c.String(),
                        Price = c.Double(nullable: false),
                        Category = c.String(),
                        Description = c.String(),
                        BookQuantity = c.Int(nullable: false),
                        BookCover = c.String(),
                        PublishedYear = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.BookId);
            
            CreateTable(
                "dbo.Carts",
                c => new
                    {
                        CartId = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        BookId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CartId)
                .ForeignKey("dbo.Books", t => t.BookId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.BookId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Email = c.String(),
                        Password = c.String(),
                        Role = c.String(),
                    })
                .PrimaryKey(t => t.UserId);
            
            CreateTable(
                "dbo.OrderItems",
                c => new
                    {
                        OrderItemId = c.Int(nullable: false, identity: true),
                        OrderId = c.Int(nullable: false),
                        BookId = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                        OrderItemPrice = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.OrderItemId)
                .ForeignKey("dbo.Books", t => t.BookId, cascadeDelete: true)
                .ForeignKey("dbo.Orders", t => t.OrderId, cascadeDelete: true)
                .Index(t => t.OrderId)
                .Index(t => t.BookId);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        OrderId = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        OrderDate = c.DateTime(nullable: false),
                        TotalPrice = c.Int(nullable: false),
                        Status = c.String(),
                        DeliveryAddress = c.String(),
                    })
                .PrimaryKey(t => t.OrderId)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Payments",
                c => new
                    {
                        PaymentId = c.Int(nullable: false, identity: true),
                        OrderId = c.Int(nullable: false),
                        PaymentDate = c.DateTime(nullable: false),
                        PaymentMode = c.String(),
                        PaymentStatus = c.String(),
                    })
                .PrimaryKey(t => t.PaymentId)
                .ForeignKey("dbo.Orders", t => t.OrderId, cascadeDelete: true)
                .Index(t => t.OrderId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Payments", "OrderId", "dbo.Orders");
            DropForeignKey("dbo.OrderItems", "OrderId", "dbo.Orders");
            DropForeignKey("dbo.Orders", "UserId", "dbo.Users");
            DropForeignKey("dbo.OrderItems", "BookId", "dbo.Books");
            DropForeignKey("dbo.Carts", "UserId", "dbo.Users");
            DropForeignKey("dbo.Carts", "BookId", "dbo.Books");
            DropIndex("dbo.Payments", new[] { "OrderId" });
            DropIndex("dbo.Orders", new[] { "UserId" });
            DropIndex("dbo.OrderItems", new[] { "BookId" });
            DropIndex("dbo.OrderItems", new[] { "OrderId" });
            DropIndex("dbo.Carts", new[] { "BookId" });
            DropIndex("dbo.Carts", new[] { "UserId" });
            DropTable("dbo.Payments");
            DropTable("dbo.Orders");
            DropTable("dbo.OrderItems");
            DropTable("dbo.Users");
            DropTable("dbo.Carts");
            DropTable("dbo.Books");
        }
    }
}
