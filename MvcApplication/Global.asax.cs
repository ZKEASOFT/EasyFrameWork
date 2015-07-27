using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using Easy.StartTask;
using Easy.Web.Application;
using Microsoft.Practices.Unity;
using MvcApplication.Controllers;

namespace MvcApplication
{
    // 注意: 有关启用 IIS6 或 IIS7 经典模式的说明，
    // 请访问 http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : UnityMvcApplication
    {
        public override void Application_StartUp()
        {
            TaskManager
                .Include<ConfigTask>()
                .Include<ResourceTask>();
        }
    }
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
    public class ResourceTask : IStartTask
    {
        public void Excute()
        {
            var mgr = new ResourceManager();
            mgr.InitScript();
            mgr.InitStyle();
        }
    }

}