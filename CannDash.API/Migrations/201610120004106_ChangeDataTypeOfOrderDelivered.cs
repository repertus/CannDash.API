namespace CannDash.API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeDataTypeOfOrderDelivered : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "OrderStatus", c => c.Int(nullable: false));
            DropColumn("dbo.Orders", "OrderDelivered");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Orders", "OrderDelivered", c => c.Boolean(nullable: false));
            DropColumn("dbo.Orders", "OrderStatus");
        }
    }
}
