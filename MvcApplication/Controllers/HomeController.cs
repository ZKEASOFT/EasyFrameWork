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
            var s = Service.Get(new DataFilter().Where("Id", OperatorType.In, new[] { 1, 2 }));
            return base.Index();
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
