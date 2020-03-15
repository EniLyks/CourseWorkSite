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
        //Страница с библиотеками семплов
        public ActionResult PageLibrarySample(string sortOrder, string currentFilter, string searchString, int page = 1)
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

            IEnumerable<LibrarySample> LibraryPerPages = db.LibrarySamples.Include(p => p.Producer);
            int totalLibrarys = LibraryPerPages.Count();//Общее колчество DAW в БД

            ViewBag.CurrentFilter = searchString;
            if (!String.IsNullOrEmpty(searchString))
            {
                LibraryPerPages = LibraryPerPages.Where(s => s.LibraryName.ToUpper().Contains(searchString.ToUpper()));
            }

            switch (sortOrder)
            {
                case "Name desk":
                    LibraryPerPages = LibraryPerPages.OrderByDescending(x => x.LibraryName).Skip((page - 1) * pageSize).Take(pageSize).ToList();
                    break;
                case "Cost":
                    LibraryPerPages = LibraryPerPages.OrderBy(x => x.Cost).Skip((page - 1) * pageSize).Take(pageSize).ToList();
                    break;
                case "Cost desk":
                    LibraryPerPages = LibraryPerPages.OrderByDescending(x => x.Cost).Skip((page - 1) * pageSize).Take(pageSize).ToList();
                    break;
                default:
                    LibraryPerPages = LibraryPerPages.OrderBy(x => x.LibraryName).Skip((page - 1) * pageSize).Take(pageSize).ToList();
                    break;
            }


            PageInfo pageInfo = new PageInfo { PageNumber = page, PageSize = pageSize, TotalItems = totalLibrarys };
            LibrarySampleViewModel lsvm = new LibrarySampleViewModel { PageInfo = pageInfo, LibrarySamples = LibraryPerPages };
            return View(lsvm);
        }

        public ActionResult DetailsLibrarySample(int id)
        {
            LibrarySample library = db.LibrarySamples.Find(id);
            library.Producer = db.Producers.Find(library.ProducerId);

            return View(library);
        }
    }
}