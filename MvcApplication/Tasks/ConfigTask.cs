/* http://www.zkea.net/ Copyright 2016 ZKEASOFT http://www.zkea.net/licenses */
using System.Web.Mvc;
using System.Web.Routing;
using Easy.StartTask;

namespace MvcApplication.Tasks
{
    public class ConfigTask : IStartTask
    {
        public void Excute()
        {
            AreaRegistration.RegisterAllAreas();

            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            AuthConfig.RegisterAuth();
        }
    }
}