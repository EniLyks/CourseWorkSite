using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace СourseWorkSite.Models
{
    public class DAW
    {
        [Key]
        public int DAWId { get; set; }

        [Display(Name = "Название DAW")]
        [Required]
        [StringLength(50,ErrorMessage = "Достигнута максимальная длина строки")]
        public string DAWName { get; set; }

        [Display(Name = "Производитель")]
        [Required]
        public int ProducerId { get; set; }//Внешний ключ

        [Display(Name = "Цена")]
        [Range(0,double.MaxValue,ErrorMessage = "Поле не может быть отрицательным")]
        public double Cost { get; set; }

        [Display(Name = "Описание DAW")]
        [Required]
        public string Description { get; set; }

        public Producer Producer { get; set; }

    }
}