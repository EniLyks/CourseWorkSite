using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace СourseWorkSite.Models
{
    public class Admin
    {
        [Key]
        public int AdminId { get; set; }

        [StringLength(50, ErrorMessage = "Достигнута максимальная длина строки")]
        public string AdminName { get; set; }

        [StringLength(50, ErrorMessage = "Достигнута максимальная длина строки")]
        public string AdminPassword { get; set; }


        public int AdminStatus { get; set; }
    }
}