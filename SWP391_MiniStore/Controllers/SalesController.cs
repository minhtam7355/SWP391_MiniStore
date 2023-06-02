using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SWP391_MiniStore.Data;
using SWP391_MiniStore.Models;
using SWP391_MiniStore.Models.Domain;
using SWP391_MiniStore.Models.ViewModels.Sales;
using System.Security.Claims;

namespace SWP391_MiniStore.Controllers
{
    [Authorize(Roles = "Sales")]
    public class SalesController : Controller
    {
        private readonly MiniStoreDbContext _dbContext;

        public SalesController(MiniStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> SalesProfile()
        {
            StoreStaff? sales = await this.GetCurrentStoreStaffAsync(_dbContext, HttpContext);

            return View(sales);
        }

        [HttpGet]
        public async Task<IActionResult> EditSalesProfile()
        {
            StoreStaff? sales = await this.GetCurrentStoreStaffAsync(_dbContext, HttpContext);

            if (sales == null)
            {
                return NotFound();
            }

            EditSalesViewModel editSales = new EditSalesViewModel()
            {
                Username = sales.Username,
                Email = sales.Email,
                PhoneNumber = sales.PhoneNumber,
                Dob = sales.Dob,
                Address = sales.Address,
            };

            return View(editSales);
        }

        [HttpPost]
        public async Task<IActionResult> EditSalesProfile(EditSalesViewModel editSales)
        {
            StoreStaff? sales = await this.GetCurrentStoreStaffAsync(_dbContext, HttpContext);

            if (ModelState.IsValid)
            {
                try
                {
                    if (sales != null)
                    {
                        sales.Username = editSales.Username;
                        sales.Email = editSales.Email;
                        sales.PhoneNumber = editSales.PhoneNumber;
                        sales.Dob = editSales.Dob;
                        sales.Address = editSales.Address;

                        _dbContext.Update(sales);
                        await _dbContext.SaveChangesAsync();

                        // Update claims
                        await this.UpdateClaims(_dbContext, HttpContext, sales);
                    }
                    else
                    {
                        // Handle the case where sales is null
                        RedirectToAction("Error", "Home");
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    RedirectToAction("Error", "Home");
                }

                // Success
                return RedirectToAction("SalesProfile");
            }

            // ModelState is not valid
            return View(editSales);
        }

        [HttpGet]
        public IActionResult ChangeSalesPassword()
        {
            string? errorMessage = TempData["ErrorMessage"] as string;
            if (!string.IsNullOrEmpty(errorMessage))
            {
                ModelState.AddModelError(string.Empty, errorMessage);
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ChangeSalesPassword(ChangePasswordViewModel changePassword)
        {
            StoreStaff? sales = await this.GetCurrentStoreStaffAsync(_dbContext, HttpContext);

            if (ModelState.IsValid)
            {
                if (sales != null)
                {
                    if (changePassword.NewPassword != changePassword.ConfirmPassword)
                    {
                        ModelState.AddModelError(string.Empty, "New password and confirm password must be the same");
                        TempData["ErrorMessage"] = "New password and confirm password must be the same";
                        return RedirectToAction("ChangeSalesPassword");
                    }
                    else if (changePassword.NewPassword == sales.Password)
                    {
                        ModelState.AddModelError(string.Empty, "New password cannot be the same as the current password");
                        TempData["ErrorMessage"] = "New password cannot be the same as the current password";
                        return RedirectToAction("ChangeSalesPassword");
                    }
                    else if (changePassword.NewPassword != null && sales.Username != null && changePassword.NewPassword.Contains(sales.Username))
                    {
                        ModelState.AddModelError(string.Empty, "New password cannot contain the username");
                        TempData["ErrorMessage"] = "New password cannot contain the username";
                        return RedirectToAction("ChangeSalesPassword");
                    }
                    else if (changePassword.CurrentPassword != sales.Password)
                    {
                        ModelState.AddModelError(string.Empty, "Incorrect current password");
                        TempData["ErrorMessage"] = "Incorrect current password";
                        return RedirectToAction("ChangeSalesPassword");
                    }
                    else
                    {
                        sales.Password = changePassword.NewPassword;

                        _dbContext.Update(sales);
                        await _dbContext.SaveChangesAsync();

                        // Update claims
                        await this.UpdateClaims(_dbContext, HttpContext, sales);

                        // Success
                        return RedirectToAction("SalesProfile");
                    }
                }
                else
                {
                    // Handle the case where sales is null
                    RedirectToAction("Error", "Home");
                }
            }

            // ModelState is not valid
            return View(changePassword);
        }
    }
}
