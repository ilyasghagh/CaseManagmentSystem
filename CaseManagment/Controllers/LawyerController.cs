using Case.Services;
using Case.web.Factories.UserFactory;
using Case.web.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Case.web.Controllers
{
    public class LawyerController : Controller
    {
        private readonly IUserService _userService;
        private readonly IUserModelFactory _userModelFactory;
        private readonly IUserRoleService _userRoleService;
        private readonly IRolesService _rolesService;
        private readonly ICaseService _caseService;
        private readonly ICaseDocumentService _documentService;
        private readonly IHostingEnvironment _environment;
        private readonly ICityService _cityService;
        public LawyerController(IUserService userService, IUserModelFactory userModelFactory,
            IUserRoleService userRoleService,
            IRolesService rolesService,
            ICaseService caseService,
            ICaseDocumentService documentService,
            IHostingEnvironment environment,
            ICityService cityService)
        {
            _userService = userService;
            _userModelFactory = userModelFactory;
            _userRoleService = userRoleService;
            _rolesService = rolesService;
            _caseService = caseService;
            _documentService = documentService;
            _environment = environment;
            _cityService = cityService;
        }
        public IActionResult Index()
        {
            var model = new LawyerSearchModel();
            // int Id = Convert.ToInt32(HttpContext.User.Claims.Where(x => x.Type == "Id").Select(x => x.Value).SingleOrDefault());
            model.LawyerId = 0;//Id;
          var model1 =  _userModelFactory.PrepareLawyerList(model);
            return View(model1);
        }
        [HttpPost]
        public IActionResult Index(LawyerSearchModel model)
        {
            //int Id = Convert.ToInt32(HttpContext.User.Claims.Where(x => x.Type == "Id").Select(x => x.Value).SingleOrDefault());
            model.LawyerId = 0;//Id;
             model = _userModelFactory.PrepareLawyerList(model);
            return View(model);
        }
        public IActionResult AddEditCase(int id=0)
        {
            if(id > 0)
            {
                var cas = _caseService.GetById(id);
                return View(_userModelFactory.PrepareCaseModel(cas, new CaseViewModel()));
            }
                
            var model = _userModelFactory.PrepareCaseModel(null, new CaseViewModel());
            return View(model);
        }
        [HttpPost]
        public IActionResult AddEditCase(CaseViewModel model)
        {
            if(ModelState.IsValid)
            {
                if (model.Id > 0)
                {
                    _userModelFactory.UpdateCase(model);
                }
                else
                {
                    _userModelFactory.InsertNewCase(model);
                }
                return RedirectToAction("Index", "Lawyer");
            }
           
            var model1 = _userModelFactory.PrepareCaseModel(null, model);
            return View(model1);
        }
        public IActionResult DeleteCase(int id)
        {
            var cas = _caseService.GetById(id);
            if (cas != null)
                _caseService.Delete(cas);
            return RedirectToAction("Index", "Lawyer");
        }
        public JsonResult DeleteFile(int id)
        {
            var file = _documentService.GetById(id);
            if(file != null)
            {
                var path = _environment.WebRootPath +"/Uploads/";
                var favicon = Path.Combine(path, file.FileName);
               // var favicon2 = Path.Combine(wwwroot, "favicon2.ico");

                // true
                var exists = System.IO.File.Exists(favicon);
                if (exists)
                    System.IO.File.Delete(favicon);

                _documentService.Delete(file);
            }
            return Json(true);
        }
        public FileResult DownloadDocument(int id)
        {
            var file = _documentService.GetById(id);
            string path = Path.Combine(_environment.WebRootPath, "Uploads/") + file.FileName;
            byte[] fileBytes = System.IO.File.ReadAllBytes(path);

            return File(fileBytes,file.MimeType, file.FileName);
        }
        public IActionResult GetCityList(int ProvinceId)
        {
            var data = _cityService.GetAllByProvinceId(ProvinceId).Select(x => new SelectListItem { Text = x.CityName, Value = x.Id.ToString() }).ToList();
            return Json(data);
        }
    }
}
