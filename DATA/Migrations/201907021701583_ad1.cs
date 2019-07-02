namespace DATA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ad1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Claims",
                c => new
                    {
                        idclaim = c.Int(nullable: false, identity: true),
                        email = c.String(),
                        obj = c.String(),
                        body = c.String(),
                        name = c.String(),
                        claimdate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.idclaim);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Claims");
        }
    }
}
