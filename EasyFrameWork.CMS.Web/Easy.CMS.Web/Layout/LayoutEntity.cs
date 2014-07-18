using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Easy.Attribute;
using Easy.CMS.Web.Zone;
using Easy.Models;

namespace Easy.CMS.Web.Layout
{
    [DataConfigure(typeof(LayoutEntityMetaData))]
    public class LayoutEntity : EditorEntity
    {
        public string LayoutId { get; set; }
        public string LayoutName { get; set; }
        public string StylePath { get; set; }
        public ZoneCollection Zones { get; set; }
        public LayoutHtmlCollection Html { get; set; }
    }

    public class LayoutEntityMetaData : DataViewMetaData<LayoutEntity>
    {
        public override void DataConfigure()
        {
            DataTable("CMS_Layout");
            DataConfig(m => m.LayoutId).AsPrimaryKey();
        }

        public override void ViewConfigure()
        {

        }
    }

}
