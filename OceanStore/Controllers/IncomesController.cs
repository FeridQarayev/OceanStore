using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OceanStore.BusinessLayer.Managers;
using OceanStore.DataAccesLayer.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OceanStore.Controllers
{
    [Authorize(Roles = "Admin,SuperAdmin")]
    public class IncomesController : Controller
    {
        #region ctor
        private readonly AmmountManager _ammountManager;
        public IncomesController(AmmountManager ammountManager)
        {
            _ammountManager= ammountManager;
        }
        #endregion

        #region Index
        public async Task<IActionResult> Index()
        {
            List<Ammount> ammounts = await _ammountManager.ListAllIncomes();
            return View(ammounts);
        }
        #endregion

        #region Create
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Ammount ammount)
        {
            if(!ModelState.IsValid)
                return View();
            await _ammountManager.CreateAmmount(ammount, User.Identity.Name);
            return RedirectToAction("Index");
        }
        #endregion
    }
}
