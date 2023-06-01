using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SWP391_MiniStore.Data;
using SWP391_MiniStore.Models.Domain;
using System.Security.Claims;

namespace SWP391_MiniStore.Controllers
{
    public static class ControllerExtensions
    {
        public static async Task<StoreStaff> GetCurrentStoreStaffAsync(this Controller controller, MiniStoreDbContext _dbContext, HttpContext httpContext)
        {
            // Get the staff ID claim for the current user
            string? staffID = httpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            // Retrieve the StoreStaff data based on the user ID
            StoreStaff? staff = await _dbContext.StoreStaff.FirstOrDefaultAsync(s => s.StaffID.ToString() == staffID);

            return staff;
        }
    }
}
