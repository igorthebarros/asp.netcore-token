using API.Models;
using API.Repository;
using API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("v1/account")]
    public class HomeController : ControllerBase
    {
        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<ActionResult<dynamic>> Authenticate([FromBody] User model)
        {
            var user = UserRepository.Get(model.Username, model.Password);

            if (user == null)
                return NotFound(new { message = "Username or Password are invalid" });

            var token = TokenService.GenerateToken(user);

            //Hides the user password
            user.Password = "";

            return new
            { 
                user = user,
                token = token
            };
        }

        [HttpGet]
        [Route("anonymous")]
        [AllowAnonymous]
        public string Anonymous() => "Everyone has access!";

        [HttpGet]
        [Route("authenticated")]
        [Authorize]
        //We can retrieve the user name from User.Identity because in TokenService we bind the User.Username to the ClaimTypes.Name
        public string Authenticated() => String.Format("Hello, {0}", User.Identity.Name);

        [HttpGet]
        [Route("employee")]
        [Authorize(Roles = "emmployee, manager")]
        public string Employee() => "You have the Employee access!";

        [HttpGet]
        [Route("manager")]
        [Authorize(Roles = "manager")]
        public string Manager() => "You have the Manager access!";
    }
}
