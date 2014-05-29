using Easy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Easy.Attribute;

namespace Easy.Modules.Discuss
{
    [DataConfigure(typeof(DiscussEntityMetaData))]
    public class DiscussEntity : BasicEntity
    {
        public long ParentID { get; set; }
        public int RelationType { get; set; }
        public long RelationID { get; set; }
        public string Message { get; set; }
        public string ImageUrl { get; set; }

    }
    class DiscussEntityMetaData : DataViewMetaData<DiscussEntity>
    {
        public override void DataConfigure()
        {
            DataTable("Discuss");
            DataPrimarykey("ID");

            DataConfig(m => m.CreateBy).Update(false);
            DataConfig(m => m.CreatebyName).Update(false);
            DataConfig(m => m.CreateDate).Update(false);
        }

        public override void ViewConfigure()
        {
            
        }
    }

}
