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

        //Вывод таблицы плагинов в админской панели
        public ActionResult TablePlugin(int? producerId, int? typePluginId, int? packagePluginId, string searchString)
        {
            var plugin = db.Plugins.Include(p => p.Producer).Include(p => p.TypePlugin).Include(p => p.PackagePlugin);
            SelectList typePlugins = new SelectList(db.TypePlugins, "TypePluginId", "Type");
            SelectList producers = new SelectList(db.Producers, "ProducerId", "ProducerName");
            SelectList packagePlugins = new SelectList(db.PackagePlugins, "PackagePluginId", "PackageName");
            ViewBag.TypePlugins = typePlugins;
            ViewBag.Producers = producers;
            ViewBag.PackagePlugins = packagePlugins;

            if((producerId!=0)&&(producerId!=null))
            {
                plugin = plugin.Where(p => p.ProducerId == producerId);
            }
            if ((typePluginId != 0) && (typePluginId != null))
            {
                plugin = plugin.Where(p => p.TypePluginId == typePluginId);
            }
            if ((packagePluginId != 0) && (packagePluginId != null))
            {
                plugin = plugin.Where(p => p.PackageId == packagePluginId);
            }

            if (!String.IsNullOrEmpty(searchString))
            {
                plugin = plugin.Where(s => s.PluginName.ToUpper().Contains(searchString.ToUpper()));
            }

            return View(plugin.ToList());
        }

        //Редактирование таблицы плагинов
        [HttpGet]
        public ActionResult EditPlugin(int Id)
        {
            Plugin plugin = db.Plugins.Find(Id);
            SelectList typePlugins = new SelectList(db.TypePlugins, "TypePluginId", "Type");
            SelectList producers = new SelectList(db.Producers, "ProducerId", "ProducerName");
            SelectList packagePlugins = new SelectList(db.PackagePlugins, "PackagePluginId", "PackageName");
            ViewBag.TypePlugins = typePlugins;
            ViewBag.Producers = producers;
            ViewBag.PackagePlugins = packagePlugins;
            return View(plugin);
        }

        [HttpPost]
        public ActionResult EditPlugin(Plugin plugin)
        {
            db.Entry(plugin).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("TablePlugin");
        }

        //Создание нового плагина в таблице
        [HttpGet]
        public ActionResult CreatePlugin()
        {
            //Plugin plugin = db.Plugins.Find(Name);
            SelectList typePlugins = new SelectList(db.TypePlugins, "TypePluginId", "Type");
            SelectList producers = new SelectList(db.Producers, "ProducerId", "ProducerName");
            SelectList packagePlugins = new SelectList(db.PackagePlugins, "PackagePluginId", "PackageName");
            ViewBag.TypePlugins = typePlugins;
            ViewBag.Producers = producers;
            ViewBag.PackagePlugins = packagePlugins;
            return View();
        }

        [HttpPost]
        public ActionResult CreatePlugin(Plugin plugin)
        {
            db.Plugins.Add(plugin);
            db.SaveChanges();
            return RedirectToAction("TablePlugin");
        }

        //Удаление плагина
        public ActionResult DeletePlugin(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            Plugin plugin = db.Plugins.Find(id);
            if (plugin != null)
            {
                db.Plugins.Remove(plugin);
                db.SaveChanges();
            }
            return RedirectToAction("TablePlugin");
        }
    }
}