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
        //
        public ActionResult TableLibrarySample(int? producerId, string searchString)
        {
            var library = db.LibrarySamples.Include(p => p.Producer);
            SelectList producers = new SelectList(db.Producers, "ProducerId", "ProducerName");
            ViewBag.Producers = producers;

            if ((producerId != 0) && (producerId != null))
            {
                library = library.Where(p => p.ProducerId == producerId);
            }

            if (!String.IsNullOrEmpty(searchString))
            {
                library = library.Where(s => s.LibraryName.ToUpper().Contains(searchString.ToUpper()));
            }

            return View(library.ToList());
        }

        [HttpGet]
        public ActionResult CreateLibrarySample()
        {
            SelectList producers = new SelectList(db.Producers, "ProducerId", "ProducerName");
            ViewBag.Producers = producers;
            return View();
        }

        [HttpPost]
        public ActionResult CreateLibrarySample(LibrarySample library)
        {
            db.LibrarySamples.Add(library);
            db.SaveChanges();
            return RedirectToAction("TableLibrarySample");
        }

        [HttpGet]
        public ActionResult EditLibrarySample(int Id)
        {
            LibrarySample library = db.LibrarySamples.Find(Id);
            SelectList producers = new SelectList(db.Producers, "ProducerId", "ProducerName");
            ViewBag.Producers = producers;
            return View(library);
        }

        [HttpPost]
        public ActionResult EditLibrarySample(LibrarySample library)
        {
            db.Entry(library).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("TableLibrarySample");
        }

        //Удаление библиотеки
        public ActionResult DeleteLibrarySample(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            LibrarySample library = db.LibrarySamples.Find(id);
            if (library != null)
            {
                db.LibrarySamples.Remove(library);
                db.SaveChanges();
            }
            return RedirectToAction("TableLibrarySample");
        }
    }
}