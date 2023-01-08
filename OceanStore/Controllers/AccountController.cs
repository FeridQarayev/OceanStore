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
        #region ctor
        private readonly AccountManager _accountManager;
        public AccountController(AccountManager accountManager)
        {
            _accountManager = accountManager;
        }
        #endregion

        #region Register
        public async Task<IActionResult> Register()
        {
            int accountsCount = await _accountManager.GetAllUsersCount();
            if (accountsCount != 0)
                return RedirectToAction("Login", "Account");
            ViewBag.RolesCount = await _accountManager.GetAllRolesCount();
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
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBag.AccountCount = await _accountManager.GetAllUsersCount();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginVM loginVM)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            User appUser = await _accountManager.FindBynNameUser(loginVM.Username);
            if (appUser == null)
            {
                ModelState.AddModelError("", "UserName or Password is wrong !");
                return View();
            }
            if (appUser.IsDeactive)
            {
                ModelState.AddModelError("", "This account has been deactivated");
                return View();
            }
            Microsoft.AspNetCore.Identity.SignInResult signInResult = await _accountManager.PasswordCheck(appUser, loginVM.Password, loginVM.RememberMe);
            if (signInResult.IsLockedOut)
            {
                ModelState.AddModelError("", "1 day banned!");
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

        #region LogOut
        public async Task<IActionResult> Logout()
        {
            await _accountManager.SignOut();
            return RedirectToAction("Index", "Home");
        }
        #endregion

        #region CreateRoles
        public async Task CreateRoles()
        {
            await _accountManager.CreateAllRoles();
        }
        #endregion
    }
}
