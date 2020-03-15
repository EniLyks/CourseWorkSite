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
        DBSiteContext db = new DBSiteContext();
        Admin admin;
        bool globalLoginFailed = false;

        public ActionResult AdminPanel()
        {
            admin = db.Admins.Find(0);//Извлекаем админа из БД
            if (admin.AdminStatus == 1)
            {
                admin.AdminStatus = 0;
                db.Entry(admin).State = EntityState.Modified;
                db.SaveChanges();
                return View("AdminPanel");
            }
            else
                return RedirectToAction("GainAccess");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpGet]
       public ActionResult GainAccess()
        {
            if (globalLoginFailed)
            {
                ViewBag.Fail = "ОШИБКА ДОСТУПА!!! ВВЕДИТЕ ДЕЙСТВУЮЩИЙ ЛОГИН И ПАРОЛЬ!!!";
                globalLoginFailed = false;
            }
            else
            { ViewBag.Fail = ""; }

            return View();
        }

        [HttpPost]
        public ActionResult GainAccess(string login, string password)
        {
            admin = db.Admins.Find(0);//Извлекаем админа из БД
            if ((login == admin.AdminName) && (password == admin.AdminPassword))
            {
                admin.AdminStatus = 1;
                db.Entry(admin).State = EntityState.Modified;
                db.SaveChanges();
                return AdminPanel();
            }
            else
            {
                globalLoginFailed = true;
                return GainAccess();
            }
        }

    }
}