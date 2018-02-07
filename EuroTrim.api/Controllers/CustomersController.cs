using EuroTrim.api.Models;
using EuroTrim.api.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EuroTrim.api.Controllers
{
    [Route("api/customers")]
    public class CustomersController : Controller
    {

        private IEuroTrimRepository _euroTrimRepository;

        public CustomersController(IEuroTrimRepository euroTrimRepository)
        {
            _euroTrimRepository = euroTrimRepository;
        }

        [HttpGet()]  
        public IActionResult GetCustomers()
        {
            //return Ok(CustomersDataStore.Current.Customers);
            var customerEntities = _euroTrimRepository.GetCustomers();

            var results = new List<CustomersWithoutProductsDto>();

            foreach (var item in customerEntities)
            {
                results.Add(new CustomersWithoutProductsDto
                {
                    Id = item.Id,
                    Name = item.Name,
                    Address1 = item.Address1,
                    ContactNumber = item.ContactNumber
                });

            }

            return Ok(results);
            
        }

        [HttpGet("{id}")]
        public IActionResult GetCustomer(int id, bool includeProducts =false)
        {
            var customer = _euroTrimRepository.GetCustomer(id, includeProducts);
            if(customer == null)
            {
                return NotFound();
            }

            if(includeProducts)
            {
                var customerResult = new CustomerDto()
                {
                    Id = customer.Id,
                    Name = customer.Name,
                    ContactNumber = customer.ContactNumber,
                    Decription = customer.Address1
                };

                foreach (var item in customer.Product)
                {
                    customerResult.Product.Add(
                        new ProductDto()
                        {
                            Id = item.Id,
                            ProdName = item.ProdName,
                            Description = item.Description
                        });
                }

                return Ok(customerResult);
            }

            var customersWithoutProductsResult =
                new CustomersWithoutProductsDto
                {
                    Id = customer.Id,
                    Name = customer.Name,
                    Address1 = customer.Address1,
                    ContactNumber = customer.ContactNumber
                };

            return Ok(customersWithoutProductsResult);
            
            //var customerToReturn = CustomersDataStore.Current.Customers.FirstOrDefault(c => c.Id == id);
            //if (customerToReturn == null)
            //{
            //    return NotFound();
            //}
            //return Ok(customerToReturn);
        }
    }
}
