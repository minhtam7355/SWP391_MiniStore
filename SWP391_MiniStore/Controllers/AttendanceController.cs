using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SWP391_MiniStore.Data;

namespace SWP391_MiniStore.Controllers
{
    [Authorize]
    public class AttendanceController : Controller
    {
        private readonly MiniStoreDbContext _dbContext;

        public AttendanceController(MiniStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [Authorize(Roles = "Manager")]
        public IActionResult ManagerWorkSchedule()
        {
            return View();
        }

    }
}
