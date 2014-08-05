using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Easy.RepositoryPattern;
using Easy.Extend;

namespace Easy.CMS.Zone
{
    public class ZoneService : ServiceBase<ZoneEntity>
    {
        public override void Add(ZoneEntity item)
        {
            if (item.ID.IsNullOrEmpty())
            {
                item.ID = Guid.NewGuid().ToString("N");
            }
            base.Add(item);
        }
        public IEnumerable<ZoneEntity> GetZones(string pageId)
        {
            var page = new Easy.CMS.Page.PageService().Get(pageId);
            var layout = new Easy.CMS.Layout.LayoutService().Get(page.LayoutId);
            var zones = new Easy.CMS.Zone.ZoneService().Get(new Data.DataFilter().Where("LayoutId", Constant.OperatorType.Equal, layout.ID));
            return zones;
        }
        public override int Delete(Data.DataFilter filter)
        {
            var deletes = this.Get(filter).ToList(m => m.ID);
            if (deletes.Any())
            {
                Widget.WidgetService widgetService = new Widget.WidgetService();
                var widgets = widgetService.Get(new Data.DataFilter().Where("ZoneId", Constant.OperatorType.In, deletes));
                Loader.ResolveAll<Widget.IWidgetPartDriver>().Each(m =>
                {
                    widgets.Each(n => m.DeleteWidget(n.ID));
                });
            }
            return base.Delete(filter);
        }
        public override int Delete(params object[] primaryKeys)
        {
            Widget.WidgetService widgetService = new Widget.WidgetService();
            var widgets = widgetService.Get(new Data.DataFilter().Where("ZoneId", Constant.OperatorType.Equal, primaryKeys[0]));
            Loader.ResolveAll<Widget.IWidgetPartDriver>().Each(m =>
            {
                widgets.Each(n => m.DeleteWidget(n.ID));
            });
            return base.Delete(primaryKeys);
        }
    }
}
