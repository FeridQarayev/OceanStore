using Microsoft.AspNetCore.Mvc;
using OceanStore.BusinessLayer.Managers;
using OceanStore.DataAccesLayer.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OceanStore.Controllers
{
    public class UserController : Controller
    {
        private readonly UserAppManager _userAppManager;
        public async Task<IActionResult> Index()
        {
            List<UserVM> users = await _userAppManager.GetUsers();
            return View(users);
        }
    }
}
