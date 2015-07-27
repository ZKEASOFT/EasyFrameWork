using System.Web;
using System.Web.Mvc;
using Easy.Modules.DataDictionary;
using Easy.Modules.MutiLanguage;
using Easy.Web.ControllerActivator;
using Easy.Web.DependencyResolver;
using Easy.Web.MetadataProvider;
using Easy.Web.ValidatorProvider;
using Microsoft.Practices.Unity;

namespace Easy.Web.Application
{
    public abstract class UnityMvcApplication : TaskApplication
    {
        public IUnityContainer Container { get; private set; }
        protected void Application_Start()
        {
            ModelValidatorProviders.Providers.Clear();
            ModelValidatorProviders.Providers.Add(new EasyModelValidatorProvider());
            ModelMetadataProviders.Current = new EasyModelMetaDataProvider();
            //ModelBinderProviders.BinderProviders.Add(new EasyBinderProvider());

            Container = new UnityContainer();
            Container.RegisterType<IControllerActivator, EasyControllerActivator>();
            Container.RegisterType<IApplicationContext, ApplicationContext>();
            Container.RegisterType<IDataDictionaryService, DataDictionaryService>();
            Container.RegisterType<ILanguageService, LanguageService>();

            System.Web.Mvc.DependencyResolver.SetResolver(new EasyDependencyResolver());

            Application_StartUp();

            new IOC.UnityRegister(Container).Regist();
            TaskManager.ExcuteAll();
        }
    }
}