using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Easy.CMS.Web.Widget
{
    public class WidgetPart
    {
        public const string WidgetPartKey = "ViewDataKey_WidgetPart";
        public int PageId { get; set; }
        public string Zone { get; set; }
        public string Name { get; set; }
        public int Position { get; set; }
        public string PartialView { get; set; }
        public object ViewModel { get; set; }

    }
}
