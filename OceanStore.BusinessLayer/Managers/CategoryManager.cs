using Microsoft.EntityFrameworkCore;
using OceanStore.BusinessLayer.Helpers;
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
    public class CategoryManager : GenericRepository<Category, AppDbCotext>
    {
        private readonly AppDbCotext _db;
        public CategoryManager(AppDbCotext db) : base(db)
        {
            _db = db;
        }
        public override async Task<List<Category>> GetAllAsync(Expression<Func<Category, bool>> filter = null)
        {
            return await _db.Set<Category>().Include(x=>x.Children).ToListAsync();
        }
        public async Task<List<Category>> GetAllCategories()
        {
            return await GetAllAsync();
        }
        public async Task<List<Category>> GetMainCategories()
        {
            return await GetAllAsync(x => x.IsMain);
        }
        public async Task<bool> IsExistCategoryName(Category category) => await CheckExist(x => x.Name == category.Name);
        public async Task ActivityCategory(Category category)
        {
            category.IsDeactive = Helper.CheckActive(category.IsDeactive);
            await UpdateAsync(category);
        }
        public async Task<Category> GetChildCategory()
        {
            return (await GetMainCategories()).FirstOrDefault();
        }
    }
}
