using Easy.StartTask;

namespace MvcApplication.Tasks
{

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