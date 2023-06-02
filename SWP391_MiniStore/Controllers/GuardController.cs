using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SWP391_MiniStore.Data;
using SWP391_MiniStore.Models;
using SWP391_MiniStore.Models.Domain;
using SWP391_MiniStore.Models.ViewModels.Guard;
using System.Security.Claims;

namespace SWP391_MiniStore.Controllers
{
    [Authorize(Roles = "Guard")]
    public class GuardController : Controller
    {
        private readonly MiniStoreDbContext _dbContext;

        public GuardController(MiniStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GuardProfile()
        {
            StoreStaff? guard = await this.GetCurrentStoreStaffAsync(_dbContext, HttpContext);

            return View(guard);
        }

        [HttpGet]
        public async Task<IActionResult> EditGuardProfile()
        {
            StoreStaff? guard = await this.GetCurrentStoreStaffAsync(_dbContext, HttpContext);

            if (guard == null)
            {
                return NotFound();
            }

            EditGuardViewModel editGuard = new EditGuardViewModel()
            {
                Username = guard.Username,
                Email = guard.Email,
                PhoneNumber = guard.PhoneNumber,
                Dob = guard.Dob,
                Address = guard.Address,
            };

            return View(editGuard);
        }

        [HttpPost]
        public async Task<IActionResult> EditGuardProfile(EditGuardViewModel editGuard)
        {
            StoreStaff? guard = await this.GetCurrentStoreStaffAsync(_dbContext, HttpContext);

            if (ModelState.IsValid)
            {
                try
                {
                    if (guard != null)
                    {
                        guard.Username = editGuard.Username;
                        guard.Email = editGuard.Email;
                        guard.PhoneNumber = editGuard.PhoneNumber;
                        guard.Dob = editGuard.Dob;
                        guard.Address = editGuard.Address;

                        _dbContext.Update(guard);
                        await _dbContext.SaveChangesAsync();

                        // Update claims
                        await this.UpdateClaims(_dbContext, HttpContext, guard);
                    }
                    else
                    {
                        // Handle the case where guard is null
                        RedirectToAction("Error", "Home");
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    RedirectToAction("Error", "Home");
                }

                // Success
                return RedirectToAction("GuardProfile");
            }

            // ModelState is not valid
            return View(editGuard);
        }

        [HttpGet]
        public IActionResult ChangeGuardPassword()
        {
            string? errorMessage = TempData["ErrorMessage"] as string;
            if (!string.IsNullOrEmpty(errorMessage))
            {
                ModelState.AddModelError(string.Empty, errorMessage);
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ChangeGuardPassword(ChangePasswordViewModel changePassword)
        {
            StoreStaff? guard = await this.GetCurrentStoreStaffAsync(_dbContext, HttpContext);

            if (ModelState.IsValid)
            {
                if (guard != null)
                {
                    if (changePassword.NewPassword != changePassword.ConfirmPassword)
                    {
                        ModelState.AddModelError(string.Empty, "New password and confirm password must be the same");
                        TempData["ErrorMessage"] = "New password and confirm password must be the same";
                        return RedirectToAction("ChangeGuardPassword");
                    }
                    else if (changePassword.NewPassword == guard.Password)
                    {
                        ModelState.AddModelError(string.Empty, "New password cannot be the same as the current password");
                        TempData["ErrorMessage"] = "New password cannot be the same as the current password";
                        return RedirectToAction("ChangeGuardPassword");
                    }
                    else if (changePassword.NewPassword != null && guard.Username != null && changePassword.NewPassword.Contains(guard.Username))
                    {
                        ModelState.AddModelError(string.Empty, "New password cannot contain the username");
                        TempData["ErrorMessage"] = "New password cannot contain the username";
                        return RedirectToAction("ChangeGuardPassword");
                    }
                    else if (changePassword.CurrentPassword != guard.Password)
                    {
                        ModelState.AddModelError(string.Empty, "Incorrect current password");
                        TempData["ErrorMessage"] = "Incorrect current password";
                        return RedirectToAction("ChangeGuardPassword");
                    }
                    else
                    {
                        guard.Password = changePassword.NewPassword;

                        _dbContext.Update(guard);
                        await _dbContext.SaveChangesAsync();

                        // Update claims
                        await this.UpdateClaims(_dbContext, HttpContext, guard);

                        // Success
                        return RedirectToAction("GuardProfile");
                    }
                }
                else
                {
                    // Handle the case where guard is null
                    RedirectToAction("Error", "Home");
                }
            }

            // ModelState is not valid
            return View(changePassword);
        }
    }
}
