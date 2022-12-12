using Microsoft.AspNetCore.Mvc;
using OceanStore.BusinessLayer.Managers;
using System.Threading.Tasks;

namespace OceanStore.Controllers
{
    public class ProductController : Controller
    {
        private readonly ProductManager _productManager;

        public ProductController(ProductManager productManager)
        {
            _productManager = productManager;
        }
        public async Task<IActionResult> IndexAsync()
        {
            var data = await _productManager.GettAllProduct();
            return View(data);
        }
    }
}
