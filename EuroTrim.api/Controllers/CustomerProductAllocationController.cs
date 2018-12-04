using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using EuroTrim.api.Models;
using EuroTrim.api.Services;
using Microsoft.AspNetCore.Mvc;

namespace EuroTrim.api.Controllers
{
    public class CustomerProductAllocationController : Controller
    {
        private IEuroTrimRepository _euroTrimRepository;

        public CustomerProductAllocationController(IEuroTrimRepository euroTrimRepository)
        {
            _euroTrimRepository = euroTrimRepository;
        }


  
        [HttpGet()]
        public IActionResult GetCustomerProductAllocation(Guid customerId)
        {
            if (!_euroTrimRepository.CustomerExists(customerId))
            {
                return NotFound();
            }

            var entity = _euroTrimRepository.GetCustomerProductAllocationByCustomerId(customerId);
            var dto = Mapper.Map<IEnumerable<CustomerProductAllocationDto>>(entity);
            return Ok(dto);
        }


      /*
        [HttpPost("api/products/{id}")]
        public IActionResult UpdateCustomerProductAllocation(Guid customerId, Guid productId, Guid bandId, decimal val)
        {
            if (!_euroTrimRepository.CustomerExists(customerId))
            {
                return NotFound();
            }

            //var orderEntity = Mapper.Map<Order>(order);

            _euroTrimRepository.AddCustomerProductAllocation(customerId, productId, bandId, val);

            if (!_euroTrimRepository.Save())
            {
                throw new Exception($"error creating order for cus id {customerId} failed on save");

            }

            var ordersForCustomerFromRepo = _euroTrimRepository.GetCustomerProductAllocationByCustomerId(customerId);
            var ordersForCustomer = Mapper.Map<IEnumerable<CustomerProductAllocationDto>>(ordersForCustomerFromRepo);


            return CreatedAtRoute("GetOrderForCustomerByOrderId",
                new { customerId = customerId }, ordersForCustomer);
        }
        */
    }
}