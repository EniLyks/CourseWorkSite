using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using СourseWorkSite.Models;

namespace СourseWorkSite.Controllers
{
    public partial class AdminController : Controller
    {

        public ActionResult TableTypePlugin(string searchString)
        {
            IQueryable typePlugin;

            if (!String.IsNullOrEmpty(searchString))
            {
                typePlugin = db.TypePlugins.Where(s => s.Type.ToUpper().Contains(searchString.ToUpper()));
            }
            else
            {
                typePlugin = db.TypePlugins;
            }

            return View(typePlugin);
        }

        //Создание нового производителя
        [HttpGet]
        public ActionResult CreateTypePlugin()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateTypePlugin(TypePlugin typePlugin)
        {
            db.TypePlugins.Add(typePlugin);
            db.SaveChanges();
            return RedirectToAction("TableTypePlugin");
        }

        //Редактирование производителя
        [HttpGet]
        public ActionResult EditTypePlugin(int Id)
        {
            TypePlugin typePlugin = db.TypePlugins.Find(Id);
            return View(typePlugin);
        }

        [HttpPost]
        public ActionResult EditTypePlugin(TypePlugin typePlugin)
        {
            db.Entry(typePlugin).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("TableTypePlugin");
        }
    }
}