using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using СourseWorkSite.Models;

namespace СourseWorkSite.Controllers
{
    public partial class MainController : Controller
    {
        //Страница с Производителями
        public ActionResult PageProducer(string sortOrder, string currentFilter, string searchString, int page = 1)
        {
            int pageSize = globalPageSize;//Количество объектов на странице
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParam = String.IsNullOrEmpty(sortOrder) ? "Name desk" : "";
            if (Request.HttpMethod == "GET")
            {
                searchString = currentFilter;
            }
            else
            {
                page = 1;
            }

            // SelectList producers = new SelectList(db.Producers, "ProducerId", "ProducerName");
            IEnumerable<Producer> ProducersPerPages = db.Producers;
            int totalProducer = ProducersPerPages.Count();//Общее колчество производителей в БД

            ViewBag.CurrentFilter = searchString;
            if (!String.IsNullOrEmpty(searchString))
            {
                ProducersPerPages = ProducersPerPages.Where(s => s.ProducerName.ToUpper().Contains(searchString.ToUpper()));
            }

            switch (sortOrder)
            {
                case "Name desk":
                    ProducersPerPages = ProducersPerPages.OrderByDescending(x => x.ProducerName).Skip((page - 1) * pageSize).Take(pageSize).ToList();
                    break;
                default:
                    ProducersPerPages = ProducersPerPages.OrderBy(x => x.ProducerName).Skip((page - 1) * pageSize).Take(pageSize).ToList();
                    break;
            }


            PageInfo pageInfo = new PageInfo { PageNumber = page, PageSize = pageSize, TotalItems = totalProducer };
            ProducerViewModel pim = new ProducerViewModel { PageInfo = pageInfo, Producers = ProducersPerPages };
            return View(pim);
        }


        //Вывод детальной информации о производителе
        public ActionResult DetailsProducer(int Id)
        {
            Producer producer = db.Producers.Find(Id);
            producer.Plugins = db.Plugins.Include(p => p.Producer).Include(p => p.TypePlugin).Include(p => p.PackagePlugin).Where(p => p.ProducerId == producer.ProducerId).ToList();
            producer.DAWs = db.DAWs.Include(p => p.Producer).Where(p => p.ProducerId == producer.ProducerId).ToList();
            producer.LibrarySamples = db.LibrarySamples.Include(p => p.Producer).Where(p => p.ProducerId == producer.ProducerId).ToList();
            producer.PackagePlugins = db.PackagePlugins.Include(p => p.Producer).Where(p => p.ProducerId == producer.ProducerId).ToList();
            return View(producer);
        }
    }
}