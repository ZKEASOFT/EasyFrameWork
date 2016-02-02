using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Easy.Data.DataBase;
using Easy.Data;
using Easy.Models;
using Microsoft.Practices.ServiceLocation;

namespace Easy.RepositoryPattern
{
    public class RepositoryBase<T> : IRepository<T> where T : class
    {
        public IApplicationContext ApplicationContext { get; private set; }

        public DataBasic DataBase
        {
            get;
            private set;
        }
        public RepositoryBase()
        {
            string dataBase = System.Configuration.ConfigurationManager.AppSettings[DataBasic.DataBaseAppSetingKey];
            DataBase = ServiceLocator.Current.GetAllInstances<DataBasic>().FirstOrDefault(m => m.DataBaseTypeNames().Any(n => n == dataBase)) ?? new Sql();
            ApplicationContext = ServiceLocator.Current.GetInstance<IApplicationContext>();
        }

        public virtual T Get(params object[] primaryKeys)
        {
            return DataBase.Get<T>(primaryKeys);
        }
        public virtual IEnumerable<T> Get(DataFilter filter)
        {
            return DataBase.Get<T>(filter);
        }
        public virtual IEnumerable<T> Get(DataFilter filter, Pagination pagin)
        {
            return DataBase.Get<T>(filter, pagin);
        }
        public virtual void Add(T item)
        {
            DataBase.Add(item);
        }
        public virtual int Delete(params object[] primaryKeys)
        {
            return DataBase.Delete<T>(primaryKeys);
        }
        public virtual int Delete(DataFilter filter)
        {
            return DataBase.Delete<T>(filter);
        }
        public virtual bool Update(T item, DataFilter filter)
        {
            return DataBase.Update(item, filter);
        }
        public virtual bool Update(T item, params object[] primaryKeys)
        {
            return DataBase.Update(item, primaryKeys);
        }
        public virtual long Count(DataFilter filter)
        {
            return DataBase.Count<T>(filter);
        }
    }
}
