using System;
using System.Collections.Generic;
using Easy.RepositoryPattern;
namespace Easy.Modules.DataDictionary
{
    public interface IDataDictionaryService : IService<IDataDictionaryEntity>
    {
        /// <summary>
        /// 根据类别获取数据字典
        /// </summary>
        /// <param name="dicType"></param>
        /// <returns></returns>
        List<IDataDictionaryEntity> GetDictionaryByType(string dicType);
        /// <summary>
        /// 获取数据字典所有类别
        /// </summary>
        /// <returns></returns>
        List<string> GetDictionaryType();
    }
}
