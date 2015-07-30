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
        private static readonly string DataBaseCategory;
        private static readonly string ConnectionString;
        public IApplicationContext ApplicationContext { get; private set; }
        static RepositoryBase()
        {
            DataBaseCategory = System.Configuration.ConfigurationManager.AppSettings[DataBasic.DataBaseAppSetingKey];
            var con = System.Configuration.ConfigurationManager.ConnectionStrings[DataBasic.ConnectionKey];
            ConnectionString = con != null ? con.ConnectionString : System.Configuration.ConfigurationManager.ConnectionStrings[0].ConnectionString;
        }
        public DataBasic Database
        {
            get;
            private set;
        }
        public RepositoryBase()
        {
            if (DataBaseCategory == DataBasic.Ace)
            {
                Database = new Access(ConnectionString);
                ((Access) Database).DbType = Access.DdTypes.Ace;
            }
            else if (DataBaseCategory == DataBasic.Jet)
            {
                Database = new Access(ConnectionString);
                ((Access) Database).DbType = Access.DdTypes.JET;
            }
            else if (DataBaseCategory == DataBasic.SQL)
            {
                Database = new SQL(ConnectionString);
            }
            else
            {
                Database = new SQL(ConnectionString);
            }
            ApplicationContext = ServiceLocator.Current.GetInstance<IApplicationContext>();
        }

        public virtual T Get(params object[] primaryKeys)
        {
            return Database.Get<T>(primaryKeys);
        }
        public virtual List<T> Get(DataFilter filter)
        {
            return Database.Get<T>(filter);
        }
        public virtual List<T> Get(DataFilter filter, Pagination pagin)
        {
            return Database.Get<T>(filter, pagin);
        }
        public virtual void Add(T item)
        {
            Database.Add<T>(item);
        }
        public virtual int Delete(params object[] primaryKeys)
        {
            return Database.Delete<T>(primaryKeys);
        }
        public virtual int Delete(DataFilter filter)
        {
            return Database.Delete<T>(filter);
        }
        public virtual bool Update(T item, DataFilter filter)
        {
            return Database.Update<T>(item, filter);
        }
        public virtual bool Update(T item, params object[] primaryKeys)
        {
            return Database.Update<T>(item, primaryKeys);
        }
        public virtual long Count(DataFilter filter)
        {
            return Database.Count<T>(filter);
        }
    }
}
