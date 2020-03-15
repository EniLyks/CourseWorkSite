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
        public ActionResult TableDAW(int? producerId, string searchString)
        {
            var dAW = db.DAWs.Include(p => p.Producer);
            SelectList producers = new SelectList(db.Producers, "ProducerId", "ProducerName");
            ViewBag.Producers = producers;

            if ((producerId != 0) && (producerId != null))
            {
                dAW = dAW.Where(p => p.ProducerId == producerId);
            }

            if (!String.IsNullOrEmpty(searchString))
            {
                dAW = dAW.Where(s => s.DAWName.ToUpper().Contains(searchString.ToUpper()));
            }

            return View(dAW.ToList());
        }

        [HttpGet]
        public ActionResult CreateDAW()
        {
            SelectList producers = new SelectList(db.Producers, "ProducerId", "ProducerName");
            ViewBag.Producers = producers;
            return View();
        }

        [HttpPost]
        public ActionResult CreateDAW(DAW dAW)
        {
            db.DAWs.Add(dAW);
            db.SaveChanges();
            return RedirectToAction("TableDAW");
        }

        [HttpGet]
        public ActionResult EditDAW(int Id)
        {
            DAW dAW = db.DAWs.Find(Id);
            SelectList producers = new SelectList(db.Producers, "ProducerId", "ProducerName");
            ViewBag.Producers = producers;
            return View(dAW);
        }

        [HttpPost]
        public ActionResult EditDAW(DAW dAW)
        {
            db.Entry(dAW).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("TableDAW");
        }

        //Удаление DAW
        public ActionResult DeleteDAW(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            DAW dAW = db.DAWs.Find(id);
            if (dAW != null)
            {
                db.DAWs.Remove(dAW);
                db.SaveChanges();
            }
            return RedirectToAction("TableDAW");
        }
    }
}