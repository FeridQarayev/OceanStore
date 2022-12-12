using Microsoft.EntityFrameworkCore;
using OceanStore.BusinessLayer.Interfaces;
using OceanStore.BusinessLayer.Repositorys;
using OceanStore.DataAccesLayer.DataContext;
using OceanStore.DataAccesLayer.Interface;
using OceanStore.DataAccesLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OceanStore.BusinessLayer.Managers
{
    public class ProductManager : GenericRepository<Product,AppDbCotext>
    {
        private readonly AppDbCotext _db;
        public ProductManager(AppDbCotext db) : base(db)
        {
            _db = db;
        }
        public async Task<List<Product>> GettAllProduct()
        {
            var data = await GetAllAsync(x => x.Id == 1);
            return data;
        }
        public override Task<List<Product>> GetAllAsync(Expression<Func<Product, bool>> filter = null)
        {
            //return base.GetAllAsync(filter);
            return _db.Products.Where(x => x.Price == 12).ToListAsync();
        }
    }
}
