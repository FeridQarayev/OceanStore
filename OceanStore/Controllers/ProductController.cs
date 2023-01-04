using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OceanStore.BusinessLayer.Managers;
using OceanStore.DataAccesLayer.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace OceanStore.Controllers
{
    [Authorize(Roles = "Admin,SuperAdmin")]
    public class ProductController : Controller
    {
        #region ctor
        private readonly ProductManager _productManager;
        private readonly CategoryManager _categoryManager;
        private readonly IWebHostEnvironment _env;
        public ProductController(ProductManager productManager, IWebHostEnvironment env, CategoryManager categoryManager)
        {
            _productManager = productManager;
            _env = env;
            _categoryManager = categoryManager;
        }
        #endregion

        #region Index
        public async Task<IActionResult> Index()
        {
            List<Product> products = await _productManager.GettAllProduct();
            return View(products);
        }
        #endregion

        #region Create
        public async Task<IActionResult> Create()
        {
            ViewBag.MainCategories = await _categoryManager.GetMainCategories();
            Category childCat = await _categoryManager.GetChildCategory();
            ViewBag.ChildCategory = childCat != null ? childCat.Children : null;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product product, int? mainId, int? childCatId)
        {
            ViewBag.MainCategories = await _categoryManager.GetMainCategories();
            Category childCat = await _categoryManager.GetChildCategory();
            ViewBag.ChildCategory = childCat != null ? childCat.Children : null;
            if (mainId == null)
            {
                ModelState.AddModelError("", "Select Main Category!");
                return View();
            }
            #region SaveImage
            List<ProductImage> images = new();
            foreach (IFormFile Photo in product.Photos)
            {
                string checkImage = Photo != null ? await _productManager.CheckImage(Photo) : "Please choose photo";
                if (checkImage != null)
                {
                    ModelState.AddModelError("Photos", checkImage);
                    return View();
                }
                string folder = Path.Combine(_env.WebRootPath, "assets", "images", "product");
                ProductImage productImage = new();

                productImage.Image = await _productManager.SavePhotoProject(Photo, folder);
                images.Add(productImage);
            }
            #endregion

            #region SaveCategory
            List<ProductCategory> productCategories = new();
            ProductCategory mainProductCategory = new();
            mainProductCategory.CategoryId = (int)mainId;
            productCategories.Add(mainProductCategory);
            if (childCatId != null)
            {
                ProductCategory childProductCategory = new();
                childProductCategory.CategoryId = (int)childCatId;
                productCategories.Add(childProductCategory);
            }
            #endregion

            product.ProductCategories = productCategories;
            product.ProductImages = images;
            product.ProductDetails.CreateTime = DateTime.UtcNow;

            await _productManager.AddAsync(product);
            return RedirectToAction("Index");
        }
        #endregion

        #region LoadChild
        public async Task<IActionResult> LoadChild(int mainId)
        {
            List<Category> childcategories = await _categoryManager.GetAllAsync(x => x.ParentId == mainId);
            return PartialView("_ChildCategories", childcategories);
        }
        #endregion


    }
}
