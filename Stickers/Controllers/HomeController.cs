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