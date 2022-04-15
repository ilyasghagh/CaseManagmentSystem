using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Case.web.Areas.Admin.Models
{
    public class CityModel
    {
        public CityModel()
        {
            ProvinceList = new List<SelectListItem>();
        }
        public int Id { get; set; }
        [Required]
        [Display(Name = "Province Name")]
        public int ProvinceId { get; set; }
        [Required]
        [Display(Name = "City Name")]
        public string CityName { get; set; }
        public string ProvinceName { get; set; }
        public List<SelectListItem> ProvinceList { get; set; }
    }
}
