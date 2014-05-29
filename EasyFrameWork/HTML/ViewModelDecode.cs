using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Easy.Attribute;
using Easy.Reflection;
using System.Web;
using Easy.HTML.Tags;

namespace Easy.HTML
{
    public class ViewModelDecode<T>
    {
        DataConfigureAttribute attribute;
        T entity;
        bool WithValue = false;
        Type EntityType;
        public ViewModelDecode()
        {
            EntityType = IOCAdapter.Loader.GetType<T>();
            attribute = DataConfigureAttribute.GetAttribute<T>();         
        }
        public ViewModelDecode(T Entity)
        {
            if (Entity != null)
            {
                entity = Entity;
                WithValue = true;
            }
            else
            {
                entity = IOCAdapter.Loader.CreateInstance<T>();
            }
            EntityType = IOCAdapter.Loader.GetType<T>();
            attribute = DataConfigureAttribute.GetAttribute<T>();
            if (attribute == null)
            {
                throw new Exception(EntityType.FullName + "未使用特性,请在其上使用[EasyFrameWork.Attribute.DataConfigureAttribute]特性！");
            }
        }
        /// <summary>
        /// 获取所有可显示属性标签
        /// </summary>
        /// <param name="widthLabel">是否显示Label</param>
        /// <returns></returns>
        public List<string> GetViewModelPropertyHtmlTag(bool widthLabel)
        {
            List<string> lists = new List<string>();
            foreach (var item in attribute.GetHtmlTags(false))
            {
                if (WithValue)
                {
                    object Val = ClassAction.GetPropertyValue<T>(entity, item.Name);
                    SetValue(item, Val);
                }
                lists.Add(item.ToString(widthLabel));
            }
            return lists;
        }
        public List<HtmlTagBase> GetViewModelPropertyHtmlTag()
        {
            List<HtmlTagBase> results = new List<HtmlTagBase>();
            foreach (var item in attribute.GetHtmlTags(false))
            {
                object val = ClassAction.GetPropertyValue<T>(entity, item.Name);
                this.SetValue(item, val);
                results.Add(item);
            }
            return results;
        }
        /// <summary>
        /// 获取所有隐藏控件
        /// </summary>
        /// <returns></returns>
        public List<string> GetViewModelHiddenTargets()
        {
            List<string> lists = new List<string>();
            foreach (var item in attribute.GetHtmlHiddenTags())
            {
                if (this.WithValue)
                {
                    object Val = ClassAction.GetPropertyValue(this.entity, item.Name);
                    SetValue(item, Val);
                }
                lists.Add(item.ToString(false));
            }
            return lists;
        }
        /// <summary>
        /// 获取对应属性的标签
        /// </summary>
        /// <param name="property"></param>
        /// <returns></returns>
        public string GetViewModelPropertyHtmlTag(string property)
        {
            object Val = ClassAction.GetPropertyValue(this.entity, property);
            var html = attribute.GetHtmlTag(property);
            SetValue(html, Val);
            return html.ToString();
        }
        /// <summary>
        /// 获取对应属性的显示名称
        /// </summary>
        /// <param name="property"></param>
        /// <returns></returns>
        public string GetPropertyDisplayName(string property)
        {
            return attribute.GetDisplayName(property);
        }
        void SetValue(HtmlTagBase tab, object val)
        {
            if (val != null)
            {
                if (tab.DataType.Name == "DateTime")
                {
                    DateTime time = DateTime.Now;
                    if (DateTime.TryParse(val.ToString(), out time))
                    {
                        if (tab is TextBoxHtmlTag)
                        {
                            tab.Value = time.ToString((tab as TextBoxHtmlTag).DateFormat);
                        }
                        else tab.Value = val;
                    }
                    else tab.Value = val;
                }
                else tab.Value = val;
            }
            else tab.Value = "";
        }
    }
}
