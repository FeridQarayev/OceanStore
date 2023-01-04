using OceanStore.DataAccesLayer.Interface;

namespace OceanStore.DataAccesLayer.Models
{
    public class ProductImage :IEntity
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public Product Product { get; set; }
        public int ProductId { get; set; }
    }
}
