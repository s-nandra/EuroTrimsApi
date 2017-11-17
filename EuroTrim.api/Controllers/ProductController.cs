using EuroTrim.api.Models;
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

            if (customer == null)
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


        [HttpGet("api/products/")]
        public IActionResult GetProducts()
        {
            return Ok(ProductsDataStore.Current.Products);
        }

        [HttpGet("api/products/{id}", Name = "GetProduct")]
        public IActionResult GetProduct(int id)
        {
            var productToReturn = ProductsDataStore.Current.Products.FirstOrDefault(c => c.Id == id);
            if (productToReturn == null)
            {
                return NotFound();
            }
            return Ok(productToReturn);
        }

        [HttpPost("api/products")]
        public IActionResult CreateProduct([FromBody] ProductForCreationDto product)
        {
            if (product == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            var products = ProductsDataStore.Current.Products.ToList();
            var maxprodid = products.Max(p => p.Id);

            //var maxprodid = ProductsDataStore.Current.Products.Max(p => p.Id);

            var newProduct = new ProductDto()
            {

                Id = ++maxprodid,
                PartNo = product.PartNo,
                ProdName = product.ProdName,
                Description = product.Description
            };



            products.Add(newProduct);

            return CreatedAtRoute("GetProduct", new
            { id = newProduct.Id }, newProduct);
             
        }
    }
}
