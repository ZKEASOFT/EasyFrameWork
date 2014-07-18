using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Easy.RepositoryPattern;
using System.ComponentModel;

namespace Easy.CMS.Widget
{
    public abstract class WidgetPartDriver : ServiceBase<WidgetBase>
    {
        public override void Add(WidgetBase item)
        {
            Type thisType = this.GetType();
            item.AssemblyName = thisType.Assembly.GetName().Name;
            item.FullTypeName = thisType.FullName;
            base.Add(item);
        }
        public abstract WidgetPart Display(WidgetBase wedget, System.Web.HttpContextBase httpContext);
    }
    
}
