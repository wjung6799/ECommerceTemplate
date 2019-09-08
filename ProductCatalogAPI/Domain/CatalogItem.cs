using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductCatalogAPI.Domain
{
    public class CatalogItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string PictureUrl { get; set; }

        public int CatalogTypeId { get; set; }
        public virtual CatalogType CatalogType { get; set; }

        public int CatalogBrandId { get; set; }
        public virtual CatalogBrand CatalogBrand { get; set; }
    }
}
