namespace practicingbiteme2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ImplementCart : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CartProduct",
                c => new
                    {
                        CustomerID = c.String(nullable: false, maxLength: 128),
                        ProductID = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.CustomerID, t.ProductID })
                .ForeignKey("dbo.Cart", t => t.CustomerID, cascadeDelete: true)
                .ForeignKey("dbo.Product", t => t.ProductID, cascadeDelete: true)
                .Index(t => t.CustomerID)
                .Index(t => t.ProductID);
            
            CreateTable(
                "dbo.Cart",
                c => new
                    {
                        CustomerID = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.CustomerID)
                .ForeignKey("dbo.AspNetUsers", t => t.CustomerID)
                .Index(t => t.CustomerID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CartProduct", "ProductID", "dbo.Product");
            DropForeignKey("dbo.Cart", "CustomerID", "dbo.AspNetUsers");
            DropForeignKey("dbo.CartProduct", "CustomerID", "dbo.Cart");
            DropIndex("dbo.Cart", new[] { "CustomerID" });
            DropIndex("dbo.CartProduct", new[] { "ProductID" });
            DropIndex("dbo.CartProduct", new[] { "CustomerID" });
            DropTable("dbo.Cart");
            DropTable("dbo.CartProduct");
        }
    }
}
