using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OceanStore.BusinessLayer.Repositorys;
using OceanStore.DataAccesLayer.DataContext;
using OceanStore.DataAccesLayer.Models;
using OceanStore.DataAccesLayer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OceanStore.BusinessLayer.Managers
{
    public class UserAppManager 
        //: GenericRepository<User, AppDbCotext>
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public UserAppManager(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        //public UserAppManager(AppDbCotext db) : base(db)
        //{
        //    _db = db;
        //}
        public async Task<List<UserVM>> GetAllUsers()
        {
            try
            {
                List<User> users = await _userManager.Users.ToListAsync();
                List<UserVM> userVMs = new();
                foreach (User user in users)
                {
                    UserVM userVM = new UserVM
                    {
                        Id = user.Id,
                        Name = user.Name,
                        Surname = user.Surname,
                        Username = user.UserName,
                        Email = user.Email,
                        IsDeactive = user.IsDeactive,
                        Role = (await _userManager.GetRolesAsync(user)).FirstOrDefault()
                    };
                    userVMs.Add(userVM);
                }
                return userVMs;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<IdentityRole>> GetRoles()
        {
            try
            {
                return await _roleManager.Roles.ToListAsync();
            }
            catch { throw; }
        }

        public async Task<IEnumerable<IdentityError>> CreateUser(RegisterVM registerVM, string role)
        {
            try
            {
                User user = new User
                {
                    Name = registerVM.Name,
                    Surname = registerVM.Surname,
                    UserName = registerVM.Username,
                    Email = registerVM.Email
                };
                IdentityResult identityResult = await _userManager.CreateAsync(user, registerVM.Password);
                if (!identityResult.Succeeded)
                {
                    return identityResult.Errors;
                }
                await _userManager.AddToRoleAsync(user, role);
                return null;
            }
            catch { throw; }
        }

        public async Task<User> GetUserById(string id)
        {
            try
            {
                return await _userManager.FindByIdAsync(id);
            }
            catch { return null; }
            }

        public async Task<UserUpd>
    }
}
