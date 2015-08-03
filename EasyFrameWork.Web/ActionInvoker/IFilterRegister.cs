using System;
using System.Linq.Expressions;
using System.Web.Mvc;
using Easy.IOC;

namespace Easy.Web.ActionInvoker
{
    public interface IFilterRegister : IDependency, ISingleInstance
    {
        void Register<TController>(Expression<Action<TController>> action, params Type[] filterTypes)
            where TController : System.Web.Mvc.Controller;
        FilterInfo GetMatched(ControllerContext controllerContext, ActionDescriptor actionDescriptor);
    }
}