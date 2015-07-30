using Easy.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Easy.Models;

namespace Easy.RepositoryPattern
{
    public interface IServiceBase<T> : IDependency
    {
        T Get(params object[] primaryKeys);
        IEnumerable<T> Get();
        IEnumerable<T> Get(DataFilter filter);
        IEnumerable<T> Get(DataFilter filter, Pagination pagin);
        IEnumerable<T> Get(string property, OperatorType operatorType, object value);
        void Add(T item);
        int Delete(params object[] primaryKeys);
        int Delete(DataFilter filter);
        bool Update(T item, DataFilter filter);
        bool Update(T item, params object[] primaryKeys);
        long Count(DataFilter filter);
    }
}
