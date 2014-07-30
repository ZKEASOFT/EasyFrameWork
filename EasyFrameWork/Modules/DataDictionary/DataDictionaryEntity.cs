using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Easy.Attribute;
using Easy.Models;

namespace Easy.Modules.DataDictionary
{
    [DataConfigure(typeof(DataDictionaryEntityMetaData))]
    public class DataDictionaryEntity : EditorEntity, IImage, IBasicEntity<long>
    {
        public long ID { get; set; }
        public string DicName { get; set; }

        public string Title { get; set; }

        public string DicValue { get; set; }

        public int Order { get; set; }

        public long Pid { get; set; }

        public bool IsSystem { get; set; }

        public string ImageUrl { get; set; }

        public string ImageThumbUrl { get; set; }

        public string Description { get; set; }

        public int Status { get; set; }
    }
    class DataDictionaryEntityMetaData : DataViewMetaData<DataDictionaryEntity>
    {
        protected override void DataConfigure()
        {
            DataTable("DataDictionary");
            DataConfig(m => m.ID).AsIncreasePrimaryKey();
        }

        protected override void ViewConfigure()
        {
            ViewConfig(m => m.ID).AsHidden();
            ViewConfig(m => m.DicName).AsTextBox().Required().MaxLength(25);
            ViewConfig(m => m.IsSystem).AsCheckBox().ReadOnly();
            ViewConfig(m => m.DicValue).AsTextBox();
            ViewConfig(m => m.Title).AsHidden();
            ViewConfig(m => m.Pid).AsHidden();
        }
    }

}
