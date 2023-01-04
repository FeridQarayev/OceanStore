using Microsoft.AspNetCore.Http;
using OceanStore.DataAccesLayer.Interface;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OceanStore.DataAccesLayer.Models
{
    public class Product:IEntity
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        public double Price { get; set; }
        public double Rate { get; set; }
        public bool IsDeactive { get; set; }
        public ProductDetail ProductDetails { get; set; }
        public List<ProductCategory> ProductCategories { get; set; }
        public List<ProductImage> ProductImages { get; set; }
        [NotMapped]
        public IFormFile[] Photos { get; set; }
    }
}
