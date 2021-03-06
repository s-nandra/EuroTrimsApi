﻿using EuroTrim.api.Models;
using EuroTrim.api.Services;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EuroTrim.api.Controllers
{
    //[Route("api/customer/{id}")]
    public class ProductController : Controller
    {
        private ILogger<ProductController> _logger;
        private IMailService _mailService;
        private IEuroTrimRepository _euroTrimRepository;

        public ProductController(ILogger<ProductController> logger,
            IMailService mailService, IEuroTrimRepository euroTrimRepository)
        {
            _logger = logger;
            _mailService = mailService;
            _euroTrimRepository = euroTrimRepository;

        }

        [HttpGet("api/customers/{customerId}/products")]
        public IActionResult GetCustomerProducts(int customerId)
        {

            try
            {
                if (!_euroTrimRepository.CustomerExists(customerId))
                {
                    _logger.LogInformation($"Customer with id {customerId} was not found!");
                    return NotFound();
                }

                var customerProducts = _euroTrimRepository.GetProductsForCustomer(customerId);

                var customerProductsResults = new List<ProductDto>();
                foreach (var item in customerProducts)
                {
                    customerProductsResults.Add(
                        new ProductDto()
                        {
                            Id = item.Id,
                            ProdName = item.ProdName,
                            Description = item.Description
                        });
                }

                return Ok(customerProductsResults);

                /*
                var customer = CustomersDataStore.Current.Customers
                .FirstOrDefault(c => c.Id == id);


                if (customer == null)
                {
                    _logger.LogInformation($"Customer with id {id} was not found!");
                    return NotFound();
                }
                return Ok(customer.Product);
                */
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exceprion wile getting Customer with id {customerId} was not found!", ex);
                return StatusCode(500, "A problem happened with your request");
            }



        }

        [HttpGet("api/customers/{customerId}/products/{productid}")]
        public IActionResult GetCustomerProduct(int customerId, int productId)
        {
            if (!_euroTrimRepository.CustomerExists(customerId))
            {
                _logger.LogInformation($"Customer with id {customerId} was not found!");
                return NotFound();
            }

            var customerProduct = _euroTrimRepository.GetProductForCustomer(customerId, productId);

            if(customerProduct == null)
            {
                return NotFound();
            }

            var customerProductResult = new ProductDto()
            {
                Id = customerProduct.Id,
                ProdName = customerProduct.ProdName,
                Description = customerProduct.Description
            };

            return Ok(customerProductResult);

            //var customer = CustomersDataStore.Current.Customers
            //     .FirstOrDefault(c => c.Id == customerId);

            //if (customer == null)
            //{
            //    return NotFound();
            //}

            //var product = customer.Product.FirstOrDefault(p => p.Id == productId);

            //return Ok(product);
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

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);

            }

            if (productToUpdate.ProdName == patchProduct.Description)
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


        [HttpDelete("api/products/{id}")]
        public IActionResult DeleteProduct(int id)
        {
            //var product  = ProductsDataStore.Current.Products.FirstOrDefault(
            //   p => p.Id == id);

            var product = ProductsDataStore.Current.Products.Find(p => p.Id == id);

            if (product == null)
            {
                return NotFound();
            }


            ProductsDataStore.Current.Products.Remove(product);

            _mailService.Send("product deleted", $"Product {product.ProdName} with id {product.Id} was deleted");


            return NoContent();
        }
    }
}
