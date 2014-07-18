using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Easy.CMS.Widget
{
    public class WidgetPart
    {
        public const string WidgetPartKey = "ViewDataKey_WidgetPart";
        public string ZoneId { get; set; }
        public string Name { get; set; }
        public int Position { get; set; }
        public string PartialView { get; set; }
        public object ViewModel { get; set; }

    }
}
