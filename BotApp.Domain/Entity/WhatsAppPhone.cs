
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotApp.Domain.Entity
{
    [Table("tWhatsAppPhone")]
    public class WhatsAppPhone
    {
        [Key]
        public int WhatsAppPhoneId { get; set; }
        public string Wa_id { get; set; }  
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public DateTime? IsDeleted { get; set; }
    } 
}
