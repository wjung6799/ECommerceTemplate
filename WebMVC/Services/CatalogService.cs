using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using WebMVC.Infrastructure;
using WebMVC.Models;

namespace WebMVC.Services
{
    public class CatalogService : ICatalogService
    {
        private readonly IHttpClient _client;
        private readonly string _baseUri;
        public CatalogService(IConfiguration config,
            IHttpClient client)
        {
            _baseUri = $"{config["CatalogUrl"]}/api/catalog/";
            _client = client;
        }
        public async Task<IEnumerable<SelectListItem>> GetBrandsAsync()
        {
            var brandUri = ApiPaths.Catalog.GetAllBrands(_baseUri);
            var dataString = await _client.GetStringAsync(brandUri);
            var items = new List<SelectListItem>
            {
                new SelectListItem
                {
                    Value=null,
                    Text = "All",
                    Selected = true
                }
            };
            var brands = JArray.Parse(dataString);
            foreach (var brand in brands)
            {
                items.Add(
                    new SelectListItem
                    {
                        Value = brand.Value<string>("id"),
                        Text = brand.Value<string>("brand")
                    }
                 );
            }

            return items;
        }

        public async Task<Catalog> GetCatalogItemsAsync(int page, 
            int size, int? brand, int? type)
        {
            var catalogItemsUri = ApiPaths.Catalog
                            .GetAllCatalogItems(_baseUri,
                                page, size, brand, type);
            var dataString = await _client.GetStringAsync(catalogItemsUri);
            var response = JsonConvert.DeserializeObject<Catalog>(dataString);
            return response;
        }

        public async Task<IEnumerable<SelectListItem>> GetTypesAsync()
        {
            var typeUri = ApiPaths.Catalog.GetAllTypes(_baseUri);
            var dataString = await _client.GetStringAsync(typeUri);
            var items = new List<SelectListItem>
            {
                new SelectListItem
                {
                    Value=null,
                    Text="All",
                    Selected = true
                }
            };

            var types = JArray.Parse(dataString);
            foreach (var type in types)
            {
                items.Add(
                    new SelectListItem
                    {
                        Value = type.Value<string>("id"),
                        Text = type.Value<string>("type")
                    }
                 );
            }

            return items;
        }
    }
}
