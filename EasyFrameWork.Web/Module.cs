using Easy.IOCAdapter;
using Easy.Web.ControllerFactory;
using Easy.Web.ViewEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace Easy.Web
{
    public class Module
    {
        public static void Load(Easy.Web.Constant.Enums.ControllerFactoryType controllerFactory)
        {
            Container.Register(typeof(IApplicationContext), typeof(ApplicationContext));
            ModelValidatorProviders.Providers.Clear();
            ModelValidatorProviders.Providers.Add(new Easy.Web.ValidatorProvider.EasyModelValidatorProvider());
            ModelMetadataProviders.Current = new Easy.Web.MetadataProvider.EasyModelMetaDataProvider();
            ModelBinderProviders.BinderProviders.Add(new Easy.Web.ModelBinder.EasyBinderProvider());
            //ViewEngines.Engines.Add(new PlugViewEngine());
            switch (controllerFactory)
            {
                case Easy.Web.Constant.Enums.ControllerFactoryType.None:
                    break;
                case Easy.Web.Constant.Enums.ControllerFactoryType.DefaultIndexControllerFactory:
                    ControllerBuilder.Current.SetControllerFactory(typeof(DefaultIndexControllerFactory));
                    break;
                default:
                    break;
            }
        }
    }
}
