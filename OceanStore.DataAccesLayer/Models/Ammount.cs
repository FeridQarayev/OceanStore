using OceanStore.DataAccesLayer.Interface;
using System;
using System.ComponentModel.DataAnnotations;

namespace OceanStore.DataAccesLayer.Models
{
    public class Ammount :IEntity
    {
        public int Id { get; set; }
        [Required]
        public double Price { get; set; }
        public string Description { get; set; }
        public DateTime CreateTime { get; set; }
        public string CreatedBy { get; set; }

        public bool RecorderKind { get; set; }
    }
}
