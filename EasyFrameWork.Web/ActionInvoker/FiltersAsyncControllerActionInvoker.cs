using System.Web.Mvc;
using System.Web.Mvc.Async;
namespace Easy.Web.ActionInvoker
{
    public class FiltersAsyncControllerActionInvoker : AsyncControllerActionInvoker
    {
        protected override FilterInfo GetFilters(ControllerContext controllerContext, ActionDescriptor actionDescriptor)
        {
            return base.GetFilters(controllerContext, actionDescriptor);
        }
    }
}