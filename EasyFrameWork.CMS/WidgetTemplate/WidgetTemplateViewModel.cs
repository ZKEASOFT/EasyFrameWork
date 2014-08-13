using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Easy.CMS.WidgetTemplate
{
    public class WidgetTemplateViewModel
    {
        public string PageId { get; set; }
        public string LayoutId { get; set; }
        public List<WidgetTemplateEntity> WidgetTemplates { get; set; }
    }
}
