using OceanStore.BusinessLayer.Repositorys;
using OceanStore.DataAccesLayer.DataContext;
using OceanStore.DataAccesLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OceanStore.BusinessLayer.Managers
{
    public class UserManager : GenericRepository<User, AppDbCotext>
    {
        private readonly AppDbCotext _db;
        public UserManager(AppDbCotext db) : base(db)
        {
            _db = db;
        }
        public async Task<List<User>> GetUsers()
        {
            var data = await GetAllAsync(x=>x.Surname=="Nesee");
            return data;
        }
    }
}
