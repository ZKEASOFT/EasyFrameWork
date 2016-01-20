using Easy;
using Easy.IOC;
using Easy.Modules.MutiLanguage;
using Easy.Modules.User.Service;
using Easy.Web;
using Easy.Web.Application;
using MvcApplication.Tasks;

namespace MvcApplication
{

    public class MvcApplication : UnityMvcApplication
    {
        public override void Application_Starting()
        {

            ContainerAdapter.RegisterType<IApplicationContext, ApplicationContext>(DependencyLifeTime.PerRequest);

            ContainerAdapter.RegisterType<IUserService, UserService>();
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