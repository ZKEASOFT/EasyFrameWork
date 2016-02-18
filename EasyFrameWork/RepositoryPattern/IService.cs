using System;
using Easy.Data;
using System.Collections.Generic;
using Easy.IOC;

namespace Easy.RepositoryPattern
{
    public interface IService<T> : IDependency where T : class
    {
        T Get(params object[] primaryKeys);
        IEnumerable<T> Get();
        IEnumerable<T> Get(DataFilter filter);
        IEnumerable<T> Get(DataFilter filter, Pagination pagin);
        IEnumerable<T> Get(string property, OperatorType operatorType, object value);
        void Add(T item);
        int Delete(params object[] primaryKeys);
        int Delete(DataFilter filter);
        int Delete(T item);
        bool Update(T item, DataFilter filter);
        bool Update(T item, params object[] primaryKeys);
        long Count(DataFilter filter);
    }
}
