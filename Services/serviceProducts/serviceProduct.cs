using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

using DATA;

using Model;



namespace Services
{
   public class serviceProduct:IserviceProduct
    {
         
        
          
            public serviceProduct() 
            {

            }

        public void add_product(Product prod)
        {
            this.Add(prod);
            
        }

        public void deleteprod(int id)
        {
            this.Delete(this.GetById(id));
        
        }

        public List<Product> listprod()
        {
            return this.GetMany(x => x.qteprod > 0).OrderBy(s=>s.cmd==null?s.qteprod:s.cmd.Count()).ToList();
        }

       public List<Product> listprodadmin()
        {
            return this.GetAll().OrderByDescending(s => s.qteprod).ToList();


        }

        public Product oneprod(int id)
        {
            return this.GetById(id);
        }

        public List<Product> search_kw(string kw)
        {
            List<Product> _product = new List<Product>();

            _product = this.GetMany(x => x.description.Contains(kw) ||
            x.nameprod.Contains(kw)).ToList();


            return _product;
        }

        /*********************************************************************************/



        public void Add(Product entity)
        {
            using (var ctx = new DatabContext())
            {
                ctx.Products.Add(entity);
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

        public void Delete(Expression<Func<Product, bool>> where)
        {
            IEnumerable<Product> objects;
            using (var ctx = new DatabContext())
            {
                objects = ctx.Products.Where(where).AsEnumerable();
                foreach (Product obj in objects)
                    ctx.Products.Remove(obj);
                ctx.SaveChanges();

            }
        }

        public void Delete(Product entity)
        {
            using (var ctx = new DatabContext())
            {

                ctx.Products.Remove(entity);
                ctx.SaveChanges();

            }
        }

        public Product Get(Expression<Func<Product, bool>> where)
        {
            using (var ctx = new DatabContext())
            {

                return ctx.Products.Where(where).FirstOrDefault();


            }
        }

        public IEnumerable<Product> GetAll()
        {
            using (var ctx = new DatabContext())
            {

                return ctx.Products.ToList();

            }
        }

        public Product GetById(long id)
        {
            using (var ctx = new DatabContext())
            {

                return ctx.Products.Find(id);

            }
        }

        public Product GetById(string id)
        {
            using (var ctx = new DatabContext())
            {

                return ctx.Products.Find(id);

            }
        }

        public IEnumerable<Product> GetMany(Expression<Func<Product, bool>> where = null, Expression<Func<Product, bool>> orderBy = null)
        {
            IQueryable<Product> Query;
            using (var ctx = new DatabContext())
            {

               Query = ctx.Products.Include(x=>x.cmd);
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


        public void Update(Product entity)
        {
            using (var ctx = new DatabContext())
            {

                ctx.Products.Attach(entity);
                ctx.Entry(entity).State = EntityState.Modified;
                ctx.SaveChanges();

            }
        }

    }
}
