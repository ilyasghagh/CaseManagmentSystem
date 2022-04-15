using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Case.web.Models
{
    public class CaseViewModel
    {
        public CaseViewModel()
        {
            Categories = new List<SelectListItem>();
            LaywersList = new List<SelectListItem>();
            JudgeList = new List<SelectListItem>();
            CourtList = new List<SelectListItem>();
            DistrictList = new List<SelectListItem>();
            ClientList = new List<SelectListItem>();
            ProvinceList = new List<SelectListItem>();
            CityList = new List<SelectListItem>();
            YeaList = new List<SelectListItem>()
            {
                new SelectListItem { Text="2022",Value="2022"},
                new SelectListItem { Text="2021",Value="2021"},
                new SelectListItem { Text="2020",Value="2020"},
                new SelectListItem { Text="2019",Value="2019"},
                new SelectListItem { Text="2018",Value="2018"},
                new SelectListItem { Text="2017",Value="2017"},
                new SelectListItem { Text="2016",Value="2016"},
                new SelectListItem { Text="2015",Value="2015"},
                new SelectListItem { Text="2014",Value="2014"},
                new SelectListItem { Text="2013",Value="2013"},
            };
            StationList = new List<SelectListItem>();
            FilesAttached = new List<AttachFiles>();
            HearingDate = DateTime.UtcNow;
        }
        public int Id { get; set; }
        [Required]
        [Display(Name = "Case Title")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "CNIC")]
        public string Cnic { get; set; }
        [Required]
        public string Mobile { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        [Display(Name="Case Category")]
        public int CategoryId { get; set; }
        [Required]
        [Display(Name = "Lawyer")]
        public int LawyerId { get; set; }
        [Required]
        [Display(Name = "Judge")]
        public int JudgeId { get; set; }
        [Required]
        [Display(Name = "Client")]
        public int ClientId { get; set; }
        [Required]
        [Display(Name = "Province")]
        public int ProvinceId { get; set; }
        [Required]
        [Display(Name = "City")]
        public int CityId { get; set; }
        [Required]
        [Display(Name = "Police Station")]
        public int PoliceStationId { get; set; }
        [Required]
        [Display(Name = "District Name")]
        public int DistrictId { get; set; }
        [Required]
        [Display(Name = "Court Name")]
        public int CourtId { get; set; }
        [Required]
        [Display(Name = "FIR No")]
        public string FirNo { get; set; }
        [Required]
        [Display(Name = "FIR Year")]
        public int FirYear { get; set; }
        [Required]
        [Display(Name = "Case Detail")]
        public string CaseDetail { get; set; }
        [Required]
        [Display(Name = "Hearing Date")]
        public DateTime HearingDate { get; set; }
        public List<SelectListItem> Categories { get; set; }
        public List<SelectListItem> LaywersList { get; set; }
        public List<SelectListItem> JudgeList { get; set; }
        public List<SelectListItem> ClientList { get; set; }
        public List<SelectListItem> ProvinceList { get; set; }
        public List<SelectListItem> CityList { get; set; }
        public List<SelectListItem> StationList { get; set; }
        public List<SelectListItem> YeaList { get; set; }
        public List<SelectListItem> DistrictList { get; set; }
        public List<SelectListItem> CourtList { get; set; }
        [Display(Name = "File")]
        public List<IFormFile> FormFiles { get; set; }
        public List<AttachFiles> FilesAttached { get; set; }
        public class AttachFiles
        {
            public int FileId { get; set; }
            public string FilePath { get; set; }
            public string FileName { get; set; }
        }
    }
}
