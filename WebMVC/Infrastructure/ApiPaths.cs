using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMVC.Infrastructure
{
    public class ApiPaths
    {
        public static class Catalog
        {
            public static string GetAllTypes(string baseUri)
            {
                return $"{baseUri}catalogtypes";
            }
            public static string GetAllBrands(string baseUri)
            {
                return $"{baseUri}catalogbrands";
            }

            public static string GetAllCatalogItems(string baseUri,
                int page, int take, int? brand, int? type)
            {
                var filterQs = string.Empty;

                if (brand.HasValue || type.HasValue)
                {
                    var brandsQs = (brand.HasValue) ? brand.Value.ToString() : "null";
                    var typeQs = (type.HasValue) ? type.Value.ToString() : "null";
                    filterQs = $"/type/{typeQs}/brand/{brandsQs}";
                }

                return $"{baseUri}items{filterQs}?pageIndex={page}&pageSize={take}";

            }

        }

    }
}
