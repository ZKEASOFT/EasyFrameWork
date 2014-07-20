using Easy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Easy.Attribute;

namespace Easy.CMS.Page
{
    [DataConfigure(typeof(PageBaseMetaData))]
    public class PageEntity : EditorEntity
    {
        public string PageId { get; set; }
        public string PageName { get; set; }
        public string ParentId { get; set; }
        public string Url { get; set; }
        public string LayoutId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string MetaKeyWorlds { get; set; }
        public string MetaDescription { get; set; }
        public string IncludeScript { get; set; }
        public string IncludeStyle { get; set; }
    }
    public class PageBaseMetaData : DataViewMetaData<PageEntity>
    {
        protected override void DataConfigure()
        {
            DataTable("CMS_Page");
            DataConfig(m => m.PageId).AsPrimaryKey();
        }

        protected override void ViewConfigure()
        {
            
        }
    }

}
