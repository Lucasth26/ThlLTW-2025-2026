// Author: THL

using Microsoft.AspNetCore.Mvc;

namespace NetCoreLA7THL.ViewComponents
{
    public class THLNavLeftViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
