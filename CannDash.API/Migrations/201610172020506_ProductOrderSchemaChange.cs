namespace CannDash.API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProductOrderSchemaChange : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ProductOrders", "Discount", c => c.Int());
            AlterColumn("dbo.ProductOrders", "ItemSale", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ProductOrders", "ItemSale", c => c.Int(nullable: false));
            AlterColumn("dbo.ProductOrders", "Discount", c => c.Int(nullable: false));
        }
    }
}
