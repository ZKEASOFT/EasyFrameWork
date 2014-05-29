using System;
using Easy.RepositoryPattern;
using System.Collections.Generic;
namespace Easy.Modules.MutiLanguage
{
    public interface ILanguageService : IService<ILanguageEntity>
    {
        IList<ILanguageEntity> GetAllTypes();
        Dictionary<string, string> InitLan(Dictionary<string, string> source);
    }
}
