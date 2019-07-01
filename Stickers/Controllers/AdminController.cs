using Model;
using Services;
using Services.serviceAdmin;
using Stickers.Security;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Security;

using System.Linq;
using System.Web;

namespace Stickers.Controllers
{
   
    public class AdminController : Controller
    {
        serviceProduct sp = new serviceProduct();
        serviceCommand sc = new serviceCommand();


        public ActionResult login()
        {
            FormsAuthentication.SignOut();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult login(Admin ad, string ReturnUrl)
        {
            IserviceAdmin spa = new serviceAdmin();
            if (spa.authAdmin(ad.username, ad.password))//check serviceAdmin
            {

                FormsAuthentication.SetAuthCookie(ad.username, true);//store user mail in cookies 
           

                return RedirectToAction("index");
            }



            return View();
        }


        [Authorize]
        public ActionResult logout()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();



            return RedirectToAction("login");
        }


        // GET: Admin
        [CustomAuthorizeAttribute(Roles = "Admin")]
        public ActionResult Index()
        {

            //products number
            //List<Product> ls = sp.GetMany().ToList();
            ViewBag.pr = sp.listprodadmin().Count;

            //command this week
            DateTime d = DateTime.Today;
            DateTime d1 = DateTime.Today.AddDays(-7);
            List<Command> lc = sc.GetMany(a=>a.datecmd>=d1&&a.datecmd<=d).ToList();
            ViewBag.cmdweek = lc.Count;


            //total earnings
            List<Command> xc = sc.GetMany(a => a.isComfirmed == true).ToList();
            float v = xc.Sum(w => w.product.price);
            ViewBag.earnings = v;


            return View();

        }
        [CustomAuthorizeAttribute(Roles = "Admin")]
        public ActionResult Index2()
        {
            return View();
        }
        // GET: Admin/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Admin/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Admin/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Admin/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Admin/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Admin/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
