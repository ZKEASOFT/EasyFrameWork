using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Autofac;
using Easy.IOC.Autofac;
using Microsoft.Practices.ServiceLocation;
using Easy.Extend;
using Easy.Models;
using Easy.Reflection;

namespace Easy.IOC
{
    public sealed class AutofacRegister : AssemblyInfo
    {
        readonly Type _dependencyType = typeof(IDependency);
        readonly Type _entityType = typeof(IEntity);

        public AutofacRegister(ContainerBuilder builder)
        {
            PublicTypes.Each(p =>
            {
                if (p.IsClass && !p.IsAbstract && !p.IsInterface && !p.IsGenericType)
                {
                    if (_dependencyType.IsAssignableFrom(p) ||
                        _entityType.IsAssignableFrom(p))
                    {
                        if (_entityType.IsAssignableFrom(p))
                        {
                            builder.RegisterType(p);
                        }
                        else
                        {
                            builder.RegisterType(p).As(p.GetInterfaces());
                        }
                    }

                }
            });
        }
        public ILifetimeScopeProvider Regist(IContainer container)
        {
            var locator = new AutofacServiceLocator(container);

            ServiceLocator.SetLocatorProvider(() => locator);
            return locator.LifetimeScopeProvider;
        }
    }
}