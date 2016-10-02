namespace CannDash.API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        CategoryId = c.Int(nullable: false, identity: true),
                        DispensaryId = c.Int(nullable: false),
                        CategoryName = c.String(),
                    })
                .PrimaryKey(t => t.CategoryId)
                .ForeignKey("dbo.Dispensaries", t => t.DispensaryId, cascadeDelete: true)
                .Index(t => t.DispensaryId);
            
            CreateTable(
                "dbo.Dispensaries",
                c => new
                    {
                        DispensaryId = c.Int(nullable: false, identity: true),
                        CompanyName = c.String(nullable: false),
                        Street = c.String(),
                        UnitNo = c.String(),
                        City = c.String(),
                        State = c.String(),
                        ZipCode = c.Int(nullable: false),
                        Email = c.String(),
                        Phone = c.String(),
                        Zone = c.String(),
                        StatePermit = c.String(),
                        PermitExpirationDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.DispensaryId);
            
            CreateTable(
                "dbo.Drivers",
                c => new
                    {
                        DriverId = c.Int(nullable: false, identity: true),
                        DispenaryId = c.Int(nullable: false),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        Phone = c.String(),
                        DriverPic = c.String(),
                        DriversLicense = c.String(),
                        LicensePlate = c.String(),
                        VehicleMake = c.String(),
                        VehicleModel = c.String(),
                        VehicleColor = c.String(),
                        VehicleInsurance = c.String(),
                        UnitsInRoute = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.DriverId)
                .ForeignKey("dbo.Dispensaries", t => t.DispenaryId, cascadeDelete: true)
                .Index(t => t.DispenaryId);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        OrderId = c.Int(nullable: false, identity: true),
                        DispensaryId = c.Int(nullable: false),
                        DriverId = c.Int(nullable: false),
                        DeliveryNotes = c.String(),
                        PickUp = c.Boolean(nullable: false),
                        Street = c.String(),
                        UnitNo = c.String(),
                        City = c.String(),
                        State = c.String(),
                        ZipCode = c.Int(nullable: false),
                        ItemQuantity = c.Int(nullable: false),
                        TotalCost = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.OrderId)
                .ForeignKey("dbo.Drivers", t => t.DriverId, cascadeDelete: true)
                .ForeignKey("dbo.Dispensaries", t => t.DispensaryId)
                .Index(t => t.DispensaryId)
                .Index(t => t.DriverId);
            
            CreateTable(
                "dbo.ProductOrders",
                c => new
                    {
                        OrderId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                        OrderQty = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.OrderId, t.ProductId })
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .ForeignKey("dbo.Orders", t => t.OrderId, cascadeDelete: true)
                .Index(t => t.OrderId)
                .Index(t => t.ProductId);
            
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
                        Price = c.Int(nullable: false),
                        UnitsOfMeasure = c.Int(nullable: false),
                        UnitsinStore = c.Int(nullable: false),
                        Discontinued = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ProductId)
                .ForeignKey("dbo.Categories", t => t.CategoryId)
                .Index(t => t.CategoryId);
            
            CreateTable(
                "dbo.PickUps",
                c => new
                    {
                        DriverId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                        OnHandQty = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.DriverId, t.ProductId })
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .ForeignKey("dbo.Drivers", t => t.DriverId, cascadeDelete: true)
                .Index(t => t.DriverId)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.Ratings",
                c => new
                    {
                        CustomerId = c.Int(nullable: false),
                        DispensaryId = c.Int(nullable: false),
                        Comments = c.String(),
                        DispensaryRating = c.String(),
                    })
                .PrimaryKey(t => new { t.CustomerId, t.DispensaryId })
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: true)
                .ForeignKey("dbo.Dispensaries", t => t.DispensaryId, cascadeDelete: true)
                .Index(t => t.CustomerId)
                .Index(t => t.DispensaryId);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        CustomerId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        Street = c.String(),
                        UnitNo = c.String(),
                        City = c.String(),
                        State = c.String(),
                        ZipCode = c.Int(nullable: false),
                        Email = c.String(),
                        Phone = c.String(),
                        Gender = c.String(),
                        DateOfBirth = c.DateTime(nullable: false),
                        Age = c.String(),
                        MedicalReason = c.String(),
                        DriversLicense = c.String(),
                        MmicId = c.String(),
                        MmicExpiration = c.DateTime(nullable: false),
                        DoctorLetter = c.String(),
                    })
                .PrimaryKey(t => t.CustomerId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        CustomerId = c.Int(nullable: false),
                        DispensaryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.CustomerId, t.DispensaryId })
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: true)
                .ForeignKey("dbo.Dispensaries", t => t.DispensaryId, cascadeDelete: true)
                .Index(t => t.CustomerId)
                .Index(t => t.DispensaryId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Products", "CategoryId", "dbo.Categories");
            DropForeignKey("dbo.Ratings", "DispensaryId", "dbo.Dispensaries");
            DropForeignKey("dbo.Users", "DispensaryId", "dbo.Dispensaries");
            DropForeignKey("dbo.Users", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.Ratings", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.Orders", "DispensaryId", "dbo.Dispensaries");
            DropForeignKey("dbo.Drivers", "DispenaryId", "dbo.Dispensaries");
            DropForeignKey("dbo.PickUps", "DriverId", "dbo.Drivers");
            DropForeignKey("dbo.Orders", "DriverId", "dbo.Drivers");
            DropForeignKey("dbo.ProductOrders", "OrderId", "dbo.Orders");
            DropForeignKey("dbo.ProductOrders", "ProductId", "dbo.Products");
            DropForeignKey("dbo.PickUps", "ProductId", "dbo.Products");
            DropForeignKey("dbo.Categories", "DispensaryId", "dbo.Dispensaries");
            DropIndex("dbo.Users", new[] { "DispensaryId" });
            DropIndex("dbo.Users", new[] { "CustomerId" });
            DropIndex("dbo.Ratings", new[] { "DispensaryId" });
            DropIndex("dbo.Ratings", new[] { "CustomerId" });
            DropIndex("dbo.PickUps", new[] { "ProductId" });
            DropIndex("dbo.PickUps", new[] { "DriverId" });
            DropIndex("dbo.Products", new[] { "CategoryId" });
            DropIndex("dbo.ProductOrders", new[] { "ProductId" });
            DropIndex("dbo.ProductOrders", new[] { "OrderId" });
            DropIndex("dbo.Orders", new[] { "DriverId" });
            DropIndex("dbo.Orders", new[] { "DispensaryId" });
            DropIndex("dbo.Drivers", new[] { "DispenaryId" });
            DropIndex("dbo.Categories", new[] { "DispensaryId" });
            DropTable("dbo.Users");
            DropTable("dbo.Customers");
            DropTable("dbo.Ratings");
            DropTable("dbo.PickUps");
            DropTable("dbo.Products");
            DropTable("dbo.ProductOrders");
            DropTable("dbo.Orders");
            DropTable("dbo.Drivers");
            DropTable("dbo.Dispensaries");
            DropTable("dbo.Categories");
        }
    }
}
