using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Easy.Models;
using Easy.Attribute;

namespace Easy.Modules.SEO
{
    [DataConfigure(typeof(SEOEntityMetaData))]
    public class SEOEntity : EditorEntity, IBasicEntity<string>, ISEOEntity
    {
        public string ID { get; set; }
        public string Url { get; set; }

        public string Title { get; set; }
        public string SEOTitle { get; set; }
        public string SEOKeyWords { get; set; }
        public string SEODescription { get; set; }

        public string Description { get; set; }

        public string SecondTitle { get; set; }
        public string SecondDescription { get; set; }

        public bool IsPassed { get; set; }
    }

    class SEOEntityMetaData : DataViewMetaData<SEOEntity>
    {
        public override void DataConfigure()
        {
            DataTable("SEO");
            DataConfig(m => m.ID).AsPrimaryKey();
        }

        public override void ViewConfigure()
        {
            ViewConfig(m => m.ID).AsTextBox().Required();
            ViewConfig(m => m.SEOTitle).AsTextBox().Required();
            ViewConfig(m => m.SEOKeyWords).AsTextBox().Required();
            ViewConfig(m => m.SEODescription).AsTextBox().Required();
        }
    }

}
