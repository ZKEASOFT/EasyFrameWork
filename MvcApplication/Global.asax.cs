using Easy.IOC;
using Easy.Web.Application;
using MvcApplication.Tasks;

namespace MvcApplication
{

    public class MvcApplication : AutofacMvcApplication
    {
        public override void Application_StartUp()
        {
            TaskManager
                .Include<ConfigTask>()
                .Include<ResourceTask>();
        }
    }

    public class WebModule : IModule
    {
        public void Load(IContainerAdapter adapter)
        {
            
        }
    }

}