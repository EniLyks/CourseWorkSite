using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace СourseWorkSite.Models
{
    public class PageInfo
    {
        public int PageNumber { get; set; } // номер текущей страницы
        public int PageSize { get; set; } // кол-во объектов на странице
        public int TotalItems { get; set; } // всего объектов
        public int TotalPages  // всего страниц
        {
            get { return (int)Math.Ceiling((decimal)TotalItems / PageSize); }
        }
    }

    //Связь пагинации с таблицей DAW
    public class DAWViewModel
    {
        public IEnumerable<DAW> DAWs { get; set; }
        public PageInfo PageInfo { get; set; }
    }

    public class ProducerViewModel
    {
        public IEnumerable<Producer> Producers { get; set; }
        public PageInfo PageInfo { get; set; }
    }

    public class PluginTypeViewModel
    {
        public IEnumerable<TypePlugin> TypePlugins { get; set; }
        public PageInfo PageInfo { get; set; }
     }

    public class PackagePluginViewModel
    {
        public IEnumerable<PackagePlugin> PackagePlugins { get; set; }
        public PageInfo PageInfo { get; set; }
    }

    public class LibrarySampleViewModel
    {
        public IEnumerable<LibrarySample> LibrarySamples { get; set; }
        public PageInfo PageInfo { get; set; }
    }

    public class PluginViewModel
    {
        public IEnumerable<Plugin> Plugins { get; set; }
        public PageInfo PageInfo { get; set; }
    }
}