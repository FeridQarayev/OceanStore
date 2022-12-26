using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OceanStore.BusinessLayer.Managers;
using OceanStore.DataAccesLayer.Models;
using OceanStore.DataAccesLayer.ViewModels;
using System.Threading.Tasks;

namespace OceanStore.Controllers
{
    public class AccountController : Controller
    {
        private readonly AccountManager _accountManager;

        #region Register
        public async Task<IActionResult> Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterVM registerVM)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            IdentityResult identityResult = await _accountManager.RegisterPost(registerVM);
            if (!identityResult.Succeeded)
            {
                foreach (IdentityError error in identityResult.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View();
            }
            return RedirectToAction("Index", "Home");
        }
        #endregion

        #region Login
        public async Task<IActionResult> Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginVM loginVM)
        {
            User appUser = await _accountManager.FindBynNameUser(loginVM.Username);
            if (appUser == null)
            {
                ModelState.AddModelError("", "UserName or Password is wrong !");
                return View();
            }
            Microsoft.AspNetCore.Identity.SignInResult signInResult = await _accountManager.PasswordCheck(appUser,loginVM.Password);
            if (signInResult.IsLockedOut)
            {
                ModelState.AddModelError("", "Error cix get");
                return View();
            }
            if (!signInResult.Succeeded)
            {
                ModelState.AddModelError("", "UserName or Password is wrong !");
                return View();
            }
            return RedirectToAction("Index", "Home");
        }
        #endregion
        public async Task<IActionResult> Logout()
        {
            await _accountManager.SignOut();
            return RedirectToAction("Index", "Home");
        }
        //public async Task CreateRoles()
        //{
        //    if (!await _roleManager.RoleExistsAsync(Helper.Roles.Admin.ToString()))
        //    {
        //        await _roleManager.CreateAsync(new IdentityRole { Name = Helper.Roles.Admin.ToString() });
        //    }
        //    if (!await _roleManager.RoleExistsAsync(Helper.Roles.Member.ToString()))
        //    {
        //        await _roleManager.CreateAsync(new IdentityRole { Name = Helper.Roles.Member.ToString() });
        //    }
        //}
    }
}
