using Lab5.Data;
using Lab5.Models;
using Microsoft.AspNetCore.Mvc;

namespace Lab5.Controllers;

public class LearnerController(SchoolContext db) : Controller
{
    [HttpGet]
    public IActionResult Index(int? mid)
    {
        return View(mid.HasValue ? FilterByMajor(mid.Value) : db.Learners);
    }

    [HttpGet]
    public IActionResult LearnerByMajorID(int mid)
    {
        return PartialView("LearnerTable", FilterByMajor(mid));
    }

    private IEnumerable<Learner> FilterByMajor(int mid)
    {
        return db.Learners.Where(learner => learner.MajorID == mid);
    }
}
