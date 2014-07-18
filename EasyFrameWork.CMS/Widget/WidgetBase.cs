using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Easy.CMS.Widget
{
    public class WidgetBase
    {
        public string WidgetId { get; set; }
        public string Name { get; set; }
        public int Position { get; set; }
        public string PageId { get; set; }

        public string LayouId { get; set; }

        public string AssemblyName { get; set; }
        public string FullTypeName { get; set; }
    }
}
