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
            List<Product> lp = new List<Product>();
            lp.Add(new Product() { nameprod = "Sticker Got", price = 30, idprod = 1 });
            lp.Add(new Product() { nameprod = "Sticker Vikings", price = 40, idprod = 1 });
            lp.Add(new Product() { nameprod = "Sticker Friends", price = 20, idprod = 1 });
            lp.Add(new Product() { nameprod = "Sticker Dark", price = 20, idprod = 1 });
            lp.Add(new Product() { nameprod = "Sticker TEST", price = 150, idprod = 1 });
            lp.Add(new Product() { nameprod = "Sticker Flash", price = 70, idprod = 1 });

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