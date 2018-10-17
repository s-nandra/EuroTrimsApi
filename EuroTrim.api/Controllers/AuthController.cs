using System;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authorization;
using EuroTrim.api.Helpers;
using EuroTrim.api.Services;
using Microsoft.AspNetCore.Cors;

namespace EuroTrim.api.Controllers
{
    //[EnableCors("AllowAll")]
    [Route("api/[controller]")]
    public class AuthController : Controller
    {

        private IEuroTrimRepository _euroTrimRepository;

        public AuthController(IEuroTrimRepository euroTrimRepository)
        {

            _euroTrimRepository = euroTrimRepository;

        }

        //[EnableCors("AllowAll")]
        [HttpPost("token")]
        public IActionResult Token()
        {
            //IActionResult response = Unauthorized();
            //string tokenString = "test";
            var header = Request.Headers["Authorization"];
            if (header.ToString().StartsWith("Basic"))
            {
                var credValue = header.ToString().Substring("Basic ".Length).Trim();
                var usernameAndPassenc = Encoding.UTF8.GetString(Convert.FromBase64String(credValue)); //admin:pass
                var usernameAndPass = usernameAndPassenc.Split(":");

                //check in DB username and pass exist
                if (CheckUser(usernameAndPass[0], usernameAndPass[1]))
                {
                    string username = usernameAndPass[0];

                    var tokenString = JwtManager.GenerateToken(username);

                    return   Ok(new { userData = new { token = tokenString, name = username } } );
                    //return Ok(tokenString);
                }
            }
            return BadRequest("wrong password");

        }

        public bool CheckUser(string username, string password)
        {

            var user = _euroTrimRepository.GetUser(username, password);

            if (user == null)
            {
                return false;
            }

            return true;
        }

        [HttpPost]
        [Route("auth")]
        public string Get(string username, string password)
        {
            if (CheckUser(username, password))
            {
                return JwtManager.GenerateToken(username);
            }

            return null;
        }
    }



  
    



      /**/

}

