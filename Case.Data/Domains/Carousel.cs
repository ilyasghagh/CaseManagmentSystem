using System;
using System.Collections.Generic;
using System.Text;

namespace Case.Data.Domains
{
    public class Carousel :BaseEntity
     {
        public string SliderText { get; set; }
        public string ImageName { get; set; }
        public string Title { get; set; }
    }
}
