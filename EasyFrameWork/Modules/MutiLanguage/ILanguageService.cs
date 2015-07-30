using System;
using Easy.RepositoryPattern;
using System.Collections.Generic;
using Easy.Models;
namespace Easy.Modules.MutiLanguage
{
    public interface ILanguageService : IService, IFreeDependency
    {
        IEnumerable<LanguageEntity> GetAllTypes();
        Dictionary<string, string> InitLan(Dictionary<string, string> source);
    }
}
