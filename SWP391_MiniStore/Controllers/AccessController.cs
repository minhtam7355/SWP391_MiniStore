using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using SWP391_MiniStore.Models;

namespace SWP391_MiniStore.Controllers
{
    public class AccessController : Controller
    {

        [HttpGet]
        public IActionResult Login()
        {
            ClaimsPrincipal claimsUser = HttpContext.User;

            return claimsUser.Identity.IsAuthenticated ? RedirectToAction("Index", "Home") : View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel modelLogin)
        {
            if (modelLogin.EmailOrUsername == "user@gmail.com" && modelLogin.Password == "pass123")
            {
                List<Claim> claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, "This stores the user's ID/unique identifier"),
                    new Claim(ClaimTypes.Name, "This stores the user's name or display name"),
                    new Claim(ClaimTypes.Email, "This stores the user's email address"),
                    new Claim(ClaimTypes.Role, "This stores the user's role(s) for authorization"),
                    new Claim(ClaimTypes.GivenName, "This stores the user's given name (first name)"),
                    new Claim(ClaimTypes.Surname, "This stores the user's surname (last name)"),
                    new Claim(ClaimTypes.DateOfBirth, "This stores the user's date of birth"),
                    new Claim(ClaimTypes.MobilePhone, "This stores the user's mobile phone number"),
                    new Claim(ClaimTypes.StreetAddress, "This stores the user's street address"),
                    new Claim(ClaimTypes.Country, "This stores the user's country"),
                    // Add more claims as needed
                    
                };

                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims,
                    CookieAuthenticationDefaults.AuthenticationScheme);

                AuthenticationProperties properties = new AuthenticationProperties()
                {
                    AllowRefresh = true,
                    IsPersistent = modelLogin.KeepLoggedIn
                };

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity), properties);

                return RedirectToAction("Index", "Home");
            }

            ViewData["ValidateMessage"] = "User not found";

            return View();
        }
    }
}
