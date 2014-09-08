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
                item.ParentId = "#";
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

        public void Publish(string pageID)
        {
            this.Update(new PageEntity { IsPublish = true, PublishDate = DateTime.Now },
                new Data.DataFilter(new List<string> { "IsPublish", "PublishDate" })
                .Where("ID", Constant.OperatorType.Equal, pageID));

        }
        public void Move(string id, int position, int oldPosition)
        {
            var page = this.Get(id);
            page.DisplayOrder = position;
            var filter = new Data.DataFilter()
                .Where("ParentId", Constant.OperatorType.Equal, page.ParentId)
                .Where("Id", Constant.OperatorType.NotEqual, page.ID);
            if (position > oldPosition)
            {
                filter.Where("DisplayOrder", Constant.OperatorType.LessThanOrEqualTo, position);
                filter.Where("DisplayOrder", Constant.OperatorType.GreaterThanOrEqualTo, oldPosition);
                var pages = this.Get(filter);
                pages.Each(m =>
                {
                    m.DisplayOrder--;
                    this.Update(m);
                });
            }
            else
            {
                filter.Where("DisplayOrder", Constant.OperatorType.LessThanOrEqualTo, oldPosition);
                filter.Where("DisplayOrder", Constant.OperatorType.GreaterThanOrEqualTo, position);
                var pages = this.Get(filter);
                pages.Each(m =>
                {
                    m.DisplayOrder++;
                    this.Update(m);
                });
            }
            this.Update(page);
        }
    }
}
