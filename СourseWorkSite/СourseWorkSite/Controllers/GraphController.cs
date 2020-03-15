using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using СourseWorkSite.Models;

namespace СourseWorkSite.Controllers
{
    public class GraphController : Controller
    {
        // GET: Graph

        DBSiteContext db = new DBSiteContext();

        public ActionResult GraphOut()
        {
            //Данные для диаграммы с количеством объектов на сайте
            ViewData["CountPlugins"] = db.Plugins.Count();
            ViewData["CountLibrarySamples"] = db.LibrarySamples.Count();
            ViewData["CountDAW"] = db.DAWs.Count();

            List<Producer> listAllProducer = db.Producers.Include(p => p.Plugins).Include(d => d.DAWs).Include(ls => ls.LibrarySamples).ToList();

            for (int i = 0; i < listAllProducer.Count; i++)
            {
                for (int j = 0; j < listAllProducer.Count; j++)
                {
                    if ((listAllProducer[i].Plugins.Count + listAllProducer[i].DAWs.Count + listAllProducer[i].LibrarySamples.Count) >
                        (listAllProducer[j].Plugins.Count + listAllProducer[j].DAWs.Count + listAllProducer[j].LibrarySamples.Count))
                    {
                        Producer prod = listAllProducer[j];
                        listAllProducer[j] = listAllProducer[i];
                        listAllProducer[i] = prod;
                    }
                }
            }

            for (int i = 0; i < 5; i++)
            {
                if (i != 4)
                {
                    ViewData["NameProducer" + (i + 1)] = listAllProducer[0].ProducerName;
                    ViewData["CountObjectProducer" + (i + 1)] = listAllProducer[0].Plugins.Count
                        + listAllProducer[0].DAWs.Count + listAllProducer[0].LibrarySamples.Count;
                    listAllProducer.RemoveAt(0);
                }
                else
                {
                    int countObject = 0;
                    foreach(Producer prod in listAllProducer)
                    {
                        countObject += prod.Plugins.Count
                        + prod.DAWs.Count + prod.LibrarySamples.Count;
                    }
                    ViewData["CountObjectProducerOther"] = countObject;
                }
            }

            List<TypePlugin> listTypePlugins = db.TypePlugins.Include(p => p.Plugins).ToList();
            listTypePlugins.RemoveAt(0);

            for (int i = 0; i < listTypePlugins.Count; i++)
            {
                ViewData["NameTypePlugin" + (i + 1)] = listTypePlugins[i].Type;
                ViewData["CountPlugin" + (i + 1)] = listTypePlugins[i].Plugins.Count;
            }

            return View();
        }


    }
}