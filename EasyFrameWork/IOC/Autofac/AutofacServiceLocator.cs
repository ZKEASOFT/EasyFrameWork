using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Autofac;
using Microsoft.Practices.ServiceLocation;

namespace Easy.IOC.Autofac
{
    public class AutofacServiceLocator : ServiceLocatorImplBase
    {
        readonly IContainer _container;

        public AutofacServiceLocator(IContainer container)
        {
            if (container == null)
                throw new ArgumentNullException("container");
            _container = container;
            LifetimeScopeProvider = new RequestLifetimeScopeProvider(container);
        }
        public ILifetimeScopeProvider LifetimeScopeProvider { get; private set; }

        protected override object DoGetInstance(Type serviceType, string key)
        {
            try
            {
                if (_container.IsRegistered(serviceType))
                {
                    var scope = LifetimeScopeProvider.GetLifetimeScope();
                    return key != null ? scope.ResolveNamed(key, serviceType) : scope.Resolve(serviceType);
                }
                return null;
            }
            catch
            {
                return null;
            }
        }

        protected override IEnumerable<object> DoGetAllInstances(Type serviceType)
        {
            try
            {
                if (_container.IsRegistered(serviceType))
                {
                    var scope = LifetimeScopeProvider.GetLifetimeScope();
                    var enumerableType = typeof(IEnumerable<>).MakeGenericType(serviceType);
                    object instance = scope.Resolve(enumerableType);
                    return ((IEnumerable)instance).Cast<object>();
                }
                return new List<object>();
            }
            catch
            {
                return new List<object>();
            }
        }
    }
}