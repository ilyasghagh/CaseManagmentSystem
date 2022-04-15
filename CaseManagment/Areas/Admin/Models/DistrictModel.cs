using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Case.web.Areas.Admin.Models
{
    public class DistrictModel
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "District Name")]
        public string DistirictName { get; set; }
    }
}
