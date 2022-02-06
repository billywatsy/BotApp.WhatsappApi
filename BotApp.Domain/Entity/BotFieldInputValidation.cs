
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotApp.Domain.Entity
{
    [Table("tBotFieldInputValidation")]
    public class BotFieldInputValidation
    {
        [Key]
        public int BotFieldInputValidationId { get; set; }
        public int BotFieldValidationTypeId { get; set; }
        public int BotFieldInputId { get; set; }
        public string Value { get; set; }
        public string Description { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public DateTime? IsDeleted { get; set; }
    } 
}
