
using DATA;

using Model;

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Text;
using System.Web.Security;


namespace Services.serviceAdmin
{
    public class serviceAdmin : IserviceAdmin

    {
     
        public serviceAdmin() 
        {

        }

       

        public bool authAdmin(string username, string password)
        {
            SHA256 hash = new SHA256CryptoServiceProvider();
            Byte[] originalBytes = ASCIIEncoding.Default.GetBytes(password);
            Byte[] encodedBytes = hash.ComputeHash(originalBytes);
            password = BitConverter.ToString(encodedBytes);
            Admin _ad = this.Get(x => x.username == username && x.password == password);

            if (_ad != null)
            {
                return true;
            }
            else
            {
                return false;
            }




        }

       

        public void modifyAccount(Admin _admin)
        {
            Admin ad = this.GetById(_admin.idAdmin);
            if(_admin.username!="" && _admin.username != ad.username)
            {
                ad.username = _admin.username;
            }
            if(_admin.email != "" && _admin.email != ad.email)
            {

                ad.email = _admin.email;
            }
            if (_admin.password != "" && _admin.password != ad.password)
            {

                ad.password = _admin.password;
            }
            this.Update(ad);
            this.Commit();


        }


        public void Add(Admin entity)
        {
            using (var ctx = new DatabContext())
            {
                ctx.Admins.Add(entity);
                ctx.SaveChanges();
            }
        }

        public void Commit()
        {
            using (var ctx = new DatabContext())
            {
                ctx.SaveChanges();
            }
        }

        public void Delete(Expression<Func<Admin, bool>> where)
        {
            using (var ctx = new DatabContext())
            {
                IEnumerable<Admin> objects = ctx.Admins.Where(where).AsEnumerable();
                foreach (Admin obj in objects)
                    ctx.Admins.Remove(obj);
                ctx.SaveChanges();

            }
        }

        public void Delete(Admin entity)
        {
            using (var ctx = new DatabContext())
            {

                ctx.Admins.Remove(entity);
                ctx.SaveChanges();

            }
        }

        public Admin Get(Expression<Func<Admin, bool>> where)
        {
            using (var ctx = new DatabContext())
            {

                return ctx.Admins.Where(where).FirstOrDefault();
                

            }
        }

        public IEnumerable<Admin> GetAll()
        {
            using (var ctx = new DatabContext())
            {

                return ctx.Admins;

            }
        }

        public Admin GetById(long id)
        {
            using (var ctx = new DatabContext())
            {

                return ctx.Admins.Find(id);

            }
        }

        public Admin GetById(string id)
        {
            using (var ctx = new DatabContext())
            {

                return ctx.Admins.Find(id);

            }
        }

        public IEnumerable<Admin> GetMany(Expression<Func<Admin, bool>> where = null, Expression<Func<Admin, bool>> orderBy = null)
        {
            IQueryable<Admin> Query;
            using (var ctx = new DatabContext())
            {

                Query = ctx.Admins;
                if (where != null)
                {
                    Query = Query.Where(where);
                }
                if (orderBy != null)
                {
                    Query = Query.OrderBy(orderBy);
                }
                return Query.ToList();

            }
        }


        public void Update(Admin entity)
        {
            using (var ctx = new DatabContext())
            {

                ctx.Admins.Attach(entity);
                ctx.Entry(entity).State = EntityState.Modified;
                ctx.SaveChanges();

            }
        }
    }
}
