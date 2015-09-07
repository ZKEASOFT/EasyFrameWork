using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Easy.Modules.User.Models;
using Easy.Modules.User.Service;
using Easy.Web.Controller;
using Microsoft.Practices.ServiceLocation;
using MvcApplication.Models;
using MvcApplication.Service;

namespace MvcApplication.Controllers
{
    public class HomeController : BasicController<UserEntity, string, IUserService>
    {
        public HomeController(IUserService service) :
            base(service)
        {
            var user= service.Get("admin");
            service.Update(user);
            service.Get("admin");
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
