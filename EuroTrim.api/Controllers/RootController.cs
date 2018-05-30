using EuroTrim.api.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EuroTrim.api.Controllers
{

    [Route("api")]
    public class RootController : Controller
    {
        private IUrlHelper _urlHelper;

        public RootController(IUrlHelper urlHelper)
        {
            _urlHelper = urlHelper;
        }

        [HttpGet(Name = "GetRoot")]
        public IActionResult GetRoot([FromHeader(Name = "Accept")] string mediaType)
        {
            if (mediaType == "application/vnd.marvin.hateoas+json")
            {
                var links = new List<LinkDto>();

                links.Add(
                  new LinkDto(_urlHelper.Link("GetRoot", new { }),
                  "self",
                  "GET"));

                links.Add(
                 new LinkDto(_urlHelper.Link("GetCustomers", new { }),
                 "customers",
                 "GET"));

                links.Add(
                  new LinkDto(_urlHelper.Link("CreateCustomer", new { }),
                  "create_customer",
                  "POST"));

                return Ok(links);
            }

            return NoContent();
        }
    }
}
