using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Easy.Attribute;
using Easy.Models;

namespace Easy.Modules.DataDictionary
{
    [DataConfigure(typeof(DataDictionaryEntityMetaData))]
    public class DataDictionaryEntity : EditorEntity, IImage, IDataDictionaryEntity
    {
        public string TypeName { get; set; }

        public string Name { get; set; }

        public string Value { get; set; }

        public int OrderIndex { get; set; }

        public long Pid { get; set; }

        public string SEOTitle { get; set; }

        public string SEOKeyWord { get; set; }
        public string SEODescription { get; set; }
        public bool IsSystem { get; set; }

        public string ImageUrl { get; set; }

        public string ImageThumbUrl { get; set; }

        public string UpImage { get; set; }

        public long ID { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public bool IsPassed { get; set; }
    }
    class DataDictionaryEntityMetaData : DataViewMetaData<DataDictionaryEntity>
    {
        public override void DataConfigure()
        {
            DataTable("DataDictionary");
            DataConfig(m => m.ID).AsIncreasePrimaryKey();
            DataConfig(m => m.TypeName).Mapper("DicType");
            DataConfig(m => m.Name).Mapper("DicName");
            DataConfig(m => m.Value).Mapper("DicValue");
            DataConfig(m => m.Pid).Mapper("DicPid");

            DataConfig(m => m.Title).Ignore();
            DataConfig(m => m.UpImage).Ignore();
        }

        public override void ViewConfigure()
        {
            ViewConfig(m => m.ID).AsHidden();
            ViewConfig(m => m.TypeName).AsHidden();
            ViewConfig(m => m.Name).AsTextBox().Required().MaxLength(25);
            ViewConfig(m => m.IsPassed).AsCheckBox().SetColumnWidth(60);
            ViewConfig(m => m.IsSystem).AsCheckBox().ReadOnly();
            ViewConfig(m => m.UpImage).AsFileUp().HideInGrid();
            ViewConfig(m => m.Value).AsTextBox();
            ViewConfig(m => m.Title).AsHidden();
            ViewConfig(m => m.Pid).AsHidden();
        }
    }

}
