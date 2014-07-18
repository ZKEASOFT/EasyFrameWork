using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Easy.Attribute;
using System.Collections.ObjectModel;
using Easy.Models;

namespace Easy.CMS.Zone
{
    [DataConfigure(typeof(ZoneEntityMetaData))]
    public class ZoneEntity : EditorEntity
    {
        public const string ZoneTag = "<zone>";
        public const string ZoneEndTag = "</zone>";
        public string ZoneId { get; set; }
        public string LayoutId { get; set; }
        public string ZoneName { get; set; }
    }
    public class ZoneCollection : Collection<ZoneEntity>
    {

    }
    public class ZoneEntityMetaData : DataViewMetaData<ZoneEntity>
    {
        public override void DataConfigure()
        {
            DataTable("CMS_Zone");
            DataConfig(m => m.ZoneId).AsPrimaryKey();
        }

        public override void ViewConfigure()
        {

        }
    }

}
