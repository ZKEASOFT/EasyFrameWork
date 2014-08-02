using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Easy.CMS.WidgetTemplate
{
    public class WidgetTemplateViewModel
    {
        public string PageID { get; set; }
        public List<WidgetTemplateEntity> WidgetTemplates { get; set; }
    }
}
