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
    public class CategoryController : Controller
    {
        #region ctor
        private readonly IWebHostEnvironment _env;
        private readonly CategoryManager _categoryManager;
        public CategoryController(IWebHostEnvironment env, CategoryManager categoryManager)
        {
            _env = env;
            _categoryManager = categoryManager;
        }
        #endregion

        #region Index
        public async Task<IActionResult> Index()
        {
            List<Category> categories = await _categoryManager.GetAllCategories();
            return View(categories);
        }
        #endregion
    }
}
