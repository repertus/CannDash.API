namespace CannDash.API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedPickUpController : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Dispensaries", "WeedMapMenu", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Dispensaries", "WeedMapMenu");
        }
    }
}
