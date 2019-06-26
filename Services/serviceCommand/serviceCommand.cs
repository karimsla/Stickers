using Data.Infrastructure;
using DATA.Infrastructure;
using Infrastructure;
using Model;
using MyFinance.Data.Infrastructure;
using Service;
using System;

namespace Services
{
    public class serviceCommand:servicePattern<Command>,IserviceCommand
    {

       
            static IDatabaseFactory dbf = new DatabaseFactory();
            static IUnitOfWork uow = new UnitOfWork(dbf);
            public serviceCommand() : base(uow)
            {

            }

        public void add_commande(Command cmd)
        {
            cmd.datecmd = DateTime.Now;
            cmd.isComfirmed = false;
            this.Add(cmd);
            this.Commit();
        }

        public void validateCommande(Command cmd)
        {
            IserviceProduct spp = new serviceProduct();
            Command _cmd = this.GetById(cmd.idcmd);
            Product prod=spp.GetById(_cmd.idprod);

            _cmd.dateliv = cmd.dateliv;
            _cmd.isComfirmed = true;
            this.Update(_cmd);
            this.Commit();
            prod.qteprod =prod.qteprod- cmd.qteprod;
            spp.Update(prod);
            spp.Commit();

            
        }
    }
}
