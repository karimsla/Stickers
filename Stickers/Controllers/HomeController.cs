using Model;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Stickers.Controllers
{
    public class HomeController : Controller
    {
        public PartialViewResult Home()
        {
            //
            return PartialView();
        }

        public PartialViewResult AboutUs()
        {

            return PartialView();
        }

        public PartialViewResult Products()
        {
            IserviceProduct ip = new serviceProduct();
           
            List<Product> lp = new List<Product>();
            lp = ip.GetMany().Reverse().Where(a=>a.qteprod>0).Take(6).ToList();

            ViewData["list"] = lp;
            return PartialView();
        }

        public PartialViewResult ContactUs()
        {

            return PartialView();
        }


        public ActionResult Index()
        {
            return View();
        }


        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}