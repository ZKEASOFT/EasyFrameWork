using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Easy.Data;
using Easy.MetaData;
using Easy.Models;
using MvcApplication.Service;

namespace MvcApplication.Models
{
    [DataConfigure(typeof(ExampleMetaData))]
    public class Example : EditorEntity
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string Value { get; set; }
        public ICollection<ExampleItem> Items { get; set; }
    }

    class ExampleMetaData : DataViewMetaData<Example>
    {

        protected override void DataConfigure()
        {
            DataTable("Example");
            DataConfig(m => m.Id).AsIncreasePrimaryKey();
            DataConfig(m => m.Value).Mapper("ValueText");
            DataConfig(m => m.Title).Ignore();
            DataConfig(m => m.Items)
                .SetReference<ExampleItem, IExampleItemService>((example, exampleItem) => exampleItem.ExampleID == example.Id);
        }

        protected override void ViewConfigure()
        {
            ViewConfig(m => m.Id).AsHidden();
            ViewConfig(m => m.Text).AsTextBox().Required();
            ViewConfig(m => m.Value).AsMutiLineTextBox().MaxLength(200);
            ViewConfig(m => m.Items).AsListEditor();
        }

    }
}