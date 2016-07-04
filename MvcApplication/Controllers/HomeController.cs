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
using System.Linq.Expressions;

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
            Example m1 = new Example { Id = 1 };

            ExampleItem m2 = new ExampleItem { ID = 2 };

            Expression<Func<Example, ExampleItem, bool>> express = (p1, p2) => p1.Id == p2.ID;

            Expression.Lambda((express.Body as BinaryExpression).Left, Expression.Parameter(typeof(Example), "p1")).Compile().DynamicInvoke(m1);

            Expression.Lambda((express.Body as BinaryExpression).Right, Expression.Parameter(typeof(Example), "p2")).Compile().DynamicInvoke(m2);

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
