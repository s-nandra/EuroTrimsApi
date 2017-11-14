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
        [HttpGet()]  
        public IActionResult GetCustomers()
        {
            return Ok(CustomersDataStore.Current.Customers);
        }

        [HttpGet("{id}")]
        public IActionResult GetCustomer(int id)
        {
            var customerToReturn = CustomersDataStore.Current.Customers.FirstOrDefault(c => c.Id == id);
            if (customerToReturn == null)
            {
                return NotFound();
            }
            return Ok(customerToReturn);
        }
    }
}
