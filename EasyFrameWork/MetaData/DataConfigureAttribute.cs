using Easy.Cache;
using Easy.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Easy.MetaData
{
    /// <summary>
    /// 数据和视图的配置特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class DataConfigureAttribute : Attribute
    {
        public IDataViewMetaData MetaData
        {
            get;
            private set;
        }
        public DataConfigureAttribute(Type metaDataType)
        {
            MetaData = Activator.CreateInstance(metaDataType) as IDataViewMetaData;
            //HTML标签的多语言
            if (Localization.IsMultiLanReady())
            {
                Dictionary<string, string> lan = MetaData.HtmlTags.ToDictionary(item => item.Key, item => item.Value.ModelType.Name + "@" + item.Key);
                lan = Localization.InitLan(lan);
                foreach (var item in lan)
                {
                    MetaData.HtmlTags[item.Key].DisplayName = item.Value;
                }
            }
        }
        /// <summary>
        /// 获取属性对表的映射
        /// </summary>
        /// <param name="property"></param>
        /// <returns></returns>
        public string GetPropertyMapper(string property)
        {
            if (this.MetaData.PropertyDataConfig.ContainsKey(property))
            {
                PropertyDataInfo config = MetaData.PropertyDataConfig[property];
                if (!string.IsNullOrEmpty(config.ColumnName))
                {
                    string alias = config.IsRelation ? config.TableAlias : MetaData.Alias;
                    return string.Format("[{0}].[{1}]", alias, config.ColumnName);
                }
                else
                {
                    string alias = config.IsRelation ? config.TableAlias : MetaData.Alias;
                    return string.Format("[{0}].[{1}]", alias, property);
                }
            }
            else return property;
        }
        /// <summary>
        /// 获取所有HTML标签
        /// </summary>
        /// <returns></returns>
        public List<HTML.Tags.HtmlTagBase> GetHtmlTags(bool widthHidden)
        {
            List<HTML.Tags.HtmlTagBase> returnValue = (from item in MetaData.HtmlTags where !item.Value.IsIgnore where (item.Value.TagType != HTML.HTMLEnumerate.HTMLTagTypes.Hidden && !item.Value.IsHidden) || widthHidden select item.Value).ToList();
            return returnValue.OrderBy(q => q.OrderIndex).ToList();
        }
        /// <summary>
        /// 获取HTML标签
        /// </summary>
        /// <param name="property"></param>
        /// <returns></returns>
        public HTML.Tags.HtmlTagBase GetHtmlTag(string property)
        {
            if (MetaData.HtmlTags.ContainsKey(property))
            {
                return MetaData.HtmlTags[property];
            }
            return new HTML.Tags.TextBoxHtmlTag(MetaData.TargetType, property);
        }
        public HTML.Tags.HtmlTagBase GetHtmlTag<T>(System.Linq.Expressions.Expression<Func<T, object>> expression)
        {
            string property = Reflection.LinqExpression.GetPropertyName(expression.Body);
            return GetHtmlTag(property);
        }
        /// <summary>
        /// 获取显示名称
        /// </summary>
        /// <param name="property"></param>
        /// <returns></returns>
        public string GetDisplayName(string property)
        {
            if (MetaData.HtmlTags.ContainsKey(property))
            {
                return MetaData.HtmlTags[property].DisplayName;
            }
            else return property;
        }
        /// <summary>
        /// 获取隐藏域标签
        /// </summary>
        /// <returns></returns>
        public List<HTML.Tags.HtmlTagBase> GetHtmlHiddenTags()
        {
            List<HTML.Tags.HtmlTagBase> returnValue = (from item in MetaData.HtmlTags where !item.Value.IsIgnore where item.Value.TagType == HTML.HTMLEnumerate.HTMLTagTypes.Hidden || item.Value.IsHidden select item.Value).ToList();
            return returnValue.OrderBy(q => q.OrderIndex).ToList();
        }
        /// <summary>
        /// 获取数据配置的特性
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static DataConfigureAttribute GetAttribute<T>()
        {
            Type targetType = typeof(T);
            StaticCache cache = new StaticCache();
            string typeName = targetType.FullName;
            var attribute = cache.Get("DataConfigureAttribute_" + typeName, m => GetCustomAttribute(targetType, typeof(DataConfigureAttribute)) as DataConfigureAttribute);
            return attribute;
        }
        public static DataConfigureAttribute GetAttribute(Type type)
        {
            StaticCache cache = new StaticCache();
            string typeName = type.FullName;
            var attribute = cache.Get("DataConfigureAttribute_" + typeName, m => GetCustomAttribute(type, typeof(DataConfigureAttribute)) as DataConfigureAttribute);
            return attribute;
        }
    }
}
