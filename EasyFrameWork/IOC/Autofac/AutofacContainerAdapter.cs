using System;
using Autofac;

namespace Easy.IOC.Autofac
{
    public class AutofacContainerAdapter : IContainerAdapter
    {
        private readonly ContainerBuilder _builder;
        public AutofacContainerAdapter(ContainerBuilder builder)
        {
            _builder = builder;
        }
        public IContainerAdapter RegisterType(Type itype, Type type)
        {
            return RegisterType(itype, type, DependencyLifeTime.PerDependency);

        }

        public IContainerAdapter RegisterType<TIt, T>() where T : TIt
        {
            return RegisterType<TIt, T>(DependencyLifeTime.PerDependency);
        }


        public IContainerAdapter RegisterType(Type itype, Type type, DependencyLifeTime lifeTime)
        {
            var reg = _builder.RegisterType(type).As(itype);
            switch (lifeTime)
            {
                case DependencyLifeTime.PerDependency:
                    {
                        reg.InstancePerDependency();
                        break;
                    }
                case DependencyLifeTime.PerRequest:
                    {
                        reg.InstancePerLifetimeScope();
                        break;
                    }
                case DependencyLifeTime.SingleInstance:
                    {
                        reg.SingleInstance();
                        break;
                    }
            }
            return this;
        }

        public IContainerAdapter RegisterType<TIt, T>(DependencyLifeTime lifeTime) where T : TIt
        {
            var reg = _builder.RegisterType<T>().As<TIt>();
            switch (lifeTime)
            {
                case DependencyLifeTime.PerDependency:
                    {
                        reg.InstancePerDependency();
                        break;
                    }
                case DependencyLifeTime.PerRequest:
                    {
                        reg.InstancePerRequest();
                        break;
                    }
                case DependencyLifeTime.SingleInstance:
                    {
                        reg.SingleInstance();
                        break;
                    }
            }
            return this;
        }
    }
}