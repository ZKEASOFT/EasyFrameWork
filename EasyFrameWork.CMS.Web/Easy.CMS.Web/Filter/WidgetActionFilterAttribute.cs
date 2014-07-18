using Easy.CMS.Web.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace Easy.CMS.Web.Filter
{
    public class WidgetActionFilterAttribute : FilterAttribute, IActionFilter
    {

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            WidgetZoneCollection parts = null;
            if (filterContext.Controller.ViewData.ContainsKey(WidgetPart.WidgetPartKey))
            {
                parts = filterContext.Controller.ViewData[WidgetPart.WidgetPartKey] as WidgetZoneCollection;
            }
            else
            {
                parts = new WidgetZoneCollection();
            }
            WidgetCollection col = new WidgetCollection();
            col.Add(new WidgetPart() { ViewModel = "ddd", PartialView = "~/Modules/Easy.CMS.Page/Views/Partial_T1.cshtml" });
            parts.Add("zone1", col);
            string path = filterContext.RequestContext.HttpContext.Request.Path;

            filterContext.Controller.ViewData[WidgetPart.WidgetPartKey] = parts;

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
