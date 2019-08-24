
namespace DATA.Migrations
{
    using Model;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Security.Cryptography;
    using System.Text;

    internal sealed class Configuration : DbMigrationsConfiguration<DATA.DatabContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;

        }

        protected override void Seed(DATA.DatabContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
            string password = "admin";

            SHA256 hash = new SHA256CryptoServiceProvider();
            Byte[] originalBytes = ASCIIEncoding.Default.GetBytes(password);
            Byte[] encodedBytes = hash.ComputeHash(originalBytes);
            password = BitConverter.ToString(encodedBytes);

            var Admins = new List<Admin>
                {


            new Admin{username="karim",email="admin@admin.com",password=password},

                };
            Admins.ForEach(e => context.Admins.AddOrUpdate(c => c.email, e));
            context.SaveChanges();
        }
    }
}