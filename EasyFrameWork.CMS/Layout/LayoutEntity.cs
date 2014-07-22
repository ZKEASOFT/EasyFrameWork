using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Easy.Attribute;
using Easy.CMS.Zone;
using Easy.Models;
using Easy.CMS.Page;
using Easy.CMS.Widget;

namespace Easy.CMS.Layout
{
    [DataConfigure(typeof(LayoutEntityMetaData))]
    public class LayoutEntity : EditorEntity
    {
        public const string LayoutKey = "ViewDataKey_Layout";
        public string LayoutId { get; set; }
        public string LayoutName { get; set; }
        public string ContainerClass { get; set; }
        public string StylePath { get; set; }
        public string IncludeScript { get; set; }
        public string IncludeStyle { get; set; }
        public ZoneCollection Zones { get; set; }
        public ZoneWidgetCollection ZoneWidgets { get; set; }
        public LayoutHtmlCollection Html { get; set; }

        public PageEntity Page { get; set; }
    }

    public class LayoutEntityMetaData : DataViewMetaData<LayoutEntity>
    {
        protected override void DataConfigure()
        {
            DataTable("CMS_Layout");
            DataConfig(m => m.LayoutId).AsPrimaryKey();
        }

        protected override void ViewConfigure()
        {

        }
    }

}
