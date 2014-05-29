using Easy.RepositoryPattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Easy.Modules.MutiLanguage
{
    public class LanguageService : ServiceBase<ILanguageEntity>, ILanguageService
    {
        LanguageRepository rep = new LanguageRepository();
        public IList<ILanguageEntity> GetAllTypes()
        {
            IList<ILanguageEntity> result = rep.GetAllTypes();
            foreach (LanguageEntity item in result)
            {
                item.LanKey = item.Module;
            }
            return result;
        }
        public Dictionary<string, string> InitLan(Dictionary<string, string> source)
        {
            return rep.InitLan(source);
        }
    }
}
