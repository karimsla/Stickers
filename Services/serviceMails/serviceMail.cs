using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
   public class serviceMail:IserviceMail
    {
        public void sendMail(string mails, string obj, string body)
        {

            try
            {
                string sendermail = "ri9tounsii@gmail.com";
                string senderpassword = "Saber1999";
                SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
                client.EnableSsl = true;
                client.Timeout = 1000000;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                MailMessage mailMessage = new MailMessage();

                mailMessage.To.Add(new MailAddress(mails));

                mailMessage.From = new MailAddress("ri9tounsii@gmail.com");

                mailMessage.Body = body;
                mailMessage.Subject = obj;

                client.Credentials = new NetworkCredential(sendermail, senderpassword);

                mailMessage.IsBodyHtml = true;

                mailMessage.BodyEncoding = UTF8Encoding.UTF8;

                client.Send(mailMessage);

            }
            catch (Exception e)
            {

            }
        }
    }
}
