using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using SWP391_MiniStore.Models;
using System.Diagnostics;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace SWP391_MiniStore.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Index()
        {
            // Retrieve the user's roles from claims
            var roles = User.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value);

            // Redirect to different homepages based on roles
            if (roles.Contains("Manager"))
            {
                // Redirect to Manager's homepage
                return RedirectToAction("ManagerIndex");
            }
            else if (roles.Contains("Sales"))
            {
                // Redirect to Sale's homepage
                return RedirectToAction("SalesIndex");
            }
            else if (roles.Contains("Guard"))
            {
                // Redirect to Guard's homepage
                return RedirectToAction("GuardIndex");
            }
            else
            {
                // Handle other roles or scenarios
                return View();
            }
        }

        [HttpGet]
        public IActionResult ManagerIndex()
        {
            return View();
        }

        [HttpGet]
        public IActionResult SalesIndex()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GuardIndex()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}