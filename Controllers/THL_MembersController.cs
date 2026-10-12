using Microsoft.AspNetCore.Mvc;
using THL_Lab05.Models;
using System.Text.RegularExpressions;

namespace THL_Lab05.Controllers
{
    public class THL_MembersController : Controller
    {
        private static List<THL_Members> members = new List<THL_Members>();
        private static int nextId = 1;

        public ActionResult Index()
        {
            return View(members);
        }

        public ActionResult Details(int id)
        {
            var member = members.FirstOrDefault(m => m.Id == id);
            if (member == null) return NotFound();
            return View(member);
        }

        public ActionResult Create()
        {
            THL_Members model = new THL_Members();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(THL_Members model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            model.Id = nextId++;
            members.Add(model);
            return RedirectToAction(nameof(Index));
        }

        public ActionResult Edit(int id)
        {
            var member = members.FirstOrDefault(m => m.Id == id);
            if (member == null) return NotFound();
            return View(member);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(THL_Members model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            int index = members.FindIndex(m => m.Id == model.Id);
            if (index < 0) return NotFound();

            members[index] = model;
            return RedirectToAction(nameof(Index));
        }

        public ActionResult Delete(int id)
        {
            var member = members.FirstOrDefault(m => m.Id == id);
            if (member == null) return NotFound();
            return View(member);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            members.RemoveAll(m => m.Id == id);
            return RedirectToAction(nameof(Index));
        }

        [AcceptVerbs("GET", "POST")]
        public IActionResult VerifyPhone(string phone)
        {
            Regex _isPhone = new Regex(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$");
            if (!_isPhone.IsMatch(phone))
            {
                return Json($"Số điện thoại {phone} Không đúng định dạng, VD: 0986421127 hoặc 098.421.1127");
            }

            return Json(true);
        }
    }
}