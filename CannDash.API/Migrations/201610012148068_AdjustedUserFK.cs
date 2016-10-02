namespace CannDash.API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AdjustedUserFK : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Users", "CustomerId");
            DropColumn("dbo.Users", "DispensaryId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "DispensaryId", c => c.Int());
            AddColumn("dbo.Users", "CustomerId", c => c.Int());
        }
    }
}
