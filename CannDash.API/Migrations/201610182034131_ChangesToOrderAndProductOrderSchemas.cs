namespace CannDash.API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangesToOrderAndProductOrderSchemas : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "itemQuantity", c => c.Int(nullable: false));
            AddColumn("dbo.ProductOrders", "TotalSale", c => c.Int());
            DropColumn("dbo.ProductOrders", "ItemSale");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ProductOrders", "ItemSale", c => c.Int());
            DropColumn("dbo.ProductOrders", "TotalSale");
            DropColumn("dbo.Orders", "itemQuantity");
        }
    }
}
