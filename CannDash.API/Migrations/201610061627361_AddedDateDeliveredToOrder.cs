namespace CannDash.API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedDateDeliveredToOrder : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "OrderDate", c => c.DateTime());
            AddColumn("dbo.Orders", "OrderDelivered", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "OrderDelivered");
            DropColumn("dbo.Orders", "OrderDate");
        }
    }
}
