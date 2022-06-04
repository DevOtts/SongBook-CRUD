using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SongBook2.Models.Entities.Client;
using SongBook2.Repositories;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SongBook2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {

        private readonly IUserRepository repos;

        public UserController(IUserRepository _repos)
        {
            repos = _repos;
        }
        // GET: /<controller>/
        [HttpGet]
        public IActionResult Get()
        {
            return Ok();
        }

        [Route("List")]
        [HttpGet]
        [Authorize]
        public IActionResult List()
        {
            return Ok();
        }

        [Route("Login")]
        [HttpGet]
        public async Task<IActionResult> Login()
        {
            return Ok(new { Data = "Pagina de Login!"});
        }

        [Route("Login")]
        [HttpPost]
        public async Task<IActionResult> Login(LoginUser model)
        {
            if (model != null)
            {
                var claims = new List<Claim>();
                claims.Add(new Claim("username", model.Email));
                claims.Add(new Claim(ClaimTypes.Email, model.Email));
                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                await HttpContext.SignInAsync(claimsPrincipal);
            }
            return Ok(new {  Name = User.Identity.Name, Logado = User.Identity.IsAuthenticated});
        }

        [Route("Logout")]
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return Redirect("/");
        }

        [HttpPost]
        public IActionResult Post([FromBody] UserDetail client)
        {
            if (repos.Create(client))
            {
                return Ok();
            }
            return BadRequest();

        }

        [HttpPut]
        public IActionResult Put()
        {
            return Ok();
        }

        [HttpDelete]
        public IActionResult Delete()
        {
            return Ok();
        }

    }

}
