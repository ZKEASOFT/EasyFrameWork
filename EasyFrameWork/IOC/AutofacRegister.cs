using System;
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
        public AutofacRegister(ContainerBuilder builder)
        {
            PublicTypes.Each(p =>
            {
                if (p.IsClass && !p.IsAbstract && !p.IsInterface && !p.IsGenericType)
                {
                    if ((KnownTypes.DependencyType.IsAssignableFrom(p) ||
                        KnownTypes.EntityType.IsAssignableFrom(p)) && !KnownTypes.FreeDependencyType.IsAssignableFrom(p))
                    {
                        if (KnownTypes.EntityType.IsAssignableFrom(p))
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