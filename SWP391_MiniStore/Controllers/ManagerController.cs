using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SWP391_MiniStore.Data;
using SWP391_MiniStore.Models;
using SWP391_MiniStore.Models.Domain;
using SWP391_MiniStore.Models.ViewModels.Manager;
using System.Security.Claims;

namespace SWP391_MiniStore.Controllers
{
    [Authorize(Roles = "Manager")]
    public class ManagerController : Controller
    {
        private readonly MiniStoreDbContext _dbContext;

        public ManagerController(MiniStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ManagerProfile()
        {
            StoreStaff? manager = await this.GetCurrentStoreStaffAsync(_dbContext, HttpContext);

            return View(manager);
        }

        [HttpGet]
        public async Task<IActionResult> EditManagerProfile()
        {
            StoreStaff? manager = await this.GetCurrentStoreStaffAsync(_dbContext, HttpContext);

            if (manager == null)
            {
                return NotFound();
            }

            EditManagerViewModel editManager = new EditManagerViewModel()
            {
                Username = manager.Username,
                Email = manager.Email,
                PhoneNumber = manager.PhoneNumber,
                Dob = manager.Dob,
                Address = manager.Address,
            };

            return View(editManager);
        }

        [HttpPost]
        public async Task<IActionResult> EditManagerProfile(EditManagerViewModel editManager)
        {
            StoreStaff? manager = await this.GetCurrentStoreStaffAsync(_dbContext, HttpContext);

            if (ModelState.IsValid)
            {
                try
                {
                    if (manager != null)
                    {
                        manager.Username = editManager.Username;
                        manager.Email = editManager.Email;
                        manager.PhoneNumber = editManager.PhoneNumber;
                        manager.Dob = editManager.Dob;
                        manager.Address = editManager.Address;

                        _dbContext.Update(manager);
                        await _dbContext.SaveChangesAsync();

                        // Update claims
                        await this.UpdateClaims(_dbContext, HttpContext, manager);
                    }
                    else
                    {
                        // Handle the case where manager is null
                        RedirectToAction("Error", "Home");
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    RedirectToAction("Error", "Home");
                }

                // Success
                return RedirectToAction("ManagerProfile");
            }

            // ModelState is not valid
            return View(editManager);
        }

        [HttpGet]
        public IActionResult ChangeManagerPassword()
        {
            string? errorMessage = TempData["ErrorMessage"] as string;
            if (!string.IsNullOrEmpty(errorMessage))
            {
                ModelState.AddModelError(string.Empty, errorMessage);
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ChangeManagerPassword(ChangePasswordViewModel changePassword)
        {
            StoreStaff? manager = await this.GetCurrentStoreStaffAsync(_dbContext, HttpContext);
            
            if (ModelState.IsValid)
            {
                if (manager != null)
                {
                    if (changePassword.NewPassword != changePassword.ConfirmPassword)
                    {
                        ModelState.AddModelError(string.Empty, "New password and confirm password must be the same");
                        TempData["ErrorMessage"] = "New password and confirm password must be the same";
                        return RedirectToAction("ChangeManagerPassword");
                    }
                    else if (changePassword.NewPassword == manager.Password)
                    {
                        ModelState.AddModelError(string.Empty, "New password cannot be the same as the current password");
                        TempData["ErrorMessage"] = "New password cannot be the same as the current password";
                        return RedirectToAction("ChangeManagerPassword");
                    }
                    else if (changePassword.NewPassword != null && manager.Username != null && changePassword.NewPassword.Contains(manager.Username))
                    {
                        ModelState.AddModelError(string.Empty, "New password cannot contain the username");
                        TempData["ErrorMessage"] = "New password cannot contain the username";
                        return RedirectToAction("ChangeManagerPassword");
                    }
                    else if (changePassword.CurrentPassword != manager.Password)
                    {
                        ModelState.AddModelError(string.Empty, "Incorrect current password");
                        TempData["ErrorMessage"] = "Incorrect current password";
                        return RedirectToAction("ChangeManagerPassword");
                    }
                    else
                    {
                        manager.Password = changePassword.NewPassword;

                        _dbContext.Update(manager);
                        await _dbContext.SaveChangesAsync();

                        // Update claims
                        await this.UpdateClaims(_dbContext, HttpContext, manager);

                        // Success
                        return RedirectToAction("ManagerProfile");
                    }
                }
                else
                {
                    // Handle the case where manager is null
                    RedirectToAction("Error", "Home");
                }
            }

            // ModelState is not valid
            return View(changePassword);
        }
    }
}
