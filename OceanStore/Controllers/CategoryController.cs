﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using OceanStore.BusinessLayer.Managers;
using OceanStore.DataAccesLayer.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace OceanStore.Controllers
{
    [Authorize(Roles = "SuperAdmin")]
    public class CategoryController : Controller
    {
        #region ctor
        private readonly IWebHostEnvironment _env;
        private readonly CategoryManager _categoryManager;
        private readonly ProductManager _productManager;
        public CategoryController(IWebHostEnvironment env, CategoryManager categoryManager, ProductManager productManager)
        {
            _env = env;
            _categoryManager = categoryManager;
            _productManager = productManager;
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
                string folder = Path.Combine(_env.WebRootPath, "assets", "images", "category");
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

        #region Activity
        public async Task<IActionResult> Activity(int? id)
        {
            if (id == null)
                return NotFound();
            Category category = await _categoryManager.GetAsync(x => x.Id == id);
            if (category == null)
                return BadRequest();
            await _categoryManager.ActivityCategory(category);
            return RedirectToAction("Index");
        }
        #endregion

        #region Delete
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();
            Category category = await _categoryManager.GetAsync(x => x.Id == id);
            if (category == null)
                return BadRequest();

            #region DeleteProduct
            if (category.IsMain)
            {
                List<Product> products = await _productManager.GetAllAsync(x => x.ProductCategories.Any(x => x.CategoryId == category.Id && x.Category.IsMain));
                if (products != null)
                {
                    foreach (Product product in products)
                    {
                        foreach (ProductImage productImage in product.ProductImages)
                        {
                            string folder = Path.Combine(_env.WebRootPath, "assets", "images", "product");
                            string path = Path.Combine(folder, productImage.Image);
                            if (System.IO.File.Exists(path))
                            {
                                System.IO.File.Delete(path);
                            }
                        }
                        await _productManager.DeleteAsync(product);
                    }
                }
            }
            #endregion

            await _categoryManager.DeleteAsync(category);
            if (category.Image != null)
            {
                string folder = Path.Combine(_env.WebRootPath, "assets", "images", "category");
                string path = Path.Combine(folder, category.Image);
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
            }
            return RedirectToAction("Index");
        }
        #endregion
    }
}
