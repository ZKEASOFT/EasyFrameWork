using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Easy.Attribute;
using Easy.Models;

namespace Easy.CMS.Widget
{
    [DataConfigure(typeof(WidgetBaseMetaData))]
    public class WidgetBase : EditorEntity
    {
        public string WidgetId { get; set; }
        public string WidgetName { get; set; }
        public int Position { get; set; }
        public string LayouId { get; set; }
        public string PageId { get; set; }
        public string ZoneId { get; set; }
        public string PartialView { get; set; }
        public string AssemblyName { get; set; }
        public string FullTypeName { get; set; }
        public WidgetPart ToWidgetPart()
        {
            return new WidgetPart
            {
                PartialView = PartialView,
                Position = Position,
                ViewModel = this,
                WidgetId = WidgetId,
                WidgetName = WidgetName,
                ZoneId = ZoneId
            };
        }
        public WidgetPart ToWidgetPart(object viewModel)
        {
            return new WidgetPart
            {
                PartialView = PartialView,
                Position = Position,
                WidgetId = WidgetId,
                ViewModel = viewModel,
                WidgetName = WidgetName,
                ZoneId = ZoneId
            };
        }
    }
    public class WidgetBaseMetaData : DataViewMetaData<WidgetBase>
    {
        protected override void DataConfigure()
        {
            DataTable("CMS_WidgetBase");
            DataConfig(m => m.WidgetId).AsPrimaryKey();
        }

        protected override void ViewConfigure()
        {

        }
    }


}
