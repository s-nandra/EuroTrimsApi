using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EuroTrim.api.Controllers
{
    //[Route("api/customer/{id}")]
    public class ProductController : Controller
    {
        [HttpGet("api/customers/{id}/products")]
        public IActionResult GetCustomerProduct(int id)
        {
            var customer = CustomersDataStore.Current.Customers
                .FirstOrDefault(c => c.Id == id);

            if (customer==null)
            {
                return NotFound();
            }

            return Ok(customer.Product);
        }

        [HttpGet("api/customers/{id}/products/{productid}")]
        public IActionResult GetCustomerProduct(int id, int productId)
        {
            var customer = CustomersDataStore.Current.Customers
                 .FirstOrDefault(c => c.Id == id);

            if (customer == null)
            {
                return NotFound();
            }

            var product = customer.Product.FirstOrDefault(p => p.Id == productId);

            return Ok(product);
        }
    }
}
