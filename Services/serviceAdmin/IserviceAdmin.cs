using Model;
using Service;


namespace Services.serviceAdmin
{
    public interface IserviceAdmin : IservicePattern<Admin>
    {
        bool authAdmin(string username,string password);
        void modifyAccount(Admin _admin);
      
    }
}
