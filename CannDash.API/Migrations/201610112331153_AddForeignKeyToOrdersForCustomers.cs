namespace CannDash.API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddForeignKeyToOrdersForCustomers : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Orders", name: "Customer_CustomerId", newName: "CustomerId");
            RenameIndex(table: "dbo.Orders", name: "IX_Customer_CustomerId", newName: "IX_CustomerId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Orders", name: "IX_CustomerId", newName: "IX_Customer_CustomerId");
            RenameColumn(table: "dbo.Orders", name: "CustomerId", newName: "Customer_CustomerId");
        }
    }
}
