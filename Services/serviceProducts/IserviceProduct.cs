using Model;
using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface IserviceProduct:IservicePattern<Product>
    {
        void add_product(Product prod);
        void updateprod(Product prod);
        List<Product> listprod();
        List<Product> listprodadmin();
        List<Product> search_kw(string kw);//search by keyword
        Product oneprod(int id);
        void deleteprod(int id);

    }
}
