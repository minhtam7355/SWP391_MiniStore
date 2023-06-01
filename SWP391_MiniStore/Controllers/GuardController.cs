using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SWP391_MiniStore.Data;
using SWP391_MiniStore.Models.Domain;
using System.Data;

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
    }
}
