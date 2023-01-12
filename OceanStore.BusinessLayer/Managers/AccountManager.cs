using Microsoft.AspNetCore.Identity;
using OceanStore.BusinessLayer.Helpers;
using OceanStore.DataAccesLayer.Models;
using OceanStore.DataAccesLayer.ViewModels;
using System.Linq;
using System.Threading.Tasks;

namespace OceanStore.BusinessLayer.Managers
{
    public class AccountManager
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public AccountManager(UserManager<User> userManager, SignInManager<User> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
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
            await _userManager.AddToRoleAsync(appUser, Helper.Roles.SuperAdmin.ToString());
            return identityResult;
        }
        public async Task<User> FindBynNameUser(string username)
        {
            return await _userManager.FindByNameAsync(username);
        }
        public async Task<SignInResult> PasswordCheck(User user,string password,bool rememberMe)
        {
            return await _signInManager.PasswordSignInAsync(user, password, rememberMe, true);
        }
        public async Task SignOut()
        {
            await _signInManager.SignOutAsync();
        }
        public async Task CreateAllRoles()
        {
            if (!await _roleManager.RoleExistsAsync(Helper.Roles.SuperAdmin.ToString()))
            {
                await _roleManager.CreateAsync(new IdentityRole { Name = Helper.Roles.SuperAdmin.ToString() });
            }
            if (!await _roleManager.RoleExistsAsync(Helper.Roles.Admin.ToString()))
            {
                await _roleManager.CreateAsync(new IdentityRole { Name = Helper.Roles.Admin.ToString() });
            }
            if (!await _roleManager.RoleExistsAsync(Helper.Roles.Member.ToString()))
            {
                await _roleManager.CreateAsync(new IdentityRole { Name = Helper.Roles.Member.ToString() });
            }
        }
        public async Task<int> GetAllRolesCount()
        {
            return (_roleManager.Roles).Count() ;
        }
        public async Task<int> GetAllUsersCount()
        {
            return (_userManager.Users).Count() ;
        }
    }
}
