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
        private readonly PositionManager _positionManager;
        public EmployeeController(EmployeeManager employeeManager, PositionManager positionManager)
        {
            _employeeManager = employeeManager;
            _positionManager = positionManager;
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
            ViewBag.Positions = await _positionManager.GetAllPositions();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Employee employee,int? positionId)
        {
            ViewBag.Positions = await _positionManager.GetAllPositions();
            if (!ModelState.IsValid)
                return View();
            if(positionId == null)
            {
                ModelState.AddModelError("Position.Id", "Select position");
                return View();
            }
            bool IsExistEmail = await _employeeManager.IsExistEmployeeEmail(employee);
            if (IsExistEmail)
            {
                ModelState.AddModelError("Email", "This Email is already exist");
                return View();
            }
            bool IsExistPhoneNumber = await _employeeManager.IsExistEmployeeEmail(employee);
            if (IsExistPhoneNumber)
            {
                ModelState.AddModelError("PhoneNumber", "This PhoneNumber is already exist");
                return View();
            }
            employee.PositionId = (int)positionId;
            await _employeeManager.CreateEmployee(employee);
            return RedirectToAction("Index");
        }
        #endregion
    }
}
