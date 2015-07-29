using System;
using Autofac;
using Easy.IOC.Unity;
using Microsoft.Practices.ServiceLocation;

namespace Easy.IOC.Autofac
{
    public class RequestLifetimeScopeProvider : ILifetimeScopeProvider
    {
        private readonly IContainer _container;
        private ILifetimeScope _lifetimeScope;

        public RequestLifetimeScopeProvider(IContainer container)
        {
            this._container = container;
        }

        #region ILifetimeScopeProvider Members

        public ILifetimeScope BeginLifetimeScope()
        {
            return _lifetimeScope ?? (_lifetimeScope = _container.BeginLifetimeScope());
        }

        public void EndLifetimeScope()
        {
            if (_lifetimeScope != null)
            {
                _lifetimeScope.Dispose();
                _lifetimeScope = null;
            }
        }

        #endregion


        public ILifetimeScope GetLifetimeScope()
        {
            return _lifetimeScope;
        }
    }
}