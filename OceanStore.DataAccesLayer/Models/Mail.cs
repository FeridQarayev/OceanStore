using System.ComponentModel.DataAnnotations;

namespace OceanStore.DataAccesLayer.Models
{
    public class Mail
    {
        [Required]
        public string MessageSubject { get; set; }
        public string MessageBody { get; set; }
        public string MailTo { get; set; }
    }
}
