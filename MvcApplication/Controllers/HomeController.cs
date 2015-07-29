using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Easy;
using Easy.Data;
using Easy.IOC.Unity;
using Easy.Modules.DataDictionary;
using Easy.Web.Controller;
using Microsoft.Practices.ServiceLocation;
using MvcApplication.Models;
using MvcApplication.Service;

namespace MvcApplication.Controllers
{
    public class HomeController : BasicController<Example,IExampleService>
    {
        public HomeController(IExampleService service) :
            base(service)
        {
           int a= ServiceLocator.Current.GetInstance<IHttpItemsValueProvider>().GetHashCode();
           int b = ServiceLocator.Current.GetInstance<IHttpItemsValueProvider>().GetHashCode();
        }
        public ActionResult Index()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

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
