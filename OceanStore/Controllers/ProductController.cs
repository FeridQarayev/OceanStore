using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using OceanStore.BusinessLayer.Managers;
using OceanStore.DataAccesLayer.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OceanStore.Controllers
{
    [Authorize(Roles = "Admin,SuperAdmin")]
    public class ProductController : Controller
    {
        #region ctor
        private readonly ProductManager _productManager;
        private readonly IWebHostEnvironment _env;
        public ProductController(ProductManager productManager, IWebHostEnvironment env)
        {
            _productManager = productManager;
            _env = env;
        }
        #endregion

        #region Index
        public async Task<IActionResult> Index()
        {
            List<Product> products = await _productManager.GettAllProduct();
            return View(products);
        }
        #endregion
    }
}
