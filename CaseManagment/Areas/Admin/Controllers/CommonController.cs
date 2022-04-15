using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Case.Services;
using Case.web.Areas.Admin.Models;
using Case.Data.Domains;
using System.IO;
using Case.web.Areas.Admin.Factories;

namespace Case.web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class CommonController : Controller
    {
        private readonly ITermConditionService _termConditionService;
        private readonly ICarouselService _carouselService;
        private readonly IProvinceService  _provinceService;
        private readonly ICityService  _cityService;
        private readonly IUserModelFactory _userModelFactory;
        public CommonController(ITermConditionService termConditionService,
            ICarouselService carouselService,
            IProvinceService provinceService,
            ICityService cityService,
            IUserModelFactory userModelFactory)
        {
            _termConditionService = termConditionService;
            _carouselService = carouselService;
            _provinceService = provinceService;
            _cityService = cityService;
            _userModelFactory = userModelFactory;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult CarouselList()
        {
            var list = new List<CarouselModel>();
            var cats = _carouselService.GetAll();
            if (cats.Count > 0)
                foreach (var item in cats)
                {
                    list.Add(new CarouselModel { Id = item.Id, SliderText = item.SliderText,ImageName = item.ImageName,Title = item.Title });
                }
            return View(list);
        }
        public IActionResult AddEditCarousel(int id = 0)
        {
            var model = new CarouselModel();
            model.Id = 0;
            if (id > 0)
            {
                var obj = _carouselService.GetById(id);
                model.Id = obj.Id;
                model.SliderText = obj.SliderText;
                model.ImageName = obj.ImageName;
                model.Title = obj.Title;
            }

            return View(model);
        }

        [HttpPost]
        public IActionResult AddEditCarousel(CarouselModel courtModel)
        {
            if (ModelState.IsValid)
            {
                if (courtModel.Id > 0)
                {
                    var station = _carouselService.GetById(courtModel.Id);
                    station.SliderText = courtModel.SliderText;
                    station.Title = courtModel.Title;
                    if (courtModel.FormFile != null)
                        station.ImageName = UploadImage(courtModel);
                    _carouselService.Update(station);
                }

                else
                {
                    var file = "";
                    if (courtModel.FormFile != null)
                        file = UploadImage(courtModel);
                    _carouselService.Insert(new Carousel() { SliderText = courtModel.SliderText, Title = courtModel.Title, ImageName = file});
                }
                  
            }
            else
            {
                return View(courtModel);
            }
            return RedirectToAction("CarouselList", "Common");
        }
        public string UploadImage(CarouselModel model)
        {
            var customeFileName = string.Empty;
            var fullPath = @"wwwroot/Carosel";
            if (!Directory.Exists(fullPath))
            {
                Directory.CreateDirectory(fullPath);
            }
            if (model.FormFile == null)
                return string.Empty;
          
                var formFile = model.FormFile;
                if (formFile.Length > 0)
                {
                    var extension = formFile.FileName.Split(".").ToList().LastOrDefault();
                     customeFileName = DateTime.Now.Ticks.ToString() + "." + extension;
                    var filePath = Path.Combine(fullPath, customeFileName);
                    using (var fileSteam = new FileStream(filePath, FileMode.Create))
                    {
                        formFile.CopyTo(fileSteam);
                    }
                }           
            return customeFileName;
        }
        public IActionResult DeleteCarousel(int id)
        {
            var station = _carouselService.GetById(id);
            if (station != null)
            {
                var path = @"wwwroot/Carosel";
                var favicon = Path.Combine(path, station.ImageName);
                // var favicon2 = Path.Combine(wwwroot, "favicon2.ico");

                // true
                var exists = System.IO.File.Exists(favicon);
                if (exists)
                    System.IO.File.Delete(favicon);
                _carouselService.Delete(station);
            }
               
            return RedirectToAction("CarouselList", "Common");
        }
        public IActionResult TermsList()
        {
            var list = new List<TermConditionModel>();
            var cats = _termConditionService.GetAll();
            if (cats.Count > 0)
                foreach (var item in cats)
                {
                    list.Add(new TermConditionModel { Id = item.Id, Terms = item.Terms });
                }
            return View(list);
        }
        public IActionResult AddEditTerms(int id = 0)
        {
            var model = new TermConditionModel();
            model.Id = 0;
            if (id > 0)
            {
                var obj = _termConditionService.GetById(id);
                model.Id = obj.Id;
                model.Terms = obj.Terms;
            }

            return View(model);
        }

        [HttpPost]
        public IActionResult AddEditTerms(TermConditionModel courtModel)
        {
            if (ModelState.IsValid)
            {
                if (courtModel.Id > 0)
                {
                    var station = _termConditionService.GetById(courtModel.Id);
                    station.Terms = courtModel.Terms;
                    _termConditionService.Update(station);
                }

                else
                    _termConditionService.Insert(new TermCondition() { Terms = courtModel.Terms });
            }
            else
            {
                return View(courtModel);
            }
            return RedirectToAction("TermsList", "Common");
        }
        public IActionResult DeleteTerms(int id)
        {
            var station = _termConditionService.GetById(id);
            if (station != null)
                _termConditionService.Delete(station);
            return RedirectToAction("TermsList", "Common");
        }
        public IActionResult ProvinceList()
        {
            var list = new List<ProvinceModel>();
            var cats = _provinceService.GetAll();
            if (cats.Count > 0)
                foreach (var item in cats)
                {
                    list.Add(new ProvinceModel { Id = item.Id, ProvinceName = item.ProvinceName });
                }
            return View(list);
        }
        public IActionResult AddEditProvince(int id = 0)
        {
            var model = new ProvinceModel();
            model.Id = 0;
            if (id > 0)
            {
                var obj = _provinceService.GetById(id);
                model.Id = obj.Id;
                model.ProvinceName = obj.ProvinceName;
            }

            return View(model);
        }

        [HttpPost]
        public IActionResult AddEditProvince(ProvinceModel courtModel)
        {
            if (ModelState.IsValid)
            {
                if (courtModel.Id > 0)
                {
                    var station = _provinceService.GetById(courtModel.Id);
                    station.ProvinceName = courtModel.ProvinceName;
                    _provinceService.Update(station);
                }

                else
                    _provinceService.Insert(new Provinces() { ProvinceName = courtModel.ProvinceName });
            }
            else
            {
                return View(courtModel);
            }
            return RedirectToAction("ProvinceList", "Common");
        }
        public IActionResult DeleteProvince(int id)
        {
            var station = _provinceService.GetById(id);          
            if (station != null)
            {
                var cities = _cityService.GetAllByProvinceId(id);
                if (cities.Count > 0)
                    cities.ToList().ForEach(x => _cityService.Delete(x));
                _provinceService.Delete(station);
            }
            return RedirectToAction("ProvinceList", "Common");
        }
        public IActionResult CityList()
        {
            var list = new List<CityModel>();
            var cats = _cityService.GetAll();
            if (cats.Count > 0)
                foreach (var item in cats)
                {
                    list.Add(new CityModel { Id = item.Id, CityName = item.CityName,
                        ProvinceId = item.ProvinceId,
                        ProvinceName = _provinceService.GetById(item.ProvinceId).ProvinceName });
                }
            return View(list);
        }
        public IActionResult AddEditCity(int id = 0)
        {
            var model = _userModelFactory.PrepareModelForCity(null, new CityModel());
            model.Id = 0;
            if (id > 0)
            {
                var obj = _cityService.GetById(id);
                model = _userModelFactory.PrepareModelForCity(obj, new CityModel());
            }

            return View(model);
        }

        [HttpPost]
        public IActionResult AddEditCity(CityModel courtModel)
        {
            if (ModelState.IsValid)
            {
                if (courtModel.Id > 0)
                {
                    var station = _cityService.GetById(courtModel.Id);
                    station.ProvinceId = courtModel.ProvinceId;
                    station.CityName = courtModel.CityName;
                    _cityService.Update(station);
                }

                else
                    _cityService.Insert(new Cities() { CityName = courtModel.CityName ,ProvinceId = courtModel.ProvinceId });
            }
            else
            {
                courtModel = _userModelFactory.PrepareModelForCity(null, courtModel);
                return View(courtModel);
            }
            return RedirectToAction("CityList", "Common");
        }
        public IActionResult DeleteCity(int id)
        {
            var station = _cityService.GetById(id);
            if (station != null)
                _cityService.Delete(station);
            return RedirectToAction("CityList", "Common");
        }
    }
}
