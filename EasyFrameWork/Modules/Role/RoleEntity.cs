using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Easy.MetaData;
using Easy.Models;

namespace Easy.Modules.Role
{
    [DataConfigure(typeof(RoleMetaData))]
    public class RoleEntity : EditorEntity
    {
        public int ID { get; set; }
        public int RolePermission { get; set; }
    }

    class RoleMetaData : DataViewMetaData<RoleEntity>
    {
        protected override void DataConfigure()
        {
            DataTable("Role");
            DataConfig(m => m.ID).AsIncreasePrimaryKey();
        }

        protected override void ViewConfigure()
        {
            ViewConfig(m => m.ID).AsHidden();
            ViewConfig(m => m.RolePermission).AsHidden();
        }
    }
}
