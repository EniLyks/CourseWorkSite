using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace СourseWorkSite.Models
{
    public class Producer
    {
        [Key]
        public int ProducerId { get; set; }

        [Display(Name = "Название производителя")]
        [Required]
        [StringLength(50, ErrorMessage = "Достигнута максимальная длина строки")]
        public string ProducerName { get; set; }

        [Display(Name = "Описание")]
        [Required]
        public string Description { get; set; }

        [Display(Name = "Сайт производителя")]
        [Required]
        [StringLength(50, ErrorMessage = "Достигнута максимальная длина строки")]
        //[RegularExpression(@"http://+[]")]
        public string Site { get; set; }

        public ICollection<DAW> DAWs { get; set; }
        public ICollection<Plugin> Plugins { get; set; }
        public ICollection<LibrarySample> LibrarySamples { get; set; }
        public ICollection<PackagePlugin> PackagePlugins { get; set; }

        public Producer()
        {
            DAWs = new List<DAW>();
            Plugins = new List<Plugin>();
            LibrarySamples = new List<LibrarySample>();
            PackagePlugins = new List<PackagePlugin>();
        }
    }
}