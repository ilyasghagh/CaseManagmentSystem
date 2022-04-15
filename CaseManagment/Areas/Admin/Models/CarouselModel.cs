using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Case.web.Areas.Admin.Models
{
    public class CarouselModel
    {

        public int Id { get; set; }
        public string SliderText { get; set; }
        public string ImageName { get; set; }
        public string Title { get; set; }
        public IFormFile FormFile { get; set; }
    }
}
