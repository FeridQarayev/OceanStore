using Microsoft.AspNetCore.Http;
using OceanStore.DataAccesLayer.Interface;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace OceanStore.DataAccesLayer.Models
{
    public class Product:IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public int Rate { get; set; }
        public bool IsDeactive { get; set; }
        public ProductDetail ProductDetails { get; set; }
        public List<ProductCategory> ProductCategories { get; set; }
        public List<ProductImage> ProductImages { get; set; }
        [NotMapped]
        public IFormFile[] Photos { get; set; }
    }
}
