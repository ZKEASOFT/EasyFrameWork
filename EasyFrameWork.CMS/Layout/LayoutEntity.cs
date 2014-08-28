using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Easy.MetaData;
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
        public string ID { get; set; }

        public string LayoutName { get; set; }
        public string ContainerClass { get; set; }
        public string Script { get; set; }
        public string Style { get; set; }
        public ZoneCollection Zones { get; set; }
        public ZoneWidgetCollection ZoneWidgets { get; set; }
        public LayoutHtmlCollection Html { get; set; }

        public PageEntity Page { get; set; }
    }

    class LayoutEntityMetaData : DataViewMetaData<LayoutEntity>
    {
        protected override void DataConfigure()
        {
            DataTable("CMS_Layout");
            DataConfig(m => m.ID).AsPrimaryKey();
        }

        protected override void ViewConfigure()
        {
            ViewConfig(m => m.ID).AsHidden();
            ViewConfig(m => m.ContainerClass).AsHidden();
            ViewConfig(m => m.Title).AsHidden();
            ViewConfig(m => m.LayoutName).AsTextBox().Required();
        }
    }

}
