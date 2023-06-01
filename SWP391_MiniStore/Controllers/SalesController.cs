using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SWP391_MiniStore.Data;
using SWP391_MiniStore.Models.Domain;
using System.Data;

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
    }
}
