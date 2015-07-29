using System.Linq;
using System.Web.Mvc;
using Easy.Modules.DataDictionary;
using Easy.Modules.MutiLanguage;
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


        protected void Application_Start()
        {
            ModelValidatorProviders.Providers.Clear();
            ModelValidatorProviders.Providers.Add(new EasyModelValidatorProvider());
            ModelMetadataProviders.Current = new EasyModelMetaDataProvider();
            //ModelBinderProviders.BinderProviders.Add(new EasyBinderProvider());

            AutofacContainerBuilder = new ContainerBuilder();
            AutofacContainerBuilder.RegisterType<HttpItemsValueProvider>().As<IHttpItemsValueProvider>().SingleInstance();
            AutofacContainerBuilder.RegisterType<RequestLifetimeScopeProvider>().As<ILifetimeScopeProvider>().SingleInstance();
            AutofacContainerBuilder.RegisterType<EasyControllerActivator>().As<IControllerActivator>();
            AutofacContainerBuilder.RegisterType<ApplicationContext>().As<IApplicationContext>().InstancePerLifetimeScope();
            AutofacContainerBuilder.RegisterType<DataDictionaryService>().As<IDataDictionaryService>();
            AutofacContainerBuilder.RegisterType<LanguageService>().As<ILanguageService>();

            //register controller
            var controllerType = typeof(System.Web.Mvc.Controller);
            var moduleType = typeof(IModule);
            PublicTypes.Each(t =>
            {
                if (!t.IsInterface && !t.IsAbstract && t.IsPublic && !t.IsGenericType)
                {
                    if (controllerType.IsAssignableFrom(t))
                    {
                        AutofacContainerBuilder.RegisterType(t);
                    }
                    if (moduleType.IsAssignableFrom(t))
                    {
                        ((IModule)Activator.CreateInstance(t)).Load(new AutofacContainerAdapter(AutofacContainerBuilder));
                    }
                }

            });

            System.Web.Mvc.DependencyResolver.SetResolver(new EasyDependencyResolver());

            Application_StartUp();

            _lifetimeScopeProvider = new AutofacRegister(AutofacContainerBuilder).Regist(AutofacContainerBuilder.Build());

            TaskManager.ExcuteAll();
        }
    }
}