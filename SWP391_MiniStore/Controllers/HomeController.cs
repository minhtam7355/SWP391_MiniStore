using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using SWP391_MiniStore.Models;
using System.Diagnostics;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using SWP391_MiniStore.Models.Domain;
using Microsoft.EntityFrameworkCore;
using SWP391_MiniStore.Data;

namespace SWP391_MiniStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly MiniStoreDbContext _dbContext;

        public HomeController(ILogger<HomeController> logger, MiniStoreDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
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
                return RedirectToAction("Index", "Manager");
            }
            else if (roles.Contains("Sales"))
            {
                // Redirect to Sales's homepage
                return RedirectToAction("Index", "Sales");
            }
            else if (roles.Contains("Guard"))
            {
                // Redirect to Guard's homepage
                return RedirectToAction("Index", "Guard");
            }
            else
            {
                // Handle other roles or scenarios
                return View();
            }
        }

        [HttpGet]
        public IActionResult StaffProfile()
        {
            // Retrieve the user's roles from claims
            var roles = User.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value);

            // Redirect to different profilepages based on roles
            if (roles.Contains("Manager"))
            {
                // Redirect to Manager's profilepage
                return RedirectToAction("ManagerProfile", "Manager");
            }
            else if (roles.Contains("Sales"))
            {
                // Redirect to Sales's profilepage
                return RedirectToAction("SalesProfile", "Sales");
            }
            else if (roles.Contains("Guard"))
            {
                // Redirect to Guard's profilepage
                return RedirectToAction("GuardProfile", "Guard");
            }
            else
            {
                // Handle other roles or scenarios
                return RedirectToAction("Error");
            }
        }

        [HttpGet]
        public IActionResult About()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Contact()
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