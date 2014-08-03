using Easy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Easy.Attribute;

namespace Easy.CMS.Page
{
    [DataConfigure(typeof(PageBaseMetaData))]
    public class PageEntity : EditorEntity, IBasicEntity<string>
    {
        public string ParentId { get; set; }
        public string Url { get; set; }
        public string LayoutId { get; set; }
        public string PageName { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string MetaKeyWorlds { get; set; }
        public string MetaDescription { get; set; }
        public string Script { get; set; }
        public string Style { get; set; }

        public string ID { get; set; }

        public string Description { get; set; }

        public int Status { get; set; }
    }
    public class PageBaseMetaData : DataViewMetaData<PageEntity>
    {
        protected override void DataConfigure()
        {
            DataTable("CMS_Page");
            DataConfig(m => m.ID).AsPrimaryKey();
        }

        protected override void ViewConfigure()
        {
            ViewConfig(m => m.PageName).AsTextBox().Order(1);
            ViewConfig(m => m.Status).AsDropDownList().DataSource(Constant.DicKeys.RecordStatus, Constant.SourceType.Dictionary);
            ViewConfig(m => m.LayoutId).AsDropDownList().DataSource(new Layout.LayoutService().Get().ToDictionary(m => m.ID, m => m.LayoutName));
            ViewConfig(m => m.ParentId).AsHidden();
            ViewConfig(m => m.ID).AsHidden();
            ViewConfig(m => m.Content).AsHidden();
        }
    }

}
