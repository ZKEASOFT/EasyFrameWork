using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Easy.Web.Controller;
using Microsoft.Practices.ServiceLocation;
using MvcApplication.Models;
using MvcApplication.Service;

namespace MvcApplication.Controllers
{
    public class HomeController : BasicController<Example, IExampleService>
    {
        public HomeController(IExampleService service) :
            base(service)
        {

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
