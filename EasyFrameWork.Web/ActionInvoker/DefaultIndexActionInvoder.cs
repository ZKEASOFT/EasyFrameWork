using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Mvc.Async;

namespace Easy.Web.ActionInvoker
{

    public class DefaultIndexActionInvoder : AsyncControllerActionInvoker
    {
        protected override ActionDescriptor FindAction(ControllerContext controllerContext, ControllerDescriptor controllerDescriptor, string actionName)
        {
            ActionDescriptor descriptor = base.FindAction(controllerContext, controllerDescriptor, actionName);
            if (descriptor == null)
            {
                actionName = (controllerContext.RouteData.Route as System.Web.Routing.Route).Defaults["action"].ToString();
                var keys = (controllerContext.RouteData.Route as System.Web.Routing.Route).Defaults.Keys;
                List<object> values = new List<object>();
                foreach (var item in controllerContext.RouteData.Values)
                {
                    values.Add(item.Value);
                }
                controllerContext.RouteData.Values.Clear();
                values.Insert(1, actionName);
                int index = 0;
                foreach (var item in keys)
                {
                    if (index < values.Count)
                    {
                        controllerContext.RouteData.Values.Add(item, values[index]);
                    }
                    index++;
                }

                descriptor = base.FindAction(controllerContext, controllerDescriptor, actionName);
            }
            return descriptor;
        }
    }
}
