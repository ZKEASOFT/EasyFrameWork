using Easy.CMS.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Easy;

namespace Easy.CMS.Filter
{
    public class WidgetActionFilterAttribute : FilterAttribute, IActionFilter
    {

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            WidgetZoneCollection zones = null;
            if (filterContext.Controller.ViewData.ContainsKey(WidgetPart.WidgetPartKey))
            {
                zones = filterContext.Controller.ViewData[WidgetPart.WidgetPartKey] as WidgetZoneCollection;
            }
            else
            {
                zones = new WidgetZoneCollection();
            }
            //Page
            string path = filterContext.RequestContext.HttpContext.Request.Path;
            //Layout

            WidgetService widgetService = new WidgetService();
            List<WidgetBase> widgets = widgetService.Get(new Data.DataFilter().Where<WidgetBase>(m => m.PageId, Constant.DataEnumerate.OperatorType.Equal, ""));

            widgets.ForEach(m =>
            {
                WidgetPart part = Loader.CreateInstance<WidgetPartDriver>(m.AssemblyName, m.FullTypeName).Display(m, filterContext.HttpContext);
                if (zones.ContainsKey(part.ZoneId))
                {
                    zones[part.ZoneId].Add(part);
                }
                else
                {
                    WidgetCollection partCollection = new WidgetCollection();
                    partCollection.Add(part);
                    zones.Add(part.ZoneId, partCollection);
                }
            });





            filterContext.Controller.ViewData[WidgetPart.WidgetPartKey] = zones;
            ViewResult viewResult = (filterContext.Result as ViewResult);
            if (viewResult != null)
            {
                //viewResult.MasterName = "~/Modules/Easy.CMS.Page/Views/Shared/_Layout.cshtml";
            }
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {

        }
    }

}
