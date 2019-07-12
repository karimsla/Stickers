namespace DATA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class modelannotation : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Commands", "phone2", c => c.String(maxLength: 8));
            AddColumn("dbo.Commands", "gov", c => c.String(nullable: false));
            AddColumn("dbo.Commands", "code", c => c.String(nullable: false, maxLength: 4));
            AlterColumn("dbo.Claims", "email", c => c.String(nullable: false));
            AlterColumn("dbo.Claims", "obj", c => c.String(nullable: false));
            AlterColumn("dbo.Claims", "body", c => c.String(nullable: false));
            AlterColumn("dbo.Claims", "name", c => c.String(nullable: false));
            AlterColumn("dbo.Commands", "name", c => c.String(nullable: false));
            AlterColumn("dbo.Commands", "phone", c => c.String(nullable: false, maxLength: 8));
            AlterColumn("dbo.Commands", "adresse", c => c.String(nullable: false));
            AlterColumn("dbo.Commands", "email", c => c.String(nullable: false));
            AlterColumn("dbo.Products", "nameprod", c => c.String(nullable: false));
            AlterColumn("dbo.Products", "description", c => c.String(nullable: false));
            AlterColumn("dbo.Products", "imgprod", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Products", "imgprod", c => c.String());
            AlterColumn("dbo.Products", "description", c => c.String());
            AlterColumn("dbo.Products", "nameprod", c => c.String());
            AlterColumn("dbo.Commands", "email", c => c.String());
            AlterColumn("dbo.Commands", "adresse", c => c.String());
            AlterColumn("dbo.Commands", "phone", c => c.String());
            AlterColumn("dbo.Commands", "name", c => c.String());
            AlterColumn("dbo.Claims", "name", c => c.String());
            AlterColumn("dbo.Claims", "body", c => c.String());
            AlterColumn("dbo.Claims", "obj", c => c.String());
            AlterColumn("dbo.Claims", "email", c => c.String());
            DropColumn("dbo.Commands", "code");
            DropColumn("dbo.Commands", "gov");
            DropColumn("dbo.Commands", "phone2");
        }
    }
}
