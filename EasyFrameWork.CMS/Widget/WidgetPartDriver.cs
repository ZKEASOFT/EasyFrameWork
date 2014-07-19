using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Easy.RepositoryPattern;
using System.ComponentModel;

namespace Easy.CMS.Widget
{
    public interface IWidgetPartDriver
    {
        WidgetBase GetWidget(string widgetId);
        WidgetPart Display(WidgetBase widget, System.Web.HttpContextBase httpContext);
    }

}
