using Autofac;

namespace Easy.IOC.Autofac
{
    public interface ILifetimeScopeProvider
    {
        ILifetimeScope LifetimeScope { get; }
        ILifetimeScope BeginLifetimeScope();
        void EndLifetimeScope(); 
    }
}