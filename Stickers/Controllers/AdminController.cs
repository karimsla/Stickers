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
        serviceClaim scc = new serviceClaim();
        IserviceAdmin spa = new serviceAdmin();
        [CustomAuthorizeAttribute(Roles = "Admin")]
        [HttpGet]
        public ActionResult editProfile()
        {
            //this is the get method
            //it will put the current connected user in the admin object
            Admin ad = spa.Get(x => x.username == User.Identity.Name);
            return View(ad);
        }

        [CustomAuthorizeAttribute(Roles = "Admin")]
        [HttpPost]
        public ActionResult editProfile(Admin ad, string confirmpassword)
        {
           
                //we will get the admin from the data base to attach it
                Admin _admin = spa.GetById(ad.idAdmin);
                if (_admin.email != ad.email && !string.IsNullOrEmpty(ad.email) && !string.IsNullOrWhiteSpace(ad.email))
                {
                    //check if the email is not null , empty or white space and the change it
                    _admin.email = ad.email;
                }
                if (_admin.username != ad.username && !string.IsNullOrEmpty(ad.username) && !string.IsNullOrWhiteSpace(ad.username))
                {
                    //check if the username isn't null , emtpy or white space and then change it
                    _admin.username = ad.username;
                }
                if (ad.password != "" && !string.IsNullOrWhiteSpace(ad.password) && ad.password == confirmpassword)
                {
                    //for the password it s more tricky 
                    //check if it s empty the check it s not white space and check if the two passord match
                    _admin.password = ad.password;

                }
                else if (ad.password != "" && ad.password != confirmpassword)
                {
                    //if the two password doesn't math return the same view with error msg
                    ViewBag.error = "password doesn't math";
                    return View();
                }
            //now update and commit


            spa.Update(_admin);
                spa.Commit();

                ViewBag.success = "Profile updated";
                return View();

          

        }



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


            //Last 4 claims not seen
            List<Claim> cno = scc.GetMany(a => a.seen == false).ToList().OrderBy(a=>a.claimdate).ToList();
            ViewBag.Claims = cno.Count;




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

        [CustomAuthorizeAttribute(Roles = "Admin")]
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
        [CustomAuthorizeAttribute(Roles = "Admin")]
        public ActionResult IndexProducts()
        {


            var products = sp.GetMany();

            return View(products);
        }


        // GET: Products


        // Update quantity product
        [CustomAuthorizeAttribute(Roles = "Admin")]
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


        [CustomAuthorizeAttribute(Roles = "Admin")]
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
        [CustomAuthorizeAttribute(Roles = "Admin")]
        [HttpPost]
        public ActionResult IndexProducts(String SearchString)
        {
            var Products = sp.GetMany(p => p.nameprod.Contains(SearchString));
            return View(Products);
        }

        // GET: Products/Details/5
        public ActionResult Details(int id)
        {
            IserviceProduct ip = new serviceProduct();
            Product p = ip.GetById(id);
            List<Command> xc = sc.GetMany(a => a.idprod == id).ToList();
            int v = xc.Sum(w => w.qteprod);
            ViewBag.quantite = v;
            return View(p);
        }



        // GET: Products/Create
        public ActionResult Create()
        {
            return View();
        }
        protected bool verifyFiles(HttpPostedFileBase item)
        {
            bool flag = true;



            if (item != null)
            {
                if (item.ContentLength > 0 && item.ContentLength < 5000000)
                {


                    if (!(Path.GetExtension(item.FileName).ToLower() == ".jpg" ||
                        Path.GetExtension(item.FileName).ToLower() == ".png" ||
                        Path.GetExtension(item.FileName).ToLower() == ".bmp" ||
                        Path.GetExtension(item.FileName).ToLower() == ".jpeg"))
                    {
                        flag = false;
                    }





                }
                else { flag = false; }

            }





            else { flag = false; }

            return flag;
        }


        // POST:Create product
        // POST: Products/Create
        [HttpPost]
        public ActionResult Create(Product prod, HttpPostedFileBase item, HttpPostedFileBase img1, HttpPostedFileBase img2, HttpPostedFileBase img3)
        {
            try
            {
                //probléme dans le modelState du coup j'ai vérifier champ  par champ
                if (prod.nameprod!="" && prod.price!=0 &&  verifyFiles(item) && verifyFiles(img1) && verifyFiles(img2) && verifyFiles(img3))// check if the model state is valid , and the file (image in the input is valid)

                {
                    string path = Server.MapPath("/Content/stickerspic/");
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    item.SaveAs(path + Path.GetFileName(item.FileName));
                    img1.SaveAs(path + Path.GetFileName(img1.FileName));
                    img2.SaveAs(path + Path.GetFileName(img2.FileName));
                    img3.SaveAs(path + Path.GetFileName(img3.FileName));
                    ViewBag.Message = "Pictures uploaded successfully.";


                    prod.imgprod = Path.GetFileName(item.FileName);
                    prod.img1 = Path.GetFileName(img1.FileName);
                    prod.img2 = Path.GetFileName(img2.FileName);
                    prod.img3 = Path.GetFileName(img3.FileName);
                    // just call the service and it will do the work check service production for more informations
                    prod.imgprod = prod.imgprod;
                    prod.img1 = prod.img1;
                    prod.img2 = prod.img2;
                    prod.img3 = prod.img3;
                    sp.add_product(prod);
                }
                else
                {
                    ViewBag.error = "files or input are invalid";
                    return View();
                }

                return RedirectToAction("IndexProducts");
            }
            catch
            {
                return View();
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
                    string path = Server.MapPath("/Content/stickerspic/");
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }

                    postedFile.SaveAs(path + Path.GetFileName(postedFile.FileName));
                    ViewBag.Message = "Picture uploaded successfully.";


                    prod.imgprod =  Path.GetFileName(postedFile.FileName);
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



        //Searching Command by name
        [CustomAuthorizeAttribute(Roles = "Admin")]
        [HttpPost]
        public ActionResult ListCommand(String SearchString)
        {
            var Commands = sc.GetMany(p => p.name.Contains(SearchString));
            return View(Commands);
        }



        //Confirm command
        [HttpPost]
        public ActionResult Confirmcommand(int id, DateTime datee)
        {
            IserviceCommand spc = new serviceCommand();


            Command cmd = new Command();

            cmd = sc.GetById(id);
            //the admin will put the date of the delievery and the command will be validated
            cmd.dateliv = datee;
            spc.validateCommande(cmd);
            IserviceMail sm = new serviceMail();
            sm.sendMail(cmd.email, "order from ri9 Tounsi have been reviewed",
                "your order have been reviewed and it will be delievered " + datee.ToString() + "<br>We will call you as soon as possible");

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


        //All claims
        [HttpGet]
        public ActionResult AllClaims()
        {   //Update attribute seen=True
            List<Claim> cno = scc.GetMany(a => a.seen == false).ToList();
            foreach(var x in cno)
            {
                Claim c = scc.GetById(x.idclaim);
                c.seen = true;
                scc.Update(c);
                scc.Commit();

            }



            List<Claim> cc = scc.GetMany().ToList().OrderByDescending(a => a.claimdate).ToList();
            return View(cc);
        }


        [CustomAuthorizeAttribute(Roles = "Admin")]
        public ActionResult LastClaims()
        {
            List<Claim> cno = scc.GetMany(a => a.seen == false).Take(4).ToList().OrderBy(a => a.claimdate).ToList();

            return View(cno);
        }


        //les ventes des produits
        [CustomAuthorizeAttribute(Roles = "Admin")]
        public ActionResult Ventes()
        {
            
            List<Product> products = sp.GetMany().ToList();

            foreach (var x in products)
            {
                x.vente = sc.GetMany(a => a.idprod == x.idprod).Sum(y => y.qteprod);

            }
            List<Product> productss = products.OrderByDescending(a => a.vente).Take(8).ToList();
            return View(productss);
        }
        //nos clients
        [CustomAuthorizeAttribute(Roles = "Admin")]
        public ActionResult Customers()
        {

            List<Command> comm = sc.GetMany().ToList();

         
            return View(comm);
        }




        //---------------------------------------------------------------------------------

   
        // GET: Admin/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }



    }
}
