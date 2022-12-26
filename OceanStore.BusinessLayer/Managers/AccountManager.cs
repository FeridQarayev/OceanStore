using Microsoft.AspNetCore.Identity;
using Microsoft.VisualBasic;
using OceanStore.BusinessLayer.Helpers;
using OceanStore.DataAccesLayer.Models;
using OceanStore.DataAccesLayer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OceanStore.BusinessLayer.Managers
{
    public class AccountManager
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        public AccountManager(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public async Task<IdentityResult> RegisterPost(RegisterVM registerVM)
        {
            User appUser = new User
            {
                Name = registerVM.Name,
                Surname = registerVM.Surname,
                UserName = registerVM.Username,
                Email = registerVM.Email
            };
            IdentityResult identityResult = await _userManager.CreateAsync(appUser, registerVM.Password);
            if (!identityResult.Succeeded)
            {
                return identityResult;
            }
            await _signInManager.SignInAsync(appUser, true);
            await _userManager.AddToRoleAsync(appUser, Helper.Roles.Member.ToString());
            return identityResult;
        }
        public async Task<User> FindBynNameUser(string username)
        {
            return await _userManager.FindByNameAsync(username);
        }
        public async Task<SignInResult> PasswordCheck(User user,string password)
        {
            return await _signInManager.PasswordSignInAsync(user, password, true, true);
        }
        public async Task SignOut()
        {
            await _signInManager.SignOutAsync();
        }
    }
}
