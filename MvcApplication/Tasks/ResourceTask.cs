using Easy.StartTask;

namespace MvcApplication.Tasks
{

    public class ResourceTask : IStartTask
    {
        public void Excute()
        {
            new ResourceManager().Excute();

        }
    }
}