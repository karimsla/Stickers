namespace DATA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class phamas : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Products", "Product_idprod", "dbo.Products");
            DropIndex("dbo.Products", new[] { "Product_idprod" });
            AddColumn("dbo.Commands", "isComfirmed", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Commands", "dateliv", c => c.DateTime());
            DropColumn("dbo.Products", "Product_idprod");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Products", "Product_idprod", c => c.Int());
            AlterColumn("dbo.Commands", "dateliv", c => c.DateTime(nullable: false));
            DropColumn("dbo.Commands", "isComfirmed");
            CreateIndex("dbo.Products", "Product_idprod");
            AddForeignKey("dbo.Products", "Product_idprod", "dbo.Products", "idprod");
        }
    }
}
