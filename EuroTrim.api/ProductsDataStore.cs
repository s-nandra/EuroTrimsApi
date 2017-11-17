using EuroTrim.api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EuroTrim.api
{
    public class ProductsDataStore
    {
        public static ProductsDataStore Current { get; } = new ProductsDataStore();
        public List<ProductDto> Products { get; set; }

        public ProductsDataStore()
        {
            Products = new List<ProductDto>()
            {
                new ProductDto()
                {
                    Id=1,
                    ProdName="xyz",
                    BuyPrice=2

                },
                new ProductDto()
                {
                    Id=2,
                    ProdName="Test",
                    BuyPrice=4

                },

            };
        }
    }
}
