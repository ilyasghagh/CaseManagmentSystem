using System;
using System.Collections.Generic;
using System.Text;

namespace Case.Data.Domains
{
    public class Cases : BaseEntity
    {
        public string Name { get; set; }
        public string Cnic { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public int CategoryId { get; set; }
        public int LawyerId { get; set; }
        public int JudgeId { get; set; }
        public int ClientId { get; set; }
        public int ProvinceId { get; set; }
        public int CityId { get; set; }
        public int PoliceStationId { get; set; }
        public string FirNo { get; set; }
        public int FirYear { get; set; }
        public string CaseDetail { get; set; }
        public string Address { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public DateTime HearingDate { get; set; }
        public int DistrictId { get; set; }
        public int CourtId { get; set; }
    }
}
