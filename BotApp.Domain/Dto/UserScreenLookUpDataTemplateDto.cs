using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotApp.Domain.Entity.Dto
{
    public class UserScreenLookUpDataTemplateDto
    {
        public UserScreenLookUpDataTemplateDto(string code, string description)
        {
            Code = code;
            Description = description;
        }
        public string Code { get; set; } 
        public string Description { get; set; } 
    }
}
