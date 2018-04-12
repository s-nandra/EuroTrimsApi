using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EuroTrim.api.Entities;
using EuroTrim.api.Models;
using EuroTrim.api.Services;
using Microsoft.AspNetCore.Mvc;

namespace EuroTrim.api.Controllers
{
    [Route("api/customers/{customerId}/orders")]
    public class OrdersController : Controller
    {

        private IEuroTrimRepository _euroTrimRepository;

        public OrdersController(IEuroTrimRepository euroTrimRepository)
        {
            _euroTrimRepository = euroTrimRepository;
        }

        //[HttpGet("api/customers/{customerId}/orders")]
        [HttpGet()]
        public IActionResult GetOrdersForCustomer(Guid customerId)
        {
            if (!_euroTrimRepository.CustomerExists(customerId))
            {
                return NotFound();
            }

            var ordersForCustomerFromRepo = _euroTrimRepository.GetOrdersForCustomer(customerId);

            var ordersForCustomer = Mapper.Map<IEnumerable<OrderDto>>(ordersForCustomerFromRepo);

            return Ok(ordersForCustomer);
        }

        [HttpGet("{productId}", Name = "GetOrderForCustomer")]
        public IActionResult GetOrderForCustomer(Guid customerId, Guid productId)
        {
            if (!_euroTrimRepository.CustomerExists(customerId))
            {
                return NotFound();
            }

            var ordersForCustomerFromRepo = _euroTrimRepository.GetOrderForCustomer(customerId, productId);

            if (ordersForCustomerFromRepo == null)
            {
                return NotFound();
            }
            var ordersForCustomer = Mapper.Map<OrderDto>(ordersForCustomerFromRepo);

            return Ok(ordersForCustomer);
        }

        [HttpGet("{orderId}", Name = "GetOrderForCustomerByOrderId")]
        public IActionResult GetOrderForCustomerByOrderId(Guid customerId, Guid orderId)
        {
            if (!_euroTrimRepository.CustomerExists(customerId))
            {
                return NotFound();
            }

            var ordersForCustomerFromRepo = _euroTrimRepository.GetOrderForCustomerByOrderId(customerId, orderId);

            if (ordersForCustomerFromRepo == null)
            {
                return NotFound();
            }
            var ordersForCustomer = Mapper.Map<OrderDto>(ordersForCustomerFromRepo);

            return Ok(ordersForCustomer);
        }



        [HttpPost()]

        public IActionResult CreateOrderForCustomer(Guid customerId,
            [FromBody] OrderForCreationDto order)
        {
            if (order == null)
            {
                return BadRequest();
            }

            if(!_euroTrimRepository.CustomerExists(customerId) || !_euroTrimRepository.ProductExists(order.ProductId))
            {
                return NotFound();
            }

            var orderEntity = Mapper.Map<Order>(order);

            _euroTrimRepository.AddOrderForCustomer(customerId, orderEntity);

            if (!_euroTrimRepository.Save())
            {
                throw new Exception($"error creating order for cus id {customerId} failed on save");

            }

            var orderToReturn = Mapper.Map<OrderDto>(orderEntity);

            return CreatedAtRoute("GetOrderForCustomerByOrderId",
                new { customerId=customerId, orderId= orderToReturn.Id},
                orderToReturn);
        }

        [HttpDelete("{orderId}")]
        public IActionResult DeleteOrderForCustomer(Guid customerId, Guid orderId)
        {
            if (!_euroTrimRepository.CustomerExists(customerId))
            {
                return NotFound();
            }

            var ordersForCustomerFromRepo = _euroTrimRepository.GetOrderForCustomerByOrderId(customerId, orderId);

            if (ordersForCustomerFromRepo == null)
            {
                return NotFound();
            }

            _euroTrimRepository.DeleteOrder(ordersForCustomerFromRepo);

            if (!_euroTrimRepository.Save())
            {
                throw new Exception($"deleting order {customerId} for customer {customerId} failed");
                //return StatusCode(500, "A problem occured while deleting your order");
            }
 
            return NoContent();
 
        }

    }
}