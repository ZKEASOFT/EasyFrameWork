using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Easy.MetaData;

namespace Easy.Data.DataBase
{
    public class CustomerSqlHelper
    {
        string command;
        List<KeyValuePair<string, object>> keyValue;
        DataBasic DB;
        CommandType Ct;
        public CustomerSqlHelper(string command, CommandType ct, DataBasic db)
        {
            if (ct == CommandType.StoredProcedure && !command.Contains('['))
            {
                this.command = string.Format("[{0}]", command);
            }
            else this.command = command;
            keyValue = new List<KeyValuePair<string, object>>();
            this.Ct = ct;
            this.DB = db;
        }
        private List<KeyValuePair<string, string>> GetMap<T>()
        {
            DataConfigureAttribute attribute = DataConfigureAttribute.GetAttribute<T>();
            List<KeyValuePair<string, string>> map = new List<KeyValuePair<string, string>>();
            if (attribute != null && attribute.MetaData != null && attribute.MetaData.PropertyDataConfig != null)
            {
                foreach (var item in attribute.MetaData.PropertyDataConfig)
                {
                    map.Add(new KeyValuePair<string, string>(item.Value.ColumnName, item.Key));
                }
            }
            else
            {
                var proertyInfoArray = Easy.Loader.GetType<T>().GetProperties();
                foreach (var item in proertyInfoArray)
                {
                    if (item.CanWrite)
                    {
                        map.Add(new KeyValuePair<string, string>(item.Name, item.Name));
                    }
                }
            }
            return map;
        }
        public CustomerSqlHelper AddParameter(string name, object value)
        {
            keyValue.Add(new KeyValuePair<string, object>(name, value));
            return this;
        }
        public int ExecuteNonQuery()
        {
            return DB.ExecuteNonQuery(command, Ct, keyValue);
        }
        public T To<T>()
        {
            if (Ct == CommandType.StoredProcedure)
            {
                command = "EXEC " + command;
            }
            DataTable table = DB.GetTable(command, keyValue);
            var map = GetMap<T>();
            if (map.Count == 0)
            {
                return Easy.Reflection.ClassAction.GetModel<T>(table, 0);
            }
            else
            {
                return Easy.Reflection.ClassAction.GetModel<T>(table, 0, map);
            }
        }
        public List<T> ToList<T>()
        {
            if (Ct == CommandType.StoredProcedure)
            {
                command = "EXEC " + command;
            }
            DataTable table = DB.GetTable(command, keyValue);
            List<T> lists = new List<T>();
            var map = GetMap<T>();
            for (int i = 0; i < table.Rows.Count; i++)
            {
                if (map.Count == 0)
                {
                    lists.Add(Easy.Reflection.ClassAction.GetModel<T>(table, i));
                }
                else
                {
                    lists.Add(Easy.Reflection.ClassAction.GetModel<T>(table, i, map));
                }
            }
            return lists;
        }
        public DataTable ToDataTable()
        {
            if (Ct == CommandType.StoredProcedure)
            {
                command = "EXEC " + command;
            }
            return DB.GetTable(command, keyValue);
        }

    }
}
