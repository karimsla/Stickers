using System.Collections.Generic;
using System.Linq;
using Data.Infrastructure;
using DATA.Infrastructure;
using Infrastructure;
using Model;
using MyFinance.Data.Infrastructure;
using Service;


namespace Services
{
   public class serviceProduct:servicePattern<Product>,IserviceProduct
    {

        
            static IDatabaseFactory dbf = new DatabaseFactory();
            static IUnitOfWork uow = new UnitOfWork(dbf);
            public serviceProduct() : base(uow)
            {

            }

        public void add_product(Product prod)
        {
            this.Add(prod);
            this.Commit();
        }

        public void deleteprod(int id)
        {
            this.Delete(this.GetById(id));
            this.Commit();
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



        public void updateprod(Product prod)
        {
            
        }
    }
}
