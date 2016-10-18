namespace CannDash.API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifiedDriverSchemaRemovedUnitInRoute : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Drivers", "UnitsInRoute");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Drivers", "UnitsInRoute", c => c.Int());
        }
    }
}
