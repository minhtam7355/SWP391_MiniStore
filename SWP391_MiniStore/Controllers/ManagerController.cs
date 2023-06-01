using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SWP391_MiniStore.Data;
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

                return RedirectToAction("ManagerProfile");
            }

            return View(editManager);
        }
    }
}
