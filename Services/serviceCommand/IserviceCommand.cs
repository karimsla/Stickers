using Model;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface IserviceCommand
    {
        void validateCommande(int id,DateTime datee);
        void add_commande(Command cmd);
        IEnumerable<Command> ListCommand();



        /*********************/


        void Add(Command entity);
        void Update(Command entity);
        void Delete(Command entity);
        void Delete(Expression<Func<Command, bool>> where);
        Command GetById(long id);
        Command GetById(string id);
        IEnumerable<Command> GetAll();
        IEnumerable<Command> GetMany(Expression<Func<Command, bool>> where = null, Expression<Func<Command, bool>> orderBy = null);
        Command Get(Expression<Func<Command, bool>> where);

        void Commit();



    }
}
