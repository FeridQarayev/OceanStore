using Microsoft.EntityFrameworkCore;
using OceanStore.DataAccesLayer.DataContext;
using OceanStore.DataAccesLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OceanStore.BusinessLayer.Managers
{
    public class ProductManager
    {
        private readonly AppDbCotext _db;
        public ProductManager(AppDbCotext db)
        {
            _db = db;
        }
        public async Task<List<Product>> GettAllProduct()
        {
            return await _db.Products.ToListAsync();
        }
    }
}
