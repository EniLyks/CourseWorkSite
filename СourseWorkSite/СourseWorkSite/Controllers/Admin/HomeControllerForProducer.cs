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
        //Вывод списка производителей в виде таблицы
        public ActionResult TableProducer(string searchString)
        {
            IQueryable producer;

            if (!String.IsNullOrEmpty(searchString))
            {
                producer = db.Producers.Where(s => s.ProducerName.ToUpper().Contains(searchString.ToUpper()));
            }
            else
            {
                producer = db.Producers;
            }
            return View(producer);
        }

        //Создание нового производителя
        [HttpGet]
        public ActionResult CreateProducer()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateProducer(Producer producer)
        {
            db.Producers.Add(producer);
            db.SaveChanges();
            return RedirectToAction("TableProducer");
        }

        //Редактирование производителя
        [HttpGet]
        public ActionResult EditProducer(int Id)
        {
            Producer producer = db.Producers.Find(Id);
            return View(producer);
        }

        [HttpPost]
        public ActionResult EditProducer(Producer producer)
        {
            db.Entry(producer).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("TableProducer");
        }

    }
}