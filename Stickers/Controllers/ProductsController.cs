using Model;
using Services;
using Stickers.Security;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Stickers.Controllers
{
    public class ProductsController : Controller
    {

        serviceProduct sp = new serviceProduct();

        // GET: Products
        public ActionResult Index()
        {
            //list prod for users
            IserviceProduct ip = new serviceProduct();
            List<Product> lp = new List<Product>();
            lp = ip.listprod();

            return View(lp);
        }
        [HttpPost]
        public ActionResult Index(string search,string type)
        {
            //search product for user
            IserviceProduct ip = new serviceProduct();

            string ch = search;//valeur à chercher
            string ch1 = type;//type de trie
            List<Product> lp = ip.search_kw(search).Where(a => a.qteprod > 0).ToList() ;
            if (type.Equals("desc"))
                lp.Reverse();
            
            return View(lp);
        }
        
        public ActionResult order(Command com)
        {  //make an order

            IserviceCommand spcl = new serviceCommand();

            if (ModelState.IsValid)
            {
                com.datecmd = DateTime.Today;
                spcl.Add(com);
                spcl.Commit();

            }


            return RedirectToAction("Details/"+com.idprod);
        }

        // GET: Products
        public ActionResult IndexProducts()
        {

            
            var products = sp.GetMany();

            return View(products);
        }


        // POST: Service/Create
        [HttpPost]
        public ActionResult Index2(int id , int txtQt)
        {

            //this method adds quantity to an existing product 
            Product s = new Product();
            
            s = sp.GetById(id);
            s.qteprod = s.qteprod + txtQt;
           
            sp.Update(s);
            sp.Commit();

         return   RedirectToAction("IndexProducts");
        }


        [HttpPost]
        public ActionResult Serach(String SearchString)
        {
            //search for product by key word
            var Products = sp.search_kw(SearchString);
            return View(Products);
        }


        // GET: Products/Details/5
        public ActionResult Details(int id)
        {
            IserviceProduct ip = new serviceProduct();
            Product p = ip.GetById(id);
            return View(p);
        }

        // GET: Products/Create
       

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



      
        // POST: Products/Edit/5
        [CustomAuthorizeAttribute(Roles = "Admin")]
        [HttpPost]
        public ActionResult Edit(Product prod)
        {
            try
            {
                // just call the service and it will do the work check service production for more informations
                sp.updateprod(prod);

                return RedirectToAction("IndexProducts");
            }
            catch
            {
                ModelState.AddModelError("","error occured try again");
                return View(prod);
            }
        }

        // GET: Products/Delete/5
        [CustomAuthorizeAttribute(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            sp.deleteprod(id);
            return RedirectToAction("IndexProducts");
        }



        /// <summary>
        /// 7/2/2019
        /// </summary>
       


        // GET: Products/Edit/5
        [CustomAuthorizeAttribute(Roles = "Admin")]
        public ActionResult Edit(int id)
        {

            // this one have no view 
            //request a product by id and returning the product model in the view
            return View(sp.GetById(id));
        }



        [CustomAuthorizeAttribute(Roles = "Admin")]
        public ActionResult Create()
        {
            // this one have no view add the view for it 
            // to create a product by the admin
            return View();
        }



        // POST: Products/Create
        [CustomAuthorizeAttribute(Roles = "Admin")]
        [HttpPost]
        public ActionResult Create(Product prod, HttpPostedFileBase item)
        {



            if (!verifyFiles(item))
            {
                ModelState.AddModelError("", "verify pictures format and size must be trype jpg jpeg png bmp ");
            }
            try
            {
                if (ModelState.IsValid)// check if the model state is valid , and the file (image in the input is valid)

                {
                    string name = "name" + prod.nameprod + "im" + DateTime.Now.Minute + DateTime.Now.Millisecond + Path.GetExtension(item.FileName);
                    //creating the name of the product
                    var path = Path.Combine(Server.MapPath("../Content/stickerspic/"), name);
                    //creating the path of the image combining the name of the product with the minute and millisecond to get a unique name for it

                    item.SaveAs(path);
                    //image saved in the path
                    prod.imgprod = path;

                    sp.add_product(prod);
                    return RedirectToAction("IndexProducts");
                }

                return View(prod);
            }
            catch
            {
                ModelState.AddModelError("", "try again later an error occured");

                return View();
            }
        }





    }
}
