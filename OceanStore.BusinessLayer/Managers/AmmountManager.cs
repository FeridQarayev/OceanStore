using Microsoft.EntityFrameworkCore;
using OceanStore.BusinessLayer.Repositorys;
using OceanStore.DataAccesLayer.DataContext;
using OceanStore.DataAccesLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace OceanStore.BusinessLayer.Managers
{
    public class AmmountManager : GenericRepository<Ammount, AppDbCotext>
    {
        private readonly UserAppManager _userAppManager;
        private readonly AppDbCotext _db;
        public AmmountManager(AppDbCotext db, UserAppManager userAppManager) : base(db)
        {
            _db = db;
            _userAppManager = userAppManager;
        }
        public async override Task<List<Ammount>> GetAllAsync(Expression<Func<Ammount, bool>> filter = null)
        {
            return filter == null ?
                await _db.Ammounts.OrderByDescending(x => x.CreateTime).ToListAsync() :
                await _db.Ammounts.Where(filter).OrderByDescending(x => x.CreateTime).ToListAsync();
        }
        public async Task<List<Ammount>> GetIncomes() => await GetAllAsync(x => !x.RecorderKind);
        public async Task<List<Ammount>> GetExpenses() => await GetAllAsync(x => x.RecorderKind);
        public async Task<List<Ammount>> ListAllIncomes()
        {
            List<Ammount> ammounts = await GetIncomes();
            foreach (Ammount ammount in ammounts)
            {
                ammount.CreatedBy = (await _userAppManager.GetUserById(ammount.CreatedBy)).Name;
            }
            return ammounts;
        }
        public async Task<List<Ammount>> ListAllExpenses()
        {
            List<Ammount> ammounts = await GetExpenses();
            foreach (Ammount ammount in ammounts)
            {
                ammount.CreatedBy = (await _userAppManager.GetUserById(ammount.CreatedBy)).Name;
            }
            return ammounts;
        }
        public async Task CreateAmmount(Ammount ammount, string name)
        {
            ammount.CreateTime = DateTime.UtcNow.AddHours(4);
            ammount.CreatedBy = (await _userAppManager.GetUserByName(name)).Id;
            await AddAsync(ammount);
        }
        public async Task<double> GetTotalAmmount(int month = 0)
        {
            return month == 0 ?
                (await GetAllAsync()).Sum(x => x.RecorderKind ? -x.Price : x.Price) :
                (await GetAllAsync(x => x.CreateTime >= DateTime.UtcNow.AddMonths(-month))).Sum(x => x.RecorderKind ? -x.Price : x.Price);
        }
        public async Task<double> GetTotalExpensesAmmount(int month = 0)
        {
            return month == 0 ?
                (await GetAllAsync()).Sum(x => x.RecorderKind ? x.Price : 0) :
                (await GetAllAsync(x => x.CreateTime >= DateTime.UtcNow.AddMonths(-month))).Sum(x => x.RecorderKind ? x.Price : 0);
        }
        public async Task<double> GetTotalIncomesAmmount(int month = 0)
        {
            return month == 0 ?
                (await GetAllAsync()).Sum(x => !x.RecorderKind ? x.Price : 0) :
                (await GetAllAsync(x => x.CreateTime >= DateTime.UtcNow.AddMonths(-month))).Sum(x => !x.RecorderKind ? x.Price : 0);
        }
        public async Task<double> GetTotalAmmountAsDay(int day)
        {
            return (await GetAllAsync(x => x.CreateTime >= DateTime.UtcNow.AddDays(-day))).Sum(x => x.RecorderKind ? -x.Price : x.Price);
        }
        public async Task<double> GetTotalExpensessAmmountAsDay(int day)
        {
            return (await GetAllAsync(x => x.CreateTime >= DateTime.UtcNow.AddDays(-day))).Sum(x => x.RecorderKind ? x.Price : 0);
        }
        public async Task<double> GetTotalIncomesAmmountAsDay(int day)
        {
            return (await GetAllAsync(x => x.CreateTime >= DateTime.UtcNow.AddDays(-day))).Sum(x => !x.RecorderKind ? x.Price : 0);
        }
        
    }
}
