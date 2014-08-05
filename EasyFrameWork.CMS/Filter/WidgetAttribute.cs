using Easy.CMS.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Easy;
using Easy.CMS.Page;
using Easy.CMS.Layout;
using Easy.Constant;
using Easy.Extend;

namespace Easy.CMS.Filter
{
    public class WidgetAttribute : FilterAttribute, IActionFilter
    {

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var zones = new ZoneWidgetCollection();

            //Page
            string path = filterContext.RequestContext.HttpContext.Request.Path;
            var pageService = new PageService();
            IEnumerable<PageEntity> pages = pageService.Get(new Data.DataFilter().Where<PageEntity>(m => m.Url, OperatorType.Equal, path));
            if (pages.Any())
            {
                PageEntity page = pages.First();
                var layoutService = new LayoutService();
                LayoutEntity layout = layoutService.Get(page.LayoutId);
                layout.Page = page;
                var widgetService = new WidgetService();
                IEnumerable<WidgetBase> widgets = widgetService.Get(new Data.DataFilter().Where<WidgetBase>(m => m.PageId, OperatorType.Equal, page.ID));

                widgets.Each(m =>
                {
                    var partDriver = Loader.CreateInstance<IWidgetPartDriver>(m.AssemblyName, m.ServiceTypeName);
                    WidgetPart part = partDriver.Display(partDriver.GetWidget(m.ID), filterContext.HttpContext);
                    if (zones.ContainsKey(part.ZoneId))
                    {
                        zones[part.ZoneId].Add(part);
                    }
                    else
                    {
                        var partCollection = new WidgetCollection { part };
                        zones.Add(part.ZoneId, partCollection);
                    }
                });

                layout.ZoneWidgets = zones;
                var viewResult = (filterContext.Result as ViewResult);
                if (viewResult != null)
                {
                    //viewResult.MasterName = "~/Modules/Easy.CMS.Page/Views/Shared/_Layout.cshtml";
                    viewResult.ViewData[LayoutEntity.LayoutKey] = layout;
                }
            }
            else
            {
                filterContext.Result = new HttpStatusCodeResult(404);
            }
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {

        }
    }

}
