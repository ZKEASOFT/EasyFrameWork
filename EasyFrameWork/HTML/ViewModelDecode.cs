using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Easy.MetaData;
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
            EntityType = Loader.GetType<T>();
            attribute = DataConfigureAttribute.GetAttribute<T>();
        }
        public ViewModelDecode(T Entity)
        {
            if (Entity != null)
            {
                entity = Entity;
                WithValue = true;
                EntityType = entity.GetType();
                attribute = DataConfigureAttribute.GetAttribute(EntityType);
            }
            else
            {
                entity = Loader.CreateInstance<T>();
                EntityType = Loader.GetType<T>();
                attribute = DataConfigureAttribute.GetAttribute<T>();
            }
            if (attribute == null)
            {
                throw new Exception(EntityType.FullName + "未使用特性,请在其上使用[EasyFrameWork.Attribute.DataConfigureAttribute]特性！");
            }
        }
        public IDictionary<string, object> ExtendPropertyValue { get; set; }
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
                    if (item is DropDownListHtmlTag)
                    {
                        DropDownListHtmlTag tag = item as DropDownListHtmlTag;
                        if (tag.SourceType == Constant.SourceType.ViewData &&
                           ExtendPropertyValue.ContainsKey(tag.SourceKey))
                        {
                            if (ExtendPropertyValue[tag.SourceKey] is Dictionary<string, string>)
                            {
                                tag.DataSource(ExtendPropertyValue[tag.SourceKey] as Dictionary<string, string>);
                            }
                        }
                    }
                    object Val = ClassAction.GetObjPropertyValue(entity, item.Name);
                    item.SetValue(Val);
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
                object val = ClassAction.GetObjPropertyValue(entity, item.Name);
                item.SetValue(val);
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
                    object Val = ClassAction.GetObjPropertyValue(this.entity, item.Name);
                    item.SetValue(Val);
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
            object Val = ClassAction.GetObjPropertyValue(this.entity, property);
            var html = attribute.GetHtmlTag(property);
            html.SetValue(Val);
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
        
    }
}
