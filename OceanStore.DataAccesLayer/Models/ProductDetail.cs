using OceanStore.DataAccesLayer.Interface;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OceanStore.DataAccesLayer.Models
{
    public class ProductDetail :IEntity
    {
        public int Id { get; set; }
        public double Tax { get; set; }
        [Required]
        public string Brand { get; set; }
        public string Description { get; set; }
        public bool HasStock { get; set; }
        public DateTime CreateTime { get; set; }
        public Product Product { get; set; }
        [ForeignKey("Product")]
        public int ProductId { get; set; }
    }
}
