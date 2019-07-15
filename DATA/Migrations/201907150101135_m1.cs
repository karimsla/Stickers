namespace DATA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "img1", c => c.String());
            AddColumn("dbo.Products", "img2", c => c.String());
            AddColumn("dbo.Products", "img3", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "img3");
            DropColumn("dbo.Products", "img2");
            DropColumn("dbo.Products", "img1");
        }
    }
}
