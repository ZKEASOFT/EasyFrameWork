using Easy.Web.ActionInvoker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Async;

namespace Easy.Web.ControllerFactory
{
    /// <summary>
    ///Global-->APP_Start ControllerBuilder.Current.SetControllerFactory(typeof(ExtControllerFactory));
    /// </summary>
    public class DefaultIndexControllerFactory : DefaultControllerFactory
    {
        protected override IController GetControllerInstance(System.Web.Routing.RequestContext requestContext, Type controllerType)
        {
            IController controller = base.GetControllerInstance(requestContext, controllerType);
            if (controller is Controller)
            {
                (controller as Controller).ActionInvoker = new DefaultIndexActionInvoder();
            }
            return controller;
        }
    }
}