using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Easy.RepositoryPattern;
using Easy.Extend;
using Easy.CMS.Zone;
using Easy.Constant;

namespace Easy.CMS.Layout
{
    public class LayoutService : ServiceBase<LayoutEntity>
    {
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
                zoneService.Delete(new Data.DataFilter().Where<ZoneEntity>(m => m.LayoutId, OperatorType.Equal, item.ID));
                item.Zones.Each(m =>
                {
                    m.LayoutId = item.ID;
                    zoneService.Add(m);

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
        public override LayoutEntity Get(params object[] primaryKeys)
        {
            LayoutEntity entity = base.Get(primaryKeys);
            if (entity == null)
                return null;
            List<ZoneEntity> zones = new ZoneService().Get(new Data.DataFilter().Where<ZoneEntity>(m => m.LayoutId, OperatorType.Equal, entity.ID));
            entity.Zones = new ZoneCollection();
            zones.Each(entity.Zones.Add);
            List<LayoutHtml> htmls = new LayoutHtmlService().Get(new Data.DataFilter().Where<LayoutHtml>(m => m.LayoutId, OperatorType.Equal, entity.ID));
            entity.Html = new LayoutHtmlCollection();
            htmls.Each(entity.Html.Add);
            return entity;
        }
    }
}
