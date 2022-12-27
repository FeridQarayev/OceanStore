using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OceanStore.BusinessLayer.Managers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace OceanStore.Controllers
{
    [Authorize(Roles = "Admin")]
    public class HomeController : Controller
    {
        //private readonly UserAppManager _userManager;
        //public HomeController(UserAppManager userManager)
        //{
        //    _userManager = userManager;
        //}

        public async Task<IActionResult> Index()
        {
            //var data = await _userManager.GetUsers();
            return View();
        }
    }
}
