using System;
using System.Web;
using System.Web.Mvc;
using Easy.Modules.DataDictionary;
using Easy.Modules.MutiLanguage;
using Easy.Web.ControllerActivator;
using Easy.Web.DependencyResolver;
using Easy.Web.MetadataProvider;
using Easy.Web.ValidatorProvider;
using Microsoft.Practices.Unity;
using Easy.Extend;
using Easy.IOC;
using Easy.IOC.Unity;

namespace Easy.Web.Application
{
    public abstract class UnityMvcApplication : TaskApplication
    {
        public override void Init()
        {
            base.Init();
        }

        public IUnityContainer Container { get; private set; }

        private IContainerAdapter _containerAdapter;

        public override IContainerAdapter ContainerAdapter
        {
            get { return _containerAdapter ?? (_containerAdapter = new UnityContainerAdapter(Container)); }
        }

        protected void Application_Start()
        {
            ModelValidatorProviders.Providers.Clear();
            ModelValidatorProviders.Providers.Add(new EasyModelValidatorProvider());
            ModelMetadataProviders.Current = new EasyModelMetaDataProvider();
            //ModelBinderProviders.BinderProviders.Add(new EasyBinderProvider());

            Container = new UnityContainer();
            Container.RegisterType<IControllerActivator, EasyControllerActivator>();
            Container.RegisterType<IHttpItemsValueProvider, HttpItemsValueProvider>(new ContainerControlledLifetimeManager());
            Container.RegisterType<IApplicationContext, ApplicationContext>(new PerRequestLifetimeManager());
            //Container.RegisterType<IDataDictionaryService, DataDictionaryService>();
            //Container.RegisterType<ILanguageService, LanguageService>(new ContainerControlledLifetimeManager());
            var moduleType = typeof(IModule);
            PublicTypes.Each(t =>
            {
                if (!t.IsInterface && !t.IsAbstract && t.IsPublic && !t.IsGenericType)
                {
                    if (moduleType.IsAssignableFrom(t))
                    {
                        ((IModule)Activator.CreateInstance(t)).Load(new UnityContainerAdapter(Container));
                    }
                }

            });

            System.Web.Mvc.DependencyResolver.SetResolver(new EasyDependencyResolver());

            Application_StartUp();

            new UnityRegister(Container).Regist();
            TaskManager.ExcuteAll();
        }
    }
}