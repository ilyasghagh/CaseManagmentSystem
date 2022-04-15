using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Case.web.Models
{
    public class LawyerSearchModel:CaseViewModel
    {
        public LawyerSearchModel()
        {
            caseLists = new List<CaseList>();
        }
        public List<CaseList> caseLists { get; set; }
        public class CaseList
        {         
            public int Id { get; set; }
            public string Name { get; set; }
            public string Email { get; set; }
            public string judgeName { get; set; }
            public string HearingDate { get; set; }
        }
    }
}
