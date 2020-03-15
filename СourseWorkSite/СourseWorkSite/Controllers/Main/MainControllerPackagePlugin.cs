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
        //Страница с пакетами плагинов
        public ActionResult PagePackagePlugin(string sortOrder, string currentFilter, string searchString, int page = 1)
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

            IEnumerable<PackagePlugin> PackagePluginPerPages = db.PackagePlugins.Include(p => p.Producer);
            

            ViewBag.CurrentFilter = searchString;
            if (!String.IsNullOrEmpty(searchString))
            {
                PackagePluginPerPages = PackagePluginPerPages.Where(s => s.PackageName.ToUpper().Contains(searchString.ToUpper()));
            }
            int totalPackage = PackagePluginPerPages.Count();//Общее колчество производителей в БД
            switch (sortOrder)
            {
                case "Name desk":
                    PackagePluginPerPages = PackagePluginPerPages.OrderByDescending(x => x.PackageName).Skip((page - 1) * pageSize).Take(pageSize).ToList();
                    break;
                default:
                    PackagePluginPerPages = PackagePluginPerPages.OrderBy(x => x.PackageName).Skip((page - 1) * pageSize).Take(pageSize).ToList();
                    break;
            }


            PageInfo pageInfo = new PageInfo { PageNumber = page, PageSize = pageSize, TotalItems = totalPackage };
            PackagePluginViewModel ppvm = new PackagePluginViewModel { PageInfo = pageInfo, PackagePlugins = PackagePluginPerPages };
            return View(ppvm);
        }

        //Вывод детальной информации о пакете
        public ActionResult DetailsPackagePlugin(int Id)
        {
            PackagePlugin package = db.PackagePlugins.Find(Id);
            package.Producer = db.Producers.Find(package.ProducerId);
            package.Plugin = db.Plugins.Include(t => t.TypePlugin).Include(p => p.PackagePlugin).Where(p => p.PackageId == package.PackagePluginId).ToList();
            return View(package);
        }
    }
}