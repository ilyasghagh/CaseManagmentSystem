using Case.Data.Domains;
using Case.web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Case.Services;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.IO;
using Microsoft.AspNetCore.Http;
using System.Text.RegularExpressions;

namespace Case.web.Factories.UserFactory
{
    public class UserModelFactory : IUserModelFactory
    {
        private readonly IUserService _userService;
        private readonly IRolesService  _rolesService;
        private readonly IUserRoleService _userRoleService;
        private readonly IPoliceStationService _policeStationService;
        private readonly ICaseCategoryService _caseCategoryService;
        private readonly ICaseService _caseService;
        private readonly ICaseDocumentService _documentService;
        private readonly IHttpContextAccessor  _httpContextAccessor;
        private readonly ICourtService _courtService;
        private readonly IDistirictService _distirictService;
        private readonly IProvinceService _provinceService;
        private readonly ICityService _cityService;
        public UserModelFactory(IUserService userService,
            IUserRoleService userRoleService,
            IRolesService rolesService,
            ICaseCategoryService caseCategoryService,
            IPoliceStationService policeStationService,
            ICaseService caseService,
            ICaseDocumentService documentService,
            IHttpContextAccessor httpContextAccessor,
            ICourtService courtService,
            IDistirictService distirictService,
            IProvinceService provinceService,
            ICityService cityService)
        {
            _userService = userService;
            _rolesService = rolesService;
            _userRoleService = userRoleService;
            _policeStationService = policeStationService;
            _caseCategoryService = caseCategoryService;
            _caseService = caseService;
            _documentService = documentService;
            _httpContextAccessor = httpContextAccessor;
            _courtService = courtService;
            _distirictService = distirictService;
            _provinceService = provinceService;
            _cityService = cityService;
        }
        public UserViewModel PrepareUserModel(User user,UserViewModel model)
        {
            //var model = new UserViewModel();
            model.roles = _rolesService.GetAllRoles().Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.RoleName });
            if (user == null)
                return model;
            else
            {
                model.Email = user.Email;
                model.FirstName = user.FirstName;
                model.LastName = user.LastName;            
                model.username = user.username;
                model.Phone = user.Phone;
                model.Gender = user.Gender;
                model.Address = user.Address;
                model.DateOfBirth = user.DateOfBirth;
                model.Id = user.Id;
                model.selectedRoles = _userRoleService.GetAllRolesById(user.Id).Select(x => x.RoleId).ToList();
            }
            return model;
        }
        public CaseViewModel PrepareCaseModel(Cases cas , CaseViewModel model)
        {
            model.Categories = _caseCategoryService.GetAll().Select(x => new SelectListItem { Text = x.CategoryName, Value = x.Id.ToString() }).ToList();
            model.StationList = _policeStationService.GetAll().Select(x => new SelectListItem { Text = x.SatationName, Value = x.Id.ToString() }).ToList();
            model.LaywersList = _userService.GetAllUsersByrole("lawyer").Select(x => new SelectListItem { Text = x.FirstName +" "+ x.LastName, Value = x.Id.ToString() }).ToList();
            model.JudgeList = _userService.GetAllUsersByrole("judge").Select(x => new SelectListItem { Text = x.FirstName +" "+ x.LastName, Value = x.Id.ToString() }).ToList();                         
            model.ClientList = _userService.GetAllUsersByrole("client").Select(x => new SelectListItem { Text = x.FirstName +" "+ x.LastName, Value = x.Id.ToString() }).ToList();                         
            model.DistrictList = _distirictService.GetAll().Select(x => new SelectListItem { Text = x.DistirictName , Value = x.Id.ToString() }).ToList();                         
            model.CourtList = _courtService.GetAll().Select(x => new SelectListItem { Text = x.CourtName, Value = x.Id.ToString() }).ToList(); 
            model.ProvinceList = _provinceService.GetAll().Select(x => new SelectListItem { Text = x.ProvinceName, Value = x.Id.ToString() }).ToList();            
            if (cas == null)
                return model;
            else
            {
                model.Id = cas.Id;
                model.LawyerId = cas.LawyerId;
                model.JudgeId = cas.JudgeId;
                model.DistrictId = cas.DistrictId;
                model.CourtId = cas.CourtId;
                model.FirYear = cas.FirYear;
                model.Mobile = cas.Mobile;
                model.Name = cas.Name;
                model.PoliceStationId = cas.PoliceStationId;
                model.Email = cas.Email;
                model.Cnic = cas.Cnic;               
                model.FirNo = cas.FirNo;
                model.ProvinceId = cas.ProvinceId;
                model.CityId = cas.CityId;
                model.ClientId = cas.ClientId;
                model.CategoryId = cas.CategoryId;
                model.CaseDetail = cas.CaseDetail;
                model.Address = cas.Address;
                model.HearingDate = cas.HearingDate;
                model.CityList = _cityService.GetAllByProvinceId(cas.ProvinceId).Select(x => new SelectListItem { Text = x.CityName, Value = x.Id.ToString() }).ToList();
                model.FilesAttached = _documentService.GetAllByCaseId(cas.Id).Select(x => new CaseViewModel.AttachFiles() { FileId = x.Id, FileName = x.FileName, FilePath = x.FilePath }).ToList();
            }
            return model;
        }
        public LawyerSearchModel PrepareLawyerList(LawyerSearchModel model)
        {
            
            var lists = PrepareCaseModel(null, new CaseViewModel());
            model.Categories = lists.Categories;
            model.LaywersList = lists.LaywersList;
            model.YeaList = lists.YeaList;
            model.StationList = lists.StationList;
            model.CourtList = lists.CourtList;
            model.DistrictList = lists.DistrictList;
            model.caseLists = _caseService.GetAllbyFilters(
                LawerId: model.LawyerId,
                JudgeId: model.JudgeId,
                CaseCategoryId: model.CategoryId,
                PoliceStationId: model.PoliceStationId,
                Name: model.Name,
                FirNo: model.FirNo,
                courtId:model.CourtId,
                districtId:model.DistrictId,
                CaseNumber:model.Id               
                ).Select(x => new LawyerSearchModel.CaseList() {
                    Id = x.Id,
                    Name = x.Name,
                    Email = x.Email,
                    judgeName = x.JudgeId > 0 ? _userService.GetUserFullNamebyId(x.JudgeId) : "",
                    HearingDate = x.HearingDate.ToString("dd-MM-yyyy")
                }).ToList();
            return model;
        }

        public void InsertNewCase(CaseViewModel cas)
        {
            var model = new Cases();           
            model.LawyerId = cas.LawyerId;
            model.JudgeId = cas.JudgeId;
            model.ProvinceId = cas.ProvinceId;
            model.CityId = cas.CityId;
            model.ClientId = cas.ClientId;
            model.FirYear = cas.FirYear;
            model.Mobile = cas.Mobile;
            model.Name = cas.Name;
            model.PoliceStationId = cas.PoliceStationId;
            model.Email = cas.Email;
            model.Cnic = cas.Cnic;
            model.FirNo = cas.FirNo;
            model.CategoryId = cas.CategoryId;
            model.CaseDetail = cas.CaseDetail;
            model.Address = cas.Address;
            model.UpdatedDate = DateTime.UtcNow;
            model.CreatedDate = DateTime.UtcNow;
            model.DistrictId = cas.DistrictId;
            model.CourtId = cas.CourtId;
            model.HearingDate = cas.HearingDate;
            _caseService.Insert(model);
            cas.Id = model.Id;
            UploadDocuments(cas);
        }
        public void UpdateCase(CaseViewModel cas)
        {
            var model = _caseService.GetById(cas.Id);
            model.LawyerId = cas.LawyerId;
            model.JudgeId = cas.JudgeId;
            model.ProvinceId = cas.ProvinceId;
            model.CityId = cas.CityId;
            model.ClientId = cas.ClientId;
            model.FirYear = cas.FirYear;
            model.Mobile = cas.Mobile;
            model.Name = cas.Name;
            model.PoliceStationId = cas.PoliceStationId;
            model.Email = cas.Email;
            model.Cnic = cas.Cnic;
            model.FirNo = cas.FirNo;
            model.CategoryId = cas.CategoryId;
            model.CaseDetail = cas.CaseDetail;
            model.Address = cas.Address;
            model.UpdatedDate = DateTime.UtcNow;
            model.CourtId = cas.CourtId;
            model.DistrictId = cas.DistrictId;
            model.HearingDate = cas.HearingDate;
            _caseService.Update(model);
            UploadDocuments(cas);
        }
        public void UploadDocuments(CaseViewModel model)
        {          
            string host = _httpContextAccessor.HttpContext.Request.Host.Value;
            string ddd = _httpContextAccessor.HttpContext.Request.Scheme;
                   
            var fullPath = @"wwwroot/Uploads";
            if (!Directory.Exists(fullPath))
            {
                Directory.CreateDirectory(fullPath);
            }
            if (model.FormFiles == null || model.FormFiles.Count == 0)
                return;

            foreach (var aformFile in model.FormFiles)
            {                
                var formFile = aformFile;
              
                if (formFile.Length > 0)
                {
                    var extension = formFile.FileName.Split(".").ToList().LastOrDefault();
                    var customeFileName = Regex.Replace(formFile.FileName, "[*/\"?:<>|]", "-", RegexOptions.Compiled);
                    var filePath = Path.Combine(fullPath, customeFileName);                   
                    using (var fileSteam = new FileStream(filePath, FileMode.Create))
                    {
                         formFile.CopyTo(fileSteam);
                    }                                     
                    var url = ddd + "://" + host + "/Uploads/" + customeFileName;
                    var docs = new CaseDocuments();
                    docs.FileName = customeFileName;
                    docs.MimeType = formFile.ContentType;
                    docs.CaseId = model.Id;
                    docs.FilePath = url;
                    _documentService.Insert(docs);
                }
            }
        }
    }
}
