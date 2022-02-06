
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotApp.Domain.Entity
{
    [Table("tClient")]
    public class Client
    {
        [Key] 
        public int ClientId { get; set; }
        public string Code { get; set; } 
        public string Description { get; set; } 
        public string ClientWebHookUrl { get; set; } 
        public string ClientWebHookHeaderKey { get; set; } 
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public DateTime? IsDeleted { get; set; }
    } 
}
