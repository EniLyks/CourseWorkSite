using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace СourseWorkSite.Models
{
    public class Plugin
    {
        [Key]
        public int PluginId { get; set; }

        [Display(Name = "Название плагина")]
        [Required]
        [StringLength(50, ErrorMessage = "Достигнута максимальная длина строки")]
        public string PluginName { get; set; }

        [Display(Name = "Производитель")]
        [Required]
        public int ProducerId { get; set; }//Внешний ключ

        [Display(Name = "Входит в пакет")]
        [Required]
        public int PackageId { get; set; }//Внешний ключ

        [Display(Name = "Тип")]
        [Required]
        public int TypePluginId { get; set; }//Внешний ключ

        [Display(Name = "Цена")]
        [Range(0, double.MaxValue, ErrorMessage = "Поле не может быть отрицательным")]
        public double Cost { get; set; }

        [Display(Name = "Описание плагина")]
        [Required]
        public string Description { get; set; }

        public Producer Producer { get; set; }
        public TypePlugin TypePlugin { get; set; }
        [ForeignKey("PackageId")]
        public PackagePlugin PackagePlugin { get; set; }
            
    }
}