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
        //Страница с DAW
        public ActionResult PageDAW(string sortOrder, string currentFilter, string searchString, int page = 1)
        {
            int pageSize = globalPageSize;//Количество объектов на странице
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParam = String.IsNullOrEmpty(sortOrder) ? "Name desk" : "";
            ViewBag.CostSortParam = sortOrder == "Cost" ? "Cost desk" : "Cost";
            if (Request.HttpMethod == "GET")
            {
                searchString = currentFilter;
            }
            else
            {
                page = 1;
            }

            SelectList producers = new SelectList(db.Producers, "ProducerId", "ProducerName");
            IEnumerable<DAW> DAWsPerPages = db.DAWs.Include(p => p.Producer);
            int totalDAW = DAWsPerPages.Count();//Общее колчество DAW в БД

            ViewBag.CurrentFilter = searchString;
            if (!String.IsNullOrEmpty(searchString))
            {
                DAWsPerPages = DAWsPerPages.Where(s => s.DAWName.ToUpper().Contains(searchString.ToUpper()));
            }

            switch (sortOrder)
            {
                case "Name desk":
                    DAWsPerPages = DAWsPerPages.OrderByDescending(x => x.DAWName).Skip((page - 1) * pageSize).Take(pageSize).ToList();
                    break;
                case "Cost":
                    DAWsPerPages = DAWsPerPages.OrderBy(x => x.Cost).Skip((page - 1) * pageSize).Take(pageSize).ToList();
                    break;
                case "Cost desk":
                    DAWsPerPages = DAWsPerPages.OrderByDescending(x => x.Cost).Skip((page - 1) * pageSize).Take(pageSize).ToList();
                    break;
                default:
                    DAWsPerPages = DAWsPerPages.OrderBy(x => x.DAWName).Skip((page - 1) * pageSize).Take(pageSize).ToList();
                    break;
            }


            PageInfo pageInfo = new PageInfo { PageNumber = page, PageSize = pageSize, TotalItems = totalDAW };
            DAWViewModel div = new DAWViewModel { PageInfo = pageInfo, DAWs = DAWsPerPages };
            return View(div);
        }

        //Вывод детальной информации о DAW
        public ActionResult DetailsDAW(int Id)
        {
            DAW dAW = db.DAWs.Find(Id);
            Producer producer = db.Producers.Find(dAW.ProducerId);
            ViewBag.Producer = producer.ProducerName;
            return View(dAW);
        }
    }
}