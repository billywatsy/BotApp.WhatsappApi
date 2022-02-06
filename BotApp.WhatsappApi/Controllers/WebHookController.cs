using BotApp.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotApp.WhatsappApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WebHookController : ControllerBase 
    {
        [HttpPost]
        public async Task<IActionResult> Index()
        {
            var response = "";
            try
            {
                HttpContext.Request.EnableBuffering();
                Request.Body.Position = 0;

                var queryString = HttpContext?.Request?.Query?.ToList() ?? new List<KeyValuePair<string, Microsoft.Extensions.Primitives.StringValues>>();
                var headers = HttpContext.Request.Headers;

                using (StreamReader stream = new StreamReader(HttpContext.Request.Body))
                {
                    var task = stream
                        .ReadToEndAsync()
                        .ContinueWith(t => {
                            var dataRequest = t.Result;
                            // TODO: Handle the post result! 

                            response =  WebHookHandler.SendMessage(WebHookHandler.Handler(dataRequest , GetPairs(queryString)));
                        });
                    // await processing of the result
                    task.Wait();
                }
            }
            catch (Exception ex)
            {
            }
            return new OkObjectResult(response);
        }

        public static List<Service.Model.StringKeyPair> GetPairs(List<KeyValuePair<string, Microsoft.Extensions.Primitives.StringValues>> queryParams)
        {
            if (queryParams == null || queryParams?.Count() <= 0 )
                return new List<Service.Model.StringKeyPair>();

            var items = new List<Service.Model.StringKeyPair>(); ;
            foreach (var t in queryParams)
                items.Add(new Service.Model.StringKeyPair(t.Key ,t.Value.ToString()));
            return items;
        }
    }
}
