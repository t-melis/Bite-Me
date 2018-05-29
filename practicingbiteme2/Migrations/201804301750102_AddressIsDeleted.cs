namespace practicingbiteme2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddressIsDeleted : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DeliveryAddress", "IsDeleted", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.DeliveryAddress", "IsDeleted");
        }
    }
}
