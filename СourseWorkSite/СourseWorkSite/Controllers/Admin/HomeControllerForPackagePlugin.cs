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
        //Вывод таблицы пакетов плагинов в админской панели
        public ActionResult TablePackagePlugin(string searchString)
        {
            var package = db.PackagePlugins.Include(p => p.Producer);
            SelectList producers = new SelectList(db.Producers, "ProducerId", "ProducerName");
            ViewBag.Producers = producers;

            if (!String.IsNullOrEmpty(searchString))
            {
                package = package.Where(s => s.PackageName.ToUpper().Contains(searchString.ToUpper()));
            }

            return View(package.ToList());
        }

        [HttpGet]
        public ActionResult CreatePackagePlugin()
        {
            SelectList producers = new SelectList(db.Producers, "ProducerId", "ProducerName");
            ViewBag.Producers = producers;
            return View();
        }

        [HttpPost]
        public ActionResult CreatePackagePlugin(PackagePlugin package)
        {
            db.PackagePlugins.Add(package);
            db.SaveChanges();
            return RedirectToAction("TablePackagePlugin");
        }

        [HttpGet]
        public ActionResult EditPackagePlugin(int Id)
        {
            PackagePlugin package = db.PackagePlugins.Find(Id);
            SelectList producers = new SelectList(db.Producers, "ProducerId", "ProducerName");
            ViewBag.Producers = producers;
            return View(package);
        }

        [HttpPost]
        public ActionResult EditPackagePlugin(PackagePlugin package)
        {
            db.Entry(package).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("TablePackagePlugin");
        }
    }
}