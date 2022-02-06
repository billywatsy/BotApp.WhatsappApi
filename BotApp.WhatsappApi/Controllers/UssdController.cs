using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BotApp.WhatsappApi.Extensions;
using System.Net;
using BotApp.DataAccess;
using System.Net.Http;

namespace BotApp.WhatsappApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UssdController : ControllerBase
    { 

        [HttpGet]
        public IActionResult Index()
        {

            string responseMessage = "Test 1";
            try
            {
                string hook = "BotApp.WhatsappApi.USSD.WhatsappResponse.ValueData";

                List<string> split = hook.Split("."[0]).ToList(); 
                string methodName = split[split.Count() - 1];
                split.RemoveAt(split.Count() - 1);
                string className = string.Join(".", split);
                Type t = Type.GetType(className);
                var instance = Activator.CreateInstance(t);
                System.Reflection.MethodInfo method = t.GetMethod(methodName);

                List<object> paramss = new List<object>();
                paramss.Add(1);
               var data = method.Invoke(instance , paramss.ToArray());
                //

                StringBuilder log = new StringBuilder();
                string P24TraceId = Guid.NewGuid().ToString();
                log.Append("WS|Messaging|Method:ussdapp|P24TraceId:" + P24TraceId);
                  
               

                List<USSD.QueryStringKeyPair> apps = new List<USSD.QueryStringKeyPair>();
                foreach (string key in this.Request.Query.Keys) {
                    this.Request.Query.TryGetValue(key, out Microsoft.Extensions.Primitives.StringValues value); 
                    apps.Add(new USSD.QueryStringKeyPair()
                    {
                        Key = key ,
                        Value = value
                    });
                }

                responseMessage = BotApp.WhatsappApi.USSD.WebHookHandler.Handler(apps).ToString();

                ApplicationLogRepository.LogRequest(log.ToString());

                return new ContentResult
                {
                    ContentType = "application/xml",
                    Content = responseMessage,
                    StatusCode = 200
                };
            }
            catch (Exception ex)
            {
            }
            return new ContentResult
            {
                ContentType = "application/xml",
                Content = responseMessage,
                StatusCode = 200
            };
        }

    }
}
