using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface IserviceMail
    {
        void sendMail(string mails, string obj, string body);
    }
}
