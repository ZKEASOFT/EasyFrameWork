using System;
namespace Easy.Models
{
    public interface IBasicEntity<T>
    {
        T ID { get; set; }
        string Title { get; set; }
        /// <summary>
        /// 说明
        /// </summary>
        string Description { get; set; }
        /// <summary>
        /// 是否通过
        /// </summary>
        int Status { get; set; }
    }
}
