using Case.Data.Domains;
using Case.Services;
using Case.web.Areas.Admin.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Case.web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class CaseController : Controller
    {
        private readonly ICaseCategoryService _caseCategoryService;
        private readonly IPoliceStationService  _policeStationService;
        private readonly IDistirictService  _distirictService;
        private readonly ICourtService _courtService;
        public CaseController(ICaseCategoryService caseCategoryService,
            IPoliceStationService policeStationService,
            IDistirictService distirictService,
            ICourtService courtService)
        {
            _caseCategoryService = caseCategoryService;
            _policeStationService = policeStationService;
            _distirictService = distirictService;
            _courtService = courtService;
        }
        public IActionResult Index()
        {
            var list = new List<CaseCategoryModel>();
            var cats = _caseCategoryService.GetAll();
            if(cats.Count > 0)
            foreach (var item in cats)
            {
                    list.Add(new CaseCategoryModel { Id = item.Id, CategoryName = item.CategoryName });
            }       
            return View(list);
        }
        public IActionResult AddEdit(int id=0)
        {
            var model = new CaseCategoryModel();
            model.Id = 0;
            if (id > 0)
            {
                var obj = _caseCategoryService.GetById(id);
                model.Id = obj.Id;
                model.CategoryName = obj.CategoryName;
            }

            return View(model);
        }

        [HttpPost]
        public IActionResult AddEdit(CaseCategoryModel categoryModel)
        {
            if (ModelState.IsValid)
            {
                if (categoryModel.Id > 0)
                {
                    var category = _caseCategoryService.GetById(categoryModel.Id);
                    category.CategoryName = categoryModel.CategoryName;
                    _caseCategoryService.Update(category);
                }                   
                else
                    _caseCategoryService.Insert(new CaseCategory() { CategoryName = categoryModel.CategoryName });
            }
            else
            {
                return View(categoryModel);
            }
            return RedirectToAction("Index", "Case");
        }
        public IActionResult DeleteCategory(int id)
        {
            var category = _caseCategoryService.GetById(id);
            if (category != null)
                _caseCategoryService.Delete(category);
            return RedirectToAction("Index", "Case");
        }
        public IActionResult StationList()
        {
            var list = new List<PoliceStationModel>();
            var cats = _policeStationService.GetAll();
            if (cats.Count > 0)
                foreach (var item in cats)
                {
                    list.Add(new PoliceStationModel { Id = item.Id, StationName = item.SatationName });
                }
            return View(list);
        }

      
        public IActionResult AddEditStation(int id = 0)
        {
            var model = new PoliceStationModel();
            model.Id = 0;
            if (id > 0)
            {
                var obj = _policeStationService.GetById(id);
                model.Id = obj.Id;
                model.StationName = obj.SatationName;
            }

            return View(model);
        }
       
        [HttpPost]
        public IActionResult AddEditStation(PoliceStationModel stationModel)
        {
            if (ModelState.IsValid)
            {
                if (stationModel.Id > 0)
                {
                    var station = _policeStationService.GetById(stationModel.Id);
                    station.SatationName = stationModel.StationName;   
                    _policeStationService.Update(station);
                }
                  
                else
                    _policeStationService.Insert(new PoliceStation() { SatationName = stationModel.StationName });
            }
            else
            {
                return View(stationModel);
            }
            return RedirectToAction("StationList", "Case");
        }
        public IActionResult DeleteStation(int id)
        {
            var station = _policeStationService.GetById(id);
            if (station != null)
                _policeStationService.Delete(station);
            return RedirectToAction("StationList", "Case");
        }
        public IActionResult DistrictList()
        {
            var list = new List<DistrictModel>();
            var cats = _distirictService.GetAll();
            if (cats.Count > 0)
                foreach (var item in cats)
                {
                    list.Add(new DistrictModel { Id = item.Id, DistirictName = item.DistirictName });
                }
            return View(list);
        }
        public IActionResult AddEditDistrict(int id = 0)
        {
            var model = new DistrictModel();
            model.Id = 0;
            if (id > 0)
            {
                var obj = _distirictService.GetById(id);
                model.Id = obj.Id;
                model.DistirictName = obj.DistirictName;
            }

            return View(model);
        }

        [HttpPost]
        public IActionResult AddEditDistrict(DistrictModel  districtModel)
        {
            if (ModelState.IsValid)
            {
                if (districtModel.Id > 0)
                {
                    var station = _distirictService.GetById(districtModel.Id);
                    station.DistirictName = districtModel.DistirictName;
                    _distirictService.Update(station);
                }

                else
                    _distirictService.Insert(new Distirict() { DistirictName = districtModel.DistirictName });
            }
            else
            {
                return View(districtModel);
            }
            return RedirectToAction("DistrictList", "Case");
        }
        public IActionResult DeleteDistrict(int id)
        {
            var station = _distirictService.GetById(id);
            if (station != null)
                _distirictService.Delete(station);
            return RedirectToAction("DistrictList", "Case");
        }
        public IActionResult CourtList()
        {
            var list = new List<CourtModel>();
            var cats = _courtService.GetAll();
            if (cats.Count > 0)
                foreach (var item in cats)
                {
                    list.Add(new CourtModel { Id = item.Id, CourtName = item.CourtName });
                }
            return View(list);
        }
        public IActionResult AddEditCourt(int id = 0)
        {
            var model = new CourtModel();
            model.Id = 0;
            if (id > 0)
            {
                var obj = _courtService.GetById(id);
                model.Id = obj.Id;
                model.CourtName = obj.CourtName;
            }

            return View(model);
        }

        [HttpPost]
        public IActionResult AddEditCourt(CourtModel courtModel)
        {
            if (ModelState.IsValid)
            {
                if (courtModel.Id > 0)
                {
                    var station = _courtService.GetById(courtModel.Id);
                    station.CourtName = courtModel.CourtName;
                    _courtService.Update(station);
                }

                else
                    _courtService.Insert(new Court() { CourtName = courtModel.CourtName });
            }
            else
            {
                return View(courtModel);
            }
            return RedirectToAction("CourtList", "Case");
        }
        public IActionResult DeleteCourt(int id)
        {
            var station = _courtService.GetById(id);
            if (station != null)
                _courtService.Delete(station);
            return RedirectToAction("CourtList", "Case");
        }
    }
}
