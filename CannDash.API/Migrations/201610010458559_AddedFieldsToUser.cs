namespace CannDash.API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedFieldsToUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "UserName", c => c.String());
            AddColumn("dbo.Users", "PassWord", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "PassWord");
            DropColumn("dbo.Users", "UserName");
        }
    }
}
