using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Easy.MetaData;
using Easy.CMS.MetaData;

namespace Easy.CMS.Widget
{
    [DataConfigure(typeof(HtmlWidgetMetaData))]
    public class HtmlWidget : WidgetBase
    {
        public string HTML { get; set; }
    }
    class HtmlWidgetMetaData : WidgetMetaData<HtmlWidget>
    {
       
        protected override void DataConfigure()
        {
            DataTable("HtmlWidget");
            DataConfig(m => m.ID).AsPrimaryKey();

        }

        protected override void ViewConfigure()
        {
            InitViewBase();
            ViewConfig(m => m.HTML).AsMutiLineTextBox().AddClass("html");
        }
    }

}
