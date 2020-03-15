using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace СourseWorkSite.Models
{
    public class LibrarySample
    {
        [Key]
        public int LibrarySampleId { get; set; }

        [Display(Name = "Название библиотеки")]
        [Required]
        [StringLength(50, ErrorMessage = "Достигнута максимальная длина строки")]
        public string LibraryName { get; set; }

        [Display(Name = "Производитель")]
        [Required]
        public int ProducerId { get; set; }//Внешний ключ

        [Display(Name = "Формат")]
        [Required]
        [StringLength(50, ErrorMessage = "Достигнута максимальная длина строки")]
        public string Format { get; set; }

        [Display(Name = "Цена")]
        [Range(0, double.MaxValue, ErrorMessage = "Поле не может быть отрицательным")]
        public double Cost { get; set; }

        [Display(Name = "Описание библиотеки")]
        [Required]
        public string Description { get; set; }

        public Producer Producer { get; set; }
    }
}