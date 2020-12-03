using Microsoft.AspNetCore.Mvc;

namespace Core31.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index(string page, string message)
        {
            if(page == null)
            {
                ViewBag.Title = "Welcome";
                ViewBag.Content = "This is a great page!";
                return View();
            }

            ViewData["Title"] = "Page " + page + " not found";
            ViewData["Content"] = message;
            return View();
        }
    }
}
