using Microsoft.AspNetCore.Mvc;
using MVCModelDemo.Models;
using System.Collections.Generic;
using System.Linq;

namespace MVCModelDemo.Controllers
{
    public class HomeController : Controller
    {
        // In-memory "data store" so the demo runs without a real database.
        // In a real app this would be replaced by a DbContext / repository.
        private static List<User> users = new List<User>
        {
            new User { Id = 1, name = "Mark Smith",    address = "Park Street",      email = "Mark@mvcexample.com" },
            new User { Id = 2, name = "John Parker",   address = "New Park",         email = "John@mvcexample.com" },
            new User { Id = 3, name = "Steave Edward", address = "Melbourne Street", email = "steave@mvcexample.com" }
        };

        // ---------- List template ----------
        public ActionResult Index()
        {
            return View(users);
        }

        // ---------- Create template ----------
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {
                user.Id = users.Any() ? users.Max(u => u.Id) + 1 : 1;
                users.Add(user);
                return RedirectToAction("Index");
            }
            return View(user);
        }

        // ---------- Edit template ----------
        public ActionResult Edit(long id)
        {
            var user = users.FirstOrDefault(u => u.Id == id);
            if (user == null) return NotFound();
            return View(user);
        }

        [HttpPost]
        public ActionResult Edit(User user)
        {
            if (ModelState.IsValid)
            {
                var existing = users.FirstOrDefault(u => u.Id == user.Id);
                if (existing != null)
                {
                    existing.name = user.name;
                    existing.address = user.address;
                    existing.email = user.email;
                }
                return RedirectToAction("Index");
            }
            return View(user);
        }

        // ---------- Details template ----------
        public ActionResult Details(long id)
        {
            var user = users.FirstOrDefault(u => u.Id == id);
            if (user == null) return NotFound();
            return View(user);
        }

        // ---------- Delete template ----------
        public ActionResult Delete(long id)
        {
            var user = users.FirstOrDefault(u => u.Id == id);
            if (user == null) return NotFound();
            return View(user);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(long id)
        {
            var user = users.FirstOrDefault(u => u.Id == id);
            if (user != null) users.Remove(user);
            return RedirectToAction("Index");
        }

        // ---------- Model binding demo (primitive vs object) ----------
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Login login)
        {
            if (login.userName == "Peter" && login.password == "pass@123")
            {
                return Content("Welcome " + login.userName);
            }
            return View();
        }
    }
}
