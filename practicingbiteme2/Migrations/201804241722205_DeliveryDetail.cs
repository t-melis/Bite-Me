namespace practicingbiteme2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeliveryDetail : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DeliveryDetail",
                c => new
                    {
                        DeliveryDetailID = c.Int(nullable: false, identity: true),
                        ReceiverName = c.String(),
                        DeliveryAddressID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.DeliveryDetailID)
                .ForeignKey("dbo.DeliveryAddress", t => t.DeliveryAddressID, cascadeDelete: true)
                .Index(t => t.DeliveryAddressID);
            
            AddColumn("dbo.Order", "DeliveryDetailID", c => c.Int(nullable: false));
            CreateIndex("dbo.Order", "DeliveryDetailID");
            AddForeignKey("dbo.Order", "DeliveryDetailID", "dbo.DeliveryDetail", "DeliveryDetailID", cascadeDelete: true);
            DropColumn("dbo.Order", "Address");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Order", "Address", c => c.String());
            DropForeignKey("dbo.Order", "DeliveryDetailID", "dbo.DeliveryDetail");
            DropForeignKey("dbo.DeliveryDetail", "DeliveryAddressID", "dbo.DeliveryAddress");
            DropIndex("dbo.DeliveryDetail", new[] { "DeliveryAddressID" });
            DropIndex("dbo.Order", new[] { "DeliveryDetailID" });
            DropColumn("dbo.Order", "DeliveryDetailID");
            DropTable("dbo.DeliveryDetail");
        }
    }
}
