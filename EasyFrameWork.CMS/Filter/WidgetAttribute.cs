using Easy.Data;
using Easy.Web.CMS.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Easy;
using Easy.Web.CMS.Page;
using Easy.Web.CMS.Layout;
using Easy.Constant;
using Easy.Extend;

namespace Easy.Web.CMS.Filter
{
    public class WidgetAttribute : FilterAttribute, IActionFilter
    {

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var zones = new ZoneWidgetCollection();

            //Page
            string path = filterContext.RequestContext.HttpContext.Request.Path;
            if (path != "/" && path.EndsWith("/"))
            {
                path = path.Substring(0, path.Length - 1);
            }
            var pageService = new PageService();

            var filter = new Data.DataFilter().Where("Url", OperatorType.Equal, "~" + path);
            if (filterContext.RequestContext.HttpContext.Request.QueryString[ReView.QueryKey] != ReView.Review)
            {
                filter.Where("Status", OperatorType.Equal, (int)RecordStatus.Active).Where("IsPublish=true");
            }
            IEnumerable<PageEntity> pages = pageService.Get(filter);
            if (!pages.Any() && path == "/")
            {
                var homePage = pageService.Get(new DataFilter().Where("ParentId", OperatorType.Equal, "#").Where("HomePage=true"));
                if (homePage.Any())
                {
                    filterContext.Result = new RedirectResult(homePage.First().Url);
                    return;
                }
            }
            if (pages.Any())
            {
                PageEntity page = pages.First();
                var layoutService = new LayoutService();
                LayoutEntity layout = layoutService.Get(page.LayoutId);
                layout.Page = page;
                var widgetService = new WidgetService();
                IEnumerable<WidgetBase> widgets = widgetService.Get(new Data.DataFilter().Where("PageID", OperatorType.Equal, page.ID));

                widgets.Each(m =>
                {
                    var partDriver = Loader.CreateInstance<IWidgetPartDriver>(m.AssemblyName, m.ServiceTypeName);
                    WidgetPart part = partDriver.Display(partDriver.GetWidget(m), filterContext.HttpContext);
                    if (zones.ContainsKey(part.Widget.ZoneID))
                    {
                        zones[part.Widget.ZoneID].Add(part);
                    }
                    else
                    {
                        var partCollection = new WidgetCollection { part };
                        zones.Add(part.Widget.ZoneID, partCollection);
                    }
                });

                IEnumerable<WidgetBase> Layoutwidgets = widgetService.Get(new Data.DataFilter().Where("LayoutID", OperatorType.Equal, page.LayoutId));

                Layoutwidgets.Each(m =>
                {
                    var partDriver = Loader.CreateInstance<IWidgetPartDriver>(m.AssemblyName, m.ServiceTypeName);
                    WidgetPart part = partDriver.Display(partDriver.GetWidget(m), filterContext.HttpContext);
                    if (zones.ContainsKey(part.Widget.ZoneID))
                    {
                        zones[part.Widget.ZoneID].Add(part);
                    }
                    else
                    {
                        var partCollection = new WidgetCollection { part };
                        zones.Add(part.Widget.ZoneID, partCollection);
                    }
                });
                layout.ZoneWidgets = zones;
                var viewResult = (filterContext.Result as ViewResult);
                if (viewResult != null)
                {
                    //viewResult.MasterName = "~/Modules/Easy.Web.CMS.Page/Views/Shared/_Layout.cshtml";
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
