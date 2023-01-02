using OceanStore.DataAccesLayer.Interface;
using System.Collections.Generic;

namespace OceanStore.DataAccesLayer.Models
{
    public class Position :IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Salary { get; set; }
        public bool IsDeactive { get; set; }
        public List<Employee> Employees { get; set; }
    }
}
