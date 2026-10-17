// Author: THL

using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetCoreLA7THL.Data;
using NetCoreLA7THL.Models;

namespace NetCoreLA7THL.Controllers
{
    public class THLHomeController : Controller
    {
        private readonly THLAppDbContext _context;

        public THLHomeController(THLAppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> THLIndex()
        {
            var model = new THLHomeViewModel
            {
                Banners = await _context.THLBanners
                    .Where(x => x.Status == 1)
                    .OrderByDescending(x => x.Priority)
                    .ToListAsync(),
                Products = await _context.THLProducts
                    .Include(x => x.Category)
                    .Where(x => x.Status == 1)
                    .OrderByDescending(x => x.Id)
                    .Take(9)
                    .ToListAsync()
            };
            return View(model);
        }

        public IActionResult THLAbout()
        {
            return View();
        }

        public IActionResult THLContact()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult THLError()
        {
            return View(new THLErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}