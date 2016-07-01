using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Easy.Data;
using Easy.Extend;
using Easy.Modules.User.Models;
using Easy.Modules.User.Service;
using Easy.Web.Controller;
using Microsoft.Practices.ServiceLocation;
using MvcApplication.Models;
using MvcApplication.Service;
using Easy.Web.Extend;
using System.Collections.Generic;

namespace MvcApplication.Controllers
{
    public class HomeController : BasicController<Example, int, IExampleService>
    {

        public HomeController(IExampleService service) :
            base(service)
        {

        }
        public override ActionResult Index()
        {
            Service.Add(new Example { Items = new List<ExampleItem> { new ExampleItem { } } });
            return base.Index();
        }
        [HttpPost]
        public ActionResult Index(string s)
        {
            var img = Request.SaveImage();
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
