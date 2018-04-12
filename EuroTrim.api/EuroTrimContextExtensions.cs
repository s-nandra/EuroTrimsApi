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
                    Id = new Guid("25320c5e-f58a-4b1f-b63a-8ee07a840bdf"),
                    Name = "Satnam Nandra",
                    Address1 = "22 tree st",
                    Address2 = "Earley",
                    PostCode = "MG1 1HL"
                },
                new Customer()
                {
                    Id = new Guid("76053df4-6687-4353-8937-b45556748abe"),
                    Name = "Indy Sogi",
                    Address1 = "32 xyz",
                    PostCode = "77D Duu"
                },
                new Customer()
                {
                    Id = new Guid("412c3012-d891-4f5e-9613-ff7aa63e6bb3"),
                    Name = "john Doe",
                    Address1 = "22 Joe st",
                    Address2 = "Nowhwre",
                    City= "Anywhere",
                    PostCode="TG4 6HH"
                },
                new Customer()
                {
                    Id = new Guid("a54ece62-3998-4334-8796-2e26ae54f86b"),
                    Name = "Jane Doe",
                    Address1 = "24 Cumberland Street",
                    Address2 = "Everywhere",
                    City= "Brums",
                    PostCode="B66 3KK",
                    Company="Micro",
                    ContactNumber=099992222,
                    DateCreated=DateTime.Now

                }
            };




            context.Customers.AddRange(customers);
            context.SaveChanges();

            var products = new List<Product>()
            {
                new Product()
                {
                    Id = new Guid("0a579eb8-4627-4aad-906f-12431ba1cf0d"),
                    ProdName="Another Prof",
                    Description="49 2nd LEMON",
                    PartNo="LEEE",
                    Colour="Blue",
                    BuyPrice=39,
                    Quantity=88
                },
                new Product()
                {
                    Id = new Guid("10993860-6514-43f4-93ff-078d97cf7584"),
                    ProdName="proddd2",
                    Description="4sfdsf LEMON",
                    PartNo="2222",
                    Colour="Red",
                    BuyPrice=333,
                    Quantity=828
                }
                ,
                new Product()
                {
                    Id = new Guid("646e7696-e24b-48e5-a091-c53210c4b4ad"),
                    ProdName="NEEE",
                    Description="4sfdsf LEMON",
                    PartNo="222",
                    Colour="Red",
                    BuyPrice=313,
                    Quantity=828,
                    Size="Large"   
                },
                new Product()
                {
                    Id = new Guid("9ef7b3f3-07cb-4409-abe5-ecc2a99d5941"),
                    ProdName="Big Zip",
                    Description="this is a LEMON",
                    PartNo="9922",
                    Colour="Black",
                    BuyPrice=8822,
                    Quantity=23,
                    Size="Big"
                }

            };


            context.Products.AddRange(products);
            context.SaveChanges();

            var orders = new List<Order>()
            {
                new Order()
                {
                     Id=new Guid("71f4069b-627d-48fb-ab06-5894d957de3a"),
                     CustomerId=new Guid("76053df4-6687-4353-8937-b45556748abe"),
                     ProductId=new Guid("10993860-6514-43f4-93ff-078d97cf7584"),
                     DateOrderCreated=DateTime.Now
                },
                new Order()
                {
                     Id=new Guid("11894656-02b2-440d-91a7-c7c19deabad5"),
                     CustomerId=new Guid("76053df4-6687-4353-8937-b45556748abe"),
                     ProductId=new Guid("646e7696-e24b-48e5-a091-c53210c4b4ad"),
                     DateOrderCreated=DateTime.Now
                },
                new Order()
                {
                     Id=new Guid("65c071e2-cb16-4c13-a923-5d3113597869"),
                     CustomerId=new Guid("a54ece62-3998-4334-8796-2e26ae54f86b"),
                     ProductId=new Guid("9ef7b3f3-07cb-4409-abe5-ecc2a99d5941"),
                     DateOrderCreated=DateTime.Now
                }
            };

            context.Orders.AddRange(orders);
            context.SaveChanges();

        }
    }
}
