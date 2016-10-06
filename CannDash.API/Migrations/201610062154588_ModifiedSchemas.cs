namespace CannDash.API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifiedSchemas : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ProductOrders", "TotalSale", c => c.Int(nullable: false));
            AddColumn("dbo.PickUps", "Inv_Each", c => c.Int());
            AddColumn("dbo.Inventories", "Inv_Each", c => c.Int());
            DropColumn("dbo.ProductOrders", "Total");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ProductOrders", "Total", c => c.Int(nullable: false));
            DropColumn("dbo.Inventories", "Inv_Each");
            DropColumn("dbo.PickUps", "Inv_Each");
            DropColumn("dbo.ProductOrders", "TotalSale");
        }
    }
}
