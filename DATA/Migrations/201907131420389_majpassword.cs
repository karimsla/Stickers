namespace DATA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class majpassword : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Admins", "ConfirmPassword", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Admins", "ConfirmPassword");
        }
    }
}
