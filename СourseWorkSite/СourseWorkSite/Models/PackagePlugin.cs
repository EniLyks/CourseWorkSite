using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace СourseWorkSite.Models
{
    public class PackagePlugin
    {
        [Key]
        public int PackagePluginId { get; set; }

        [Display(Name = "Название пакета")]
        [Required]
        [StringLength(50, ErrorMessage = "Достигнута максимальная длина строки")]
        public string PackageName { get; set; }

        [Display(Name = "Производитель")]
        [Required]
        public int ProducerId { get; set; }//Внешний ключ

        [Display(Name = "Описание пакета")]
        [Required]
        public string Description { get; set; }


        public Producer Producer { get; set; }
        public ICollection<Plugin> Plugin { get; set; }//Коллекция входящих в пакет плагинов
    }
}