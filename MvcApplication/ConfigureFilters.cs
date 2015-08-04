using System.Web.Mvc;
using Easy.Web.Filter;
using MvcApplication.Controllers;

namespace MvcApplication
{
    public class ConfigureFilters : ConfigureFilterBase
    {
        public ConfigureFilters(IFilterRegister register) : base(register)
        {
        }

        public override void Configure()
        {
            Registry.Register<HomeController, Tfilter>(m => m.Index());
        }
    }

    public class Tfilter : AuthorizeAttribute
    {

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            throw new System.NotImplementedException();
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            throw new System.NotImplementedException();
        }
    }
}