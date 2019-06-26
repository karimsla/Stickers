namespace DATA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mg1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Admins",
                c => new
                    {
                        idAdmin = c.Int(nullable: false, identity: true),
                        username = c.String(),
                        password = c.String(),
                        email = c.String(),
                    })
                .PrimaryKey(t => t.idAdmin);
            
            CreateTable(
                "dbo.Commands",
                c => new
                    {
                        idcmd = c.Int(nullable: false, identity: true),
                        idprod = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.idcmd)
                .ForeignKey("dbo.Products", t => t.idprod, cascadeDelete: true)
                .Index(t => t.idprod);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        idprod = c.Int(nullable: false, identity: true),
                        nameprod = c.String(),
                        qteprod = c.Int(nullable: false),
                        description = c.String(),
                        imgprod = c.String(),
                        Product_idprod = c.Int(),
                    })
                .PrimaryKey(t => t.idprod)
                .ForeignKey("dbo.Products", t => t.Product_idprod)
                .Index(t => t.Product_idprod);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Commands", "idprod", "dbo.Products");
            DropForeignKey("dbo.Products", "Product_idprod", "dbo.Products");
            DropIndex("dbo.Products", new[] { "Product_idprod" });
            DropIndex("dbo.Commands", new[] { "idprod" });
            DropTable("dbo.Products");
            DropTable("dbo.Commands");
            DropTable("dbo.Admins");
        }
    }
}
