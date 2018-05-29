namespace practicingbiteme2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeleteTelephoneNumber : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.AspNetUsers", "TelephoneNumber");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "TelephoneNumber", c => c.String());
        }
    }
}
