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
        #region ctor
        private readonly UserAppManager _userAppManager;
        public UserController(UserAppManager userAppManager)
        {
            _userAppManager = userAppManager;
        }
        #endregion

        #region Index
        public async Task<IActionResult> Index()
        {
            List<UserVM> users = await _userAppManager.GetAllUsers();
            return View(users);
        }
        #endregion

        #region Create
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
        #endregion

        #region Update
        public async Task<IActionResult> Update(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            UserUpdateVM userVm = await _userAppManager.GetUserVMById(id);
            if (userVm == null)
            {
                return BadRequest();
            }
            ViewBag.Roles = await _userAppManager.GetRoles();
            return View(userVm);
        }
        #endregion
    }
}
