using Easy.MetaData;

namespace MvcApplication.Models
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
            
        }

        protected override void ViewConfigure()
        {
            
        }
    }


}