using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OceanStore.BusinessLayer.Helpers;
using OceanStore.DataAccesLayer.Models;
using OceanStore.DataAccesLayer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OceanStore.BusinessLayer.Managers
{
    public class UserAppManager
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public UserAppManager(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

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

        public async Task<UserUpdateVM> GetUserVM(User user)
        {
            UserUpdateVM updateVM = new UserUpdateVM()
            {
                Name = user.Name,
                Surname = user.Surname,
                Email = user.Email,
                Username = user.UserName,
                Role = (await _userManager.GetRolesAsync(user)).FirstOrDefault()
            };
            return updateVM;
        }

        public async Task<IdentityResult> AddRoleUser(User user, string newRole)
        {
            return await _userManager.AddToRoleAsync(user, newRole);
        }

        public async Task<IdentityResult> RemoveRoleUser(User user, string role)
        {
            return await _userManager.RemoveFromRoleAsync(user, role);
        }

        public async Task ModifiedUser(User user, UserUpdateVM updateVM)
        {
            user.Name = updateVM.Name;
            user.Surname = updateVM.Surname;
            user.Email = updateVM.Email;
            user.UserName = updateVM.Username;
            await UpdateUser(user);
        }

        public async Task UpdateUser(User user)
        {
            await _userManager.UpdateAsync(user);
        }

        public async Task<string> GeneratePasswordResetToken(User user)
        {
            return await _userManager.GeneratePasswordResetTokenAsync(user);
        }

        public async Task<IdentityResult> ResetPasswordUser(User user,string token, string newPassword)
        {
            return await _userManager.ResetPasswordAsync(user, token, newPassword);
        }

        public async Task ActivityUser(User user)
        {
            user.IsDeactive = Helper.CheckActive(user.IsDeactive);
            await UpdateUser(user);
        }
    }
}
