using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Case.web.Areas.Admin.Models
{
    public class ProvinceModel
    {
        public int Id { get; set; }
        [Required]
        [Display(Name ="Province Name")]
        public string ProvinceName { get; set; }
    }
}
