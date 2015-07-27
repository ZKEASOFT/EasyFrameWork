using System.Web;
using Easy.StartTask;

namespace Easy.Web.Application
{
    public abstract class TaskApplication : HttpApplication
    {
        private readonly TaskManager _taskManager = new TaskManager();
        public TaskManager TaskManager
        {
            get { return _taskManager; }
        }
        public abstract void Application_StartUp();
    }
}