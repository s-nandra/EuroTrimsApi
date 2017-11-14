using EuroTrim.api.Models;
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
                    Id = 1,
                    Name = "Satnam Nandra",
                    Decription = "xyz"
                },
                new CustomerDto()
                {
                    Id = 2,
                    Name = "Indy Sogi",
                    Decription = "xyz"
                },
                new CustomerDto()
                {
                    Id = 3,
                    Name = "john Doe",
                    Decription = "jjjj"
                }
            };

        }
    }
}
