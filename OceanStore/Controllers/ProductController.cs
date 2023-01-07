using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OceanStore.BusinessLayer.Managers;
using OceanStore.DataAccesLayer.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
            List<Product> products = await _productManager.GetAllAsync();
            ViewBag.CategoriesCount = (await _categoryManager.GetAllCategories()).Count;
            return View(products);
        }
        #endregion

        #region Create
        public async Task<IActionResult> Create()
        {
            if ((await _categoryManager.GetAllCategories()).Count == 0)
                return BadRequest();
            ViewBag.MainCategories = await _categoryManager.GetMainCategories();
            Category childCat = await _categoryManager.GetChildCategory();
            ViewBag.ChildCategory = childCat != null ? childCat.Children : null;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product product, int? mainId, int? childCatId)
        {
            if ((await _categoryManager.GetAllCategories()).Count == 0)
                return BadRequest();
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
            if (product.Photos == null)
            {
                ModelState.AddModelError("Photos", "Please choose photo");
                return View();
            }
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
            product.ProductDetails.CreateTime = DateTime.UtcNow.AddHours(4);

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

        #region Update
        public async Task<IActionResult> Update(int? id)
        {
            if ((await _categoryManager.GetAllCategories()).Count == 0)
                return BadRequest();
            if (id == null)
                return NotFound();
            Product product = await _productManager.GetAsync(x => x.Id == id);
            if (product == null)
                return BadRequest();
            ViewBag.MainCategories = await _categoryManager.GetMainCategories();
            ViewBag.ChildCategory = product.ProductCategories.FirstOrDefault().Category.Children;
            return View(product);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, Product product, int? mainId, int? childCatId)
        {
            if ((await _categoryManager.GetAllCategories()).Count == 0)
                return BadRequest();
            if (id == null)
                return NotFound();
            Product dbProduct = await _productManager.GetAsync(x => x.Id == id);
            if (dbProduct == null)
                return BadRequest();
            ViewBag.MainCategories = await _categoryManager.GetMainCategories();
            ViewBag.ChildCategory = dbProduct.ProductCategories.FirstOrDefault().Category.Children;
            if (mainId == null)
            {
                ModelState.AddModelError("", "Select Main Category!");
                return View(dbProduct);
            }
            #region SaveImage
            List<ProductImage> images = new();
            if (product.Photos != null)
            {
                foreach (IFormFile Photo in product.Photos)
                {
                    string checkImage = Photo != null ? await _productManager.CheckImage(Photo) : "Please choose photo";
                    if (checkImage != null)
                    {
                        ModelState.AddModelError("Photos", checkImage);
                        return View(dbProduct);
                    }
                    string folder = Path.Combine(_env.WebRootPath, "assets", "images", "product");
                    ProductImage productImage = new();

                    productImage.Image = await _productManager.SavePhotoProject(Photo, folder);
                    images.Add(productImage);
                }
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

            dbProduct.ProductCategories = productCategories;
            dbProduct.ProductImages.AddRange(images);
            dbProduct.ProductDetails = product.ProductDetails;
            dbProduct.Name = product.Name;
            dbProduct.Price = product.Price;
            dbProduct.Rate = product.Rate;
            await _productManager.UpdateAsync(dbProduct);
            return RedirectToAction("Index");
        }

        #endregion

        #region DeleteImage
        public async Task<bool> DeleteImage(int id)
        {
            ProductImage productImages = await _productManager.GetProductImageById(id);
            int count = await _productManager.ProductImageCount(productImages.ProductId);
            if (productImages != null && count > 1)
            {
                await _productManager.DeleteProductImage(productImages);
                string folder = Path.Combine(_env.WebRootPath, "assets", "images", "product");
                string path = Path.Combine(folder, productImages.Image);
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
                return true;
            }
            return false;
        }
        #endregion

        #region Activity
        public async Task<IActionResult> Activity(int? id)
        {
            if (id == null)
                return NotFound();
            Product product = await _productManager.GetAsync(x => x.Id == id);
            if (product == null)
                return BadRequest();
            await _productManager.ActivityCategory(product);
            return RedirectToAction("Index");
        }
        #endregion
    }
}