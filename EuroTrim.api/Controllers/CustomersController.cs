using AutoMapper;
using EuroTrim.api.Entities;
using EuroTrim.api.Helpers;
using EuroTrim.api.Models;
using EuroTrim.api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
        private ILogger<CustomersController> _logger;
        private IUrlHelper _urlHelper;

        public CustomersController(IEuroTrimRepository euroTrimRepository,
            ILogger<CustomersController> logger, IUrlHelper urlHelper)
        {
            _logger = logger;
            _euroTrimRepository = euroTrimRepository;
            _urlHelper = urlHelper;
        }

        [HttpGet(Name ="GetCustomers")]
        public IActionResult GetCustomers(
            CustomersResourceParameters customersResourceParameters)
        {
          
            var customerEntities = _euroTrimRepository.GetCustomers(customersResourceParameters);

            var previousPageLink = customerEntities.HasNext ?
                CreateCustomersResourceUri(customersResourceParameters, 
                ResourceUriType.PreviousPage) : null;

            var nextPageLink = customerEntities.HasNext ?
            CreateCustomersResourceUri(customersResourceParameters,
            ResourceUriType.NextPage) : null;

            var paginationMetadata = new
            {
                totalCount = customerEntities.TotalCount,
                pageSize = customerEntities.PageSize,
                currentPage = customerEntities.CurrentPage,
                totalPages = customerEntities.TotalPages,
                previousPageLink = previousPageLink,
                nextPageLink = nextPageLink
            };

            Response.Headers.Add("X-Pagination",
                Newtonsoft.Json.JsonConvert.SerializeObject(paginationMetadata));

            var results = Mapper.Map<IEnumerable<CustomerDto>>(customerEntities);
            return Ok(results); 
        }

        private string CreateCustomersResourceUri(CustomersResourceParameters customersResourceParameters,
            ResourceUriType type)
        {
            switch (type)
            {
                case ResourceUriType.PreviousPage:
                    return _urlHelper.Link("GetCustomers",
                      new
                      {
                          //fields = customersResourceParameters.Fields,
                          //orderBy = customersResourceParameters.OrderBy,
                          searchQuery = customersResourceParameters.SearchQuery,
                          city = customersResourceParameters.City,
                          pageNumber = customersResourceParameters.PageNumber - 1,
                          pageSize = customersResourceParameters.PageSize
                      });
                case ResourceUriType.NextPage:
                    return _urlHelper.Link("GetCustomers",
                      new
                      {
                          //fields = authorsResourcustomersResourceParametersceParameters.Fields,
                          //orderBy = customersResourceParameters.OrderBy,
                          searchQuery = customersResourceParameters.SearchQuery,
                          city = customersResourceParameters.City,
                          pageNumber = customersResourceParameters.PageNumber + 1,
                          pageSize = customersResourceParameters.PageSize
                      });
                case ResourceUriType.Current:
                default:
                    return _urlHelper.Link("GetCustomers",
                    new
                    {
                        //fields = customersResourceParameters.Fields,
                        //orderBy = customersResourceParameters.OrderBy,
                        searchQuery = customersResourceParameters.SearchQuery,
                        city = customersResourceParameters.City,
                        pageNumber = customersResourceParameters.PageNumber,
                        pageSize = customersResourceParameters.PageSize
                    });
            }
        }

        [HttpGet("{id}", Name= "GetCustomer")]
        public IActionResult GetCustomer(Guid id)
        {
            var customerRepo = _euroTrimRepository.GetCustomer(id);

            if (customerRepo == null)
            {
                return NotFound();
            }

            var customer = Mapper.Map<CustomerDto>(customerRepo);
            return Ok(customer);
        }

        [HttpPost]
        public IActionResult CreateCustomer([FromBody] CustomerForCreationDto customer)
        {
            if(customer == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                //return 442
                return new UnprocessableEntityObjectResult(ModelState);
            }

            var customerEntity = Mapper.Map<Customer>(customer);

            _euroTrimRepository.AddCustomer(customerEntity);

            if (!_euroTrimRepository.Save())
            {
                throw new Exception("problem occured");
                //return StatusCode(500, "A problem occured");
            }

            var customerToReturn = Mapper.Map<CustomerDto>(customerEntity);

            return CreatedAtRoute("GetCustomer", new { id = customerToReturn.Id },
                customerToReturn);

        }

        [HttpPost("{id}")]
        public IActionResult BlockCustomerCreation(Guid id)
        {
            if(_euroTrimRepository.CustomerExists(id))
            {
                return new StatusCodeResult(StatusCodes.Status409Conflict);
            }

            return NotFound();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCustomer(Guid id)
        {
            var customerFromRepo = _euroTrimRepository.GetCustomer(id);
            if (customerFromRepo == null)
            {
                return NotFound();
            }

            _euroTrimRepository.DeleteCustomer(customerFromRepo);

            if (!_euroTrimRepository.Save())
            {
                throw new Exception($"Deleting customer {id} failed on save.");
            }

            _logger.LogInformation(100, $"Customer {id} was deleted.");

            return NoContent();
        }





        /*
        [HttpGet("{id}")]
        public IActionResult GetCustomer(Guid id, bool includeProducts =false)
        {
            var customer = _euroTrimRepository.GetCustomer(id, includeProducts);
            if(customer == null)
            {
                return NotFound();
            }

            if(includeProducts)
            {
                var customerResult = Mapper.Map<CustomerDto>(customer);
                return Ok(customerResult);
            }


            var customersWithoutProductsResult = Mapper.Map<CustomersWithoutProductsDto>(customer);
            return Ok(customersWithoutProductsResult);

        }*/
    }
}
