using System.Web.Mvc;
namespace Easy.Web.ActionInvoker
{
    public class FiltersControllerActionInvoker : ControllerActionInvoker
    {
        protected override FilterInfo GetFilters(ControllerContext controllerContext, ActionDescriptor actionDescriptor)
        {
            return base.GetFilters(controllerContext, actionDescriptor);
        }
    }
}