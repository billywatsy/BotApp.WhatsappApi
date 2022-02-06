using BotApp.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MiniSchool.Service;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MiniSchool.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ParentController : ControllerBase 
    {
        [HttpPost("GetParentStudentList")]
    public async Task<IActionResult> GetParentStudentList()
    {
        var response = new BotApp.Service.Model.ResponseClientWebHookTemplate();
        try
        {
            HttpContext.Request.EnableBuffering();
            Request.Body.Position = 0;

            var queryString = HttpContext?.Request?.Query?.ToList() ?? new List<KeyValuePair<string, Microsoft.Extensions.Primitives.StringValues>>();
            var headers = HttpContext.Request.Headers;
            var stream = HttpContext.Request.Body;

            using (var reader = new StreamReader(Request.Body))
            {
                var dataRequest = await reader.ReadToEndAsync();
                    JObject jObject = JObject.Parse(dataRequest);
                    response = ParentHook.GetParentStudentList(jObject) ?? new BotApp.Service.Model.ResponseClientWebHookTemplate();

                    // Do something
            }
        }
        catch (Exception ex)
        {
        }
        return new OkObjectResult(response);
    }

        [HttpPost("GetExamYearTermGroupByStudentId")]
        public async Task<IActionResult> GetExamYearTermGroupByStudentId()
        {
            var response = new BotApp.Service.Model.ResponseClientWebHookTemplate();
            try
            {
                HttpContext.Request.EnableBuffering();
                Request.Body.Position = 0;

                var queryString = HttpContext?.Request?.Query?.ToList() ?? new List<KeyValuePair<string, Microsoft.Extensions.Primitives.StringValues>>();
                var headers = HttpContext.Request.Headers;

                using (var reader = new StreamReader(Request.Body))
                {
                    var dataRequest = await reader.ReadToEndAsync();
                    JObject jObject = JObject.Parse(dataRequest);
                    response = ParentHook.GetExamYearTermGroupByStudentId(jObject) ?? new BotApp.Service.Model.ResponseClientWebHookTemplate();

                    // Do something
                }
            }
            catch (Exception ex)
            {
            }
            return new OkObjectResult(response);
        }


        [HttpPost("GetStudentExamByYearTermList")]
        public async Task<IActionResult> GetStudentExamByYearTermList()
        {
            var response = new BotApp.Service.Model.ResponseClientWebHookTemplate();
            try
            {
                HttpContext.Request.EnableBuffering();
                Request.Body.Position = 0;

                var queryString = HttpContext?.Request?.Query?.ToList() ?? new List<KeyValuePair<string, Microsoft.Extensions.Primitives.StringValues>>();
                var headers = HttpContext.Request.Headers;

                using (var reader = new StreamReader(Request.Body))
                {
                    var dataRequest = await reader.ReadToEndAsync();
                    JObject jObject = JObject.Parse(dataRequest);
                    response = ParentHook.GetStudentExamByYearTermList(jObject) ?? new BotApp.Service.Model.ResponseClientWebHookTemplate();
                    // Do something
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