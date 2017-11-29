using EuroTrim.api.Models;
using Microsoft.AspNetCore.JsonPatch;
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

        [HttpPut("api/products/{id}")]
        public IActionResult UpdateProduct(int id,
            [FromBody] ProductForUpdateDto product)
        {
            if (product == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var productToUpdate = ProductsDataStore.Current.Products.FirstOrDefault(
                p => p.Id == id);

            if (productToUpdate == null)
            {
                return NotFound();
            }

            productToUpdate.PartNo = product.PartNo;
            productToUpdate.ProdName = product.ProdName;

            //product.Category - product.Category;
            productToUpdate.Description = product.Description;
            productToUpdate.Colour = product.Colour;
            productToUpdate.Size = product.Size;
            productToUpdate.BuyPrice = product.BuyPrice;
            productToUpdate.Discount1 = product.Discount1;
            productToUpdate.Discount2 = product.Discount2;
            productToUpdate.Discount3 = product.Discount3;
            productToUpdate.Discount4 = product.Discount4;


            return NoContent();

        }

        [HttpPatch("api/products/{id}")]
        public IActionResult PartiallyUpdateProduct(int id,
            [FromBody] JsonPatchDocument<ProductForUpdateDto> product)
        {
            if (product == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var productToUpdate = ProductsDataStore.Current.Products.FirstOrDefault(
                p => p.Id == id);

            if (productToUpdate == null)
            {
                return NotFound();
            }

            var patchProduct =
                new ProductForUpdateDto()
                {

                    PartNo = productToUpdate.PartNo,
                    ProdName = productToUpdate.ProdName,
                    //product.Category - product.Category;
                    Description = productToUpdate.Description,
                    Colour = productToUpdate.Colour,
                    Size = productToUpdate.Size,
                    BuyPrice = productToUpdate.BuyPrice,
                    Discount1 = productToUpdate.Discount1,
                    Discount2 = productToUpdate.Discount2,
                    Discount3 = productToUpdate.Discount3,
                    Discount4 = productToUpdate.Discount4
                };


            product.ApplyTo(patchProduct, ModelState);

            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);

            }

            if(productToUpdate.ProdName == patchProduct.Description)
            {
                ModelState.AddModelError("Description", "Description cant be same as name");
            }

            TryValidateModel(patchProduct);

            productToUpdate.PartNo = patchProduct.PartNo;
            productToUpdate.ProdName = patchProduct.ProdName;

            //product.Category - product.Category;
            productToUpdate.Description = patchProduct.Description;
            productToUpdate.Colour = patchProduct.Colour;
            productToUpdate.Size = patchProduct.Size;
            productToUpdate.BuyPrice = patchProduct.BuyPrice;
            productToUpdate.Discount1 = patchProduct.Discount1;
            productToUpdate.Discount2 = patchProduct.Discount2;
            productToUpdate.Discount3 = patchProduct.Discount3;
            productToUpdate.Discount4 = patchProduct.Discount4;

            return NoContent();

        }
    }
}
