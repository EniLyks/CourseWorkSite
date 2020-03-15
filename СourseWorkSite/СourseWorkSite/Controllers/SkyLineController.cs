using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using СourseWorkSite.Models;

namespace СourseWorkSite.Controllers
{
    public class SkyLineController : Controller
    {
        DBSiteContext db = new DBSiteContext();
        // GET: SkyLine
        public ActionResult Index()
        {
            string src = UrlHelper.GenerateContentUrl("~/Picture/sink.jpg", this.HttpContext);
            ViewBag.Picture = src;
            return View();
        }
    }
}