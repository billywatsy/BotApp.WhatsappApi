using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
using System.Net;

namespace BotApp.WhatsappApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ConfirmController : ControllerBase
    { 


        [HttpPost]
        public async Task<IActionResult> Index()
        {
            var responseCode = "";
            var responseMessage = "";
            try
            {
                HttpContext.Request.EnableBuffering();
                Request.Body.Position = 0;
                var appResponse = new
                {
                    Code = "APP",
                    Description = "",
                    PreRenderText = "",
                    DataLookUp = new List<object>() {
                                new { Code = "YES" , Description = "Yes" },
                                new { Code = "NO" , Description = "No" }
                            },
                    ResultParameters = new List<object>() { }
                };

                return new OkObjectResult(appResponse);
            }
            catch (Exception ex)
            {
            }
            return new OkObjectResult(new
            {
                Code = "DEC",
                Description = "Error processing request",
                PreRenderText = "Error processing request",
                DataLookUp = new List<object>() { },
                ResultParameters = new List<object>() { }
            });
        }

    }
}
