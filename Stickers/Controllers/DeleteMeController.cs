using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Stickers.Controllers
{
    public class DeleteMeController : Controller
    {
        // GET: DeleteMe
        public ActionResult Index()
        {
            return View();
        }

        // GET: DeleteMe/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: DeleteMe/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DeleteMe/Create
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

        // GET: DeleteMe/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: DeleteMe/Edit/5
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

        // GET: DeleteMe/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: DeleteMe/Delete/5
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
