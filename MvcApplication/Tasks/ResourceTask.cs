/* http://www.zkea.net/ Copyright 2016 ZKEASOFT http://www.zkea.net/licenses */
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