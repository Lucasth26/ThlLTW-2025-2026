// Author: THL

using Microsoft.AspNetCore.Mvc;

namespace NetCoreLA7THL.ViewComponents
{
    public class THLNavTopViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
