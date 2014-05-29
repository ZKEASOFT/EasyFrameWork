using Easy.Data;
using System;
using System.Collections.Generic;
namespace Easy.RepositoryPattern
{
    public interface IService<T> where T : class
    {
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="item"></param>
        void Add(T item);
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        int Delete(DataFilter filter);
        /// <summary>
        /// 按主键删除
        /// </summary>
        /// <param name="primaryKeys"></param>
        /// <returns></returns>
        int Delete(params object[] primaryKeys);
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        List<T> Get(DataFilter filter);
        /// <summary>
        /// 分页获取数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="pagin"></param>
        /// <returns></returns>
        List<T> Get(DataFilter filter, Pagination pagin);
        /// <summary>
        /// 按主键获取数据
        /// </summary>
        /// <param name="primaryKeys"></param>
        /// <returns></returns>
        T Get(params object[] primaryKeys);
        /// <summary>
        /// 按条件更新
        /// </summary>
        /// <param name="item"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        bool Update(T item, DataFilter filter);
        /// <summary>
        /// 按主键更新
        /// </summary>
        /// <param name="item"></param>
        /// <param name="primaryKeys"></param>
        /// <returns></returns>
        bool Update(T item, params object[] primaryKeys);
    }
}
