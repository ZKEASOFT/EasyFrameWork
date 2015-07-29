using Autofac;

namespace Easy.IOC.Autofac
{
    public interface ILifetimeScopeProvider
    {
        ILifetimeScope BeginLifetimeScope();
        ILifetimeScope GetLifetimeScope();

        void EndLifetimeScope(); 
    }
}