using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using EuroTrim.api.Entities;
using EuroTrim.api.Helpers;
using EuroTrim.api.Models;
using EuroTrim.api.Services;
using Microsoft.AspNetCore.Mvc;

namespace EuroTrim.api.Controllers
{
    [Route("api/authorcollections")]
    public class CustomerCollectionsController : Controller
    {
        private IEuroTrimRepository _euroTrimRepository;
        public CustomerCollectionsController(IEuroTrimRepository euroTrimRepository)
        {
            _euroTrimRepository = euroTrimRepository;
        }

        [HttpPost]
        public IActionResult CreateAuthorCollection(
           [FromBody] IEnumerable<CustomerForCreationDto> customerCollection)
        {
            if (customerCollection == null)
            {
                return BadRequest();
            }

            var customerEntities = Mapper.Map<IEnumerable<Customer>>(customerCollection);

            foreach (var customer in customerEntities)
            {
                _euroTrimRepository.AddCustomer(customer);
            }

            if (!_euroTrimRepository.Save())
            {
                throw new Exception("Creating an author collection failed on save.");
            }


            var customerCollectionToReturn = Mapper.Map<IEnumerable<CustomerDto>>(customerEntities);
            var idsAsString = string.Join(",",
               customerCollectionToReturn.Select(a => a.Id));

            return CreatedAtRoute("GetCustomerCollection",
               new { ids = idsAsString },
               customerCollectionToReturn);

        }

        [HttpGet("({ids})", Name = "GetCustomerCollection")]
        public IActionResult GetCustomerCollection(
           [ModelBinder(BinderType = typeof(ArrayModelBinder))] IEnumerable<Guid> ids)
        {
            if (ids == null)
            {
                return BadRequest();
            }

            var customerEntities = _euroTrimRepository.GetCustomers(ids);

            if (ids.Count() != customerEntities.Count())
            {
                return NotFound();
            }

            var customersToReturn = Mapper.Map<IEnumerable<CustomerDto>>(customerEntities);
            return Ok(customersToReturn);
        }
    }
}