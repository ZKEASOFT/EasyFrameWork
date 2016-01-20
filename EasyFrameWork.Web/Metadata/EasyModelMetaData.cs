using Easy.Data;
using Easy.HTML.Tags;
using Easy.MetaData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Easy.Web.Metadata
{
    public sealed class EasyModelMetaData : ModelMetadata
    {
        public EasyModelMetaData(ModelMetadataProvider provider, Type containerType, Func<object> modelAccessor, Type modelType, string propertyName)
            : base(provider, containerType, modelAccessor, modelType, propertyName)
        {
            if (containerType != null)
            {
                DataConfigureAttribute custAttribute = DataConfigureAttribute.GetAttribute(containerType);
                if (custAttribute != null)
                {
                    if (custAttribute.MetaData.HtmlTags.ContainsKey(propertyName))
                    {
                        HtmlTag = custAttribute.MetaData.HtmlTags[propertyName];
                        DisplayFormatString = HtmlTag.ValueFormat;

                        if (!string.IsNullOrEmpty(HtmlTag.DisplayName))
                        {
                            DisplayName = HtmlTag.DisplayName;
                        }
                        else
                        {
                            DisplayName = HtmlTag.Name;
                        }
                        EditFormatString = HtmlTag.ValueFormat;
                        IsReadOnly = HtmlTag.IsReadOnly;
                        IsRequired = HtmlTag.IsRequired;
                        Order = HtmlTag.OrderIndex;
                        ShowForDisplay = HtmlTag.IsShowForDisplay;
                        ShowForEdit = HtmlTag.IsShowForEdit;
                        TemplateHint = HtmlTag.TemplateName;
                        HideSurroundingHtml = HtmlTag.IsHidden;
                    }
                    if (custAttribute.MetaData.PropertyDataConfig.ContainsKey(propertyName))
                    {
                        PropertyData = custAttribute.MetaData.PropertyDataConfig[propertyName];
                    }

                }
            }
        }

        public HtmlTagBase HtmlTag { get; set; }
        public PropertyDataInfo PropertyData { get; set; }
    }
}
