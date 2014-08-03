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
        public List<ZoneEntity> GetZones(string pageId)
        {
            var page = new Easy.CMS.Page.PageService().Get(pageId);
            var layout = new Easy.CMS.Layout.LayoutService().Get(page.LayoutId);
            var zones = new Easy.CMS.Zone.ZoneService().Get(new Data.DataFilter().Where("LayoutId", Constant.OperatorType.Equal, layout.ID));
            return zones;
        }
    }
}
