using Easy.Data;
using System.Collections.Generic;
using Easy.IOC;

namespace Easy.RepositoryPattern
{
    public interface IRepository<T> : IDependency where T : class
    {
        void Add(T item);
        int Delete(DataFilter filter);
        int Delete(params object[] primaryKeys);
        List<T> Get(DataFilter filter);
        List<T> Get(DataFilter filter, Pagination pagin);
        T Get(params object[] primaryKeys);
        bool Update(T item, DataFilter filter);
        bool Update(T item, params object[] primaryKeys);
    }
}
