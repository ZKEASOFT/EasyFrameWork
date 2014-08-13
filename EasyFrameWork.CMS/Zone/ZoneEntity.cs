using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Easy.MetaData;
using System.Collections.ObjectModel;
using Easy.Models;

namespace Easy.CMS.Zone
{
    [DataConfigure(typeof(ZoneEntityMetaData))]
    public class ZoneEntity : EditorEntity,IBasicEntity<string>
    {
        public const string ZoneTag = "<zone>";
        public const string ZoneEndTag = "</zone>";
        public string ID { get; set; }
        public string LayoutId { get; set; }
        public string ZoneName { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public int Status { get; set; }
    }
  
    public class ZoneEntityMetaData : DataViewMetaData<ZoneEntity>
    {
        protected override void DataConfigure()
        {
            DataTable("CMS_Zone");
            DataConfig(m => m.ID).AsPrimaryKey();
        }

        protected override void ViewConfigure()
        {

        }
    }

}
