using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Easy.RepositoryPattern;
using Easy.Extend;

namespace Easy.CMS.Web.Zone
{
    public class ZoneService : ServiceBase<ZoneEntity>
    {
        public override void Add(ZoneEntity item)
        {
            if (item.ZoneId.IsNullOrEmpty())
            {
                item.ZoneId = Guid.NewGuid().ToString("N");
            }
            base.Add(item);
        }
    }
}
