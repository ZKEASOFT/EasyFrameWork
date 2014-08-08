using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Easy.Extend;

namespace Easy.CMS.ModelBinder
{
    public class WidgetBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            DefaultModelBinder binder = new DefaultModelBinder();
            object model = binder.BindModel(controllerContext, bindingContext);
            var widgetBase = model as Widget.WidgetBase;
            if (!widgetBase.ViewModelTypeName.IsNullOrEmpty())
            {
                bindingContext.ModelMetadata = ModelMetadataProviders.Current.GetMetadataForType(() => widgetBase.CreateViewModelInstance(), widgetBase.GetViewModelType());
                model = binder.BindModel(controllerContext, bindingContext);
            }
            return model;
        }
    }
}
