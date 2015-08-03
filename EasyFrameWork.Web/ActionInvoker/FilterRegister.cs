using System;
using System.Linq.Expressions;
using System.Web.Mvc;

namespace Easy.Web.ActionInvoker
{
    public class FilterRegister : IFilterRegister
    {
        public void Register<TController>(Expression<Action<TController>> action, params Type[] filterTypes) where TController : System.Web.Mvc.Controller
        {
            var methodCall = action.Body as MethodCallExpression;
            
            throw new NotImplementedException();
        }

        public FilterInfo GetMatched(ControllerContext controllerContext, ActionDescriptor actionDescriptor)
        {
            throw new NotImplementedException();
        }
    }
}