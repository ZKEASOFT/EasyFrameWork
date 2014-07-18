using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Easy.RepositoryPattern;
using Easy.Extend;
using Easy.CMS.Zone;

namespace Easy.CMS.Layout
{
    public class LayoutService : ServiceBase<LayoutEntity>
    {
        public override void Add(LayoutEntity item)
        {
            item.LayoutId = Guid.NewGuid().ToString("N");
            base.Add(item);
            if (item.Zones != null)
            {
                ZoneService zoneService = new ZoneService();
                item.Zones.Each(m =>
                {
                    m.LayoutId = item.LayoutId;
                    zoneService.Add(m);
                });
            }
            if (item.Html != null)
            {
                LayoutHtmlService layoutHtmlService = new LayoutHtmlService();
                item.Html.Each(m =>
                {
                    m.LayoutId = item.LayoutId;
                    layoutHtmlService.Add(m);
                });
            }
        }
        public override bool Update(LayoutEntity item, params object[] primaryKeys)
        {
            bool updated = base.Update(item, primaryKeys);
            if (item.Zones != null)
            {
                ZoneService zoneService = new ZoneService();
                zoneService.Delete(new Data.DataFilter().Where<ZoneEntity>(m => m.LayoutId, Constant.DataEnumerate.OperatorType.Equal, item.LayoutId));
                item.Zones.Each(m =>
                {
                    m.LayoutId = item.LayoutId;
                    zoneService.Add(m);

                });
            }
            if (item.Html != null)
            {
                LayoutHtmlService layoutHtmlService = new LayoutHtmlService();
                layoutHtmlService.Delete(new Data.DataFilter().Where<LayoutHtml>(m => m.LayoutId, Constant.DataEnumerate.OperatorType.Equal, item.LayoutId));
                item.Html.Each(m =>
                {
                    m.LayoutId = item.LayoutId;
                    layoutHtmlService.Add(m);
                });
            }
            return updated;
        }
        public override LayoutEntity Get(params object[] primaryKeys)
        {
            LayoutEntity entity = base.Get(primaryKeys);
            if (entity == null)
                return null;
            List<ZoneEntity> zones = new ZoneService().Get(new Data.DataFilter().Where<ZoneEntity>(m => m.LayoutId, Constant.DataEnumerate.OperatorType.Equal, entity.LayoutId));
            entity.Zones = new ZoneCollection();
            zones.Each(entity.Zones.Add);
            List<LayoutHtml> htmls = new LayoutHtmlService().Get(new Data.DataFilter().Where<LayoutHtml>(m => m.LayoutId, Constant.DataEnumerate.OperatorType.Equal, entity.LayoutId));
            entity.Html = new LayoutHtmlCollection();
            htmls.Each(entity.Html.Add);
            return entity;
        }
    }
}
