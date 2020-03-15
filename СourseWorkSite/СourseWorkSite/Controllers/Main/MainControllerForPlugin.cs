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
        //Страница с плагинами
        public ActionResult PagePlugin(string sortOrder, string currentFilter, string searchString, int? typePluginId, int page = 1)
        {
            int pageSize = globalPageSize;//Количество объектов на странице
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParam = String.IsNullOrEmpty(sortOrder) ? "Name desk" : "";
            ViewBag.CostSortParam = sortOrder == "Cost" ? "Cost desk" : "Cost";
            
            IEnumerable<Plugin> PluginsPerPages = db.Plugins.Include(p => p.Producer).Include(t => t.TypePlugin).Include(p => p.PackagePlugin);
            

            //Фильтрация по типу
            if ((typePluginId != 0) && (typePluginId != null))
            {
                PluginsPerPages = PluginsPerPages.Where(p => p.TypePluginId == typePluginId);
            }


            if (Request.HttpMethod == "GET")
            {
                searchString = currentFilter;
                
            }
            else
            {
                page = 1;
            }

            

            ViewBag.CurrentFilter = searchString;
            if (!String.IsNullOrEmpty(searchString))
            {
                PluginsPerPages = PluginsPerPages.Where(s => s.PluginName.ToUpper().Contains(searchString.ToUpper()));
            }

            int totalPlugins = PluginsPerPages.Count();//Общее колчество плагинов в БД

            switch (sortOrder)
            {
                case "Name desk":
                    PluginsPerPages = PluginsPerPages.OrderByDescending(x => x.PluginName).Skip((page - 1) * pageSize).Take(pageSize).ToList();
                    break;
                case "Cost":
                    PluginsPerPages = PluginsPerPages.OrderBy(x => x.Cost).Skip((page - 1) * pageSize).Take(pageSize).ToList();
                    break;
                case "Cost desk":
                    PluginsPerPages = PluginsPerPages.OrderByDescending(x => x.Cost).Skip((page - 1) * pageSize).Take(pageSize).ToList();
                    break;
                default:
                    PluginsPerPages = PluginsPerPages.OrderBy(x => x.PluginName).Skip((page - 1) * pageSize).Take(pageSize).ToList();
                    break;
            }
           
            PageInfo pageInfo = new PageInfo { PageNumber = page, PageSize = pageSize, TotalItems = totalPlugins };
            PluginViewModel pvm = new PluginViewModel { PageInfo = pageInfo, Plugins = PluginsPerPages };
            return View(pvm);
        }


        public ActionResult DetailsPlugin(int id)
        {
            Plugin plugin = db.Plugins.Find(id);
            plugin.Producer = db.Producers.Find(plugin.ProducerId);
            plugin.TypePlugin = db.TypePlugins.Find(plugin.TypePluginId);
            plugin.PackagePlugin = db.PackagePlugins.Find(plugin.PackageId);

            return View(plugin);
        }
    }
}