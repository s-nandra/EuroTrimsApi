using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EuroTrim.api.Controllers
{
    [Route("api/customers")]
    public class CustomersController : Controller
    {
        [HttpGet()]  
        public JsonResult GetCustomers()
        {
            return new JsonResult(new List<object>()
            {
                new { id=1, Name="Satnam"},
                new { id=2, Name="Indy"}
            });
            
        }
    }
}
