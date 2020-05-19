using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Core31.Models;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Core31.Controllers
{
    public class HomeController : Controller
    {

        public IActionResult Index(string value)
        {
            ViewBag.Title = "Home Page";
            ViewData["Value"] = "ViewData: " + value;
            ViewBag.Value = "ViewBag: " + value;
            HttpContext.Items["Value"] = "HttpContextItems: " + value;

            var model = new Models.Article("Title", "Text");
            return View(model);
        }
    }
}
