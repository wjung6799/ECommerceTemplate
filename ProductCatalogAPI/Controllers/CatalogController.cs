using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ProductCatalogAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatalogController : ControllerBase
    {
        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> Items(
            [FromQuery] int pageIndex = 0, 
            [FromQuery] int pageSize = 6)
        {

        }
    }
}