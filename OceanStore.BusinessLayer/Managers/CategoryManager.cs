﻿using Microsoft.AspNetCore.Http;
using OceanStore.BusinessLayer.Helpers;
using OceanStore.BusinessLayer.Repositorys;
using OceanStore.DataAccesLayer.DataContext;
using OceanStore.DataAccesLayer.Models;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace OceanStore.BusinessLayer.Managers
{
    public class CategoryManager : GenericRepository<Category, AppDbCotext>
    {
        public CategoryManager(AppDbCotext db) : base(db)
        {
        }
        public async Task<List<Category>> GetAllCategories()
        {
            return await GetAllAsync();
        }
        public async Task<List<Category>> GetMainCategories()
        {
            return await GetAllAsync(x => x.IsMain);
        }
        public async Task<bool> IsExistCategoryName(Category category) => await CheckExist(x => x.Name == category.Name);
        public async Task<string> CheckImage(IFormFile photo)
        {
            if (!photo.IsImage())
                return "Please choose Image file";
            if (photo.IsOlderTwoMB())
                return "Image max 2MB";
            return null;
        }
        public async Task<string> SavePhotoProject(IFormFile photo, string folder)
        {
            return await photo.SaveFileAsync(folder);
        }
    }
}
