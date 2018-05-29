namespace practicingbiteme2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IsCartCreation : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Order", "IsCart", c => c.Boolean(nullable: false));
            AddColumn("dbo.Order", "OrderDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Order", "OrderDate");
            DropColumn("dbo.Order", "IsCart");
        }
    }
}
