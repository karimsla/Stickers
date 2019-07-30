
using Data.Infrastructure;
using DATA;
using Infrastructure;


namespace DATA.Infrastructure
{
    public class DatabaseFactory : Disposable, IDatabaseFactory
    {
        private DatabContext dataContext;
        public DatabContext DataContext { get { return dataContext; } }

        public DatabaseFactory()
        {
          
           
            dataContext = new DatabContext();
        }

        protected override void DisposeCore()
        {
            // libérer espace mémoire du context
            if(DataContext!=null)
            DataContext.Dispose();
        }
    }

}
