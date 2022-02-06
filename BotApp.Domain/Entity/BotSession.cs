
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotApp.Domain.Entity
{
    [Table("tBotSession")]
    public class BotSession
    {
        [Key]
        public int BotSessionId { get; set; }
        public int ClientId { get; set; }
        public int? BotMenuId  { get; set; }
        public int? BotFieldSubInputId { get; set; }
        public int WhatsAppPhoneId { get; set; }
        public bool IsValid { get; set; }
        public int? BotFieldInputId { get; set; }
        public string UserScreenText { get; set; }
        public int NumberOfLastFailOnField { get; set; }
        //JSON OBJECT
        public string DynamicFormFieldJsonObject { get; set; } 
        public string UserScreenLookUpDataTemplate { get; set; }
        public string ReplaceableFieldCode { get; set; }
        public bool WaitOnNextFieldCode { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public DateTime? IsDeleted { get; set; }
        public WhatsAppPhone WhatsAppPhone { get; set; }
    } 
}
