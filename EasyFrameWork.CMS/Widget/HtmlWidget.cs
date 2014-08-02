using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Easy.Attribute;

namespace Easy.CMS.Widget
{
    [DataConfigure(typeof(HtmlWidgetMetaData))]
    public class HtmlWidget : WidgetBase
    {
        public string HTML { get; set; }
    }
    class HtmlWidgetMetaData : DataViewMetaData<HtmlWidget>
    {
        protected override bool IsIgnoreBase()
        {
            return true;
        }
        protected override void DataConfigure()
        {
            DataTable("HtmlWidget");
            DataConfig(m => m.ID).AsPrimaryKey();

        }

        protected override void ViewConfigure()
        {
            ViewConfig(m => m.WidgetName).AsTextBox().Order(1);
            ViewConfig(m => m.ZoneId).AsDropDownList().DataSource(ViewDataKeys.Zones, Easy.Constant.SourceType.ViewData);
            ViewConfig(m => m.HTML).AsMutiLineTextBox();
        }
    }

}
