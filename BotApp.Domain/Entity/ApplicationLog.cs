
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotApp.Domain.Entity
{
    [Table("tApplicationLog")]
    public class ApplicationLog
    {
        [Key]
        public int ApplicationLogId { get; set; }
        public string Description { get; set; }
        public DateTime? CreatedDate { get; set; }
    } 
}
