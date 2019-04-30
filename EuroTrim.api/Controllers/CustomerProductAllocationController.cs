using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using EuroTrim.api.Entities;
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



        [HttpGet("api/customers/{customerId}/allocations")]
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

        [HttpGet()]
        public IActionResult GetCustomerProductAllocationById(Guid id)
        {

            var entity = _euroTrimRepository.GetCustomerProductAllocationById(id);
            var dto = Mapper.Map<IEnumerable<CustomerProductAllocationDto>>(entity);
            return Ok(dto);
        }

        //[HttpPost()]
        //public IActionResult CreateCustomerProductAllocation([FromBody] CustomerProductAllocationDto cpa)
        //{
        //    var cpalist = _euroTrimRepository.GetCustomerProductAllocationByCustomerId(cpa.CustomerId);


        //    var newCPA = new CustomerProductAllocationDto()
        //    {
        //        CustomerId = cpa.CustomerId,
        //        ProductId = cpa.ProductId,
        //        DiscountBandId = cpa.DiscountBandId
        //    };

        //    //cpalist.AddCPA(newCPA);
        //    _euroTrimRepository.AddCustomerDiscountAllocation(newCPA);
        //    //_euroTrimRepository.AddCustomerDiscountAllocation(newCPA);
        //    //cpalist.Add(newCPA);

        //}

        [HttpPost("api/test")]
        public IActionResult Test()
        {
            var test = "";
            return null;
        }


        [HttpPost("api/addCustomerProductAllocation")]
        public IActionResult AddCustomerProductAllocation(
            [FromBody] CustomerProductAllocationCreationDto cpabody)
        {

            var cpa = _euroTrimRepository.GetCustomerProductAllocationByCustomerIdandProductId(cpabody.CustomerId, cpabody.ProductId);

            var idd = new Guid();
            
            if(cpa != null)
            {
                //Update
                cpa.DiscountBandId = cpabody.DiscountBandId;
                _euroTrimRepository.UpdateCustomerDiscountAllocation(cpa);
                _euroTrimRepository.Save();

                idd = cpa.Id;
            }
            else
            {
                var newCPA = new CustomerProductAllocation()
                {
                    CustomerId = cpabody.CustomerId,
                    ProductId = cpabody.ProductId,
                    DiscountBandId = cpabody.DiscountBandId,
                    DateOrderCreated = DateTime.Now

                };

                _euroTrimRepository.AddCustomerDiscountAllocation(newCPA);
                _euroTrimRepository.Save();


                idd = newCPA.Id;
            }
            return Ok(new { id = idd });

            //var entity = _euroTrimRepository.GetCustomerProductAllocationByCustomerId(cpabody.CustomerId);
            //var dto = Mapper.Map<IEnumerable<CustomerProductAllocationDto>>(entity);
            //return Ok(dto);



            /*
            //if (!_euroTrimRepository.CustomerExists(customerId))
            //{
            //    return NotFound();
            //}

           // var cpa = _euroTrimRepository.GetCustomerProductAllocationByCustomerId(cpabody.CustomerId);


            if (cpa == null)
            {
                //insert
                cpa.CustomerId = cpabody.CustomerId;
                cpa.ProductId = cpabody.ProductId;
                cpa.DiscountBandId = cpabody.DiscountBandId;

                _euroTrimRepository.AddCustomerDiscountAllocation(cpa);

            }
            else
            {
                //update
                cpa.CustomerId = cpabody.CustomerId;
                cpa.ProductId = cpabody.ProductId;
                cpa.DiscountBandId = cpabody.DiscountBandId;

                _euroTrimRepository.UpdateCustomerDiscountAllocation(cpa);

            }
            */
            /*
            var entity = _euroTrimRepository.GetCustomerProductAllocationByCustomerId(customerId);


            var discountEntity = Mapper.Map<cust>(order);

            _euroTrimRepository.AddOrderForCustomer(customerId, orderEntity);

            if (!_euroTrimRepository.Save())
            {
                throw new Exception($"error creating order for cus id {customerId} failed on save");

            }

            var orderToReturn = Mapper.Map<OrderDto>(orderEntity);

            return CreatedAtRoute("GetOrderForCustomerByOrderId",
                new { customerId = customerId, orderId = orderToReturn.Id },
                orderToReturn);*/

            //return Ok(new  { id = idd });

            // return Ok(new { userData = new { token = tokenString, name = username } });
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