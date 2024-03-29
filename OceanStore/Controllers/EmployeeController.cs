﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OceanStore.BusinessLayer.Managers;
using OceanStore.DataAccesLayer.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OceanStore.Controllers
{
    [Authorize(Roles = "SuperAdmin")]
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
            ViewBag.PositionsCount = (await _positionManager.GetAllActivePositions()).Count;
            return View(employees);
        }
        #endregion

        #region Create
        public async Task<IActionResult> Create()
        {
            List<Position> positions = await _positionManager.GetAllActivePositions();
            if (positions.Count == 0)
                return BadRequest();
            ViewBag.Positions = positions;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Employee employee, int? positionId)
        {
            List<Position> positions = await _positionManager.GetAllActivePositions();
            if (positions.Count == 0)
                return BadRequest();
            ViewBag.Positions = positions;
            if (!ModelState.IsValid)
                return View();
            if (positionId == null)
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

        #region Update
        public async Task<IActionResult> Update(int? id)
        {
            if (id == null)
                return NotFound();
            Employee employee = await _employeeManager.GetEmployeeById((int)id);
            if (employee == null)
                return BadRequest();
            List<Position> positions = await _positionManager.GetAllActivePositions();
            if (positions.Count == 0)
                return BadRequest();
            ViewBag.Positions = positions;
            return View(employee);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, Employee employee, int? positionId)
        {
            if (!ModelState.IsValid)
                return View(employee);
            if (id == null)
                return NotFound();
            if (positionId == null)
            {
                ModelState.AddModelError("Position.Id", "Select position");
                return View();
            }
            Employee dbEmployee = await _employeeManager.GetEmployeeById((int)id);
            if (dbEmployee == null)
                return BadRequest();
            List<Position> positions = await _positionManager.GetAllActivePositions();
            if (positions.Count == 0)
                return BadRequest();
            ViewBag.Positions = positions;
            bool isExistEmployeeEmail = await _employeeManager.IsExistEmployeeEmailVariosId(employee);
            if (isExistEmployeeEmail)
            {
                ModelState.AddModelError("", "This Email is already exist");
                return View(employee);
            }
            bool isExistEmployeePhoneNumber = await _employeeManager.IsExistEmployeePhoneNumberVariosId(employee);
            if (isExistEmployeePhoneNumber)
            {
                ModelState.AddModelError("", "This PhoneNumber is already exist");
                return View(employee);
            }
            dbEmployee.PositionId = (int)positionId;
            dbEmployee.Name = employee.Name;
            dbEmployee.Email = employee.Email;
            dbEmployee.PhoneNumber = employee.PhoneNumber;
            dbEmployee.EmployeeDetail = employee.EmployeeDetail;
            await _employeeManager.UpdateEmploye(dbEmployee);
            return RedirectToAction("Index");
        }
        #endregion

        #region Activity
        public async Task<IActionResult> Activity(int? id)
        {
            if (id == null)
                return NotFound();
            Employee employee = await _employeeManager.GetEmployeeById((int)id);
            if (employee == null)
                return BadRequest();
            await _employeeManager.ActivityEmployee(employee);
            return RedirectToAction("Index");
        }
        #endregion

        #region Detail
        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null)
                return NotFound();
            Employee employee = await _employeeManager.GetEmployeeById((int)id);
            if (employee == null)
                return BadRequest();
            ViewBag.Positions = await _positionManager.GetAllPositions();
            return View(employee);
        }
        #endregion
    }
}
