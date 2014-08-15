using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Easy.RepositoryPattern;
using Easy.Extend;

namespace Easy.CMS.Page
{
    public class PageService : ServiceBase<PageEntity>
    {
        public override void Add(PageEntity item)
        {
            if (item.ID.IsNullOrEmpty())
            {
                item.ID = Guid.NewGuid().ToString("N");
            }
            if (item.ParentId.IsNullOrEmpty())
            {
                item.ParentId = "0";
            }
            base.Add(item);
        }
        public override int Delete(Data.DataFilter filter)
        {
            var deletes = this.Get(filter).ToList(m => m.ID);
            if (deletes.Any() && this.Get(new Data.DataFilter().Where("ParentId", Constant.OperatorType.In, deletes)).Any())
            {
                Widget.WidgetService widgetService = new Widget.WidgetService();
                var widgets = widgetService.Get(new Data.DataFilter().Where("PageID", Constant.OperatorType.In, deletes));
                widgets.Each(m =>
                {
                    m.CreateServiceInstance().DeleteWidget(m.ID);
                });
                this.Delete(new Data.DataFilter().Where("ParentId", Constant.OperatorType.In, deletes));
            }
            return base.Delete(filter);
        }
        public override int Delete(params object[] primaryKeys)
        {
            PageEntity page = Get(primaryKeys);
            this.Delete(new Data.DataFilter().Where("ParentId", Constant.OperatorType.Equal, page.ID));

            Widget.WidgetService widgetService = new Widget.WidgetService();
            var widgets = widgetService.Get(new Data.DataFilter().Where("PageID", Constant.OperatorType.Equal, page.ID));
            widgets.Each(m =>
            {
                m.CreateServiceInstance().DeleteWidget(m.ID);
            });
            return base.Delete(primaryKeys);
        }
    }
}
