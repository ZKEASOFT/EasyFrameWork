using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Easy.CMS.Web.Widget
{
    public class WidgetBase
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public int Position { get; set; }
        public long PageId { get; set; }
    }
}
