using EuroTrim.api.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EuroTrim.api
{
    public static class EuroTrimExtensions
    {
        public static void EnsureSeedDataForContext(this EuroTrimContext context)
        {
            if (context.Customers.Any())
            {
                return;
            }


            var customers = new List<Customer>()
            {
                new Customer()
                {

                    Name = "Satnam Nandra",
                    Address1 = "22 tree st",
                    Address2 = "Earley",
                    PostCode = "MG1 1HL",
                    Product = new List<Product>()
                    {
                        new Product()
                        {

                            ProdName="xxx",
                            Description="my xxx product",
                            Category = new Category()
                            {
                                Name="Zipper",
                                IsActive=true
                            }
                        },
                        new Product()
                        {

                            ProdName="yyy",
                            Description="my 2nd prod",
                            Category = new Category()
                            {
                                Name="Fastenings",
                                IsActive=true
                            }
                        }
                    }
                },
                new Customer()
                {
                    Name = "Indy Sogi",
                    Address1 = "32 xyz",
                    PostCode = "77D Duu",
                    Product = new List<Product>()
                    {
                        new Product()
                        {
                            ProdName="xxx",
                            Description="my xxx product",
                            Category = new Category()
                            {
                                Name="Zipper",
                                IsActive=true
                            }
                        }
                    }
                },
                new Customer()
                {
                    Name = "john Doe",
                    Address1 = "22 Joe st",
                    
                    Address2 = "Nowhwre",
                    City= "Anywhere",
                    PostCode="TG4 6HH"
                    
                }
            };

            context.Customers.AddRange(customers);
            context.SaveChanges();


        }
    }
}
