using Microsoft.EntityFrameworkCore;
using OceanStore.BusinessLayer.Repositorys;
using OceanStore.DataAccesLayer.DataContext;
using OceanStore.DataAccesLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OceanStore.BusinessLayer.Managers
{
    public class EmployeeManager : GenericRepository<Employee, AppDbCotext>
    {
        private readonly AppDbCotext _db;
        public EmployeeManager(AppDbCotext db) : base(db)
        {
            _db = db;
        }
        public override Task<List<Employee>> GetAllAsync(Expression<Func<Employee, bool>> filter = null)
        {
            return _db.Set<Employee>().Include(x=>x.Position).ToListAsync();
        }
        public async Task<List<Employee>> GetAllEmployee() => await GetAllAsync();
        public async Task<bool> IsExistEmployeeEmail(Employee employee) => await CheckExist(x => x.Email == employee.Email);
        public async Task<bool> IsExistEmployeePhoneNumber(Employee employee) => await CheckExist(x => x.PhoneNumber == employee.PhoneNumber);
        public async Task CreateEmployee(Employee employee) => await AddAsync(employee);
    }
}