using Model;
using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface IserviceCommand:IservicePattern<Command>
    {
        void validateCommande(int id,DateTime datee);
        void add_commande(Command cmd);
        List<Command> ListCommand();
    }
}
