namespace DATA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ak47 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Commands", "datecmd", c => c.DateTime(nullable: false));
            AddColumn("dbo.Commands", "dateliv", c => c.DateTime(nullable: false));
            AddColumn("dbo.Commands", "qteprod", c => c.Int(nullable: false));
            AddColumn("dbo.Commands", "name", c => c.String());
            AddColumn("dbo.Commands", "phone", c => c.String());
            AddColumn("dbo.Commands", "adresse", c => c.String());
            AddColumn("dbo.Commands", "email", c => c.String());
            AddColumn("dbo.Products", "price", c => c.Single(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "price");
            DropColumn("dbo.Commands", "email");
            DropColumn("dbo.Commands", "adresse");
            DropColumn("dbo.Commands", "phone");
            DropColumn("dbo.Commands", "name");
            DropColumn("dbo.Commands", "qteprod");
            DropColumn("dbo.Commands", "dateliv");
            DropColumn("dbo.Commands", "datecmd");
        }
    }
}
