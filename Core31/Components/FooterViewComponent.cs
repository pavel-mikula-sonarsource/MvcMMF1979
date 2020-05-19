using Core31.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core31.Components
{
    public class FooterViewComponent : ViewComponent
    {
        //public IViewComponentResult Invoke(int count)
        //{
        //    var model = new Article(count + "x Title", "Count: " + count);
        //    return View(model); //Default.cshtml
        //}
        public IViewComponentResult Invoke(string Title, string Text)
        {
            var model = new Article(Title, Text);
            return View(model); //Default.cshtml
        }
    }

    //public class Footer : ViewComponent
    //{
    //    //public IViewComponentResult Invoke(int count)
    //    //{
    //    //    var model = new Article(count + "x Title", "Count: " + count);
    //    //    return View(model); //Default.cshtml
    //    //}

    //}
}

