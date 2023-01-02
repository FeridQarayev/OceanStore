using OceanStore.DataAccesLayer.Interface;
using System;

namespace OceanStore.DataAccesLayer.Models
{
    public class Employee:IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
       
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public bool IsDeactive { get; set; }
        public Position Position { get; set; }
        public int PositionId { get; set; }
        public EmployeeDetail EmployeeDetail { get; set; }
    }
}
