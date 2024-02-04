using HotelReservationService.Data.Identity;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HotelReservationService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly SignInManager<ApplicationCustomer> custSignInManager;
        private readonly UserManager<ApplicationCustomer> custUserManager;
        private readonly SignInManager<ApplicationOwner> ownerSignInManager;
        private readonly UserManager<ApplicationOwner> ownerUserManager;

        public AuthController(SignInManager<ApplicationCustomer> signInManager, UserManager<ApplicationCustomer> userManager, 
            SignInManager<ApplicationOwner> ownerSignInManager, UserManager<ApplicationOwner> ownerUserManager)
        {
            custSignInManager = signInManager;
            custUserManager = userManager;
            this.ownerSignInManager = ownerSignInManager;
            this.ownerUserManager = ownerUserManager;
        }
        [HttpPost("login/customer")]
        public async Task<IActionResult> LoginCustomer(string email,string password)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                return BadRequest(new { Message = "Email and password are required" });
            }
            var user = await custUserManager.FindByNameAsync(email);
            if(user == null)
            {
                return BadRequest();
            }
             var isPasswordValid = await custUserManager.CheckPasswordAsync(user, password);
            if (!isPasswordValid)
                return BadRequest();
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.DisplayName),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, "Customer")
            };
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties();
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);
            return Ok(new { Message = "Login successful" });
        }
        [HttpPost("login/owner")]
        public async Task<IActionResult> LoginOwner(string email, string password)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                return BadRequest(new { Message = "Email and password are required" });
            }
            var user = await ownerUserManager.FindByEmailAsync(email);
            if (user == null)
            {
                return BadRequest();
            }
            var isPasswordValid = await ownerUserManager.CheckPasswordAsync(user, password);
            if (!isPasswordValid)
                return BadRequest();
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(ClaimTypes.Role, "Owner")
            };
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties();
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);
            return Ok(new { Message = "Login successful" });
        }
    }
}
