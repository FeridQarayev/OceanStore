using OceanStore.DataAccesLayer.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OceanStore.DataAccesLayer.Models
{
    public class User:IEntity
    {
        [Key]
        public int Idd { get; set; }
        public string Namee { get; set; }
        public string Surname { get; set; }
    }
}
