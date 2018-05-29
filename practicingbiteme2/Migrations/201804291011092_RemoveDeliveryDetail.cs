namespace practicingbiteme2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveDeliveryDetail : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.DeliveryDetail", "DeliveryAddressID", "dbo.DeliveryAddress");
            DropForeignKey("dbo.Order", "DeliveryDetailID", "dbo.DeliveryDetail");
            DropIndex("dbo.Order", new[] { "DeliveryDetailID" });
            DropIndex("dbo.DeliveryDetail", new[] { "DeliveryAddressID" });
            AddColumn("dbo.DeliveryAddress", "ReceiverName", c => c.String());
            AddColumn("dbo.Order", "DeliveryAddressID", c => c.Int(nullable: false));
            CreateIndex("dbo.Order", "DeliveryAddressID");
            AddForeignKey("dbo.Order", "DeliveryAddressID", "dbo.DeliveryAddress", "DeliveryAddressID", cascadeDelete: true);
            DropColumn("dbo.Order", "DeliveryDetailID");
            DropTable("dbo.DeliveryDetail");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.DeliveryDetail",
                c => new
                    {
                        DeliveryDetailID = c.Int(nullable: false, identity: true),
                        ReceiverName = c.String(),
                        DeliveryAddressID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.DeliveryDetailID);
            
            AddColumn("dbo.Order", "DeliveryDetailID", c => c.Int(nullable: false));
            DropForeignKey("dbo.Order", "DeliveryAddressID", "dbo.DeliveryAddress");
            DropIndex("dbo.Order", new[] { "DeliveryAddressID" });
            DropColumn("dbo.Order", "DeliveryAddressID");
            DropColumn("dbo.DeliveryAddress", "ReceiverName");
            CreateIndex("dbo.DeliveryDetail", "DeliveryAddressID");
            CreateIndex("dbo.Order", "DeliveryDetailID");
            AddForeignKey("dbo.Order", "DeliveryDetailID", "dbo.DeliveryDetail", "DeliveryDetailID", cascadeDelete: true);
            AddForeignKey("dbo.DeliveryDetail", "DeliveryAddressID", "dbo.DeliveryAddress", "DeliveryAddressID", cascadeDelete: true);
        }
    }
}
