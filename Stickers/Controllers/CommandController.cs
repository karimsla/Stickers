using Model;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Stickers.Controllers
{
    public class CommandController : Controller
    {
        IserviceCommand spc = new serviceCommand();

        // GET: Command
        public ActionResult Index()
        {
            return View();
        }



        /// <summary>
        /// 7/2/2019
        /// </summary>
        

            //all the controllers in this page have no view 



        [HttpGet]
        public ActionResult Command(int prodid)
        {

            //when the user click add command it will call this method
            Command cmd = new Command();
            cmd.idprod = prodid;
            //put in the empty model command the product id and return the view where the user will put his credentials

            return View(cmd);

        }


        [HttpPost]
        public ActionResult Command(Command cmd)
        {

            IserviceProduct ps = new serviceProduct();
            Product p=ps.GetById(cmd.idprod);
            if (p.qteprod < cmd.qteprod)
            {
                ModelState.AddModelError("quantity not enough","not enough quantity in the stock");
            }

            if (ModelState.IsValid)
            {
                
                //if modelstate is valid then add_command check service command for more informations
                IserviceCommand spc = new serviceCommand();
                spc.add_commande(cmd);
            }



            return RedirectToAction("Details/"+cmd.idprod,"Products");

        }

        private IserviceCommand serviceCommand()
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        public ActionResult ValidateCommand(Command cmd, DateTime date)
        {

            //the admin will put the date of the delievery and the command will be validated
            cmd.dateliv = date;
            spc.validateCommande(cmd);
            IserviceMail sm = new serviceMail();
            sm.sendMail(cmd.email,"order from ri9 Tounsi have been reviewed",
                "your order have been reviewed and it will be delievered "+date.ToString()+"<br>We will call you as soon as possible");

            return View(cmd);

        }

        public ActionResult ListCommand()
            
        {
            //returnin the list of the commands
            return View(spc.ListCommand());
            
        }

        public PartialViewResult Details(int id)
        {
            return PartialView("Details", spc.GetById(id));
        }


    }
}