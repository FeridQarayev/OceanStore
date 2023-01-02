using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OceanStore.BusinessLayer.Managers;
using OceanStore.DataAccesLayer.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OceanStore.Controllers
{
    [Authorize(Roles = "Admin,SuperAdmin")]
    public class EmployeeController : Controller
    {
        #region ctor
        private readonly EmployeeManager _employeeManager;
        public EmployeeController(EmployeeManager employeeManager)
        {
            _employeeManager = employeeManager;
        }
        #endregion

        #region Index
        public async Task<IActionResult> Index()
        {
            List<Employee> employees = await _employeeManager.GetAllEmployee();
            return View(employees);
        }
        #endregion

        #region Create
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Employee employee)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            //bool IsExist = await _positionManager.IsExistPositionName(position);
            //if (IsExist)
            //{
            //    ModelState.AddModelError("Name", "This Position is already exist");
            //    return View();
            //}
            //await _positionManager.CreatePosition(position);
            return RedirectToAction("Index");
        }
        #endregion
    }
}
