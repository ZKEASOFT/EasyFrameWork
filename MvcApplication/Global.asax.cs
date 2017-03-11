/* http://www.zkea.net/ Copyright 2016 ZKEASOFT http://www.zkea.net/licenses */
using Easy;
using Easy.Extend;
using Easy.IOC;
using Easy.Modules.MutiLanguage;
using Easy.Modules.User.Service;
using Easy.Web;
using Easy.Web.Application;
using Easy.Web.ViewEngine;
using MvcApplication.Tasks;
using System;
using System.Linq;
using System.Reflection;
using System.Web.Compilation;
using System.Web.Mvc;
using System.Web.WebPages;

namespace MvcApplication
{

    public class MvcApplication : UnityMvcApplication
    {
        public override void Application_Starting()
        {
            
            ContainerAdapter.RegisterType<IApplicationContext, ApplicationContext>();

            TaskManager
                .Include<ConfigTask>()
                .Include<ResourceTask>();

            Type WebPageType = typeof(WebPageBase);
            
            PrecompliedViewEngine.Regist(BuildManager.GetReferencedAssemblies().Cast<Assembly>().SelectMany(assembly => assembly.GetTypes()));
        }
    }
    public class WebModule : IModule
    {
        public void Load(IContainerAdapter adapter)
        {

        }
    }

}