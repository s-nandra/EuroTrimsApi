using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EuroTrim.api.Entities;
using EuroTrim.api.Services;
using Microsoft.AspNetCore.Mvc;

namespace EuroTrim.api.Controllers
{
    [Route("api/discountbands")]
    public class DiscountBandsController : Controller
    {
        private IEuroTrimRepository _euroTrimRepository;

        public DiscountBandsController(IEuroTrimRepository euroTrimRepository)
        {
            _euroTrimRepository = euroTrimRepository;
        }


        public IActionResult GetDiscountBands()
        {
            var discountBandsfromRepo = _euroTrimRepository.GetDiscountBands();

            var discountBands = Mapper.Map<IEnumerable<DiscountBand>>(discountBandsfromRepo);

            return Ok(discountBands);
        }

        [HttpPost()]
        public IActionResult AddDiscountBand(Guid customerId, Guid productId, int discountBandId)
        {
            var discountBandsfromRepo = _euroTrimRepository.GetDiscountBands();

            var discountBands = Mapper.Map<IEnumerable<DiscountBand>>(discountBandsfromRepo);

            return Ok(discountBands);
        }
    }
}