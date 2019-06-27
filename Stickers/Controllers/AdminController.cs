using Model;
using Services.serviceAdmin;
using Stickers.Security;
using System;

using System.Web.Mvc;
using System.Web.Security;

namespace Stickers.Controllers
{
   
    public class AdminController : Controller
    {


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
