
// Author: THL

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetCoreLA7THL.Data;

namespace NetCoreLA7THL.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class THLDashboardController : Controller
    {
        private readonly THLAppDbContext _context;

        public THLDashboardController(THLAppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> THLIndex()
        {
            ViewBag.CategoryCount = await _context.THLCategories.CountAsync();
            ViewBag.ProductCount = await _context.THLProducts.CountAsync();
            ViewBag.BlogCount = await _context.THLBlogs.CountAsync();
            ViewBag.BannerCount = await _context.THLBanners.CountAsync();

            return View();
        }
    }
}
