using OceanStore.BusinessLayer.Repositorys;
using OceanStore.DataAccesLayer.DataContext;
using OceanStore.DataAccesLayer.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OceanStore.BusinessLayer.Managers
{
    public class CategoryManager : GenericRepository<Category, AppDbCotext>
    {
        public CategoryManager(AppDbCotext db) : base(db)
        {
        }
        public async Task<List<Category>> GetAllCategories()
        {
            return await GetAllAsync();
        }
    }
}
