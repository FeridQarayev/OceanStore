using OceanStore.BusinessLayer.Helpers;
using OceanStore.BusinessLayer.Repositorys;
using OceanStore.DataAccesLayer.DataContext;
using OceanStore.DataAccesLayer.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OceanStore.BusinessLayer.Managers
{
    public class PositionManager : GenericRepository<Position, AppDbCotext>
    {
        public PositionManager(AppDbCotext db) : base(db)
        {
        }
        public async Task<List<Position>> GetAllPositions()
        {
            return await GetAllAsync();
        }
        public async Task<bool> IsExistPositionName(Position position)
        {
            return await CheckExist(x=>x.Name==position.Name);
        }
        public async Task<bool> IsExistPositionNameVariousId(Position position)
        {
            return await CheckExist(x => x.Name == position.Name && x.Id != position.Id);
        }
        public async Task<Position> GetPositionById(int id)
        {
            return await GetAsync(x=>x.Id==id);
        }
        public async Task CreatePosition(Position position) => await AddAsync(position);
        public async Task UpdatePosition(Position dbPosition,Position position)
        {
            dbPosition.Name = position.Name;
            dbPosition.Salary = position.Salary;
            dbPosition.IsDeactive = position.IsDeactive;
            await UpdateAsync(dbPosition);
        }
        public async Task ActivityPosition(Position position)
        {
            position.IsDeactive = Helper.CheckActive(position.IsDeactive);
            await UpdateAsync(position);
        }
    }
}
