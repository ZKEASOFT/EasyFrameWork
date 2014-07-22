using Easy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Easy.Attribute;

namespace Easy.Modules.File
{
    [DataConfigure(typeof(FileEntityMetaData))]
    public class FileEntity : IBasicEntity<long>
    {
        public int RelationType { get; set; }
        public long RelationID { get; set; }
        public int TypeCD { get; set; }
        public string Url { get; set; }

        public string Target { get; set; }

        public long ID { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public bool IsPassed { get; set; }
    }

    class FileEntityMetaData : DataViewMetaData<FileEntity>
    {
        protected override void DataConfigure()
        {
            DataTable("Files");
            DataConfig(m => m.ID).AsIncreasePrimaryKey();
        }

        protected override void ViewConfigure()
        {
            ViewConfig(m => m.ID).AsHidden();
            ViewConfig(m => m.RelationType).AsHidden();
            ViewConfig(m => m.RelationID).AsHidden();
            ViewConfig(m => m.Url).AsTextBox().SetColumnWidth(200);

            
            ViewConfig(m => m.TypeCD).AsHidden();

            ViewConfig(m => m.IsPassed).AsHidden();
           
            ViewConfig(m => m.Target).AsHidden();
        }
    }

}
