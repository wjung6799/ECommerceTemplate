using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ProductCatalogAPI.Data;
using ProductCatalogAPI.Domain;

namespace ProductCatalogAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatalogController : ControllerBase
    {
        private readonly CatalogContext _context;
        private readonly IConfiguration _config;
        public CatalogController(CatalogContext context, 
            IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> Items(
            [FromQuery] int pageIndex = 0, 
            [FromQuery] int pageSize = 6)
        {
            var itemsCount = await 
                _context.CatalogItems.LongCountAsync();

            var items = await _context.CatalogItems
                .OrderBy(c => c.Name)
                .Skip(pageIndex * pageSize)
                .Take(pageSize)
                .ToListAsync();

            items = ChangePictureUrl(items);

            return Ok(items);
        }

        [HttpGet]
        [Route("[action]/type/{catalogTypeId}/brand/{catalogBrandId}")]
        public async Task<IActionResult> Items(
            int? catalogTypeId,
            int? catalogBrandId,
        [FromQuery] int pageIndex = 0,
        [FromQuery] int pageSize = 6)
        {
            var root = (IQueryable<CatalogItem>)_context.CatalogItems;
            if (catalogTypeId.HasValue)
            {
                root = 
                    root.Where(c => c.CatalogTypeId == catalogTypeId);
            }
            if (catalogBrandId.HasValue)
            {
                root = 
                    root.Where(c => c.CatalogBrandId == catalogBrandId);
            }


            var itemsCount = await
                root.LongCountAsync();

            var items = await root
                .OrderBy(c => c.Name)
                .Skip(pageIndex * pageSize)
                .Take(pageSize)
                .ToListAsync();

            items = ChangePictureUrl(items);

            return Ok(items);
        }


        private List<CatalogItem> ChangePictureUrl(
            List<CatalogItem> items)
        {
            items.ForEach(
                c => c.PictureUrl = c.PictureUrl
                        .Replace("http://externalcatalogbaseurltobereplaced",
                        _config["ExternalCatalogBaseUrl"]));
            return items;
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> CatalogTypes()
        {
            var items = await _context.CatalogTypes.ToListAsync();
            return Ok(items);
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> CatalogBrands()
        {
            var items = await _context.CatalogBrands.ToListAsync();
            return Ok(items);
        }
    }
}