using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Easy.CMS.Web.Widget
{
    public interface IWidgetPartDriver
    {
        WidgetPart Display(WidgetBase wedget, System.Web.HttpContextBase httpContext);
    }
}
