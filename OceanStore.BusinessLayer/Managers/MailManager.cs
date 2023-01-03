using OceanStore.BusinessLayer.Helpers;
using OceanStore.DataAccesLayer.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OceanStore.BusinessLayer.Managers
{
    public class MailManager
    {
        private readonly EmployeeManager _employeeManager;
        public MailManager(EmployeeManager employeeManager)
        {
            _employeeManager = employeeManager;
        }
        public async Task SendMail(string messageSubject, string messageBody, string mailTo)
        {
            await Helper.SendMailAsync(messageSubject, messageBody, mailTo);
        }
        public async Task SendAllMail(Mail mail)
        {
            List<Employee> employees = await _employeeManager.GetAllEmployee();
            foreach (Employee employee in employees)
            {
                await SendMail(mail.MessageSubject, mail.MessageBody, employee.Email);
            }
        }
    }
}
