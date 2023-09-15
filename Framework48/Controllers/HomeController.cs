using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Framework48.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";
            var model = new Models.Article("Title", "Text");
            Response.Cookies.Add(cookie);
            return View(model);
        }
    }
}