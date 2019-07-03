using Model;
using Services;
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
            IserviceProduct ip = new serviceProduct();
            List<Product> lp = new List<Product>();
            lp = ip.listprod();

            return View(lp);
        }
        [HttpPost]
        public ActionResult Index(string search,string type)
        {
            IserviceProduct ip = new serviceProduct();

            string ch = search;//valeur à chercher
            string ch1 = type;//type de trie
            List<Product> lp = ip.search_kw(search).Where(a => a.qteprod > 0).ToList() ;
            if (type.Equals("desc"))
                lp.Reverse();
            
            return View(lp);
        }


      

        // GET: Products/Details/5
        public ActionResult Details(int id)
        {
            IserviceProduct ip = new serviceProduct();
            Product p = ip.GetById(id);
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



        // POST: Products/Create
        [HttpPost]
        public ActionResult Create(Product prod, HttpPostedFileBase item)
        {
            try
            {
                if (ModelState.IsValid && verifyFiles(item))// check if the model state is valid , and the file (image in the input is valid)

                {
                    string name = "name" + prod.nameprod + "im" + DateTime.Now.Minute+DateTime.Now.Millisecond+ Path.GetExtension(item.FileName);
                  //creating the name of the product
                   var path = Path.Combine(Server.MapPath("../Content/stickerspic/"), name);
                    //creating the path of the image combining the name of the product with the minute and millisecond to get a unique name for it

                    item.SaveAs(path);
                    //image saved in the path
                    prod.imgprod = path;
                                    
                    sp.add_product(prod);
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
            return View(sp.GetById(id));
        }

        // POST: Products/Edit/5
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
                return View();
            }
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int id)
        {
            sp.deleteprod(id);
            return RedirectToAction("IndexProducts");
        }






        // POST: Products/Delete/5
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
