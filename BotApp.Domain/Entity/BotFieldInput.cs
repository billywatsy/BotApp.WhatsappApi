
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotApp.Domain.Entity
{
    [Table("tBotFieldInput")]
    public class BotFieldInput
    {
        [Key]
        public int BotFieldInputId { get; set; }
        public int BotMenuId  { get; set; }
        public int BotFieldInputTypeId { get; set; } 
        public int OrderId { get; set; }
        public string FieldCode { get; set; }
        public string Description { get; set; }
        public string DisplayDescription { get; set; }
        public string PreRenderTemplateTextUrl { get; set; }
        public string AfterValidateWebHookUrl { get; set; }
        public string PreRenderTemplateTextReflection { get; set; }
        public string AfterValidateWebHookReflection { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public DateTime? IsDeleted { get; set; }
    } 
}
