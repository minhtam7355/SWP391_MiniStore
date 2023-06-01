using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SWP391_MiniStore.Data;
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

                return RedirectToAction("SalesProfile");
            }

            return View(editSales);
        }
    }
}
