using AutoMapper;
using EuroTrim.api.Entities;
using EuroTrim.api.Helpers;
using EuroTrim.api.Models;
using EuroTrim.api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EuroTrim.api.Controllers
{
    [Authorize]
    [Route("api/customers")]
    public class CustomersController : Controller
    {

        private IEuroTrimRepository _euroTrimRepository;
        private ILogger<CustomersController> _logger;
        private IUrlHelper _urlHelper;
        private IPropertyMappingService _propertyMappingService;
        private ITypeHelperService _typeHelperService;

        public CustomersController(IEuroTrimRepository euroTrimRepository,
            ILogger<CustomersController> logger, IUrlHelper urlHelper
            , IPropertyMappingService propertyMappingService,
              ITypeHelperService typeHelperService
            )
        {
            _logger = logger;
            _euroTrimRepository = euroTrimRepository;
            _urlHelper = urlHelper;
            _propertyMappingService = propertyMappingService;
            _typeHelperService = typeHelperService;
        }

        [HttpGet(Name ="GetCustomers")]
        public IActionResult GetCustomers(
            CustomersResourceParameters customersResourceParameters,
            [FromHeader(Name = "Accept")] string mediaType)
        {
          
            if (!_propertyMappingService.ValidMappingExistsFor<CustomerDto, Customer>
              (customersResourceParameters.OrderBy))
            {
                return BadRequest();
            }

            if (!_typeHelperService.TypeHasProperties<CustomerDto>
                (customersResourceParameters.Fields))
            {
                return BadRequest();
            }

            var customersFromRepo = _euroTrimRepository.GetCustomers(customersResourceParameters);

            var customers = Mapper.Map<IEnumerable<CustomerDto>>(customersFromRepo);

            if (mediaType == "application/vnd.marvin.hateoas+json")
            {
                var paginationMetadata = new
                {
                    totalCount = customersFromRepo.TotalCount,
                    pageSize = customersFromRepo.PageSize,
                    currentPage = customersFromRepo.CurrentPage,
                    totalPages = customersFromRepo.TotalPages
                };

                Response.Headers.Add("X-Pagination",
                 Newtonsoft.Json.JsonConvert.SerializeObject(paginationMetadata));

                var links = CreateLinksForCustomers(customersResourceParameters,
                    customersFromRepo.HasNext, customersFromRepo.HasPrevious);

                var shapedCustomers = customers.ShapeData(customersResourceParameters.Fields);

                var shapedCustomersWithLinks = shapedCustomers.Select(customer =>
                {
                    var customerAsDictionary = customer as IDictionary<string, object>;
                    var customerLinks = CreateLinksForCustomer(
                        (Guid)customerAsDictionary["Id"], customersResourceParameters.Fields);

                    customerAsDictionary.Add("links", customerLinks);

                    return customerAsDictionary;
                });

                var linkedCollectionResource = new
                {
                    value = shapedCustomersWithLinks,
                    links = links
                };

                return Ok(linkedCollectionResource);

            }
            else
            {
                var previousPageLink = customersFromRepo.HasPrevious ?
                    CreateCustomersResourceUri(customersResourceParameters,
                    ResourceUriType.PreviousPage) : null;

                var nextPageLink = customersFromRepo.HasNext ?
                    CreateCustomersResourceUri(customersResourceParameters,
                    ResourceUriType.NextPage) : null;

                var paginationMetadata = new
                {
                    previousPageLink = previousPageLink,
                    nextPageLink = nextPageLink,
                    totalCount = customersFromRepo.TotalCount,
                    pageSize = customersFromRepo.PageSize,
                    currentPage = customersFromRepo.CurrentPage,
                    totalPages = customersFromRepo.TotalPages
                };

                Response.Headers.Add("X-Pagination",
                    Newtonsoft.Json.JsonConvert.SerializeObject(paginationMetadata));

                return Ok(customers.ShapeData(customersResourceParameters.Fields));
            }

 
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
                          fields = customersResourceParameters.Fields,
                          orderBy = customersResourceParameters.OrderBy,
                          searchQuery = customersResourceParameters.SearchQuery,
                          city = customersResourceParameters.City,
                          pageNumber = customersResourceParameters.PageNumber - 1,
                          pageSize = customersResourceParameters.PageSize
                      });
                case ResourceUriType.NextPage:
                    return _urlHelper.Link("GetCustomers",
                      new
                      {
                          fields = customersResourceParameters.Fields,
                          orderBy = customersResourceParameters.OrderBy,
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
                        fields = customersResourceParameters.Fields,
                        orderBy = customersResourceParameters.OrderBy,
                        searchQuery = customersResourceParameters.SearchQuery,
                        city = customersResourceParameters.City,
                        pageNumber = customersResourceParameters.PageNumber,
                        pageSize = customersResourceParameters.PageSize
                    });
            }
        }

        private object CreateLinksForCustomer(Guid id, string fields)
        {
            var links = new List<LinkDto>();

            if (string.IsNullOrWhiteSpace(fields))
            {
                links.Add(
                  new LinkDto(_urlHelper.Link("GetCustomer", new { id = id }),
                  "self",
                  "GET"));
            }
            else
            {
                links.Add(
                  new LinkDto(_urlHelper.Link("GetCustomer", new { id = id, fields = fields }),
                  "self",
                  "GET"));
            }

            links.Add(
              new LinkDto(_urlHelper.Link("DeleteCustomer", new { id = id }),
              "delete_customer",
              "DELETE"));

            links.Add(
              new LinkDto(_urlHelper.Link("CreateCustomer", new { customerId = id }),
              "create_customer",
              "POST"));

            //links.Add(
            //  new LinkDto(_urlHelper.Link("CreateProductForCustomer", new { CustomerId = id }),
            //  "create_product_for_customer",
            //  "POST"));

            //links.Add(
            //   new LinkDto(_urlHelper.Link("GetproductsForCustomer", new { customerId = id }),
            //   "products",
            //   "GET"));

            return links;
        }

        private IEnumerable<LinkDto> CreateLinksForCustomers(
            CustomersResourceParameters customersResourceParameters,
            bool hasNext, bool hasPrevious)
        {
            var links = new List<LinkDto>();

            // self 
            links.Add(
               new LinkDto(CreateCustomersResourceUri(customersResourceParameters,
               ResourceUriType.Current)
               , "self", "GET"));

            if (hasNext)
            {
                links.Add(
                  new LinkDto(CreateCustomersResourceUri(customersResourceParameters,
                  ResourceUriType.NextPage),
                  "nextPage", "GET"));
            }

            if (hasPrevious)
            {
                links.Add(
                    new LinkDto(CreateCustomersResourceUri(customersResourceParameters,
                    ResourceUriType.PreviousPage),
                    "previousPage", "GET"));
            }

            return links;
        }



        [HttpGet("{id}", Name= "GetCustomer")]
        public IActionResult GetCustomer(Guid id, [FromQuery] string fields)
        {

            if (!_typeHelperService.TypeHasProperties<CustomerDto>
                 (fields))
            {
                return BadRequest();
            }

            var customerRepo = _euroTrimRepository.GetCustomer(id);

            if (customerRepo == null)
            {
                return NotFound();
            }

            var customer = Mapper.Map<CustomerDto>(customerRepo);

            var links = CreateLinksForCustomer(id, fields);

            var linkedResourceToReturn = customer.ShapeData(fields)
                as IDictionary<string, object>;

            linkedResourceToReturn.Add("links", links);

            return Ok(linkedResourceToReturn);
        }

        [HttpPost(Name = "CreateCustomer")]
        [RequestHeaderMatchesMediaType("Content-Type",
            new[] { "application/vnd.marvin.customer.full+json" })]
        public IActionResult CreateCustomer([FromBody] CustomerForCreationDto customer)
        {
            if(customer == null)
            {
                return BadRequest();
            }

            var customerEntity = Mapper.Map<Customer>(customer);
            _euroTrimRepository.AddCustomer(customerEntity);

            //if (!ModelState.IsValid)
            //{
            //    //return 442
            //    return new UnprocessableEntityObjectResult(ModelState);
            //}

            if (!_euroTrimRepository.Save())
            {
                throw new Exception("problem occured");
                //return StatusCode(500, "A problem occured");
            }

            var customerToReturn = Mapper.Map<CustomerDto>(customerEntity);

            var links = CreateLinksForCustomer(customerToReturn.Id, null);

            var linkedResourceToReturn = customerToReturn.ShapeData(null)
                as IDictionary<string, object>;

            linkedResourceToReturn.Add("links", links);

            return CreatedAtRoute("GetCustomer",
                new { id = linkedResourceToReturn["Id"] },
                linkedResourceToReturn);

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

        [HttpDelete("{id}", Name = "DeleteCustomer")]
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


        [HttpOptions]
        public IActionResult GetCustomersOptions()
        {
            Response.Headers.Add("Allow", "GET,OPTIONS,POST");
            return Ok();
        }

         
    }
}
