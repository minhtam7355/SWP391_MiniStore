using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SWP391_MiniStore.Data;

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
            return View(staffList);
        }

        
    }
}
