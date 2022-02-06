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
    public class MenuController : ControllerBase 
    {
        [HttpGet("GetMenuTree")]
        public async Task<IActionResult> GetMenuTree()
        {
            var response = "";
            try
            {
                var items = BotApp.Service.MenuService.Menu();

                return new OkObjectResult(items);
            }
            catch (Exception ex)
            {
                // RETURN ERROR OR BAD REQUEST
            }
            return new OkObjectResult(response);
        }
 
    }
}
