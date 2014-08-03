using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Easy.RepositoryPattern;
using System.Web;

namespace Easy.CMS.Widget
{
    public class WidgetService : ServiceBase<WidgetBase>
    {

    }
    public abstract class WidgetService<T> : ServiceBase<T>, IWidgetPartDriver where T : WidgetBase
    {
        public WidgetService()
        {
            WidgetBaseService = new WidgetService();
        }
        public WidgetService WidgetBaseService { get; private set; }
        public override void Add(T item)
        {
            item.ID = Guid.NewGuid().ToString("N");
            WidgetBaseService.Add(item.ToWidgetBase());
            base.Add(item);
        }
        public override bool Update(T item, params object[] primaryKeys)
        {
            WidgetBaseService.Update(item.ToWidgetBase(), primaryKeys);
            return base.Update(item, primaryKeys);
        }
        public override bool Update(T item, Data.DataFilter filter)
        {
            WidgetBaseService.Update(item.ToWidgetBase(), filter);
            return base.Update(item, filter);
        }
        public override List<T> Get(Data.DataFilter filter)
        {
            List<WidgetBase> widgetBases = WidgetBaseService.Get(filter);
            List<T> lists = base.Get(filter);
            for (int i = 0; i < widgetBases.Count; i++)
            {
                Easy.Reflection.ClassAction.CopyProperty(widgetBases[i], lists[i]);
            }
            return lists;
        }
        public override List<T> Get(Data.DataFilter filter, Data.Pagination pagin)
        {
            List<WidgetBase> widgetBases = WidgetBaseService.Get(filter, pagin);
            List<T> lists = base.Get(filter, pagin);
            for (int i = 0; i < widgetBases.Count; i++)
            {
                Easy.Reflection.ClassAction.CopyProperty(widgetBases[i], lists[i]);
            }
            return lists;
        }
        public override int Delete(Data.DataFilter filter)
        {
            WidgetBaseService.Delete(filter);
            return base.Delete(filter);
        }
        public override int Delete(params object[] primaryKeys)
        {
            WidgetBaseService.Delete(primaryKeys);
            return base.Delete(primaryKeys);
        }
        public override T Get(params object[] primaryKeys)
        {
            T model = base.Get(primaryKeys);
            Easy.Reflection.ClassAction.CopyProperty(WidgetBaseService.Get(primaryKeys), model);
            return model;
        }
        public virtual WidgetBase GetWidget(string widgetId)
        {
            return this.Get(widgetId);
        }

        public virtual WidgetPart Display(WidgetBase widget, HttpContextBase httpContext)
        {
            return widget.ToWidgetPart();
        }

        public virtual void AddWidget(WidgetBase widget)
        {
            this.Add((T)widget);
        }


        public virtual void DeleteWidget(string widgetId)
        {
            this.Delete(widgetId);
        }

        public virtual void UpdateWidget(WidgetBase widget)
        {
            this.Update((T)widget);
        }
    }
}
