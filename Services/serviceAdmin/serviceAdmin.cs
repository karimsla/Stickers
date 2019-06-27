using Data.Infrastructure;
using DATA.Infrastructure;
using Infrastructure;
using Model;
using MyFinance.Data.Infrastructure;
using Service;
using System;
using System.Security.Cryptography;
using System.Text;

namespace Services.serviceAdmin
{
    public class serviceAdmin : servicePattern<Admin>,IserviceAdmin

    {
        static IDatabaseFactory dbf = new DatabaseFactory();
        static IUnitOfWork uow = new UnitOfWork(dbf);
        public serviceAdmin() : base(uow)
        {

        }

        public bool authAdmin(string username, string password)
        {
            SHA256 hash = new SHA256CryptoServiceProvider();
            Byte[] originalBytes = ASCIIEncoding.Default.GetBytes(password);
            Byte[] encodedBytes = hash.ComputeHash(originalBytes);
            password = BitConverter.ToString(encodedBytes);
            return this.Get(x => x.username == username && x.password == password) != null;

        }

        public void modifyAccount(Admin _admin)
        {
            Admin ad = this.GetById(_admin.idAdmin);
            if(_admin.username!="" && _admin.username != ad.username)
            {
                ad.username = _admin.username;
            }
            if(_admin.email != "" && _admin.email != ad.email)
            {

                ad.email = _admin.email;
            }
            if (_admin.password != "" && _admin.password != ad.password)
            {

                ad.password = _admin.password;
            }
            this.Update(ad);
            this.Commit();


        }


    }
}
