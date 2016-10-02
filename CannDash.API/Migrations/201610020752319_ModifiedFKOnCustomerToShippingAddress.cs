namespace CannDash.API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifiedFKOnCustomerToShippingAddress : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Inventories",
                c => new
                    {
                        InventoryId = c.Int(nullable: false),
                        Inv_Gram = c.Int(),
                        Inv_TwoGrams = c.Int(),
                        Inv_Eigth = c.Int(),
                        Inv_Quarter = c.Int(),
                        Inv_HalfOnce = c.Int(),
                        Inv_Ounce = c.Int(),
                    })
                .PrimaryKey(t => t.InventoryId)
                .ForeignKey("dbo.Products", t => t.InventoryId)
                .Index(t => t.InventoryId);
            
            CreateTable(
                "dbo.Prices",
                c => new
                    {
                        PriceId = c.Int(nullable: false),
                        Price_Gram = c.Int(),
                        Price_TwoGrams = c.Int(),
                        Price_Eigth = c.Int(),
                        Price_Quarter = c.Int(),
                        Price_HalfOnce = c.Int(),
                        Price_Ounce = c.Int(),
                    })
                .PrimaryKey(t => t.PriceId)
                .ForeignKey("dbo.Products", t => t.PriceId)
                .Index(t => t.PriceId);
            
            CreateTable(
                "dbo.ShippingAddresses",
                c => new
                    {
                        ShippingAddressId = c.Int(nullable: false, identity: true),
                        CustomerId = c.Int(),
                        Street = c.String(),
                        UnitNo = c.String(),
                        City = c.String(),
                        State = c.String(),
                        ZipCode = c.Int(),
                    })
                .PrimaryKey(t => t.ShippingAddressId)
                .ForeignKey("dbo.Customers", t => t.CustomerId)
                .Index(t => t.CustomerId);
            
            AddColumn("dbo.Orders", "ShippingAddressId", c => c.Int(nullable: false));
            AddColumn("dbo.PickUps", "Inv_Gram", c => c.Int());
            AddColumn("dbo.PickUps", "Inv_TwoGrams", c => c.Int());
            AddColumn("dbo.PickUps", "Inv_Eigth", c => c.Int());
            AddColumn("dbo.PickUps", "Inv_Quarter", c => c.Int());
            AddColumn("dbo.PickUps", "Inv_HalfOnce", c => c.Int());
            AddColumn("dbo.PickUps", "Inv_Ounce", c => c.Int());
            AddColumn("dbo.ProductOrders", "UnitPrice", c => c.Int());
            AddColumn("dbo.ProductOrders", "Discount", c => c.Int(nullable: false));
            AddColumn("dbo.ProductOrders", "Total", c => c.Int(nullable: false));
            CreateIndex("dbo.Orders", "ShippingAddressId");
            AddForeignKey("dbo.Orders", "ShippingAddressId", "dbo.ShippingAddresses", "ShippingAddressId", cascadeDelete: true);
            DropColumn("dbo.PickUps", "OnHandQty");
            DropColumn("dbo.Products", "Price");
            DropColumn("dbo.Products", "UnitsinStore");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Products", "UnitsinStore", c => c.Int());
            AddColumn("dbo.Products", "Price", c => c.Int());
            AddColumn("dbo.PickUps", "OnHandQty", c => c.Int());
            DropForeignKey("dbo.ShippingAddresses", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.Orders", "ShippingAddressId", "dbo.ShippingAddresses");
            DropForeignKey("dbo.Prices", "PriceId", "dbo.Products");
            DropForeignKey("dbo.Inventories", "InventoryId", "dbo.Products");
            DropIndex("dbo.ShippingAddresses", new[] { "CustomerId" });
            DropIndex("dbo.Prices", new[] { "PriceId" });
            DropIndex("dbo.Inventories", new[] { "InventoryId" });
            DropIndex("dbo.Orders", new[] { "ShippingAddressId" });
            DropColumn("dbo.ProductOrders", "Total");
            DropColumn("dbo.ProductOrders", "Discount");
            DropColumn("dbo.ProductOrders", "UnitPrice");
            DropColumn("dbo.PickUps", "Inv_Ounce");
            DropColumn("dbo.PickUps", "Inv_HalfOnce");
            DropColumn("dbo.PickUps", "Inv_Quarter");
            DropColumn("dbo.PickUps", "Inv_Eigth");
            DropColumn("dbo.PickUps", "Inv_TwoGrams");
            DropColumn("dbo.PickUps", "Inv_Gram");
            DropColumn("dbo.Orders", "ShippingAddressId");
            DropTable("dbo.ShippingAddresses");
            DropTable("dbo.Prices");
            DropTable("dbo.Inventories");
        }
    }
}
