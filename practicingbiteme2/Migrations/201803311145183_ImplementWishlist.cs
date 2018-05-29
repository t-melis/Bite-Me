namespace practicingbiteme2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ImplementWishlist : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Wishlist",
                c => new
                    {
                        CustomerID = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.CustomerID)
                .ForeignKey("dbo.AspNetUsers", t => t.CustomerID)
                .Index(t => t.CustomerID);
            
            CreateTable(
                "dbo.WishlistProduct",
                c => new
                    {
                        Wishlist_CustomerID = c.String(nullable: false, maxLength: 128),
                        Product_ProductID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Wishlist_CustomerID, t.Product_ProductID })
                .ForeignKey("dbo.Wishlist", t => t.Wishlist_CustomerID, cascadeDelete: true)
                .ForeignKey("dbo.Product", t => t.Product_ProductID, cascadeDelete: true)
                .Index(t => t.Wishlist_CustomerID)
                .Index(t => t.Product_ProductID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.WishlistProduct", "Product_ProductID", "dbo.Product");
            DropForeignKey("dbo.WishlistProduct", "Wishlist_CustomerID", "dbo.Wishlist");
            DropForeignKey("dbo.Wishlist", "CustomerID", "dbo.AspNetUsers");
            DropIndex("dbo.WishlistProduct", new[] { "Product_ProductID" });
            DropIndex("dbo.WishlistProduct", new[] { "Wishlist_CustomerID" });
            DropIndex("dbo.Wishlist", new[] { "CustomerID" });
            DropTable("dbo.WishlistProduct");
            DropTable("dbo.Wishlist");
        }
    }
}
