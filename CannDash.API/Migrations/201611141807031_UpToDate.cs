namespace CannDash.API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpToDate : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Customers", "CustomerAddressId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Customers", "CustomerAddressId", c => c.Int(nullable: false));
        }
    }
}
