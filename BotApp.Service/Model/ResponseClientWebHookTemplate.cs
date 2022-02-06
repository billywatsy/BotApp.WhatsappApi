
using BotApp.Domain.Entity.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BotApp.Service.Model
{
    public class ResponseClientWebHookTemplate
    {
        public ResponseClientWebHookTemplate()
        {
        }

        public ResponseClientWebHookTemplate(string code , string description )
        {
            Code = code;
            Description = description;
        } 

        public ResponseClientWebHookTemplate(string code , string description , string preRenderText , Newtonsoft.Json.Linq.JObject formValues, List<UserScreenLookUpDataTemplateDto> dataLookUp)
        {
            Code = code;
            Description = description;
            FormValues = formValues;
            PreRenderText = preRenderText;
            DataLookUp = dataLookUp;
        } 

        public string ThirdyPartFieldMapCode { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public string PreRenderText { get; set; }
        public string PreRenderImageUrlFull { get; set; }
        public string ReplaceableFieldCode { get; set; }
        public List<UserScreenLookUpDataTemplateDto> DataLookUp { get; set; }
        public Newtonsoft.Json.Linq.JObject FormValues { get; set; }
    }
}
