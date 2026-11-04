using Lab6.Data;
using Lab6.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Lab6.Controllers;

public class LearnerController(SchoolContext db) : Controller
{
    private const int PageSize = 3;

    [HttpGet]
    public IActionResult Index(int? mid, string? keyword, int? pageIndex)
    {
        return View(GetLearnerPage(mid, keyword, pageIndex));
    }

    [HttpGet]
    public IActionResult LearnerFilter(int? mid, string? keyword, int? pageIndex)
    {
        return PartialView("LearnerTable", GetLearnerPage(mid, keyword, pageIndex));
    }

    [HttpGet]
    public IActionResult Edit(int id)
    {
        var learner = db.Find(id);
        if (learner is null)
        {
            return NotFound();
        }

        ViewBag.MajorID = new SelectList(db.Majors, "MajorID", "MajorName", learner.MajorID);
        return View(learner);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(int id, string firstMidName, string lastName, int majorID, DateOnly enrollmentDate)
    {
        var original = db.Find(id);
        if (original is null)
        {
            return NotFound();
        }

        var major = db.Majors.FirstOrDefault(item => item.MajorID == majorID);
        if (major is null)
        {
            ModelState.AddModelError(nameof(majorID), "Select a valid major.");
        }

        if (!ModelState.IsValid || major is null)
        {
            ViewBag.MajorID = new SelectList(db.Majors, "MajorID", "MajorName", majorID);
            return View(original);
        }

        var updated = new Learner(id, firstMidName.Trim(), lastName.Trim(), major, enrollmentDate);
        if (!db.Update(updated, original))
        {
            return Conflict();
        }

        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public IActionResult Delete(int id)
    {
        var learner = db.Find(id);
        return learner is null ? NotFound() : View(learner);
    }

    [HttpPost, ActionName(nameof(Delete))]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(int id)
    {
        return db.Delete(id) ? RedirectToAction(nameof(Index)) : NotFound();
    }

    private IReadOnlyList<Learner> GetLearnerPage(int? mid, string? keyword, int? pageIndex)
    {
        IEnumerable<Learner> learners = db.Learners;
        keyword = keyword?.Trim();

        if (mid.HasValue)
        {
            learners = learners.Where(learner => learner.MajorID == mid.Value);
        }

        if (!string.IsNullOrEmpty(keyword))
        {
            learners = learners.Where(learner =>
                learner.FirstMidName.Contains(keyword, StringComparison.OrdinalIgnoreCase));
        }

        var results = learners.OrderBy(learner => learner.LearnerID).ToList();
        int pageCount = (int)Math.Ceiling(results.Count / (double)PageSize);
        int page = Math.Clamp(pageIndex ?? 1, 1, Math.Max(1, pageCount));

        ViewBag.PageCount = pageCount;
        ViewBag.PageIndex = page;
        ViewBag.Mid = mid;
        ViewBag.Keyword = keyword;

        return results.Skip((page - 1) * PageSize).Take(PageSize).ToList();
    }
}

