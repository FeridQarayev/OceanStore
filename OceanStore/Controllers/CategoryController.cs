using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using OceanStore.BusinessLayer.Managers;
using OceanStore.DataAccesLayer.Models;
using System.Collections.Generic;
using System.IO;
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

        #region Create
        public async Task<IActionResult> Create()
        {
            ViewBag.MainCategories = await _categoryManager.GetMainCategories();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Category category, int mainCatId)
        {
            ViewBag.MainCategories = await _categoryManager.GetMainCategories();
            if (!ModelState.IsValid)
                return View();
            if (category.IsMain)
            {
                bool IsExist = await _categoryManager.IsExistCategoryName(category);
                if (IsExist)
                {
                    ModelState.AddModelError("Name", "This Category is already exist");
                    return View();
                }
                string checkImage = category.Photo != null ? await _categoryManager.CheckImage(category.Photo) : "Please choose photo";
                if (checkImage != null)
                {
                    ModelState.AddModelError("Photo", checkImage);
                    return View();
                }
                string folder = Path.Combine(_env.WebRootPath, "assets", "images");
                category.Image = await _categoryManager.SavePhotoProject(category.Photo, folder);
            }
            else
            {
                category.ParentId = mainCatId;
            }
            await _categoryManager.AddAsync(category);
            return RedirectToAction("Index");
        }
        #endregion
    }
}
