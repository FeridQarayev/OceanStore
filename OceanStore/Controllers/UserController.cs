﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OceanStore.BusinessLayer.Managers;
using OceanStore.DataAccesLayer.Models;
using OceanStore.DataAccesLayer.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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
            User user = await _userAppManager.GetUserById(id);
            if (user == null)
            {
                return BadRequest();
            }
            UserUpdateVM userVM = await _userAppManager.GetUserVM(user);
            ViewBag.Roles = await _userAppManager.GetRoles();
            return View(userVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(UserUpdateVM userUpdateVM, string newRole,string id)
        {
            #region GetCheck
            if (id == null) { return NotFound(); }
            User user = await _userAppManager.GetUserById(id);
            if (user == null)
            {
                return BadRequest();
            }
            UserUpdateVM dbUserVM = await _userAppManager.GetUserVM(user);
            ViewBag.Roles = await _userAppManager.GetRoles();
            #endregion
            if (!ModelState.IsValid)
            {
                return View(dbUserVM);
            }
            if (newRole!=dbUserVM.Role)
            {
                IdentityResult addIdentityResult = await _userAppManager.AddRoleUser(user, newRole);
                if (!addIdentityResult.Succeeded)
                {
                    foreach (IdentityError error in addIdentityResult.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                        return View();
                    }
                }
                IdentityResult removeIdentityResult = await _userAppManager.RemoveRoleUser(user, dbUserVM.Role);
                if (!removeIdentityResult.Succeeded)
                {
                    foreach (IdentityError error in removeIdentityResult.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                        return View();
                    }
                }
            }
            await _userAppManager.UpdateUser(user, userUpdateVM);
            return RedirectToAction("Index");
        }
        #endregion
    }
}
