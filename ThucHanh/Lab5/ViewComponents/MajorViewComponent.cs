using Lab5.Data;
using Microsoft.AspNetCore.Mvc;

namespace Lab5.ViewComponents;

public class MajorViewComponent(SchoolContext db) : ViewComponent
{
    public IViewComponentResult Invoke()
    {
        return View("RenderMajor", db.Majors);
    }
}
