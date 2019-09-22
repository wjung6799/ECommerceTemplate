using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
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
        public Task<IEnumerable<SelectListItem>> GetBrandsAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<Catalog> GetCatalogItemsAsync(int page, int size, int? brand, int? type)
        {
            var catalogItemsUri = ApiPaths.Catalog
                            .GetAllCatalogItems(_baseUri,
                                page, size, brand, type);
            var dataString = await _client.GetStringAsync(catalogItemsUri);
            var response = JsonConvert.DeserializeObject<Catalog>(dataString);
            return response;
        }

        public Task<IEnumerable<SelectListItem>> GetTypesAsync()
        {
            throw new NotImplementedException();
        }
    }
}
