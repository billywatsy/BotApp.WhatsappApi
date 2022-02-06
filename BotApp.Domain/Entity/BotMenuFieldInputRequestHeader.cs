
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotApp.Domain.Entity
{
    [Table("tBotMenuFieldInputRequestHeader")]
    public class BotMenuFieldInputRequestHeader
    {
        [Key]
        public int BotMenuFieldInputRequestHeaderId { get; set; }
        public int BotMenuId { get; set; }
        public string HeaderName { get; set; }
        public string HeaderValue { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public DateTime? IsDeleted { get; set; }
    } 
}
