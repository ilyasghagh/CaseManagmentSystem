using System;
using System.Collections.Generic;
using System.Text;

namespace Case.Data.Domains
{
    public class Cities:BaseEntity
    {
        public int ProvinceId { get; set; }
        public string CityName { get; set; }
    }
}
