using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Case.web.Areas.Admin.Models
{
    public class CaseCategoryModel
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Category Name")]
        public string CategoryName { get; set; }
    }
}
