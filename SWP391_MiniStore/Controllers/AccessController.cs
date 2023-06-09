﻿using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using SWP391_MiniStore.Models.Domain;
using SWP391_MiniStore.Data;
using SWP391_MiniStore.Models.ViewModels;

namespace SWP391_MiniStore.Controllers
{
    public class AccessController : Controller
    {
        private readonly MiniStoreDbContext _dbContext;

        public AccessController(MiniStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult Login()
        {
            ClaimsPrincipal claimsUser = HttpContext.User;
            
            string? errorMessage = TempData["ErrorMessage"] as string;
            if (!string.IsNullOrEmpty(errorMessage))
            {
                ModelState.AddModelError(string.Empty, errorMessage);
            }

            return claimsUser?.Identity?.IsAuthenticated == true ? RedirectToAction("Index", "Home") : View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel modelLogin)
        {
            StoreStaff? user = await _dbContext.StoreStaff.FirstOrDefaultAsync(staff =>
                (staff.Email == modelLogin.EmailOrUsername || staff.Username == modelLogin.EmailOrUsername) &&
                staff.Password == modelLogin.Password);

            if (user != null)
            {
                List<Claim> claims = new List<Claim>();
                
                claims.Add(new Claim(ClaimTypes.NameIdentifier, user.StaffID.ToString()));

                if (!string.IsNullOrEmpty(user.Username))
                {
                    claims.Add(new Claim(ClaimTypes.Name, user.Username));
                }

                if (!string.IsNullOrEmpty(user.Email))
                {
                    claims.Add(new Claim(ClaimTypes.Email, user.Email));
                }

                if (!string.IsNullOrEmpty(user.PhoneNumber))
                {
                    claims.Add(new Claim(ClaimTypes.MobilePhone, user.PhoneNumber));
                }

                if (!string.IsNullOrEmpty(user.Password))
                {
                    claims.Add(new Claim("Password", user.Password));
                }

                if (user.Dob != null)
                {
                    claims.Add(new Claim(ClaimTypes.DateOfBirth, user.Dob.Value.ToString()));
                }

                if (!string.IsNullOrEmpty(user.Address))
                {
                    claims.Add(new Claim(ClaimTypes.StreetAddress, user.Address));
                }

                if (!string.IsNullOrEmpty(user.StaffRole))
                {
                    claims.Add(new Claim(ClaimTypes.Role, user.StaffRole));
                }

                if (user.HourlyRate != null)
                {
                    claims.Add(new Claim("HourlyRate", user.HourlyRate.Value.ToString()));
                }

                if (user.StaffStatus != null)
                {
                    claims.Add(new Claim("StaffStatus", user.StaffStatus.Value.ToString()));
                }

                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims,
                    CookieAuthenticationDefaults.AuthenticationScheme);

                AuthenticationProperties properties = new AuthenticationProperties()
                {
                    AllowRefresh = true,
                    IsPersistent = modelLogin.RememberMe
                };

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity), properties);

                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError(string.Empty, "User not found");
            TempData["ErrorMessage"] = "User not found";

            return RedirectToAction("Login");
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }
    }
}
