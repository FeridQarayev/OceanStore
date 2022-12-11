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
    public class HomeController : Controller
    {

        public async Task<IActionResult> Index()
        {
            return View();
        }
    }
}
