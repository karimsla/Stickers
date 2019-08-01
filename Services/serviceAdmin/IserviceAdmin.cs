using Model;

using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Services.serviceAdmin
{
    public interface IserviceAdmin 
    {
        bool authAdmin(string username,string password);
        void modifyAccount(Admin _admin);



        /************************************/

        void Add(Admin entity);
        void Delete(Expression<Func<Admin, bool>> where);
        void Delete(Admin entity);
        Admin Get(Expression<Func<Admin, bool>> where);
        IEnumerable<Admin> GetAll();
        Admin GetById(long id);
        Admin  GetById(string id);
        IEnumerable<Admin> GetMany(Expression<Func<Admin, bool>> where = null, Expression<Func<Admin, bool>> orderBy = null);

        void Update(Admin entity);

        void Commit();


    }
}
