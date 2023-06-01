using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SWP391_MiniStore.Data;
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

                return RedirectToAction("GuardProfile");
            }

            return View(editGuard);
        }
    }
}
