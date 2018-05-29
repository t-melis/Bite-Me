namespace practicingbiteme2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DataAnnotations : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AspNetUsers", "FirstName", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.AspNetUsers", "LastName", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.DeliveryAddress", "City", c => c.String(nullable: false));
            AlterColumn("dbo.DeliveryAddress", "Street", c => c.String(nullable: false));
            AlterColumn("dbo.DeliveryAddress", "PostCode", c => c.String(nullable: false));
            AlterColumn("dbo.Product", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Product", "Price", c => c.Decimal(nullable: false, storeType: "money"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Product", "Price", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Product", "Name", c => c.String());
            AlterColumn("dbo.DeliveryAddress", "PostCode", c => c.String());
            AlterColumn("dbo.DeliveryAddress", "Street", c => c.String());
            AlterColumn("dbo.DeliveryAddress", "City", c => c.String());
            AlterColumn("dbo.AspNetUsers", "LastName", c => c.String());
            AlterColumn("dbo.AspNetUsers", "FirstName", c => c.String());
        }
    }
}
