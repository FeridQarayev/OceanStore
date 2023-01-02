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
        public async Task CreatePosition(Position position)
        {
            await AddAsync(position);
        }
    }
}
