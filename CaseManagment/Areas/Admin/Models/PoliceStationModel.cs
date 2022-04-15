using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Case.web.Areas.Admin.Models
{
    public class PoliceStationModel
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Police Station Name")]
        public string StationName { get; set; }
    }
}
