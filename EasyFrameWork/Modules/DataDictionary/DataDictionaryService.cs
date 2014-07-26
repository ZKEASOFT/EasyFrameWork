using Easy.RepositoryPattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Easy.Modules.DataDictionary
{
    public class DataDictionaryService : ServiceBase<DataDictionaryEntity>, IDataDictionaryService
    {
        DataDictionaryRepository rep = new DataDictionaryRepository();
        public List<DataDictionaryEntity> GetDictionaryByType(string dicType)
        {
            return Get(new Data.DataFilter().Where(string.Format("T0.DicValue<>'0' and T0.DicType='{0}'", dicType)));
        }
        public List<string> GetDictionaryType()
        {
            return rep.GetDictionaryType();
        }
        public override void Add(DataDictionaryEntity item)
        {
            var parent = this.Get(item.Pid);
            item.TypeName = parent.TypeName;
            base.Add(item);
        }
    }
}
