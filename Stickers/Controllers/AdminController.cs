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
using System.IO;

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




        //------------------------------------------------------
        // GET: Products
        public ActionResult IndexProducts()
        {


            var products = sp.GetMany();

            return View(products);
        }


        // GET: Products


        // Update quantity product
        [HttpPost]
        public ActionResult UpdateQuantity(int id, int txtQt)
        {



            Product s = new Product();

            s = sp.GetById(id);
            s.qteprod = s.qteprod + txtQt;

            sp.Update(s);
            sp.Commit();

            return RedirectToAction("IndexProducts");
        }



        [HttpPost]
        public ActionResult DeleteProduct(int id)
        {



            Product s = new Product();

            s = sp.GetById(id);


            sp.Update(s);
            sp.Commit();

            return RedirectToAction("IndexProducts");
        }

        //Searching product by name
        [HttpPost]
        public ActionResult IndexProducts(String SearchString)
        {
            var Products = sp.GetMany(p => p.nameprod.Contains(SearchString));
            return View(Products);
        }

        // GET: Admin/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }





        // GET: Products/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST:Create product
        [HttpPost]
        public ActionResult Create(Product prod, HttpPostedFileBase postedFile)
        {
            try
            {
                if (postedFile != null)
                {
                    string path = Server.MapPath("/stickerspictures/");
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }

                    postedFile.SaveAs(path + Path.GetFileName(postedFile.FileName));
                    ViewBag.Message = "Picture uploaded successfully.";


                    prod.imgprod = "/stickerspictures/" + Path.GetFileName(postedFile.FileName);
                    // just call the service and it will do the work check service production for more informations
                    prod.imgprod = prod.imgprod;
                    sp.add_product(prod);


                    return RedirectToAction("IndexProducts");
                }

                sp.add_product(prod);
                return RedirectToAction("IndexProducts");

            }
            catch (NullReferenceException e)
            {
                return RedirectToAction("IndexProducts");
            }

        }


        // GET: Products/Edit/5
        public ActionResult Edit(int id)
        {
            //request a product by id and returning the product model in the view
            List<Command> xc = sc.GetMany(a => a.idprod == id).ToList();
            int v = xc.Sum(w => w.qteprod);
            ViewBag.quantite = v;
            return View(sp.GetById(id));
        }

        // POST: Products/Edit/5
        [HttpPost]
        public ActionResult Edit(Product prod, HttpPostedFileBase postedFile)
        {
            Product produitt = sp.GetById(prod.idprod);
            produitt.nameprod = prod.nameprod;
            produitt.price = prod.price;
            produitt.description = prod.description;
            produitt.qteprod = prod.qteprod;
            try
            {
                if (postedFile != null)
                {
                    string path = Server.MapPath("/stickerspictures/");
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }

                    postedFile.SaveAs(path + Path.GetFileName(postedFile.FileName));
                    ViewBag.Message = "Picture uploaded successfully.";


                    prod.imgprod = "/stickerspictures/" + Path.GetFileName(postedFile.FileName);
                    // just call the service and it will do the work check service production for more informations
                    produitt.imgprod = prod.imgprod;
                    sp.Update(produitt);
                    sp.Commit();

                    return RedirectToAction("IndexProducts");
                }

                sp.Update(produitt);
                sp.Commit();
                return RedirectToAction("IndexProducts");

            }
            catch (NullReferenceException e)
            {
                return RedirectToAction("IndexProducts");
            }
        }



        [HttpGet]
        public JsonResult Description(int id)
        {
            Product pr = sp.GetById(id);

            return Json(pr, JsonRequestBehavior.AllowGet);
        }
        //-------------------------------Commandes----------------------------------------


        public ActionResult ListCommand()
        {
            //returnin the list of the commands
            return View(sc.ListCommand());

        }

        //Confirm command
        [HttpPost]
        public ActionResult Confirmcommand(int id, DateTime datee)
        {



            Command s = new Command();

            s = sc.GetById(id);
            // s.qteprod = s.qteprod + txtQt;
            s.dateliv = datee;
            s.isComfirmed = true;
            sc.Update(s);
            sc.Commit();

            return RedirectToAction("ListCommand");
        }
        [HttpGet]
        public ActionResult OrderThisWeek()
        {
            DateTime d = DateTime.Today;
            DateTime d1 = DateTime.Today.AddDays(-7);
            List<Command> lc = sc.GetMany(a => a.datecmd >= d1 && a.datecmd <= d).ToList();
            return View(lc);
        }


        [HttpGet]
        public ActionResult OrderThisMonth()
        {
            DateTime d = DateTime.Today;
            DateTime d1 = DateTime.Today.AddMonths(-1);
            List<Command> lc = sc.GetMany(a => a.datecmd >= d1 && a.datecmd <= d).ToList();
            return View(lc);
        }



        //---------------------------------------------------------------------------------




        // GET: Admin/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }



    }
}
