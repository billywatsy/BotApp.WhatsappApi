using BotApp.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MiniSchool.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AppHookController : ControllerBase 
    {
        [HttpGet("GetMenuTree")]
        public string GetMenuTree()
        {
            var response = "";
            try
            {
                var items = BotApp.Service.MenuService.Menu();
                return items.ToString();
            }
            catch (Exception ex)
            {
            }
            return "";
        }


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

                            response = WebHookHandler.SendMessage(WebHookHandler.Handler(dataRequest, GetPairs(queryString)));
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

    public static List<BotApp.Service.Model.StringKeyPair> GetPairs(List<KeyValuePair<string, Microsoft.Extensions.Primitives.StringValues>> queryParams)
    {
        if (queryParams == null || queryParams?.Count() <= 0)
            return new List<BotApp.Service.Model.StringKeyPair>();

        var items = new List<BotApp.Service.Model.StringKeyPair>(); ;
        foreach (var t in queryParams)
            items.Add(new BotApp.Service.Model.StringKeyPair(t.Key, t.Value.ToString()));
        return items;
    }
}
}