using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace PkusForum.Services.CookieAuthService
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public readonly IHttpContextAccessor HttpContextAccessor;

        public AuthController(IHttpContextAccessor HttpContextAccessor)
        {
            this.HttpContextAccessor = HttpContextAccessor;
        }

        [Route("login")]
        [HttpGet]
        public async Task<ActionResult>? Login(string tokenId)
        {
            if (!TokenDictionaryService.TryPopToken(tokenId, out string? token))
            {
                return Redirect("/signin");
            }

            List<Claim> claims = new List<Claim>
            {
                new Claim("token", token.ToString())
            };

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            AuthenticationProperties authProperties = new AuthenticationProperties
            {
                ExpiresUtc = DateTime.UtcNow.AddMonths(1)
            };

            await HttpContextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

            return Redirect("/");
        }

        [Route("logout")]
        [HttpGet]
        public async Task<ActionResult> Logout()
        {
            await HttpContextAccessor.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return Redirect("/signin");
        }
    }
}
