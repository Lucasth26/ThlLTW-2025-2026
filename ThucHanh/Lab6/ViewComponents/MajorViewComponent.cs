using Lab6.Data;
using Microsoft.AspNetCore.Mvc;

namespace Lab6.ViewComponents;

public class MajorViewComponent(SchoolContext db) : ViewComponent
{
    public IViewComponentResult Invoke()
    {
        return View("RenderMajor", db.Majors);
    }
}

