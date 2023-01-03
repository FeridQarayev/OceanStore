using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OceanStore.BusinessLayer.Managers;
using OceanStore.DataAccesLayer.Models;
using System.Threading.Tasks;

namespace OceanStore.Controllers
{
    [Authorize(Roles = "Admin,SuperAdmin")]
    public class MailController : Controller
    {
        #region ctor
        private readonly MailManager _mailManager;
        private readonly EmployeeManager _employeeManager;
        public MailController(MailManager mailManager, EmployeeManager employeeManager)
        {
            _mailManager = mailManager;
            _employeeManager = employeeManager;
        }
        #endregion
        public async Task<IActionResult> Index(int? id)
        {
            if (id == null)
                return NotFound();
            Employee employee = await _employeeManager.GetEmployeeById((int)id);
            if (employee == null)
                return BadRequest();
            Mail mail = new Mail();
            mail.MailTo = employee.Email;
            return View(mail);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(int? id, Mail mail)
        {
            if (!ModelState.IsValid)
                return View();
            if (id == null)
                return NotFound();
            Employee employee = await _employeeManager.GetEmployeeById((int)id);
            if (employee == null)
                return BadRequest();
            await _mailManager.SendMail(mail.MessageSubject,mail.MessageBody,mail.MailTo);
            return RedirectToAction("Index", "Employee");
        }
        public async Task<IActionResult> SendAll()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SendAll(Mail mail)
        {
            if (!ModelState.IsValid)
                return View();
            await _mailManager.SendAllMail(mail);
            return RedirectToAction("Index", "Employee");
        }
    }
}
