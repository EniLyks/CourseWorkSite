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

        int globalPageSize = 10;//Переменная с количеством объектов на странице

        DBSiteContext db = new DBSiteContext();
        // GET: Main

        //Главная страница
        public ActionResult HomePage()
        {
            return View();
        }
        
    }
}