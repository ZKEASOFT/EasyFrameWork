using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace Easy.CMS.ModelBinder
{
    public class WidgetBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            DefaultModelBinder binder = new DefaultModelBinder();
            object model = Easy.Reflection.ClassAction.GetModel(typeof(Widget.WidgetBase), controllerContext.RequestContext.HttpContext.Request.Form);
            bindingContext.ModelMetadata = ModelMetadataProviders.Current.GetMetadataForType(() => (model as Widget.WidgetBase).CreateViewModelInstance(), (model as Widget.WidgetBase).GetViewModelType());
            model = binder.BindModel(controllerContext, bindingContext);
            return model;
        }
    }
}
