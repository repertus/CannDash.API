namespace CannDash.API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangesToUserPtoD : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Dispensaries", "DispensaryId", "dbo.Users");
            DropForeignKey("dbo.Customers", "CustomerId", "dbo.Users");
            DropForeignKey("dbo.Categories", "DispensaryId", "dbo.Dispensaries");
            DropForeignKey("dbo.Customers", "DispensaryId", "dbo.Dispensaries");
            DropForeignKey("dbo.Drivers", "DispenaryId", "dbo.Dispensaries");
            DropForeignKey("dbo.Orders", "DispensaryId", "dbo.Dispensaries");
            DropForeignKey("dbo.Orders", "Customer_CustomerId", "dbo.Customers");
            DropIndex("dbo.Dispensaries", new[] { "DispensaryId" });
            DropIndex("dbo.Customers", new[] { "CustomerId" });
            DropPrimaryKey("dbo.Dispensaries");
            DropPrimaryKey("dbo.Customers");
            AddColumn("dbo.Dispensaries", "User_UserId", c => c.Int());
            AddColumn("dbo.Customers", "User_UserId", c => c.Int());
            AlterColumn("dbo.Dispensaries", "DispensaryId", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.Customers", "CustomerId", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Dispensaries", "DispensaryId");
            AddPrimaryKey("dbo.Customers", "CustomerId");
            CreateIndex("dbo.Dispensaries", "User_UserId");
            CreateIndex("dbo.Customers", "User_UserId");
            AddForeignKey("dbo.Dispensaries", "User_UserId", "dbo.Users", "UserId");
            AddForeignKey("dbo.Customers", "User_UserId", "dbo.Users", "UserId");
            AddForeignKey("dbo.Categories", "DispensaryId", "dbo.Dispensaries", "DispensaryId");
            AddForeignKey("dbo.Customers", "DispensaryId", "dbo.Dispensaries", "DispensaryId", cascadeDelete: true);
            AddForeignKey("dbo.Drivers", "DispenaryId", "dbo.Dispensaries", "DispensaryId", cascadeDelete: true);
            AddForeignKey("dbo.Orders", "DispensaryId", "dbo.Dispensaries", "DispensaryId", cascadeDelete: true);
            AddForeignKey("dbo.Orders", "Customer_CustomerId", "dbo.Customers", "CustomerId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "Customer_CustomerId", "dbo.Customers");
            DropForeignKey("dbo.Orders", "DispensaryId", "dbo.Dispensaries");
            DropForeignKey("dbo.Drivers", "DispenaryId", "dbo.Dispensaries");
            DropForeignKey("dbo.Customers", "DispensaryId", "dbo.Dispensaries");
            DropForeignKey("dbo.Categories", "DispensaryId", "dbo.Dispensaries");
            DropForeignKey("dbo.Customers", "User_UserId", "dbo.Users");
            DropForeignKey("dbo.Dispensaries", "User_UserId", "dbo.Users");
            DropIndex("dbo.Customers", new[] { "User_UserId" });
            DropIndex("dbo.Dispensaries", new[] { "User_UserId" });
            DropPrimaryKey("dbo.Customers");
            DropPrimaryKey("dbo.Dispensaries");
            AlterColumn("dbo.Customers", "CustomerId", c => c.Int(nullable: false));
            AlterColumn("dbo.Dispensaries", "DispensaryId", c => c.Int(nullable: false));
            DropColumn("dbo.Customers", "User_UserId");
            DropColumn("dbo.Dispensaries", "User_UserId");
            AddPrimaryKey("dbo.Customers", "CustomerId");
            AddPrimaryKey("dbo.Dispensaries", "DispensaryId");
            CreateIndex("dbo.Customers", "CustomerId");
            CreateIndex("dbo.Dispensaries", "DispensaryId");
            AddForeignKey("dbo.Orders", "Customer_CustomerId", "dbo.Customers", "CustomerId");
            AddForeignKey("dbo.Orders", "DispensaryId", "dbo.Dispensaries", "DispensaryId", cascadeDelete: true);
            AddForeignKey("dbo.Drivers", "DispenaryId", "dbo.Dispensaries", "DispensaryId", cascadeDelete: true);
            AddForeignKey("dbo.Customers", "DispensaryId", "dbo.Dispensaries", "DispensaryId", cascadeDelete: true);
            AddForeignKey("dbo.Categories", "DispensaryId", "dbo.Dispensaries", "DispensaryId");
            AddForeignKey("dbo.Customers", "CustomerId", "dbo.Users", "UserId");
            AddForeignKey("dbo.Dispensaries", "DispensaryId", "dbo.Users", "UserId");
        }
    }
}
