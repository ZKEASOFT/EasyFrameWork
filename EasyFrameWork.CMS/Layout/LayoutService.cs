using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Easy.RepositoryPattern;
using Easy.Extend;
using Easy.CMS.Zone;
using Easy.Constant;
using Easy.CMS.Widget;

namespace Easy.CMS.Layout
{
    public class LayoutService : ServiceBase<LayoutEntity>
    {
        public const string LayoutChanged = "LayoutChanged";
        public override void Add(LayoutEntity item)
        {
            item.ID = Guid.NewGuid().ToString("N");
            base.Add(item);
            if (item.Zones != null)
            {
                ZoneService zoneService = new ZoneService();
                item.Zones.Each(m =>
                {
                    m.LayoutId = item.ID;
                    zoneService.Add(m);
                });
            }
            if (item.Html != null)
            {
                LayoutHtmlService layoutHtmlService = new LayoutHtmlService();
                item.Html.Each(m =>
                {
                    m.LayoutId = item.ID;
                    layoutHtmlService.Add(m);
                });
            }
        }

        public void UpdateDesign(LayoutEntity item)
        {
            this.Update(item, new Data.DataFilter(new List<string> { "ContainerClass" }).Where("ID", OperatorType.Equal, item.ID));
            if (item.Zones != null)
            {
                ZoneService zoneService = new ZoneService();
                var zones = zoneService.Get(new Data.DataFilter().Where<ZoneEntity>(m => m.LayoutId, OperatorType.Equal, item.ID));

                item.Zones.Where(m => !zones.Any(n => n.ID == m.ID)).Each(m =>
                {
                    m.LayoutId = item.ID;
                    zoneService.Add(m);
                });
                item.Zones.Where(m => zones.Any(n => n.ID == m.ID)).Each(m =>
                {
                    m.LayoutId = item.ID;
                    zoneService.Update(m);
                });
                zones.Where(m => !item.Zones.Any(n => n.ID == m.ID)).Each(m =>
                {
                    zoneService.Delete(m.ID);
                });
            }
            if (item.Html != null)
            {
                LayoutHtmlService layoutHtmlService = new LayoutHtmlService();
                layoutHtmlService.Delete(new Data.DataFilter().Where<LayoutHtml>(m => m.LayoutId, OperatorType.Equal, item.ID));
                item.Html.Each(m =>
                {
                    m.LayoutId = item.ID;
                    layoutHtmlService.Add(m);
                });
            }

        }
        public override bool Update(LayoutEntity item, Data.DataFilter filter)
        {
            new Cache.Signal().Do(LayoutChanged);
            return base.Update(item, filter);
        }
        public override bool Update(LayoutEntity item, params object[] primaryKeys)
        {
            new Cache.Signal().Do(LayoutChanged);
            return base.Update(item, primaryKeys);
        }
        public override LayoutEntity Get(params object[] primaryKeys)
        {
            Cache.StaticCache cache = new Cache.StaticCache();
            return cache.Get("Layout_" + primaryKeys[0], m =>
            {
                m.When(LayoutChanged);
                LayoutEntity entity = base.Get(primaryKeys);
                if (entity == null)
                    return null;
                IEnumerable<ZoneEntity> zones = new ZoneService().Get(new Data.DataFilter().Where("LayoutId", OperatorType.Equal, entity.ID));
                entity.Zones = new ZoneCollection();
                zones.Each(entity.Zones.Add);
                IEnumerable<LayoutHtml> htmls = new LayoutHtmlService().Get(new Data.DataFilter().OrderBy("LayoutHtmlId", OrderType.Ascending).Where("LayoutId", OperatorType.Equal, entity.ID));
                entity.Html = new LayoutHtmlCollection();
                htmls.Each(entity.Html.Add);
                return entity;
            });

        }
        public override int Delete(Data.DataFilter filter)
        {
            var deletes = this.Get(filter).ToList(m => m.ID);
            if (deletes.Any())
            {
                LayoutHtmlService layoutHtmlService = new LayoutHtmlService();
                layoutHtmlService.Delete(new Data.DataFilter().Where<LayoutHtml>(m => m.LayoutId, OperatorType.In, deletes));

                ZoneService zoneService = new ZoneService();
                zoneService.Delete(new Data.DataFilter().Where<ZoneEntity>(m => m.LayoutId, OperatorType.In, deletes));


                Page.PageService pageService = new Page.PageService();
                pageService.Delete(new Data.DataFilter().Where("LayoutId", OperatorType.In, deletes));

                Widget.WidgetService widgetService = new Widget.WidgetService();
                var widgets = widgetService.Get(new Data.DataFilter().Where("LayoutId", Constant.OperatorType.In, deletes));
                widgets.Each(m =>
                {
                    m.CreateServiceInstance().DeleteWidget(m.ID);
                });

            }
            new Cache.Signal().Do(LayoutChanged);
            return base.Delete(filter);
        }
        public override int Delete(params object[] primaryKeys)
        {
            LayoutEntity layout = Get(primaryKeys);
            LayoutHtmlService layoutHtmlService = new LayoutHtmlService();
            layoutHtmlService.Delete(new Data.DataFilter().Where<LayoutHtml>(m => m.LayoutId, OperatorType.Equal, layout.ID));

            ZoneService zoneService = new ZoneService();
            zoneService.Delete(new Data.DataFilter().Where<ZoneEntity>(m => m.LayoutId, OperatorType.Equal, layout.ID));


            Page.PageService pageService = new Page.PageService();
            pageService.Delete(new Data.DataFilter().Where("LayoutId", OperatorType.Equal, layout.ID));

            Widget.WidgetService widgetService = new Widget.WidgetService();
            var widgets = widgetService.Get(new Data.DataFilter().Where("LayoutId", Constant.OperatorType.Equal, layout.ID));
            widgets.Each(m =>
            {
                m.CreateServiceInstance().DeleteWidget(m.ID);
            });
            new Cache.Signal().Do(LayoutChanged);
            return base.Delete(primaryKeys);
        }
    }
}
