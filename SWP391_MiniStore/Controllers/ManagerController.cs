using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SWP391_MiniStore.Data;
using SWP391_MiniStore.Models.Domain;
using System.Data;

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
    }
}
