using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Easy.Web.ActionInvoker;

namespace Easy.Web.ControllerFactory
{
    class FilterControllerFactory : DefaultControllerFactory
    {
        protected override IController GetControllerInstance(System.Web.Routing.RequestContext requestContext, Type controllerType)
        {
            var controller = base.GetControllerInstance(requestContext, controllerType);
            var controllerResult = controller as System.Web.Mvc.Controller;
            if (controllerResult != null)
                controllerResult.ActionInvoker = new FiltersAsyncControllerActionInvoker();
            return controller;
        }
    }
}
