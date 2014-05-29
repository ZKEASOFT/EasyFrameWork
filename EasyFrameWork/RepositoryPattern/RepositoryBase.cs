using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Easy.Data.DataBase;
using Easy.Data;

namespace Easy.RepositoryPattern
{
    public class RepositoryBase<T> : IRepository<T> where T : class
    {
        public DataBasic DB
        {
            get;
            private set;
        }
        public RepositoryBase()
        {
            string dataBase = System.Configuration.ConfigurationManager.AppSettings["DataBase"];
            var con = System.Configuration.ConfigurationManager.ConnectionStrings["Easy"];
            string connString = string.Empty;
            if (con != null)
            {
                connString = con.ConnectionString;
            }
            else
            {
                connString = System.Configuration.ConfigurationManager.ConnectionStrings[0].ConnectionString;
            }
            if (dataBase == "Ace")
            {
                DB = new Access(connString);
                (DB as Access).DbType = Access.DdTypes.Ace;
            }
            else if (dataBase == "Jet")
            {
                DB = new Access(connString);
                (DB as Access).DbType = Access.DdTypes.JET;
            }
            else if (dataBase == "SQL")
            {
                DB = new SQL(connString);
            }
        }

        public T Get(params object[] primaryKeys)
        {
            return DB.Get<T>(primaryKeys);
        }
        public List<T> Get(DataFilter filter)
        {
            return DB.Get<T>(filter);
        }
        public List<T> Get(DataFilter filter, Pagination pagin)
        {
            return DB.Get<T>(filter, pagin);
        }
        public void Add(T item)
        {
            DB.Add<T>(item);
        }
        public int Delete(params object[] primaryKeys)
        {
            return DB.Delete<T>(primaryKeys);
        }
        public int Delete(DataFilter filter)
        {
            return DB.Delete<T>(filter);
        }
        public bool Update(T item, DataFilter filter)
        {
            return DB.Update<T>(item, filter);
        }
        public bool Update(T item, params object[] primaryKeys)
        {
            return DB.Update<T>(item, primaryKeys);
        }
        public long Count(DataFilter filter)
        {
            return DB.Count<T>(filter);
        }
    }
}
