namespace practicingbiteme2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProductLogicalDelete : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Product", "IsDeleted", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Product", "IsDeleted");
        }
    }
}
