using Microsoft.AspNetCore.Mvc;

namespace Core31.Components
{
    public class ContentViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}

