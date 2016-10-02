namespace CannDash.API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedNullableChanges : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Dispensaries", "PermitExpirationDate", c => c.DateTime());
            AlterColumn("dbo.Customers", "MmicExpiration", c => c.DateTime());
            AlterColumn("dbo.Orders", "PickUp", c => c.Boolean());
            AlterColumn("dbo.Drivers", "UnitsInRoute", c => c.Int());
            AlterColumn("dbo.PickUps", "OnHandQty", c => c.Int());
            AlterColumn("dbo.Products", "Price", c => c.Int());
            AlterColumn("dbo.Products", "UnitsOfMeasure", c => c.Int());
            AlterColumn("dbo.Products", "UnitsinStore", c => c.Int());
            AlterColumn("dbo.Products", "Discontinued", c => c.Boolean());
            AlterColumn("dbo.ProductOrders", "OrderQty", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ProductOrders", "OrderQty", c => c.Int(nullable: false));
            AlterColumn("dbo.Products", "Discontinued", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Products", "UnitsinStore", c => c.Int(nullable: false));
            AlterColumn("dbo.Products", "UnitsOfMeasure", c => c.Int(nullable: false));
            AlterColumn("dbo.Products", "Price", c => c.Int(nullable: false));
            AlterColumn("dbo.PickUps", "OnHandQty", c => c.Int(nullable: false));
            AlterColumn("dbo.Drivers", "UnitsInRoute", c => c.Int(nullable: false));
            AlterColumn("dbo.Orders", "PickUp", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Customers", "MmicExpiration", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Dispensaries", "PermitExpirationDate", c => c.DateTime(nullable: false));
        }
    }
}
