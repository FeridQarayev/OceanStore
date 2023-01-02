using OceanStore.DataAccesLayer.Interface;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OceanStore.DataAccesLayer.Models
{
    public class Position :IEntity
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [Range(0, double.MaxValue)]
        public double Salary { get; set; }
        public bool IsDeactive { get; set; }
        public List<Employee> Employees { get; set; }
    }
}
