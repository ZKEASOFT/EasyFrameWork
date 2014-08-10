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
            if (typeof(T) != typeof(WidgetBase))
            {
                base.Add(item);
            }
        }
        public override bool Update(T item, params object[] primaryKeys)
        {
            bool result = WidgetBaseService.Update(item.ToWidgetBase(), primaryKeys);
            if (typeof(T) != typeof(WidgetBase))
            {
                return base.Update(item, primaryKeys);
            }
            return result;
        }
        public override bool Update(T item, Data.DataFilter filter)
        {
            bool result = WidgetBaseService.Update(item.ToWidgetBase(), filter);
            if (typeof(T) != typeof(WidgetBase))
            {
                return base.Update(item, filter);
            }
            return result;
        }
        public override IEnumerable<T> Get(Data.DataFilter filter)
        {
            List<WidgetBase> widgetBases = WidgetBaseService.Get(filter).ToList();

            List<T> lists = new List<T>();
            if (typeof(T) != typeof(WidgetBase))
            {
                lists = base.Get(filter).ToList();
                for (int i = 0; i < widgetBases.Count; i++)
                {
                    Easy.Reflection.ClassAction.CopyProperty(widgetBases[i], lists[i]);
                }
            }
            else
            {
                widgetBases.ForEach(m => lists.Add(m as T));
            }
            return lists;

        }
        public override IEnumerable<T> Get(Data.DataFilter filter, Data.Pagination pagin)
        {
            List<WidgetBase> widgetBases = WidgetBaseService.Get(filter, pagin).ToList();
            List<T> lists = new List<T>();
            if (typeof(T) != typeof(WidgetBase))
            {
                lists = base.Get(filter, pagin).ToList();
                for (int i = 0; i < widgetBases.Count(); i++)
                {
                    Easy.Reflection.ClassAction.CopyProperty(widgetBases[i], lists[i]);
                }
            }
            else
            {
                widgetBases.ForEach(m => lists.Add(m as T));
            }
            return lists;
        }
        public override int Delete(Data.DataFilter filter)
        {
            int result = WidgetBaseService.Delete(filter);
            if (typeof(T) != typeof(WidgetBase))
            {
                return base.Delete(filter);
            }
            return result;
        }
        public override int Delete(params object[] primaryKeys)
        {
            int result = WidgetBaseService.Delete(primaryKeys);
            if (typeof(T) != typeof(WidgetBase))
            {
                return base.Delete(primaryKeys);
            }
            return result;
        }
        public override T Get(params object[] primaryKeys)
        {
            T model = base.Get(primaryKeys);
            if (typeof(T) != typeof(WidgetBase))
            {
                Easy.Reflection.ClassAction.CopyProperty(WidgetBaseService.Get(primaryKeys), model);
            }
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
