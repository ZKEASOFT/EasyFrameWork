using Easy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Easy.Attribute;

namespace Easy.Modules.File
{
    [DataConfigure(typeof(FileEntityMetaData))]
    public class FileEntity : BasicEntity
    {
        public int RelationType { get; set; }
        public long RelationID { get; set; }
        public int TypeCD { get; set; }
        public string Url { get; set; }

        public string Target { get; set; }
    }

    class FileEntityMetaData : DataViewMetaData<FileEntity>
    {
        public override void DataConfigure()
        {
            DataTable("Files");
            DataPrimarykey("ID");
            DataConfig(m => m.ID).Update(false).Insert(false);
        }

        public override void ViewConfigure()
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
