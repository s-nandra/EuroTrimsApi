using EuroTrim.api.Models;
using System;
using System.Collections.Generic;


namespace EuroTrim.api
{
    public class CustomersDataStore
    {
        public static CustomersDataStore Current { get; } = new CustomersDataStore();
        public List<CustomerDto> Customers { get; set; }

        public CustomersDataStore()
        {
            Customers = new List<CustomerDto>()
            {
                new CustomerDto()
                {
                    Id = new Guid("25320c5e-f58a-4b1f-b63a-8ee07a840bdf"),
                    Name = "Satnam Nandra",
                    Decription = "xyz"//,
                    //Product = new List<ProductDto>()
                    //{
                    //    new ProductDto()
                    //    {
                    //        Id=1,
                    //        ProdName="xxx",
                    //        Description="my xxx product",
                    //        Category = new CategoryDto()
                    //        {
                    //            Id=2,
                    //            Name="Zipper"
                    //        }
                    //    },
                    //    new ProductDto()
                    //    {
                    //        Id=2,
                    //        ProdName="yyy",
                    //        Description="my 2nd prod",
                    //        Category = new CategoryDto()
                    //        {
                    //            Id=1,
                    //            Name="Fastenings"
                    //        }
                    //    }
                    //}
                },
                new CustomerDto()
                {
                    Id = new Guid("15320c5e-f58a-4b1f-b63a-8ee07a840bdf"),
                    Name = "Indy Sogi",
                    Decription = "xyz"//,
                    //Product = new List<ProductDto>()
                    //{
                    //    new ProductDto()
                    //    {
                    //        Id=1,
                    //        ProdName="xxx",
                    //        Description="my xxx product",
                    //        Category = new CategoryDto()
                    //        {
                    //            Id=2,
                    //            Name="Zipper"
                    //        }
                    //    } 
                    //}
                },
                new CustomerDto()
                {
                    Id = new Guid("99320c5e-f58a-4b1f-b63a-8ee07a840bdf"),
                    Name = "john Doe",
                    Decription = "jjjj"
                }
            };

        }
    }
}
