using Case.Services;
using CaseManagment.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace CaseManagment.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IRolesService _rolesService;

        public HomeController(ILogger<HomeController> logger,IRolesService rolesService)
        {
            _logger = logger;
            _rolesService = rolesService;
        }

        public IActionResult Index()
        {
            ViewBag.roles = _rolesService.GetAllRoles();
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
