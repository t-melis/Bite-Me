namespace practicingbiteme2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixOrderUserRelationship : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Order", new[] { "CustomerID" });
            AlterColumn("dbo.Order", "CustomerID", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.Order", "CustomerID");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Order", new[] { "CustomerID" });
            AlterColumn("dbo.Order", "CustomerID", c => c.String(maxLength: 128));
            CreateIndex("dbo.Order", "CustomerID");
        }
    }
}
