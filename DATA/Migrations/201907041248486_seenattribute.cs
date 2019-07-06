namespace DATA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class seenattribute : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Claims", "seen", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Claims", "seen");
        }
    }
}
