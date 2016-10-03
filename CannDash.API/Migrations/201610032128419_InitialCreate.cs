namespace CannDash.API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        CategoryId = c.Int(nullable: false, identity: true),
                        DispensaryId = c.Int(),
                        CategoryName = c.String(),
                    })
                .PrimaryKey(t => t.CategoryId);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        ProductId = c.Int(nullable: false, identity: true),
                        CategoryId = c.Int(nullable: false),
                        ProductName = c.String(),
                        ProductDescription = c.String(),
                        ProductPic = c.String(),
                        ProductType = c.String(),
                        UnitsOfMeasure = c.Int(),
                        Discontinued = c.Boolean(),
                    })
                .PrimaryKey(t => t.ProductId)
                .ForeignKey("dbo.Categories", t => t.CategoryId, cascadeDelete: true)
                .Index(t => t.CategoryId);
            
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
                "dbo.Customers",
                c => new
                    {
                        CustomerId = c.Int(nullable: false, identity: true),
                        DispensaryId = c.Int(nullable: false),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Street = c.String(),
                        UnitNo = c.String(),
                        City = c.String(),
                        State = c.String(),
                        ZipCode = c.Int(nullable: false),
                        Email = c.String(),
                        Phone = c.String(),
                        Gender = c.String(),
                        DateOfBirth = c.DateTime(nullable: false),
                        Age = c.Int(),
                        MedicalReason = c.String(),
                        DriversLicense = c.String(),
                        MmicId = c.String(),
                        MmicExpiration = c.DateTime(),
                        DoctorLetter = c.String(),
                        User_UserId = c.Int(),
                    })
                .PrimaryKey(t => t.CustomerId)
                .ForeignKey("dbo.Dispensaries", t => t.DispensaryId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.User_UserId)
                .Index(t => t.DispensaryId)
                .Index(t => t.User_UserId);
            
            CreateTable(
                "dbo.Dispensaries",
                c => new
                    {
                        DispensaryId = c.Int(nullable: false, identity: true),
                        CompanyName = c.String(),
                        WeedMapMenu = c.String(),
                        Street = c.String(),
                        UnitNo = c.String(),
                        City = c.String(),
                        State = c.String(),
                        ZipCode = c.Int(nullable: false),
                        Email = c.String(),
                        Phone = c.String(),
                        Zone = c.String(),
                        StatePermit = c.String(),
                        PermitExpirationDate = c.DateTime(),
                        User_UserId = c.Int(),
                    })
                .PrimaryKey(t => t.DispensaryId)
                .ForeignKey("dbo.Users", t => t.User_UserId)
                .Index(t => t.User_UserId);
            
            CreateTable(
                "dbo.Drivers",
                c => new
                    {
                        DriverId = c.Int(nullable: false, identity: true),
                        DispensaryId = c.Int(nullable: false),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Phone = c.String(),
                        Email = c.String(),
                        DriverPic = c.String(),
                        DriversLicense = c.String(),
                        LicensePlate = c.String(),
                        VehicleMake = c.String(),
                        VehicleModel = c.String(),
                        VehicleColor = c.String(),
                        VehicleInsurance = c.String(),
                        UnitsInRoute = c.Int(),
                    })
                .PrimaryKey(t => t.DriverId)
                .ForeignKey("dbo.Dispensaries", t => t.DispensaryId, cascadeDelete: true)
                .Index(t => t.DispensaryId);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        OrderId = c.Int(nullable: false, identity: true),
                        DispensaryId = c.Int(nullable: false),
                        DriverId = c.Int(),
                        DeliveryNotes = c.String(),
                        PickUp = c.Boolean(),
                        Street = c.String(),
                        UnitNo = c.String(),
                        City = c.String(),
                        State = c.String(),
                        ZipCode = c.Int(nullable: false),
                        ItemQuantity = c.Int(nullable: false),
                        TotalCost = c.Int(nullable: false),
                        Customer_CustomerId = c.Int(),
                    })
                .PrimaryKey(t => t.OrderId)
                .ForeignKey("dbo.Customers", t => t.Customer_CustomerId)
                .ForeignKey("dbo.Drivers", t => t.DriverId)
                .ForeignKey("dbo.Dispensaries", t => t.DispensaryId, cascadeDelete: true)
                .Index(t => t.DispensaryId)
                .Index(t => t.DriverId)
                .Index(t => t.Customer_CustomerId);
            
            CreateTable(
                "dbo.ProductOrders",
                c => new
                    {
                        ProductOrderId = c.Int(nullable: false, identity: true),
                        OrderId = c.Int(nullable: false),
                        ProductId = c.Int(),
                        OrderQty = c.Int(),
                        UnitPrice = c.Int(),
                        Discount = c.Int(nullable: false),
                        Total = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ProductOrderId)
                .ForeignKey("dbo.Orders", t => t.OrderId, cascadeDelete: true)
                .Index(t => t.OrderId);
            
            CreateTable(
                "dbo.PickUps",
                c => new
                    {
                        DriverId = c.Int(nullable: false),
                        InventoryId = c.Int(nullable: false),
                        ProductId = c.Int(),
                        Inv_Gram = c.Int(),
                        Inv_TwoGrams = c.Int(),
                        Inv_Eigth = c.Int(),
                        Inv_Quarter = c.Int(),
                        Inv_HalfOnce = c.Int(),
                        Inv_Ounce = c.Int(),
                    })
                .PrimaryKey(t => new { t.DriverId, t.InventoryId })
                .ForeignKey("dbo.Inventories", t => t.InventoryId, cascadeDelete: true)
                .ForeignKey("dbo.Drivers", t => t.DriverId, cascadeDelete: true)
                .Index(t => t.DriverId)
                .Index(t => t.InventoryId);
            
            CreateTable(
                "dbo.Inventories",
                c => new
                    {
                        InventoryId = c.Int(nullable: false, identity: true),
                        DispensaryId = c.Int(),
                        Mobile = c.Boolean(nullable: false),
                        Inv_Gram = c.Int(),
                        Inv_TwoGrams = c.Int(),
                        Inv_Eigth = c.Int(),
                        Inv_Quarter = c.Int(),
                        Inv_HalfOnce = c.Int(),
                        Inv_Ounce = c.Int(),
                    })
                .PrimaryKey(t => t.InventoryId)
                .ForeignKey("dbo.Dispensaries", t => t.DispensaryId)
                .Index(t => t.DispensaryId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                        PassWord = c.String(),
                    })
                .PrimaryKey(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Customers", "User_UserId", "dbo.Users");
            DropForeignKey("dbo.Dispensaries", "User_UserId", "dbo.Users");
            DropForeignKey("dbo.Orders", "DispensaryId", "dbo.Dispensaries");
            DropForeignKey("dbo.Inventories", "DispensaryId", "dbo.Dispensaries");
            DropForeignKey("dbo.Drivers", "DispensaryId", "dbo.Dispensaries");
            DropForeignKey("dbo.PickUps", "DriverId", "dbo.Drivers");
            DropForeignKey("dbo.PickUps", "InventoryId", "dbo.Inventories");
            DropForeignKey("dbo.Orders", "DriverId", "dbo.Drivers");
            DropForeignKey("dbo.ProductOrders", "OrderId", "dbo.Orders");
            DropForeignKey("dbo.Orders", "Customer_CustomerId", "dbo.Customers");
            DropForeignKey("dbo.Customers", "DispensaryId", "dbo.Dispensaries");
            DropForeignKey("dbo.Products", "CategoryId", "dbo.Categories");
            DropForeignKey("dbo.Prices", "PriceId", "dbo.Products");
            DropIndex("dbo.Inventories", new[] { "DispensaryId" });
            DropIndex("dbo.PickUps", new[] { "InventoryId" });
            DropIndex("dbo.PickUps", new[] { "DriverId" });
            DropIndex("dbo.ProductOrders", new[] { "OrderId" });
            DropIndex("dbo.Orders", new[] { "Customer_CustomerId" });
            DropIndex("dbo.Orders", new[] { "DriverId" });
            DropIndex("dbo.Orders", new[] { "DispensaryId" });
            DropIndex("dbo.Drivers", new[] { "DispensaryId" });
            DropIndex("dbo.Dispensaries", new[] { "User_UserId" });
            DropIndex("dbo.Customers", new[] { "User_UserId" });
            DropIndex("dbo.Customers", new[] { "DispensaryId" });
            DropIndex("dbo.Prices", new[] { "PriceId" });
            DropIndex("dbo.Products", new[] { "CategoryId" });
            DropTable("dbo.Users");
            DropTable("dbo.Inventories");
            DropTable("dbo.PickUps");
            DropTable("dbo.ProductOrders");
            DropTable("dbo.Orders");
            DropTable("dbo.Drivers");
            DropTable("dbo.Dispensaries");
            DropTable("dbo.Customers");
            DropTable("dbo.Prices");
            DropTable("dbo.Products");
            DropTable("dbo.Categories");
        }
    }
}
