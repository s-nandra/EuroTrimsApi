using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EuroTrim.api.Entities;
using Microsoft.AspNetCore.Mvc;

namespace EuroTrim.api.Controllers
{
    public class DummyController : Controller
    {
        private EuroTrimContext _ctx;

        public DummyController(EuroTrimContext ctx)
        {
            _ctx = ctx;
        }

        [HttpGet]
        [Route("api/testdatabase")]
        public IActionResult TestDatabase()
        {
            return Ok();
        }
    }
}