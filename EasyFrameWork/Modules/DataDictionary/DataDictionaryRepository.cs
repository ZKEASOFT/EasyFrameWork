using Easy.RepositoryPattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Easy.Modules.DataDictionary
{
    class DataDictionaryRepository : RepositoryBase<DataDictionaryEntity>
    {
        public List<string> GetDictionaryType()
        {
            return DB.CustomerSql("select DicType from DataDictionary group by DicType order by DicType").ToList<string>();
        }
    }
}
