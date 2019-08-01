using Model;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Services.serviceClaim
{
    public interface IserviceClaim
    {



        void Add(Claim entity);
        void Update(Claim entity);
        void Delete(Claim entity);
        void Delete(Expression<Func<Claim, bool>> where);
        Claim GetById(long id);
        Claim GetById(string id);
        IEnumerable<Claim> GetAll();
        IEnumerable<Claim> GetMany(Expression<Func<Claim, bool>> where = null, Expression<Func<Claim, bool>> orderBy = null);
        Claim Get(Expression<Func<Claim, bool>> where);

        void Commit();




    }
}
