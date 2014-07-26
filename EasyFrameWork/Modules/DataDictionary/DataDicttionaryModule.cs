using Easy.IOCAdapter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Easy.Modules.DataDictionary
{
    public class DataDicttionaryModule : IModule
    {
        public void Load()
        {
            Container.Register(typeof(IDataDictionaryService), typeof(DataDictionaryService));
        }
    }
}
