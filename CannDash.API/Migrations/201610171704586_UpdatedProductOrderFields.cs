namespace CannDash.API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedProductOrderFields : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "TotalOrderSale", c => c.Int(nullable: false));
            AddColumn("dbo.ProductOrders", "MenuCategoryId", c => c.Int());
            AddColumn("dbo.ProductOrders", "CategoryName", c => c.String());
            AddColumn("dbo.ProductOrders", "ProductName", c => c.String());
            AddColumn("dbo.ProductOrders", "ItemSale", c => c.Int(nullable: false));
            DropColumn("dbo.Orders", "MenuCategoryId");
            DropColumn("dbo.Orders", "CategoryName");
            DropColumn("dbo.Orders", "ProductId");
            DropColumn("dbo.Orders", "ProductName");
            DropColumn("dbo.Orders", "ItemQuantity");
            DropColumn("dbo.Orders", "TotalCost");
            DropColumn("dbo.ProductOrders", "TotalSale");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ProductOrders", "TotalSale", c => c.Int(nullable: false));
            AddColumn("dbo.Orders", "TotalCost", c => c.Int(nullable: false));
            AddColumn("dbo.Orders", "ItemQuantity", c => c.Int(nullable: false));
            AddColumn("dbo.Orders", "ProductName", c => c.String());
            AddColumn("dbo.Orders", "ProductId", c => c.Int());
            AddColumn("dbo.Orders", "CategoryName", c => c.String());
            AddColumn("dbo.Orders", "MenuCategoryId", c => c.Int());
            DropColumn("dbo.ProductOrders", "ItemSale");
            DropColumn("dbo.ProductOrders", "ProductName");
            DropColumn("dbo.ProductOrders", "CategoryName");
            DropColumn("dbo.ProductOrders", "MenuCategoryId");
            DropColumn("dbo.Orders", "TotalOrderSale");
        }
    }
}
