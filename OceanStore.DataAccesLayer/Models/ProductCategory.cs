using OceanStore.DataAccesLayer.Interface;

namespace OceanStore.DataAccesLayer.Models
{
    public class ProductCategory :IEntity
    {
        public int Id { get; set; }
        public Product Product { get; set; }
        public int ProductId { get; set; }
        public Category Category { get; set; }
        public int CategoryId { get; set; }
    }
}
