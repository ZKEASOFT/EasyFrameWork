using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Compilation;
using System.Web.Mvc;
using Easy.Modules.DataDictionary;
using Easy.Modules.MutiLanguage;
using Easy.Web.DependencyResolver;
using Easy.Web.MetadataProvider;
using Autofac;
using Easy.Extend;
using Easy.Web.ControllerActivator;
using Easy.Web.ValidatorProvider;

namespace Easy.Web.Application
{
    public abstract class AutofacMvcApplication : TaskApplication
    {
        public ContainerBuilder AutofacContainerBuilder { get; private set; }

        protected void Application_Start()
        {
            ModelValidatorProviders.Providers.Clear();
            ModelValidatorProviders.Providers.Add(new EasyModelValidatorProvider());
            ModelMetadataProviders.Current = new EasyModelMetaDataProvider();
            //ModelBinderProviders.BinderProviders.Add(new EasyBinderProvider());

            AutofacContainerBuilder = new ContainerBuilder();
            AutofacContainerBuilder.RegisterType<EasyControllerActivator>().As<IControllerActivator>();
            AutofacContainerBuilder.RegisterType<ApplicationContext>().As<IApplicationContext>();
            AutofacContainerBuilder.RegisterType<DataDictionaryService>().As<IDataDictionaryService>();
            AutofacContainerBuilder.RegisterType<LanguageService>().As<ILanguageService>();

            //register controller
            var controllerType = typeof(System.Web.Mvc.Controller);
            BuildManager.GetReferencedAssemblies().Cast<Assembly>().Each(m => m.GetTypes().Each(t =>
            {
                if (controllerType.IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract && t.IsPublic && !t.IsGenericType)
                {
                    AutofacContainerBuilder.RegisterType(t);
                }
            }));

            System.Web.Mvc.DependencyResolver.SetResolver(new EasyDependencyResolver());

            Application_StartUp();
            new IOC.AutofacRegister(AutofacContainerBuilder).Regist(AutofacContainerBuilder.Build());
            TaskManager.ExcuteAll();
        }
    }
}