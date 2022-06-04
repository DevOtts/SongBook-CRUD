using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using SongBook2.Models;
using SongBook2.Models.Entities.Client;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SongBook2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : Controller
    {

        private readonly SignInManager<User> signInManager;
        private readonly UserManager<User> userManager;

        public AuthController(SignInManager<User> signInManager, UserManager<User> userManager)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] UserDetail userDetails)
        {
            if (!ModelState.IsValid || userDetails == null)
            {
                return new BadRequestObjectResult(new { Message = "User Registration Failed" });
            }

            var identityUser = new User() { UserName = userDetails.Name, Email = userDetails.Email };
            var result = await userManager.CreateAsync(identityUser, userDetails.Password);
            if (!result.Succeeded)
            {
                var dictionary = new ModelStateDictionary();
                foreach (IdentityError error in result.Errors)
                {
                    dictionary.AddModelError(error.Code, error.Description);
                }

                return new BadRequestObjectResult(new { Message = "User Registration Failed", Errors = dictionary });
            }

            return Ok(new { Message = "User Reigstration Successful" });
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginUser credentials)
        {
            if (!ModelState.IsValid || credentials == null)
            {
                return new BadRequestObjectResult(new { Message = "Login failed" });
            }

            var identityUser = await userManager.FindByEmailAsync(credentials.Email);
            if (identityUser == null)
            {
                return new BadRequestObjectResult(new { Message = "Login failed" });
            }

            var result = userManager.PasswordHasher.VerifyHashedPassword(identityUser, identityUser.PasswordHash, credentials.Password);
            if (result == PasswordVerificationResult.Failed)
            {
                return new BadRequestObjectResult(new { Message = "Login failed" });
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, identityUser.Email),
                new Claim(ClaimTypes.Name, identityUser.Name),
                new Claim(ClaimTypes.OtherPhone, identityUser.MusicianType.ToString())
            };

            var claimsIdentity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity));

            return Ok(new { Message = "You are logged in" });
        }

        [HttpPost]
        [Route("Logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Ok(new { Message = "You are logged out" });
        }
    }

}
