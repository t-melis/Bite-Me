namespace practicingbiteme2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModelCreate1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.OrderProduct",
                c => new
                    {
                        OrderID = c.Int(nullable: false),
                        ProductID = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.OrderID, t.ProductID })
                .ForeignKey("dbo.Order", t => t.OrderID, cascadeDelete: true)
                .ForeignKey("dbo.Product", t => t.ProductID, cascadeDelete: true)
                .Index(t => t.OrderID)
                .Index(t => t.ProductID);
            
            CreateTable(
                "dbo.Order",
                c => new
                    {
                        OrderID = c.Int(nullable: false, identity: true),
                        CustomerID = c.String(maxLength: 128),
                        Address = c.String(),
                        Status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.OrderID)
                .ForeignKey("dbo.AspNetUsers", t => t.CustomerID)
                .Index(t => t.CustomerID);
            
            CreateTable(
                "dbo.Product",
                c => new
                    {
                        ProductID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Category = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ProductID);
            
            AddColumn("dbo.AspNetUsers", "TelephoneNumber", c => c.String());
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OrderProduct", "ProductID", "dbo.Product");
            DropForeignKey("dbo.OrderProduct", "OrderID", "dbo.Order");
            DropForeignKey("dbo.Order", "CustomerID", "dbo.AspNetUsers");
            DropIndex("dbo.Order", new[] { "CustomerID" });
            DropIndex("dbo.OrderProduct", new[] { "ProductID" });
            DropIndex("dbo.OrderProduct", new[] { "OrderID" });
            DropColumn("dbo.AspNetUsers", "TelephoneNumber");
            DropTable("dbo.Product");
            DropTable("dbo.Order");
            DropTable("dbo.OrderProduct");
        }
    }
}
