
using DATA;

using Model;

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace Services
{
    public class serviceCommand:IserviceCommand
    {

       
          
            public serviceCommand() 
            {

            }

        public void add_commande(Command cmd)
        {
            try
            {
                cmd.datecmd = DateTime.Now;
                cmd.isComfirmed = false;
                this.Add(cmd);
                this.Commit();
            }catch(Exception e)
            {

            }
        }

        public IEnumerable<Command> ListCommand()
        {
            IserviceProduct spp = new serviceProduct();
            var cmd=this.GetMany().OrderBy(d=>d.dateliv==null);
          
            return cmd;
        }

        public void validateCommande(int id,DateTime datee)
        {
            //validate command iscomfirmed =true and substract the qte of the command from the productS
        
            Command _cmd = new Command();

            _cmd = this.Get(x => x.idcmd == id);

            _cmd.dateliv = datee;
            _cmd.isComfirmed = true;
            this.Update(_cmd);
            this.Commit();
          
            IserviceProduct spp = new serviceProduct();
            Product prod = spp.Get(x => x.idprod == _cmd.idprod);
            prod.qteprod =prod.qteprod- _cmd.qteprod;
            spp.Update(prod);
            spp.Commit();
            IserviceMail sm = new serviceMail();
            sm.sendMail(_cmd.email, "order from ri9 Tounsi have been reviewed",
                "your order have been reviewed and it will be delievered " + datee.ToShortDateString() + "<br>We will call you as soon as possible");



        }




        /**************************************************************/
        public void Add(Command entity)
        {
            using (var ctx = new DatabContext())
            {
                ctx.Commands.Add(entity);
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

        public void Delete(Expression<Func<Command, bool>> where)
        {
            IEnumerable<Command> objects;
            using (var ctx = new DatabContext())
            {
               objects= ctx.Commands.Where(where).AsEnumerable();

                
                    ctx.Commands.RemoveRange(objects);
                   
                   
                
                    ctx.SaveChanges();
                
            }
        }

        public void Delete(Command entity)
        {
            using (var ctx = new DatabContext())
            {
                ctx.Commands.Attach(entity);
                ctx.Commands.Remove(entity);
                ctx.SaveChanges();

            }
        }

        public Command Get(Expression<Func<Command, bool>> where)
        {
            using (var ctx = new DatabContext())
            {

                return ctx.Commands.Where(where).Include(x=>x.product).FirstOrDefault();

            }
        }

        public IEnumerable<Command> GetAll()
        {
            using (var ctx = new DatabContext())
            {

                return ctx.Commands.Include(x => x.product).AsEnumerable();

            }
        }

        public Command GetById(long id)
        {
            using (var ctx = new DatabContext())
            {

                return ctx.Commands.Find(id);

            }
        }

        public Command GetById(string id)
        {
            using (var ctx = new DatabContext())
            {

                return ctx.Commands.Find(id);

            }
        }

        public IEnumerable<Command> GetMany(Expression<Func<Command, bool>> where = null, Expression<Func<Command, bool>> orderBy = null)
        {
            IQueryable<Command> Query;
            using (var ctx = new DatabContext())
            {

               Query= ctx.Commands.Include(x=>x.product);
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


        public void Update(Command entity)
        {
            using (var ctx = new DatabContext())
            {

                ctx.Commands.Attach(entity);
                ctx.Entry(entity).State = EntityState.Modified;
                ctx.SaveChanges();

            }
        }


    }
}
