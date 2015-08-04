using System.Web.Mvc;
using Easy.Web.DependencyResolver;
using Easy.Web.MetadataProvider;
using Autofac;
using Easy.Extend;
using Easy.Web.ControllerActivator;
using Easy.Web.ValidatorProvider;
using System;
using Easy.IOC;
using Easy.IOC.Autofac;
using Easy.IOC.Unity;
using Easy.Web.ControllerFactory;
using Easy.Web.Filter;

namespace Easy.Web.Application
{
    public abstract class AutofacMvcApplication : TaskApplication
    {
        private static ILifetimeScopeProvider _lifetimeScopeProvider;

        public override void Init()
        {
            base.Init();
            BeginRequest += AutofacMvcApplication_BeginRequest;
            EndRequest += AutofacMvcApplication_EndRequest;
        }

        void AutofacMvcApplication_BeginRequest(object sender, EventArgs e)
        {
            _lifetimeScopeProvider.BeginLifetimeScope();
        }

        void AutofacMvcApplication_EndRequest(object sender, EventArgs e)
        {
            _lifetimeScopeProvider.EndLifetimeScope();
        }

        public ContainerBuilder AutofacContainerBuilder { get; private set; }

        private IContainerAdapter _containerAdapter;
        public override IContainerAdapter ContainerAdapter
        {
            get { return _containerAdapter ?? (_containerAdapter = new AutofacContainerAdapter(AutofacContainerBuilder)); }
        }


        protected void Application_Start()
        {
            ModelValidatorProviders.Providers.Clear();
            ModelValidatorProviders.Providers.Add(new EasyModelValidatorProvider());
            ModelMetadataProviders.Current = new EasyModelMetaDataProvider();
            //ModelBinderProviders.BinderProviders.Add(new EasyBinderProvider());

            AutofacContainerBuilder = new ContainerBuilder();
            AutofacContainerBuilder.RegisterType<FilterControllerFactory>().As<IControllerFactory>();
            AutofacContainerBuilder.RegisterType<EasyControllerActivator>().As<IControllerActivator>();
            AutofacContainerBuilder.RegisterType<HttpItemsValueProvider>().As<IHttpItemsValueProvider>().SingleInstance();
            AutofacContainerBuilder.RegisterType<ApplicationContext>().As<IApplicationContext>().InstancePerLifetimeScope();

            AutofacContainerBuilder.RegisterType<RequestLifetimeScopeProvider>().As<ILifetimeScopeProvider>().SingleInstance();
            //AutofacContainerBuilder.RegisterType<DataDictionaryService>().As<IDataDictionaryService>();
            //AutofacContainerBuilder.RegisterType<LanguageService>().As<ILanguageService>().SingleInstance();

            
            var controllerType = typeof(System.Web.Mvc.Controller);
            var moduleType = typeof(IModule);
            PublicTypes.Each(t =>
            {
                if (!t.IsInterface && !t.IsAbstract && t.IsPublic && !t.IsGenericType)
                {
                    if (controllerType.IsAssignableFrom(t))
                    {//register controller
                        AutofacContainerBuilder.RegisterType(t);
                    }
                    if (moduleType.IsAssignableFrom(t))
                    {
                        ((IModule)Activator.CreateInstance(t)).Load(new AutofacContainerAdapter(AutofacContainerBuilder));
                    }
                }

            });

            System.Web.Mvc.DependencyResolver.SetResolver(new EasyDependencyResolver());

            Application_Starting();

            _lifetimeScopeProvider = new AutofacRegister(AutofacContainerBuilder).Regist(AutofacContainerBuilder.Build());

            TaskManager.ExcuteAll();
            
            Application_Started();
        }
    }
}