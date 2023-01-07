using OceanStore.DataAccesLayer.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
