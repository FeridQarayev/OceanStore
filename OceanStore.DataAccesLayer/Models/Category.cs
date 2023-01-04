using Microsoft.AspNetCore.Http;
using OceanStore.DataAccesLayer.Interface;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OceanStore.DataAccesLayer.Models
{
    public class Category:IEntity
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string? Image { get; set; }
        public bool IsMain { get; set; }
        public Category? Parent { get; set; }
        public int? ParentId { get; set; }
        public List<Category>? Children { get; set; }
        public bool IsDeactive { get; set; }
        [NotMapped]
        public IFormFile Photo { get; set; }
        public List<ProductCategory> ProductCategories { get; set; }
    }
}
