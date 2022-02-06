using BotApp.DataAccess;
using BotApp.Domain.Entity;
using BotApp.Domain.Entity.Dto;
using BotApp.Service.Extensions;
using BotApp.Service.Model; 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace BotApp.Service
{
    public static class HookUtility
    {
        public static ResponseClientWebHookTemplate ExternalWebHook(BotMenu menu, string urlCode, Newtonsoft.Json.Linq.JObject formValues)
        {
            string clientWebHookUrl = "";
            if (menu == null)
                return new ResponseClientWebHookTemplate("DEC", "Error processing menu");

            if (string.IsNullOrEmpty(menu.BaseUrl))
                return new ResponseClientWebHookTemplate("DEC", "Error menu does not have base url");

            clientWebHookUrl = menu.BaseUrl + urlCode;

            var menuHeaders =/* BotMenuFieldInputRequestHeaderRepository.GetListOfHeadersByMenuId(menu.BotMenuId) ??*/ new List<BotMenuFieldInputRequestHeader>();
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            var clientRequest = new HttpClient();

            if (menuHeaders != null && menuHeaders?.Count() >= 1)
            {
                foreach (var item in menuHeaders)
                    clientRequest.DefaultRequestHeaders.Add(item.HeaderName, item.HeaderValue);
            }
            else
            {
                clientRequest.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                clientRequest.DefaultRequestHeaders.Add("user-agent", "Mozilla/5.0 (Windows NT 6.1; WOW64; rv:25.0) Gecko/20100101 Firefox/25.0");
            }

            var uri = new Uri(clientWebHookUrl);

            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            var stringContent = new StringContent(formValues.ToString(), Encoding.UTF8, "application/json");

            var response = clientRequest.PostAsync(uri, stringContent).Result;
            var resData = response.Content.ReadAsStringAsync().Result;

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var requestJSON = resData.GetJobjectFromStringParse();
                var paramsCode = requestJSON.GetJsonObjectStringValue("code");
                var paramsDescription = requestJSON.GetJsonObjectStringValue("description");
                var paramsPreRenderText = requestJSON.GetJsonObjectStringValue("preRenderText") ?? "";
                formValues["resultParameters"] = requestJSON.GetJsonArray("resultParameters");
                var paramsDataLookUp = Newtonsoft.Json.JsonConvert.DeserializeObject<List<UserScreenLookUpDataTemplateDto>>(requestJSON.GetJsonObjectStringValue("dataLookUp"));

                var data = new ResponseClientWebHookTemplate(paramsCode, paramsDescription, paramsPreRenderText, formValues, paramsDataLookUp);

                return data;
            }
            else
            {
                return new ResponseClientWebHookTemplate("DEC", "Error processing form");
            }
        }


        public static ResponseClientWebHookTemplate ExcuteClassPathByReflection(BotMenu menu, string fullClassName, Newtonsoft.Json.Linq.JObject formValues)
        {
            if (menu == null)
                return new ResponseClientWebHookTemplate("DEC", "Error processing menu");

            if (string.IsNullOrEmpty(fullClassName))
                return new ResponseClientWebHookTemplate("DEC", "Error menu field hook not set");

            var data = new ResponseClientWebHookTemplate();
            try
            {
                List<string> split = fullClassName.Split("."[0]).ToList();
                string methodName = split[split.Count() - 1];
                split.RemoveAt(split.Count() - 1);
                string className = string.Join(".", split);
                Type t = Type.GetType(className);
                var instance = Activator.CreateInstance(t);
                System.Reflection.MethodInfo method = t.GetMethod(methodName);

                List<object> paramss = new List<object>();
                paramss.Add(formValues);
                data = method.Invoke(instance, paramss.ToArray()) as ResponseClientWebHookTemplate;
                data.Code = "APP";
                if (data == null)
                {
                    data = new ResponseClientWebHookTemplate();
                    data.Description = "Error processing hook";
                }
            }
            catch (Exception er)
            {
                data.Code = "Code";
                data.Description = "Error processing hook"; 
            }
            return data;

        }

    }
}
