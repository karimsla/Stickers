
using Model;
using System.Data.Entity;


namespace DATA
{
   
        public class DatabContext : DbContext

        {
            public DatabContext() : base("Name=Stickers")
            {


            }
     

            public DbSet<Product> Products { get; set; }
            public DbSet<Admin> Admins { get; set; }
            public DbSet<Command> Commands { get; set; }
            public DbSet<Claim> Claims { get; set; }


    }

   
}
