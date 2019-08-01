using Model;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface IserviceProduct
    {
        void add_product(Product prod);
        List<Product> listprod();
        List<Product> listprodadmin();
        List<Product> search_kw(string kw);//search by keyword
        Product oneprod(int id);
        void deleteprod(int id);



        //*********************/


        void Add(Product entity);
        void Update(Product entity);
        void Delete(Product entity);
        void Delete(Expression<Func<Product, bool>> where);
        Product GetById(long id);
        Product GetById(string id);
        IEnumerable<Product> GetAll();
        IEnumerable<Product> GetMany(Expression<Func<Product, bool>> where = null, Expression<Func<Product, bool>> orderBy = null);
        Product Get(Expression<Func<Product, bool>> where);

        void Commit();


    }
}
