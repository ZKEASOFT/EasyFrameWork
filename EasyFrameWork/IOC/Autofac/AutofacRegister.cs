using System;
using Autofac;
using Microsoft.Practices.ServiceLocation;
using Easy.Extend;
using Easy.Reflection;
using Autofac.Builder;

namespace Easy.IOC.Autofac
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
                        MakeLifeTime(
                            KnownTypes.EntityType.IsAssignableFrom(p)
                                ? builder.RegisterType(p).AsSelf()
                                : builder.RegisterType(p).As(p.GetInterfaces()), p);

                        if (p.BaseType != null && p.BaseType.IsAbstract)
                        {
                            RegistBaseType(builder, p, p.BaseType);
                        }

                    }

                }
            });
        }
        private void RegistBaseType(ContainerBuilder builder, Type type, Type baseType)
        {
            if (type != KnownTypes.ObjectType && baseType != null)
            {
                MakeLifeTime(builder.RegisterType(type).As(baseType), type);
                RegistBaseType(builder, type, baseType.BaseType);
            }
        }
        public ILifetimeScopeProvider Regist(IContainer container)
        {
            var locator = new AutofacServiceLocator(container);

            ServiceLocator.SetLocatorProvider(() => locator);
            return locator.LifetimeScopeProvider;
        }

        private void MakeLifeTime<TLimit, TActivatorData, TRegistrationStyle>(
            IRegistrationBuilder<TLimit, TActivatorData, TRegistrationStyle> reg, Type lifeTimeType)
        {
            if (KnownTypes.SingleInstanceType.IsAssignableFrom(lifeTimeType))
            {
                reg.SingleInstance();
            }
            else if (KnownTypes.PerRequestType.IsAssignableFrom(lifeTimeType))
            {
                reg.InstancePerLifetimeScope();
            }
            else
            {
                reg.InstancePerDependency();
            }
        }
    }
}