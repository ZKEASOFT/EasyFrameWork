using Easy.Attribute;
using Easy.Constant;
using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace Easy.Data.DataBase
{
    /// <summary>
    /// 数据库基类，AppConfig> DataBase>[SQL,Jet,Ace]
    /// </summary>
    public abstract class DataBasic
    {
        public abstract int Delete(string tableName, string condition, List<KeyValuePair<string, object>> keyValue);
        public abstract DataTable GetTalbe(string selectCommand);
        public abstract DataTable GetTable(string selectCommand, List<KeyValuePair<string, object>> keyValue);
        public abstract object Insert(string tableName, Dictionary<string, object> values);
        public abstract bool Update(string tableName, string parameterString, List<KeyValuePair<string, object>> keyValue);
        public abstract bool UpDateTable(DataTable table, string tableName);

        public abstract int ExecuteNonQuery(string command, CommandType ct, List<KeyValuePair<string, object>> parameter);

        public abstract CustomerSqlHelper CustomerSql(string command);
        public abstract CustomerSqlHelper StoredProcedures(string storedProcedures);
        #region 私有方法


        protected string GetSelectColumn<T>(DataConfigureAttribute custAttribute, out List<KeyValuePair<string, string>> comMatch) where T : class
        {
            StringBuilder selectCom = new StringBuilder();
            System.Reflection.PropertyInfo[] propertys = Easy.IOCAdapter.Loader.GetType<T>().GetProperties();
            selectCom = new StringBuilder();
            comMatch = new List<KeyValuePair<string, string>>();
            foreach (var item in propertys)
            {
                if (custAttribute != null)
                {
                    if (custAttribute.MetaData.PropertyDataConfig.ContainsKey(item.Name))
                    {
                        PropertyDataInfo config = custAttribute.MetaData.PropertyDataConfig[item.Name];
                        if (!config.Ignore)
                        {
                            if (!string.IsNullOrEmpty(config.ColumnName))
                            {
                                string alias = config.IsRelation ? config.TableAlias : custAttribute.MetaData.Alias;
                                selectCom.AppendFormat("[{0}].[{1}],", alias, config.ColumnName);
                                KeyValuePair<string, string> keyVale = new KeyValuePair<string, string>(config.ColumnName, item.Name);
                                comMatch.Add(keyVale);
                            }
                            else
                            {
                                string alias = config.IsRelation ? config.TableAlias : custAttribute.MetaData.Alias;
                                selectCom.AppendFormat("[{0}].[{1}],", alias, item.Name);
                                KeyValuePair<string, string> keyVale = new KeyValuePair<string, string>(item.Name, item.Name);
                                comMatch.Add(keyVale);
                            }
                        }
                    }
                    else
                    {
                        selectCom.AppendFormat("[{0}].[{1}],", custAttribute.MetaData.Alias, item.Name);
                        KeyValuePair<string, string> keyVale = new KeyValuePair<string, string>(item.Name, item.Name);
                        comMatch.Add(keyVale);
                    }
                }
                else
                {
                    selectCom.AppendFormat("[{0}],", item.Name);
                    KeyValuePair<string, string> keyVale = new KeyValuePair<string, string>(item.Name, item.Name);
                    comMatch.Add(keyVale);
                }
            }
            return selectCom.ToString().Trim(',');
        }

        protected string GetTableName<T>(DataConfigureAttribute custAttribute) where T : class
        {
            string tableName = null;
            if (custAttribute != null)
            {
                if (!string.IsNullOrEmpty(custAttribute.MetaData.Table))
                {
                    tableName = custAttribute.MetaData.Table;
                }
            }
            if (string.IsNullOrEmpty(tableName))
            {
                tableName = typeof(T).Name;
            }
            return tableName;
        }
        #endregion
        public int Delete<T>(DataFilter filter) where T : class
        {
            DataConfigureAttribute custAttribute = DataConfigureAttribute.GetAttribute<T>();
            filter = custAttribute.MetaData.DataAccess(filter);//数据权限
            return Delete(GetTableName<T>(custAttribute), filter.ToString(), filter.GetParameterValues());
        }
        public int Delete<T>(params object[] primaryKeys) where T : class
        {
            DataConfigureAttribute custAttribute = DataConfigureAttribute.GetAttribute<T>();
            Dictionary<int, string> primaryKey = new Dictionary<int, string>();
            primaryKey.Add(0, "ID");
            if (custAttribute != null)
            {
                primaryKey = custAttribute.MetaData.Primarykey == null ? primaryKey : custAttribute.MetaData.Primarykey;
            }
            if (primaryKeys.Length != primaryKey.Count)
            {
                throw new Exception("输入的参数与设置的主键个数不对应！");
            }
            DataFilter filter = new DataFilter();
            for (int i = 0; i < primaryKey.Count; i++)
            {
                filter.Where(primaryKey[i], DataEnumerate.OperatorType.Equal, primaryKeys[i]);
            }
            return Delete<T>(filter);
        }
        public T Get<T>(params object[] primaryKeys) where T : class
        {
            DataConfigureAttribute custAttribute = DataConfigureAttribute.GetAttribute<T>();
            Dictionary<int, string> primaryKey = new Dictionary<int, string>();
            primaryKey.Add(0, "ID");
            if (custAttribute != null)
            {
                primaryKey = custAttribute.MetaData.Primarykey == null ? primaryKey : custAttribute.MetaData.Primarykey;
            }
            if (primaryKeys.Length != primaryKey.Count)
            {
                throw new Exception("输入的参数与设置的主键个数不对应！");
            }
            DataFilter filter = new DataFilter();
            for (int i = 0; i < primaryKey.Count; i++)
            {
                filter.Where(primaryKey[i], DataEnumerate.OperatorType.Equal, primaryKeys[i]);
            }
            List<T> list = this.Get<T>(filter);
            if (list.Count == 1)
                return list[0];
            else return default(T);
        }
        public List<T> Get<T>(DataFilter filter) where T : class
        {
            DataConfigureAttribute custAttribute = DataConfigureAttribute.GetAttribute<T>();
            filter = custAttribute.MetaData.DataAccess(filter);//数据权限
            string tableName = GetTableName<T>(custAttribute);
            List<KeyValuePair<string, string>> comMatch;
            string selectCol = GetSelectColumn<T>(custAttribute, out comMatch);
            string condition = filter.ToString();
            string orderby = filter.GetOrderString();
            StringBuilder selectCom = new StringBuilder();
            selectCom.Append("SELECT ");
            selectCom.Append(selectCol);
            selectCom.AppendFormat(" FROM [{0}] ", tableName);
            selectCom.Append(custAttribute == null ? "T0" : custAttribute.MetaData.Alias);
            if (custAttribute != null)
            {
                foreach (var item in custAttribute.MetaData.DataRelations)
                {
                    selectCom.Append(item.ToString());
                }
            }
            if (!string.IsNullOrEmpty(condition))
            {
                selectCom.AppendFormat(" WHERE {0} ", condition);
            }
            selectCom.AppendFormat(orderby);
            DataTable table = this.GetTable(selectCom.ToString(), filter.GetParameterValues());
            if (table == null) return new List<T>();
            List<T> list = new List<T>();
            for (int i = 0; i < table.Rows.Count; i++)
            {
                list.Add(Reflection.ClassAction.GetModel<T>(table, i, comMatch));
            }
            return list;

        }
        public long Count<T>(DataFilter filter) where T : class
        {
            DataConfigureAttribute custAttribute = DataConfigureAttribute.GetAttribute<T>();
            filter = custAttribute.MetaData.DataAccess(filter);//数据权限
            string tableName = GetTableName<T>(custAttribute);
            string condition = filter.ToString();
            string orderby = filter.GetOrderString();
            StringBuilder selectCom = new StringBuilder();
            selectCom.Append("SELECT COUNT(1) ");
            selectCom.AppendFormat(" FROM [{0}] ", tableName);
            selectCom.Append(custAttribute == null ? "T0" : custAttribute.MetaData.Alias);
            if (custAttribute != null)
            {
                foreach (var item in custAttribute.MetaData.DataRelations)
                {
                    selectCom.Append(item.ToString());
                }
            }
            if (!string.IsNullOrEmpty(condition))
            {
                selectCom.AppendFormat(" WHERE {0} ", condition);
            }
            selectCom.AppendFormat(orderby);
            DataTable table = this.GetTable(selectCom.ToString(), filter.GetParameterValues());
            if (table == null) return -1;
            return Convert.ToInt64(table.Rows[0][0]);
        }
        public virtual List<T> Get<T>(DataFilter filter, Pagination pagin) where T : class
        {
            DataConfigureAttribute custAttribute = DataConfigureAttribute.GetAttribute<T>();
            filter = custAttribute.MetaData.DataAccess(filter);//数据权限
            string tableName = GetTableName<T>(custAttribute);
            List<KeyValuePair<string, string>> comMatch;
            string selectCol = GetSelectColumn<T>(custAttribute, out comMatch);
            string condition = filter.ToString();

            Dictionary<int, string> primaryKey = new Dictionary<int, string>();
            primaryKey.Add(0, "ID");
            if (custAttribute != null)
            {
                primaryKey = custAttribute.MetaData.Primarykey == null ? primaryKey : custAttribute.MetaData.Primarykey;
            }
            foreach (int item in primaryKey.Keys)
            {
                filter.OrderBy(primaryKey[item], DataEnumerate.OrderType.Ascending);
            }
            string orderby = filter.GetOrderString();
            string orderByContrary = filter.GetContraryOrderString();

            StringBuilder builderRela = new StringBuilder();
            if (custAttribute != null)
            {
                foreach (var item in custAttribute.MetaData.DataRelations)
                {
                    builderRela.Append(item.ToString());
                }
            }

            DataTable recordCound = this.GetTable(string.Format("SELECT COUNT(*) FROM [{0}] {3} {2} {1}",
                tableName,
                string.IsNullOrEmpty(condition) ? "" : "WHERE " + condition,
                builderRela.ToString(),
                custAttribute == null ? "T0" : custAttribute.MetaData.Alias), filter.GetParameterValues());
            pagin.RecordCount = Convert.ToInt64(recordCound.Rows[0][0]);
            if (pagin.AllPage == pagin.PageIndex && pagin.RecordCount > 0)
            {
                pagin.PageIndex--;
            }
            int pageSize = pagin.PageSize;
            if ((pagin.PageIndex + 1) * pagin.PageSize > pagin.RecordCount && pagin.RecordCount != 0)
            {
                pageSize = (int)(pagin.RecordCount - pagin.PageIndex * pagin.PageSize);
                if (pageSize < 0)
                {
                    pageSize = pagin.PageSize;
                }
            }
            StringBuilder builder = new StringBuilder();
            string format = "SELECT * FROM (SELECT TOP {1} * FROM (SELECT TOP {2} {0} FROM {3} {6} {5} {4}) TEMP0 {7}) TEMP1 {4}";
            builder.AppendFormat(format,
                selectCol,
                pageSize,
                pagin.PageSize * (pagin.PageIndex + 1),
                string.Format("[{0}] {1}", tableName, custAttribute == null ? "T0" : custAttribute.MetaData.Alias),
                orderby,
                string.IsNullOrEmpty(condition) ? "" : "WHERE " + condition,
                builderRela.ToString(),
                orderByContrary);


            DataTable table = this.GetTable(builder.ToString(), filter.GetParameterValues());
            if (table == null) return new List<T>(); ;
            List<T> list = new List<T>();
            for (int i = 0; i < table.Rows.Count; i++)
            {
                list.Add(Reflection.ClassAction.GetModel<T>(table, i, comMatch));
            }
            return list;
        }
        public void Add<T>(T item) where T : class
        {
            DataConfigureAttribute custAttribute = DataConfigureAttribute.GetAttribute<T>();
            string tableName = GetTableName<T>(custAttribute);
            Dictionary<string, object> allValues = Reflection.ClassAction.GetPropertieValues<T>(item);
            Dictionary<string, object> InsertValues = new Dictionary<string, object>();
            if (custAttribute != null)
            {
                foreach (var valueitem in allValues)
                {
                    if (custAttribute.MetaData.PropertyDataConfig.ContainsKey(valueitem.Key))
                    {
                        PropertyDataInfo config = custAttribute.MetaData.PropertyDataConfig[valueitem.Key];

                        if (!config.Ignore && config.CanInsert)
                        {
                            InsertValues.Add(config.ColumnName, valueitem.Value);
                        }
                    }
                    else
                    {
                        InsertValues.Add(valueitem.Key, valueitem.Value);
                    }
                }
            }
            else
            {
                InsertValues = allValues;
            }
            object resu = this.Insert(tableName, InsertValues);
            if (custAttribute != null && resu != null && resu.ToString() != "0")
            {
                if (custAttribute.MetaData.Primarykey != null && custAttribute.MetaData.Primarykey.Count == 1)
                {
                    foreach (var dataConfig in custAttribute.MetaData.PropertyDataConfig)
                    {
                        if (dataConfig.Value.IsPrimaryKey && dataConfig.Value.IsIncreasePrimaryKey)
                        {
                            PropertyInfo pro = Easy.IOCAdapter.Loader.GetType<T>().GetProperty(dataConfig.Value.PropertyName);
                            if (pro != null && pro.CanWrite)
                            {
                                pro.SetValue(item, Easy.Reflection.ClassAction.ValueConvert(pro, resu), null);
                            }
                            break;
                        }
                    }
                }
            }
        }
        public bool Update<T>(T item, DataFilter filter) where T : class
        {
            DataConfigureAttribute custAttribute = DataConfigureAttribute.GetAttribute<T>();
            string tableName = GetTableName<T>(custAttribute);
            System.Reflection.PropertyInfo[] propertys = Easy.IOCAdapter.Loader.GetType<T>().GetProperties();
            StringBuilder builder = new StringBuilder();
            List<KeyValuePair<string, object>> keyValue = new List<KeyValuePair<string, object>>();
            foreach (var property in propertys)
            {
                string name = property.Name;
                object value = property.GetValue(item, null);

                if (custAttribute != null)
                {
                    if (custAttribute.MetaData.PropertyDataConfig.ContainsKey(name))
                    {
                        PropertyDataInfo data = custAttribute.MetaData.PropertyDataConfig[name];
                        if (data.IsRelation || data.Ignore || !data.CanUpdate)
                            continue;
                        if (!string.IsNullOrEmpty(data.ColumnName))
                        {
                            name = data.ColumnName;
                        }
                    }
                }
                if (value != null)
                {
                    if (builder.Length == 0)
                    {
                        builder.AppendFormat("[{0}]=@{0}", name);
                    }
                    else
                    {
                        builder.AppendFormat(",[{0}]=@{0}", name);
                    }
                    KeyValuePair<string, object> kv = new KeyValuePair<string, object>(name, value);
                    keyValue.Add(kv);
                }
                else
                {
                    if (builder.Length == 0)
                    {
                        builder.AppendFormat("[{0}]=NULL", name);
                    }
                    else
                    {
                        builder.AppendFormat(",[{0}]=NULL", name);
                    }
                }
            }
            string condition = filter.ToString();
            if (!string.IsNullOrEmpty(condition))
            {
                builder.Append(" WHERE ");
                builder.Append(condition);
            }
            foreach (var paVal in filter.GetParameterValues())
            {
                keyValue.Add(paVal);
            }
            return Update(tableName, builder.ToString(), keyValue);

        }
        public bool Update<T>(T item, params object[] primaryKeys) where T : class
        {
            DataFilter filter = new DataFilter();
            Dictionary<int, string> primaryKey = new Dictionary<int, string>();
            primaryKey.Add(0, "ID");
            DataConfigureAttribute custAttribute = DataConfigureAttribute.GetAttribute<T>();
            if (custAttribute != null)
            {
                primaryKey = custAttribute.MetaData.Primarykey == null ? primaryKey : custAttribute.MetaData.Primarykey;
            }
            if (primaryKeys.Length != primaryKey.Count && primaryKeys.Length > 0)
            {
                throw new Exception("输入的参数与设置的主键个数不对应！");
            }
            if (primaryKeys.Length > 0)
            {
                for (int i = 0; i < primaryKey.Count; i++)
                {
                    filter.Where(primaryKey[i], DataEnumerate.OperatorType.Equal, primaryKeys[i]);
                }
            }
            else
            {
                Type entityType = Easy.IOCAdapter.Loader.GetType<T>();
                foreach (int primary in primaryKey.Keys)
                {
                    string proPerty = primaryKey[primary];
                    PropertyInfo proper = null;
                    if (custAttribute != null)
                    {
                        foreach (string key in custAttribute.MetaData.PropertyDataConfig.Keys)
                        {
                            if (custAttribute.MetaData.PropertyDataConfig[key].ColumnName == proPerty)
                            {//属性名
                                proper = entityType.GetProperty(key);
                                break;
                            }
                        }
                    }
                    if (proper == null)
                        proper = entityType.GetProperty(proPerty);
                    if (proper != null && proper.CanRead)
                    {
                        filter.Where(proPerty, DataEnumerate.OperatorType.Equal, proper.GetValue(item, null));
                    }
                }
            }
            return Update<T>(item, filter);

        }


    }
}
