namespace practicingbiteme2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddressChangeModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DeliveryAddress",
                c => new
                    {
                        DeliveryAddressID = c.Int(nullable: false, identity: true),
                        City = c.String(),
                        Street = c.String(),
                        Number = c.String(),
                        PostCode = c.String(),
                        UserID = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.DeliveryAddressID)
                .ForeignKey("dbo.AspNetUsers", t => t.UserID)
                .Index(t => t.UserID);
            
            DropColumn("dbo.AspNetUsers", "Address");
            DropColumn("dbo.Order", "IsCart");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Order", "IsCart", c => c.Boolean(nullable: false));
            AddColumn("dbo.AspNetUsers", "Address", c => c.String());
            DropForeignKey("dbo.DeliveryAddress", "UserID", "dbo.AspNetUsers");
            DropIndex("dbo.DeliveryAddress", new[] { "UserID" });
            DropTable("dbo.DeliveryAddress");
        }
    }
}
