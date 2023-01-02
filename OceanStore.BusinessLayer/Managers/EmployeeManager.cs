using Microsoft.EntityFrameworkCore;
using OceanStore.BusinessLayer.Repositorys;
using OceanStore.DataAccesLayer.DataContext;
using OceanStore.DataAccesLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
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
        public override async Task<List<Employee>> GetAllAsync(Expression<Func<Employee, bool>> filter = null)
        {
            return await _db.Set<Employee>().Include(x => x.Position).ToListAsync();
        }
        public override async Task<Employee> GetAsync(Expression<Func<Employee, bool>> filter = null)
        {
            return await _db.Set<Employee>().Include(_ => _.EmployeeDetail).FirstOrDefaultAsync(filter);
        }
        public async Task<List<Employee>> GetAllEmployee() => await GetAllAsync();
        public async Task<bool> IsExistEmployeeEmail(Employee employee) => await CheckExist(x => x.Email == employee.Email);
        public async Task<bool> IsExistEmployeePhoneNumber(Employee employee) => await CheckExist(x => x.PhoneNumber == employee.PhoneNumber);
        public async Task<bool> IsExistEmployeeEmailVariosId(Employee employee) => await CheckExist(x => x.Email == employee.Email && x.Id != employee.Id);
        public async Task<bool> IsExistEmployeePhoneNumberVariosId(Employee employee) => await CheckExist(x => x.PhoneNumber == employee.PhoneNumber && x.Id != employee.Id);
        public async Task CreateEmployee(Employee employee)
        {
            await AddAsync(employee);
        }
        public async Task<Employee> GetEmployeeById(int id)
        {
            return await GetAsync(x => x.Id == id);
        }
        public async Task UpdateEmploye(Employee employee)
        {
            await UpdateAsync(employee);
        }
    }
}