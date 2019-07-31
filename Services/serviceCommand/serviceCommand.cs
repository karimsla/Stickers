using Data.Infrastructure;
using DATA.Infrastructure;
using Infrastructure;
using Model;
using MyFinance.Data.Infrastructure;
using Service;
using System;
using System.Collections.Generic;
using System.Linq;

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

        public List<Command> ListCommand()
        {
            IserviceProduct spp = new serviceProduct();
            var cmd=this.GetMany().OrderBy(d=>d.datecmd).ToList();
            foreach(Command i in cmd)
            {
                if(i.product==null)
                i.product = spp.GetById(i.idprod);
            }
            return cmd.ToList();
        }

        public void validateCommande(int id,DateTime datee)
        {
            //validate command iscomfirmed =true and substract the qte of the command from the productS
        
            Command _cmd = this.GetById(id);
          

            _cmd.dateliv = datee;
            _cmd.isComfirmed = true;
            this.Update(_cmd);
            this.Commit();
            IserviceProduct spp = new serviceProduct();
            Product prod = spp.GetById(_cmd.idprod);
            prod.qteprod =prod.qteprod- _cmd.qteprod;
            spp.Update(prod);
            spp.Commit();
            IserviceMail sm = new serviceMail();
            sm.sendMail(_cmd.email, "order from ri9 Tounsi have been reviewed",
                "your order have been reviewed and it will be delievered " + datee.ToString() + "<br>We will call you as soon as possible");



        }
    }
}
