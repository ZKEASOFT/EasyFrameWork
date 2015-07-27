using Easy.Web.Application;
using MvcApplication.Tasks;

namespace MvcApplication
{

    public class MvcApplication : UnityMvcApplication
    {
        public override void Application_StartUp()
        {
            TaskManager
                .Include<ConfigTask>()
                .Include<ResourceTask>();
        }
    }
    

}