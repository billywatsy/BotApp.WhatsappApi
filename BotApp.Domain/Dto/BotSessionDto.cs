using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotApp.Domain.Entity.Dto
{
    public class BotSessionDto
    {
        public int BotSessionId { get; set; }
        public int? BotMenuId { get; set; }
        public int? BotFieldSubInputId { get; set; }
        public int WhatsAppPhoneId { get; set; }
        public bool IsValid { get; set; }
        public int? BotFieldInputId { get; set; }
        //JSON OBJECT
        public string UserScreenText { get; set; }
        public string DynamicFormFieldJsonObject { get; set; }
        public string UserScreenLookUpDataTemplate { get; set; } 
        public string Wa_id { get; set; }
        public string ReplaceableFieldCode { get; set; } 
        public DateTime LastDate { get; set; }
    }
}
