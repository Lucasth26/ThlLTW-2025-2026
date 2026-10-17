
// Author: THL

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetCoreLA7THL.Data;
using NetCoreLA7THL.Helpers;
using NetCoreLA7THL.Models;

namespace NetCoreLA7THL.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class THLBlogController : Controller
    {
        private readonly THLAppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public THLBlogController(THLAppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public async Task<IActionResult> THLIndex()
        {
            var list = await _context.THLBlogs
                .OrderByDescending(x => x.Id)
                .ToListAsync();

            return View(list);
        }

        public async Task<IActionResult> THLDetails(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _context.THLBlogs
                .FirstOrDefaultAsync(x => x.Id == id);

            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        public async Task<IActionResult> THLCreate()
        {
            return View(new THLBlog());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> THLCreate(THLBlog model)
        {
            await THLValidate(model);

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            model.Image = await THLFileHelper.SaveImageAsync(
                model.ImageFile, _env, "blogs");

            _context.Add(model);
            await _context.SaveChangesAsync();

            TempData["Success"] = "Thêm mới thành công";

            return RedirectToAction(nameof(THLIndex));
        }

        public async Task<IActionResult> THLEdit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _context.THLBlogs.FindAsync(id);

            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> THLEdit(int id, THLBlog model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            var item = await _context.THLBlogs.FindAsync(id);

            if (item == null)
            {
                return NotFound();
            }

            await THLValidate(model);

            if (!ModelState.IsValid)
            {
                model.Image = item.Image;
                return View(model);
            }

            item.Name = model.Name;
            item.Status = model.Status;
            item.CreatedDate = model.CreatedDate;
            item.Description = model.Description;

            if (model.ImageFile != null && model.ImageFile.Length > 0)
            {
                THLFileHelper.DeleteFile(item.Image, _env);

                item.Image = await THLFileHelper.SaveImageAsync(
                    model.ImageFile, _env, "blogs");
            }

            await _context.SaveChangesAsync();

            TempData["Success"] = "Cập nhật thành công";

            return RedirectToAction(nameof(THLIndex));
        }

        public async Task<IActionResult> THLDelete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _context.THLBlogs
                .FirstOrDefaultAsync(x => x.Id == id);

            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        [HttpPost, ActionName("THLDelete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> THLDeleteConfirmed(int id)
        {
            var item = await _context.THLBlogs.FindAsync(id);

            if (item == null)
            {
                return NotFound();
            }

            try
            {
                _context.THLBlogs.Remove(item);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                TempData["Error"] = "Không thể xóa vì dữ liệu đang được sử dụng";
                return RedirectToAction(nameof(THLIndex));
            }

            THLFileHelper.DeleteFile(item.Image, _env);

            TempData["Success"] = "Xóa thành công";

            return RedirectToAction(nameof(THLIndex));
        }

        private async Task THLValidate(THLBlog model)
        {
            if (!string.IsNullOrWhiteSpace(model.Name)
                && await _context.THLBlogs.AnyAsync(
                    x => x.Name == model.Name && x.Id != model.Id))
            {
                ModelState.AddModelError("Name", "Tên đã tồn tại");
            }

            if (!THLFileHelper.IsValidImage(model.ImageFile))
            {
                ModelState.AddModelError(
                    "ImageFile",
                    "Chỉ chấp nhận ảnh jpg, png, gif, webp tối đa 2MB");
            }
        }
    }
}
