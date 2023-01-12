using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OceanStore.BusinessLayer.Managers;
using OceanStore.DataAccesLayer.Models;
using OceanStore.DataAccesLayer.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OceanStore.Controllers
{
    [Authorize(Roles = "Admin,SuperAdmin")]
    public class ExpensesController : Controller
    {
        #region ctor
        private readonly AmmountManager _ammountManager;
        public ExpensesController(AmmountManager ammountManager)
        {
            _ammountManager = ammountManager;
        }
        #endregion

        #region Index
        public async Task<IActionResult> Index()
        {
            List<Ammount> ammounts = await _ammountManager.ListAllExpenses();
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
            if (!ModelState.IsValid)
                return View();
            ammount.RecorderKind = true;
            await _ammountManager.CreateAmmount(ammount, User.Identity.Name);
            return RedirectToAction("Index");
        }
        #endregion

        #region Delete
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();
            Ammount ammount = await _ammountManager.GetAsync(x => x.Id == id);
            if (ammount == null)
                return BadRequest();
            await _ammountManager.DeleteAsync(ammount);
            return RedirectToAction("Index");
        }
        #endregion

        #region EmployeeSalary
        public async Task<IActionResult> EmployeeSalary()
        {
            double totalSalary = await _ammountManager.GetTotalEmployeeSalary();
            return Json(totalSalary);
        }
        public async Task<IActionResult> PayEmployeeSalary()
        {
            PayEmployeeSalary data = await _ammountManager.PayEmployeeSalary(User.Identity.Name);
            return Json(data);
        }
        #endregion
    }
}
