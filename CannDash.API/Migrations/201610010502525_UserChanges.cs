namespace CannDash.API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserChanges : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Users", "CustomerId", c => c.Int());
            AlterColumn("dbo.Users", "DispensaryId", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Users", "DispensaryId", c => c.Int(nullable: false));
            AlterColumn("dbo.Users", "CustomerId", c => c.Int(nullable: false));
        }
    }
}
