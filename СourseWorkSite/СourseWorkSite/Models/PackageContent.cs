using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace СourseWorkSite.Models
{
    //НЕНУЖНАЯ ТАБЛИЦА !!!
    public class PackageContent
    {
        [Key]
        [Column(Order = 0)]
        public string PluginName { get; set; }//Внешний ключ

        

        [Key]
        [Column(Order = 1)]
        public string PackageName { get; set; }//Внешний ключ

        [ForeignKey("PluginName")]
        public Plugin Plugin { get; set; }

        [ForeignKey("PackageName")]
        public PackagePlugin PackagePlugin { get; set; }

        PackageContent()
        {
            Plugin = null;
            PackagePlugin = null;
        }
    }

}