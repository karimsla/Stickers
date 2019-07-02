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
using Services.serviceClaim;

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
  
      

   
  

        /// <summary>
        /// 7/2/2019
        /// </summary>
        /// 
        /// 

        [CustomAuthorizeAttribute(Roles = "Admin")]
        public ActionResult DetailProd(int id)
        {
            //detail prod for admin that means without add command option
            //this one have no view create the view
            return View(sp.GetById(id));
        }



        [CustomAuthorizeAttribute(Roles = "Admin")]
        public ActionResult Claims()
        {

            //atef is supposed to fix the template for this one 
            //the admin can see the list of claims ordred by date
            //the admin can delete a claim check the next action result
            IserviceClaim spcl = new serviceClaim();
            List<Claim> cl = new List<Claim>();
            cl = spcl.GetAll().OrderBy(x => x.claimdate).ToList();
            return View(cl);
        }

        public ActionResult deleteClaim(int id)
        {
            //the admin can delete a claim
            IserviceClaim spcl = new serviceClaim();
            spcl.Delete(spcl.GetById(id));
            spcl.Commit();

            return RedirectToAction("Claims");
        }



    }
}
