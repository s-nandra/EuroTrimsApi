using AutoMapper;
using EuroTrim.api.Entities;
using EuroTrim.api.Helpers;
using EuroTrim.api.Models;
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
    public class ProductsController : Controller
    {
        private ILogger<ProductsController> _logger;
        private IMailService _mailService;
        private IEuroTrimRepository _euroTrimRepository;

        public ProductsController(ILogger<ProductsController> logger,
            IMailService mailService, IEuroTrimRepository euroTrimRepository)
        {
            _logger = logger;
            _mailService = mailService;
            _euroTrimRepository = euroTrimRepository;

        }
 
        [HttpGet("api/products/")]
        public IActionResult GetProducts()
        {
            var productEntities = _euroTrimRepository.GetProducts();

            var results = Mapper.Map<IEnumerable<ProductDto>>(productEntities);
            return Ok(results);
             
        }

        [HttpGet("api/products/{id}", Name = "GetProduct")]
        public IActionResult GetProduct(Guid id)
        { 
            var productToReturn = _euroTrimRepository.GetProduct(id);
 
            if (productToReturn == null)
            {
                return NotFound();
            }

            var p = Mapper.Map<ProductDto>(productToReturn);

            return Ok(p);

        }


        [HttpPut("api/products/{id}")]
        public IActionResult UpdateProduct(Guid id,
           [FromBody] ProductForUpdateDto product)
        {
            
            if (product == null)
            {
                return BadRequest();
            }

 

            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}

            if(!_euroTrimRepository.ProductExists(id))
            {
                return NotFound();
            }

            var productToUpdate = _euroTrimRepository.GetProduct(id);

            if (productToUpdate == null)
            {
                return NotFound();
            }


            Mapper.Map(product, productToUpdate);

            _euroTrimRepository.UpdateProduct(productToUpdate);

            if (!_euroTrimRepository.Save())
            {
                throw new Exception($"updating product {id}");
            }

            return NoContent();

        }


        [HttpPatch("api/products/{id}")]
        public IActionResult PartiallyUpdateProduct(Guid id,
          [FromBody] JsonPatchDocument<ProductForUpdateDto>  patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest();
            }

            if (!_euroTrimRepository.ProductExists(id))
            {
                return NotFound();
            }

            var productEntity = _euroTrimRepository.GetProduct(id);

            if (productEntity == null)
            {
                //var productDto = new ProductForUpdateDto();
                //patchDoc.ApplyTo(productDto);

                //var productToAdd = Mapper.Map<Product>(productDto);
                //productToAdd.Id = id;

                //_euroTrimRepository.AddProduct(id, productToAdd);

               return NotFound();

            }



            var productToPatch = Mapper.Map<ProductForUpdateDto>(productEntity);

            //patchDoc.ApplyTo(productToPatch, ModelState);
            patchDoc.ApplyTo(productToPatch);


            TryValidateModel(productToPatch);

            if (!ModelState.IsValid)
            {
                return new UnprocessableEntityObjectResult(ModelState);
            }

            Mapper.Map(productToPatch, productEntity);

            _euroTrimRepository.UpdateProduct(productEntity);

            if (!_euroTrimRepository.Save())
            {
                throw new Exception($"updating product {id}");
            }

            return NoContent();
        }

            /*
            [HttpGet("api/customers/{customerId}/products")]
            public IActionResult GetCustomerProducts(Guid customerId)
            {

                try
                {
                    if (!_euroTrimRepository.CustomerExists(customerId))
                    {
                        _logger.LogInformation($"Customer with id {customerId} was not found!");
                        return NotFound();
                    }


                    var customerProducts = _euroTrimRepository.GetProductsForCustomer(customerId);



                    var customerProductsResults = Mapper.Map<IEnumerable<ProductDto>>(customerProducts);


                    return Ok(customerProductsResults);


                }
                catch (Exception ex)
                {
                    _logger.LogCritical($"Exceprion wile getting Customer with id {customerId} was not found!", ex);
                    return StatusCode(500, "A problem happened with your request");
                }



            }

            [HttpGet("api/customers/{customerId}/products/{productid}")]
            public IActionResult GetCustomerProduct(Guid customerId, Guid productId)
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

                //var customerProductResult = new ProductDto()
                //{
                //    Id = customerProduct.Id,
                //    ProdName = customerProduct.ProdName,
                //    Description = customerProduct.Description
                //};

                var customerProductResult = Mapper.Map<ProductDto>(customerProduct);

                return Ok(customerProductResult);

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


            [HttpDelete("api/products/{productId}")]
            public IActionResult DeleteProduct(Guid productId)
            {

                if (!_euroTrimRepository.ProductExists(productId))
                {
                    return NotFound();
                }

                var productEntity = _euroTrimRepository.GetProduct(productId);

                if (productEntity == null)
                {
                    return NotFound();
                }

                _euroTrimRepository.DeleteProduct(productEntity);

                if (!_euroTrimRepository.Save())
                {
                    return StatusCode(500, "A problem occured while deleting your product");
                }

                _mailService.Send("product deleted", $"Product {productEntity.ProdName} with id {productEntity.Id} was deleted");


                return NoContent();
            }
            */

        }
}
