namespace CannDash.API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedCustomerDriverSeedData : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Drivers", name: "DispenaryId", newName: "DispensaryId");
            RenameIndex(table: "dbo.Drivers", name: "IX_DispenaryId", newName: "IX_DispensaryId");
            AddColumn("dbo.Drivers", "Email", c => c.String());
            AlterColumn("dbo.Customers", "Age", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Customers", "Age", c => c.String());
            DropColumn("dbo.Drivers", "Email");
            RenameIndex(table: "dbo.Drivers", name: "IX_DispensaryId", newName: "IX_DispenaryId");
            RenameColumn(table: "dbo.Drivers", name: "DispensaryId", newName: "DispenaryId");
        }
    }
}
