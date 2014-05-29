using Easy.Models;
using System;
namespace Easy.Modules.DataDictionary
{
    public interface IDataDictionaryEntity : IBasicEntity<long>
    {
        /// <summary>
        /// 是否系统
        /// </summary>
        bool IsSystem { get; set; }
        /// <summary>
        /// 字典名称
        /// </summary>
        string Name { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        int OrderIndex { get; set; }
        /// <summary>
        /// 父级ID
        /// </summary>
        long Pid { get; set; }
        /// <summary>
        /// 类别名称
        /// </summary>
        string TypeName { get; set; }
        /// <summary>
        /// 字典值
        /// </summary>
        string Value { get; set; }
    }
}
