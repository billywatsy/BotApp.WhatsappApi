using BotApp.Service.Extensions;
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
    public class CountryController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Index()
        {
            var response = "";
            try
            {
                HttpContext.Request.EnableBuffering();
                Request.Body.Position = 0;

                var headers = HttpContext.Request.Headers;

                using (StreamReader stream = new StreamReader(HttpContext.Request.Body))
                {
                    var task = stream
                        .ReadToEndAsync()
                        .ContinueWith(t => {
                            var dataRequest = t.Result;
                            var data = dataRequest.GetJobjectFromStringParse().GetJsonArray("resultParameters");

                        });
                    // await processing of the result
                    task.Wait();


                    var appResponse = new
                    {
                        Code = "APP",
                        Description = "success",
                        PreRenderText = "",
                        DataLookUp = new List<object>() {
                                new { Code = "ZIM" , Description = "Zimbabwe" },
                                new { Code = "CAN" , Description = "Canada" },
                                new { Code = "PAN" , Description = "Panama" },
                                new { Code = "BOS" , Description = "Bostwana" },
                            },
                        ResultParameters = new List<object>() {
                                    new {
                                    Name = "Fullname" ,
                                    Value = "Billy Watsikenyere"
                                }
                                }
                    };

                    return new OkObjectResult(appResponse);

                }
            }
            catch (Exception ex)
            {
            }
            return new OkObjectResult(response);
        }



    }
}
