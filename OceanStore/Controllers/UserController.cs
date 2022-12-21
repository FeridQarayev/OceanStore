using Microsoft.AspNetCore.Identity;
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
            List<UserVM> users = await _userAppManager.GetAllUsers();
            return View(users);
        }
        public async Task<IActionResult> Create()
        {
            ViewBag.Roles = await _userAppManager.GetRoles();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RegisterVM registerVM, string role)
        {
            ViewBag.Roles = await _userAppManager.GetRoles();
            if (!ModelState.IsValid)
            {
                return View();
            }
            IEnumerable<IdentityError> errors = await _userAppManager.CreateUser(registerVM, role);
            if (errors!=null)
            {
                foreach (IdentityError error in errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View();
            }
            return RedirectToAction("Index");
        }
    }
}
