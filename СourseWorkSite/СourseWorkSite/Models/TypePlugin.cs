using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace СourseWorkSite.Models
{
    public class TypePlugin
    {
        [Key]
        public int TypePluginId { get; set; }

        [Display(Name = "Имя типа")]
        [Required]
        [StringLength(50, ErrorMessage = "Достигнута максимальная длина строки")]
        public string Type { get; set; }

        [Display(Name = "Описание")]
        [Required]
        public string Description { get; set; }

        public ICollection<Plugin> Plugins { get; set; }
    }
}