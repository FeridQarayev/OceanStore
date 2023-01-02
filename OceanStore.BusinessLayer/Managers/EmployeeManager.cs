﻿using OceanStore.BusinessLayer.Repositorys;
using OceanStore.DataAccesLayer.DataContext;
using OceanStore.DataAccesLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OceanStore.BusinessLayer.Managers
{
    public class EmployeeManager : GenericRepository<Employee, AppDbCotext>
    {
        public EmployeeManager(AppDbCotext db) : base(db)
        {
        }
        public async Task<List<Employee>> GetAllEmployee()
        {
            return await GetAllAsync();
        }
    }
}