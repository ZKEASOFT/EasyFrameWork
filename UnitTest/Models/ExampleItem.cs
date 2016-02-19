using Easy.MetaData;

namespace UnitTest.Models
{
    [DataConfigure(typeof(ExampleItemMeterData))]
    public class ExampleItem
    {
        public int? ID { get; set; }
        public int? ExampleID { get; set; }
    }

    class ExampleItemMeterData : DataViewMetaData<ExampleItem>
    {

        protected override void DataConfigure()
        {
            DataTable("ExampleItem");
            DataConfig(m => m.ID).AsIncreasePrimaryKey();
        }

        protected override void ViewConfigure()
        {

        }
    }


}