namespace CannDash.API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifiedOneToOne : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Ratings", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.Ratings", "DispensaryId", "dbo.Dispensaries");
            DropForeignKey("dbo.Categories", "DispensaryId", "dbo.Dispensaries");
            DropForeignKey("dbo.Products", "CategoryId", "dbo.Categories");
            DropForeignKey("dbo.Orders", "DispensaryId", "dbo.Dispensaries");
            DropForeignKey("dbo.Orders", "DriverId", "dbo.Drivers");
            DropForeignKey("dbo.Users", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.Users", "DispensaryId", "dbo.Dispensaries");
            DropForeignKey("dbo.Drivers", "DispenaryId", "dbo.Dispensaries");
            DropIndex("dbo.Categories", new[] { "DispensaryId" });
            DropIndex("dbo.Orders", new[] { "DriverId" });
            DropIndex("dbo.Ratings", new[] { "CustomerId" });
            DropIndex("dbo.Ratings", new[] { "DispensaryId" });
            DropIndex("dbo.Users", new[] { "CustomerId" });
            DropIndex("dbo.Users", new[] { "DispensaryId" });
            DropPrimaryKey("dbo.Dispensaries");
            DropPrimaryKey("dbo.Customers");
            DropPrimaryKey("dbo.Users");
            AddColumn("dbo.Orders", "Customer_CustomerId", c => c.Int());
            AddColumn("dbo.Customers", "DispensaryId", c => c.Int(nullable: false));
            AddColumn("dbo.Users", "UserId", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.Categories", "DispensaryId", c => c.Int());
            AlterColumn("dbo.Dispensaries", "DispensaryId", c => c.Int(nullable: false));
            AlterColumn("dbo.Dispensaries", "CompanyName", c => c.String());
            AlterColumn("dbo.Drivers", "FirstName", c => c.String());
            AlterColumn("dbo.Drivers", "LastName", c => c.String());
            AlterColumn("dbo.Orders", "DriverId", c => c.Int());
            AlterColumn("dbo.Customers", "CustomerId", c => c.Int(nullable: false));
            AlterColumn("dbo.Customers", "FirstName", c => c.String());
            AlterColumn("dbo.Customers", "LastName", c => c.String());
            AddPrimaryKey("dbo.Dispensaries", "DispensaryId");
            AddPrimaryKey("dbo.Customers", "CustomerId");
            AddPrimaryKey("dbo.Users", "UserId");
            CreateIndex("dbo.Categories", "DispensaryId");
            CreateIndex("dbo.Dispensaries", "DispensaryId");
            CreateIndex("dbo.Customers", "CustomerId");
            CreateIndex("dbo.Customers", "DispensaryId");
            CreateIndex("dbo.Orders", "DriverId");
            CreateIndex("dbo.Orders", "Customer_CustomerId");
            AddForeignKey("dbo.Orders", "Customer_CustomerId", "dbo.Customers", "CustomerId");
            AddForeignKey("dbo.Customers", "DispensaryId", "dbo.Dispensaries", "DispensaryId", cascadeDelete: true);
            AddForeignKey("dbo.Categories", "DispensaryId", "dbo.Dispensaries", "DispensaryId");
            AddForeignKey("dbo.Products", "CategoryId", "dbo.Categories", "CategoryId", cascadeDelete: true);
            AddForeignKey("dbo.Orders", "DispensaryId", "dbo.Dispensaries", "DispensaryId", cascadeDelete: true);
            AddForeignKey("dbo.Orders", "DriverId", "dbo.Drivers", "DriverId");
            AddForeignKey("dbo.Customers", "CustomerId", "dbo.Users", "UserId");
            AddForeignKey("dbo.Dispensaries", "DispensaryId", "dbo.Users", "UserId");
            AddForeignKey("dbo.Drivers", "DispenaryId", "dbo.Dispensaries", "DispensaryId", cascadeDelete: true);
            DropTable("dbo.Ratings");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Ratings",
                c => new
                    {
                        CustomerId = c.Int(nullable: false),
                        DispensaryId = c.Int(nullable: false),
                        Comments = c.String(),
                        DispensaryRating = c.String(),
                    })
                .PrimaryKey(t => new { t.CustomerId, t.DispensaryId });
            
            DropForeignKey("dbo.Drivers", "DispenaryId", "dbo.Dispensaries");
            DropForeignKey("dbo.Dispensaries", "DispensaryId", "dbo.Users");
            DropForeignKey("dbo.Customers", "CustomerId", "dbo.Users");
            DropForeignKey("dbo.Orders", "DriverId", "dbo.Drivers");
            DropForeignKey("dbo.Orders", "DispensaryId", "dbo.Dispensaries");
            DropForeignKey("dbo.Products", "CategoryId", "dbo.Categories");
            DropForeignKey("dbo.Categories", "DispensaryId", "dbo.Dispensaries");
            DropForeignKey("dbo.Customers", "DispensaryId", "dbo.Dispensaries");
            DropForeignKey("dbo.Orders", "Customer_CustomerId", "dbo.Customers");
            DropIndex("dbo.Orders", new[] { "Customer_CustomerId" });
            DropIndex("dbo.Orders", new[] { "DriverId" });
            DropIndex("dbo.Customers", new[] { "DispensaryId" });
            DropIndex("dbo.Customers", new[] { "CustomerId" });
            DropIndex("dbo.Dispensaries", new[] { "DispensaryId" });
            DropIndex("dbo.Categories", new[] { "DispensaryId" });
            DropPrimaryKey("dbo.Users");
            DropPrimaryKey("dbo.Customers");
            DropPrimaryKey("dbo.Dispensaries");
            AlterColumn("dbo.Customers", "LastName", c => c.String(nullable: false));
            AlterColumn("dbo.Customers", "FirstName", c => c.String(nullable: false));
            AlterColumn("dbo.Customers", "CustomerId", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.Orders", "DriverId", c => c.Int(nullable: false));
            AlterColumn("dbo.Drivers", "LastName", c => c.String(nullable: false));
            AlterColumn("dbo.Drivers", "FirstName", c => c.String(nullable: false));
            AlterColumn("dbo.Dispensaries", "CompanyName", c => c.String(nullable: false));
            AlterColumn("dbo.Dispensaries", "DispensaryId", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.Categories", "DispensaryId", c => c.Int(nullable: false));
            DropColumn("dbo.Users", "UserId");
            DropColumn("dbo.Customers", "DispensaryId");
            DropColumn("dbo.Orders", "Customer_CustomerId");
            AddPrimaryKey("dbo.Users", new[] { "CustomerId", "DispensaryId" });
            AddPrimaryKey("dbo.Customers", "CustomerId");
            AddPrimaryKey("dbo.Dispensaries", "DispensaryId");
            CreateIndex("dbo.Users", "DispensaryId");
            CreateIndex("dbo.Users", "CustomerId");
            CreateIndex("dbo.Ratings", "DispensaryId");
            CreateIndex("dbo.Ratings", "CustomerId");
            CreateIndex("dbo.Orders", "DriverId");
            CreateIndex("dbo.Categories", "DispensaryId");
            AddForeignKey("dbo.Drivers", "DispenaryId", "dbo.Dispensaries", "DispensaryId", cascadeDelete: true);
            AddForeignKey("dbo.Users", "DispensaryId", "dbo.Dispensaries", "DispensaryId", cascadeDelete: true);
            AddForeignKey("dbo.Users", "CustomerId", "dbo.Customers", "CustomerId", cascadeDelete: true);
            AddForeignKey("dbo.Orders", "DriverId", "dbo.Drivers", "DriverId", cascadeDelete: true);
            AddForeignKey("dbo.Orders", "DispensaryId", "dbo.Dispensaries", "DispensaryId");
            AddForeignKey("dbo.Products", "CategoryId", "dbo.Categories", "CategoryId");
            AddForeignKey("dbo.Categories", "DispensaryId", "dbo.Dispensaries", "DispensaryId", cascadeDelete: true);
            AddForeignKey("dbo.Ratings", "DispensaryId", "dbo.Dispensaries", "DispensaryId", cascadeDelete: true);
            AddForeignKey("dbo.Ratings", "CustomerId", "dbo.Customers", "CustomerId", cascadeDelete: true);
        }
    }
}
