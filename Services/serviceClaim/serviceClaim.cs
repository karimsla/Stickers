
using DATA;

using Model;

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace Services.serviceClaim
{
    public class serviceClaim:IserviceClaim
    {
    
        public serviceClaim() 
        {

        }



        public void Add(Claim entity)
        {
            using (var ctx = new DatabContext())
            {
                ctx.Claims.Add(entity);
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

        public void Delete(Expression<Func<Claim, bool>> where)
        {
            IEnumerable<Claim> objects;
            using (var ctx = new DatabContext())
            {
               objects = ctx.Claims.Where(where).AsEnumerable();
                foreach (Claim obj in objects)
                    ctx.Claims.Remove(obj);
                ctx.SaveChanges();

            }
        }

        public void Delete(Claim entity)
        {
            using (var ctx = new DatabContext())
            {

                ctx.Claims.Remove(entity);
                ctx.SaveChanges();

            }
        }

        public Claim Get(Expression<Func<Claim, bool>> where)
        {
            using (var ctx = new DatabContext())
            {

                return ctx.Claims.Where(where).FirstOrDefault();

            }
        }

        public IEnumerable<Claim> GetAll()
        {
            using (var ctx = new DatabContext())
            {

                return ctx.Claims.AsEnumerable();

            }
        }

        public Claim GetById(long id)
        {
            using (var ctx = new DatabContext())
            {

                return ctx.Claims.Find(id);

            }
        }

        public Claim GetById(string id)
        {
            using (var ctx = new DatabContext())
            {

                return ctx.Claims.Find(id);

            }
        }

        public IEnumerable<Claim> GetMany(Expression<Func<Claim, bool>> where = null, Expression<Func<Claim, bool>> orderBy = null)
        {
            IQueryable<Claim> Query;
            using (var ctx = new DatabContext())
            {

                Query = ctx.Claims;
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


        public void Update(Claim entity)
        {
            using (var ctx = new DatabContext())
            {

                ctx.Claims.Attach(entity);
                ctx.Entry(entity).State = EntityState.Modified;
                ctx.SaveChanges();

            }
        }



    }
}
