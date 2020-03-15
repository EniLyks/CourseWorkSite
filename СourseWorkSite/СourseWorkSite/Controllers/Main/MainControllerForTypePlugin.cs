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
        //Вывод детальной информации о типе плагина
        public ActionResult DetailsTypePlugin(int id)
        {
            TypePlugin typePlugin = db.TypePlugins.Find(id);
            return View(typePlugin);
        }
    }
}