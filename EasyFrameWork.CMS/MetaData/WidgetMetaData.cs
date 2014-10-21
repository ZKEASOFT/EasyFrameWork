using Easy.CMS.Widget;
using Easy.MetaData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Easy.CMS.MetaData
{
    public abstract class WidgetMetaData<T> : DataViewMetaData<T> where T : WidgetBase
    {
        protected override bool IsIgnoreBase()
        {
            return true;
        }

        private void InitViewBase()
        {
            ViewConfig(m => m.WidgetName).AsTextBox().Order(1).Required();
            ViewConfig(m => m.ZoneID).AsDropDownList().Order(2).DataSource(ViewDataKeys.Zones, Constant.SourceType.ViewData).Required();
            ViewConfig(m => m.Position).AsTextBox().Order(3).RegularExpression(Constant.RegularExpression.Integer);
        }

        protected override void DataConfigure()
        {
            DataTable(TargetType.Name);
            DataConfig(m => m.ID).AsPrimaryKey();
        }

        protected override void ViewConfigure()
        {
            InitViewBase();
        }
    }
}
