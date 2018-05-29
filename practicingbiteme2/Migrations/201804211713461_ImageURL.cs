namespace practicingbiteme2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ImageURL : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Product", "ImageURL", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Product", "ImageURL");
        }
    }
}
