using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Lab4.Data;
using Lab4.Models;

namespace Lab4.Controllers
{
    public class LearnerController : Controller
    {
        private SchoolContext db;

        public LearnerController(SchoolContext context)
        {
            db = context;
        }

        public IActionResult Index()
        {
            var learners = db.Learners.Include(m => m.Major).ToList();
            return View(learners);
        }

        // Thêm 2 action Create
        public IActionResult Create()
        {
            // Tạo SelectList hiển thị danh sách chuyên ngành (cách 1)
            var majors = new List<SelectListItem>();
            foreach (var item in db.Majors)
            {
                majors.Add(new SelectListItem
                {
                    Text = item.MajorName,
                    Value = item.MajorId.ToString()
                });
            }

            ViewBag.MajorID = majors;

            // Tạo SelectList hiển thị danh sách chuyên ngành (cách 2)
            ViewBag.MajorID = new SelectList(db.Majors, "MajorId", "MajorName");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("FirstMidName,LastName,MajorID,EnrollmentDate")] Learner learner)
        {
            if (ModelState.IsValid)
            {
                db.Learners.Add(learner);
                db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            // Tạo lại SelectList khi dữ liệu không hợp lệ
            ViewBag.MajorID = new SelectList(db.Majors, "MajorId", "MajorName");
            return View(learner);
        }

        // Thêm 2 action Edit
        public IActionResult Edit(int? id)
        {
            if (id == null || db.Learners == null)
            {
                return NotFound();
            }

            var learner = db.Learners.Find(id);
            if (learner == null)
            {
                return NotFound();
            }

            ViewBag.MajorID = new SelectList(
                db.Majors, "MajorId", "MajorName", learner.MajorID);

            return View(learner);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(
            int id,
            [Bind("LearnerID,FirstMidName,LastName,MajorID,EnrollmentDate")] Learner learner)
        {
            if (id != learner.LearnerID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    db.Update(learner);
                    db.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LearnerExists(learner.LearnerID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return RedirectToAction(nameof(Index));
            }

            ViewBag.MajorID = new SelectList(
                db.Majors, "MajorId", "MajorName", learner.MajorID);

            return View(learner);
        }

        private bool LearnerExists(int id)
        {
            return (db.Learners?.Any(e => e.LearnerID == id)).GetValueOrDefault();
        }

        // Thêm 2 action Delete
        public IActionResult Delete(int id)
        {
            if (db.Learners == null)
            {
                return NotFound();
            }

            var learner = db.Learners
                .Include(l => l.Major)
                .FirstOrDefault(m => m.LearnerID == id);

            if (learner == null)
            {
                return NotFound();
            }

            if (db.Enrollments.Any(e => e.LearnerID == id))
            {
                return Content("This learner has some enrollments, can't delete!");
            }

            return View(learner);
        }

        // POST: Learner/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            if (db.Learners == null)
            {
                return Problem("Entity set 'Learners' is null.");
            }

            var learner = db.Learners
                .FirstOrDefault(l => l.LearnerID == id);

            if (learner != null)
            {
                if (db.Enrollments.Any(e => e.LearnerID == id))
                {
                    return Content("This learner has some enrollments, can't delete!");
                }

                db.Learners.Remove(learner);
            }

            db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
