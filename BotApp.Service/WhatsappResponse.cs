using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BotApp.Service
{
    public class WhatsappResponse
    {
        public string GeneratedHtml { get; set; }
        public string FileUrl { get; set; }
        public string Wa_id { get; set; } 
       
       
        public WhatsappResponse(string responseSingleProcess , string wa_id)
        {
            GeneratedHtml = responseSingleProcess;
            Wa_id = wa_id;
        }
        public WhatsappResponse(string responseSingleProcess , string wa_id , string fileUrl)
        {
            GeneratedHtml = responseSingleProcess;
            Wa_id = wa_id;
            FileUrl = fileUrl;
        }
        public WhatsappResponse()
        {
        }

        public override string ToString()
        {
            return GeneratedHtml?.ToString() ?? "";
        }




    }
}
