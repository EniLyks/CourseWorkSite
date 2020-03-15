using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace СourseWorkSite.Models
{
    public class DBSiteContext : DbContext
    {
        public DbSet<Producer> Producers { get; set; }
        public DbSet<Plugin> Plugins { get; set; }
        public DbSet<DAW> DAWs { get; set; }
        public DbSet<TypePlugin> TypePlugins { get; set; }
        public DbSet<LibrarySample> LibrarySamples { get; set; }
        public DbSet<PackagePlugin> PackagePlugins { get; set; }
        public DbSet<Admin> Admins { get; set; }
        //public DbSet<PackageContent> PackageContents { get; set; }

    }
}