using System;
namespace Easy.Models
{
    public interface IBasicEntity<T>
    {
        /// <summary>
        /// ID主键
        /// </summary>
        T ID { get; set; }
        /// <summary>
        /// 显示标题
        /// </summary>
        string Title { get; set; }
        /// <summary>
        /// 说明
        /// </summary>
        string Description { get; set; }
        /// <summary>
        /// 是否通过
        /// </summary>
        bool IsPassed { get; set; }
    }
}
