namespace CannDash.API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovedShippingAddress : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Orders", "ShippingAddressId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Orders", "ShippingAddressId", c => c.Int());
        }
    }
}
