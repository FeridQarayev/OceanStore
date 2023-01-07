using OceanStore.BusinessLayer.Repositorys;
using OceanStore.DataAccesLayer.DataContext;
using OceanStore.DataAccesLayer.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OceanStore.BusinessLayer.Managers
{
    public class AmmountManager : GenericRepository<Ammount, AppDbCotext>
    {
        private readonly UserAppManager _userAppManager;
        public AmmountManager(AppDbCotext db, UserAppManager userAppManager) : base(db)
        {
            _userAppManager = userAppManager;
        }
        public async Task<List<Ammount>> GetIncomes()=> await GetAllAsync(x=>!x.RecorderKind);
        public async Task<List<Ammount>> GetExpenses()=> await GetAllAsync(x=>x.RecorderKind);
        public async Task<List<Ammount>> ListAllIncomes()
        {
            List<Ammount> ammounts = await GetIncomes();
            foreach (Ammount ammount in ammounts)
            {
                ammount.CreatedBy = (await _userAppManager.GetUserById(ammount.CreatedBy)).Name;
            }
            return ammounts;
        }
        public async Task CreateAmmount(Ammount ammount, string name)
        {
            ammount.CreateTime = DateTime.UtcNow;
            ammount.CreatedBy = (await _userAppManager.GetUserByName(name)).Id;
            await AddAsync(ammount);
        }
    }
}
