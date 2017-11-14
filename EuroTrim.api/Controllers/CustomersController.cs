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
        public JsonResult GetCustomers()
        {
            return new JsonResult(CustomersDataStore.Current.Customers);
            
        }

        [HttpGet("{id}")]
        public JsonResult GetCustomer(int id)
        {
            return new JsonResult(
                CustomersDataStore.Current.Customers.FirstOrDefault(c => c.Id == id));
        }
    }
}
