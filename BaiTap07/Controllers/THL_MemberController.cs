using Microsoft.AspNetCore.Mvc;
using lesson7.Models;

namespace lesson7.Controllers
{
    public class THL_MemberController : Controller
    {
        private static List<THL_Member> members = new List<THL_Member>();

        public IActionResult Index()
        {
            return View(members);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(THL_Member member)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(member);
                }

                member.Id = members.Count + 1;
                members.Add(member);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(member);
            }
        }
    }
}