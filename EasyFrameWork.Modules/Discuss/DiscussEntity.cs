using Easy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Easy.Attribute;

namespace Easy.Modules.Discuss
{
    [DataConfigure(typeof(DiscussEntityMetaData))]
    public class DiscussEntity : IBasicEntity<long>
    {
        public long ParentID { get; set; }
        public int RelationType { get; set; }
        public long RelationID { get; set; }
        public string Message { get; set; }
        public string ImageUrl { get; set; }


        public long ID { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public bool IsPassed { get; set; }
    }
    class DiscussEntityMetaData : DataViewMetaData<DiscussEntity>
    {
        protected override void DataConfigure()
        {
            DataTable("Discuss");
            DataConfig(m=>m.ID).AsIncreasePrimaryKey();
        }

        protected override void ViewConfigure()
        {
            
        }
    }

}
