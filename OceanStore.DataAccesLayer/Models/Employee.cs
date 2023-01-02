using OceanStore.DataAccesLayer.Interface;
using System.ComponentModel.DataAnnotations;

namespace OceanStore.DataAccesLayer.Models
{
    public class Employee:IEntity
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [RegularExpression(@"^(\+?\d{2,3}\-?\d{3}\-?\d{4})$",ErrorMessage = "055-444-2332 və ya 55-444-2332")]
        public string PhoneNumber { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public bool IsDeactive { get; set; }
        public Position Position { get; set; }
        public int PositionId { get; set; }
        public EmployeeDetail EmployeeDetail { get; set; }
    }
}
