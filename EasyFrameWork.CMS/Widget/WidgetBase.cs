using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Easy.Attribute;
using Easy.Models;
using Easy.RepositoryPattern;

namespace Easy.CMS.Widget
{
    [DataConfigure(typeof(WidgetBaseMetaData))]
    public class WidgetBase : EditorEntity, IBasicEntity<string>
    {
        public string ID { get; set; }
        public string WidgetName { get; set; }
        public string Title { get; set; }
        public int Position { get; set; }
        public string LayouId { get; set; }
        public string PageId { get; set; }
        public string ZoneId { get; set; }
        public string PartialView { get; set; }
        public string AssemblyName { get; set; }
        public string ServiceTypeName { get; set; }
        public string ViewModelTypeName { get; set; }
        public WidgetPart ToWidgetPart()
        {
            return new WidgetPart
            {
                PartialView = PartialView,
                Position = Position,
                ViewModel = this,
                WidgetId = ID,
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
                WidgetId = ID,
                ViewModel = viewModel,
                WidgetName = WidgetName,
                ZoneId = ZoneId
            };
        }


        public string Description { get; set; }

        public int Status { get; set; }

        public IService CreateServiceInstance()
        {
            return Loader.CreateInstance<IService>(this.AssemblyName, this.ServiceTypeName);
        }
        public Type GetViewModelType()
        {
            return Loader.GetType(this.ViewModelTypeName);
        }
    }
    public class WidgetBaseMetaData : DataViewMetaData<WidgetBase>
    {
        protected override void DataConfigure()
        {
            DataTable("CMS_WidgetBase");
            DataConfig(m => m.ID).AsPrimaryKey();
        }

        protected override void ViewConfigure()
        {

        }
    }


}
