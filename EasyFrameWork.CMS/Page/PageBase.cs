using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Easy.CMS.Page
{
    public class PageBase
    {
        public long Id { get; set; }
        public string PageName { get; set; }
        public string Parent { get; set; }
        public string Url { get; set; }
        public int LayoutId { get; set; }
        public string Title { get; set; }
        public string MetaKeyWorlds { get; set; }
        public string MetaDescription { get; set; }
        public string BackGroundImage { get; set; }
        public string IncludeScript { get; set; }
        public string IncludeStyle { get; set; }
    }
}
