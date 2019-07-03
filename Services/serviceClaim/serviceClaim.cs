using Data.Infrastructure;
using DATA.Infrastructure;
using Infrastructure;
using Model;
using MyFinance.Data.Infrastructure;
using Service;


namespace Services.serviceClaim
{
    public class serviceClaim:servicePattern<Claim>,IserviceClaim
    {
        static IDatabaseFactory dbf = new DatabaseFactory();
        static IUnitOfWork uow = new UnitOfWork(dbf);
        public serviceClaim() : base(uow)
        {

        }
    }
}
