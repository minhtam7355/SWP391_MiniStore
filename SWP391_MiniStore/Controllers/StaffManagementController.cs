using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SWP391_MiniStore.Data;
using SWP391_MiniStore.Models.Domain;
using SWP391_MiniStore.Models.ViewModels;
using System.Net;

namespace SWP391_MiniStore.Controllers
{
    [Authorize(Roles = "Manager")]
    public class StaffManagementController : Controller
    {
        private readonly MiniStoreDbContext _dbContext;

        public StaffManagementController(MiniStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var staffList = await _dbContext.StoreStaff.ToListAsync();
            StoreStaff? currentManager = await this.GetCurrentStoreStaffAsync(_dbContext, HttpContext);
            if (currentManager != null)
            {
                staffList.RemoveAll(s => s.StaffID == currentManager.StaffID);
            }
            return View(staffList);
        }

        [HttpGet]
        public IActionResult Add()
        {
            string? errorMessage = TempData["ErrorMessage"] as string;
            if (!string.IsNullOrEmpty(errorMessage))
            {
                ModelState.AddModelError(string.Empty, errorMessage);
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddStaffViewModel addStaffRequest)
        {
            if(addStaffRequest.Password != addStaffRequest.ConfirmPassword)
            {
                ModelState.AddModelError(string.Empty, "Password and confirm password must be the same");
                TempData["ErrorMessage"] = "Password and confirm password must be the same";
                return RedirectToAction("Add");
            }
            if (addStaffRequest.Password != null && addStaffRequest.Username != null && addStaffRequest.Password.Contains(addStaffRequest.Username))
            {
                ModelState.AddModelError(string.Empty, "Password cannot contain the username");
                TempData["ErrorMessage"] = "Password cannot contain the username";
                return RedirectToAction("Add");
            }

            var s = new StoreStaff()
            {
                StaffID = Guid.NewGuid(),
                Username = addStaffRequest.Username,
                Email = addStaffRequest.Email,
                PhoneNumber = addStaffRequest.PhoneNumber,
                Password = addStaffRequest.Password,
                Dob = addStaffRequest.Dob,
                Address = addStaffRequest.Address,
                StaffRole = addStaffRequest.StaffRole,
                HourlyRate = addStaffRequest.HourlyRate,
                StaffStatus = true
            };

            await _dbContext.StoreStaff.AddAsync(s);
            await _dbContext.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> View(Guid id)
        {
            var s = await _dbContext.StoreStaff.FirstOrDefaultAsync(s => s.StaffID == id);
            if (s != null)
            {
                string? errorMessage = TempData["ErrorMessage"] as string;
                if (!string.IsNullOrEmpty(errorMessage))
                {
                    ModelState.AddModelError(string.Empty, errorMessage);
                }

                var viewModel = new UpdateStaffViewModel()
                {
                    StaffID = s.StaffID,
                    Username = s.Username,
                    Email = s.Email,
                    PhoneNumber = s.PhoneNumber,
                    Password = s.Password,
                    ConfirmPassword = s.Password,
                    Dob = s.Dob,
                    Address = s.Address,
                    StaffRole = s.StaffRole,
                    HourlyRate = s.HourlyRate,
                    StaffStatus = s.StaffStatus
                };
                return View(viewModel);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateStaffViewModel updateStaffRequest)
        {
            if (updateStaffRequest.Password != updateStaffRequest.ConfirmPassword)
            {
                ModelState.AddModelError(string.Empty, "Password and confirm password must be the same");
                TempData["ErrorMessage"] = "Password and confirm password must be the same";
                return RedirectToAction("View", new { id = updateStaffRequest.StaffID });
            }
            if (updateStaffRequest.Password != null && updateStaffRequest.Username != null && updateStaffRequest.Password.Contains(updateStaffRequest.Username))
            {
                ModelState.AddModelError(string.Empty, "Password cannot contain the username");
                TempData["ErrorMessage"] = "Password cannot contain the username";
                return RedirectToAction("View", new { id = updateStaffRequest.StaffID });
            }

            var s = await _dbContext.StoreStaff.FindAsync(updateStaffRequest.StaffID);
            if (s != null)
            {
                s.Username = updateStaffRequest.Username;
                s.Email = updateStaffRequest.Email;
                s.PhoneNumber = updateStaffRequest.PhoneNumber;
                s.Password = updateStaffRequest.Password;
                s.Dob = updateStaffRequest.Dob;
                s.Address = updateStaffRequest.Address;
                s.StaffRole = updateStaffRequest.StaffRole;
                s.HourlyRate = updateStaffRequest.HourlyRate;
                s.StaffStatus = updateStaffRequest.StaffStatus;

                await _dbContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(UpdateStaffViewModel updateStaffRequest)
        {
            var s = await _dbContext.StoreStaff.FindAsync(updateStaffRequest.StaffID);
            if (s != null)
            {
                _dbContext.StoreStaff.Remove(s);
                await _dbContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
    }
}
