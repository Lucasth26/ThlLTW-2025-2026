// Author: THL

using Microsoft.AspNetCore.Mvc;

namespace NetCoreLA7THL.ViewComponents
{
    public class THLFooterViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
