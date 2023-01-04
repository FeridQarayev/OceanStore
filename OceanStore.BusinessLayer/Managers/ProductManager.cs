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
    public class ProductManager : GenericRepository<Product, AppDbCotext>
    {
        private readonly AppDbCotext _db;
        public ProductManager(AppDbCotext db) : base(db)
        {
            _db = db;
        }
        public override async Task<List<Product>> GetAllAsync(Expression<Func<Product, bool>> filter = null)
        {
            return await _db.Products.Include(x => x.ProductDetails).Include(x => x.ProductImages).Include(x => x.ProductCategories).ThenInclude(x => x.Category).ToListAsync();
        }
        public async Task<List<Product>> GettAllProduct()
        {
            return await GetAllAsync();
        }
        public async override Task<Product> GetAsync(Expression<Func<Product, bool>> filter = null)
        {
            return filter == null ?
                await _db.Products.Include(x => x.ProductDetails).Include(x => x.ProductImages).Include(x => x.ProductCategories).ThenInclude(x => x.Category).ThenInclude(x => x.Children).FirstOrDefaultAsync() :
                await _db.Products.Include(x => x.ProductDetails).Include(x => x.ProductImages).Include(x => x.ProductCategories).ThenInclude(x => x.Category).ThenInclude(x => x.Children).FirstOrDefaultAsync(filter);
        }
        public async Task<ProductImage> GetProductImageById(int id)
        {
            return await _db.ProductImages.FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<int> ProductImageCount(int id)
        {
            return await _db.ProductImages.Where(x=>x.ProductId== id).CountAsync();
        }
        public async Task DeleteProductImage(ProductImage productImage)
        {
            _db.ProductImages.Remove(productImage);
            await _db.SaveChangesAsync();
        }
    }
}
