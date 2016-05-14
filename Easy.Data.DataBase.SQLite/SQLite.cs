using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SQLite;
using System.Text;
using Easy.Extend;
using Easy.MetaData;
using System.Reflection;

namespace Easy.Data.DataBase
{
    public class SQLite : DataBasic
    {
        public SQLite()
        {
            ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings[ConnectionKey].ConnectionString;
            if (!ConnectionString.Contains(":"))
            {
                ConnectionString = AppDomain.CurrentDomain.BaseDirectory + ConnectionString;
            }
        }
        public override IEnumerable<string> DataBaseTypeNames()
        {
            yield return "SQLite";
        }

        public override bool IsExistColumn(string tableName, string columnName)
        {
            throw new NotImplementedException();
        }

        public override bool IsExistTable(string tableName)
        {
            throw new NotImplementedException();
        }

        protected override DbCommand GetDbCommand()
        {
            return new SQLiteCommand();
        }

        protected override DbCommandBuilder GetDbCommandBuilder(DbDataAdapter adapter)
        {
            return new SQLiteCommandBuilder(adapter as SQLiteDataAdapter);
        }

        protected override DbConnection GetDbConnection()
        {
            return
                new SQLiteConnection(string.Format("Data Source={0};", ConnectionString));
        }

        protected override DbDataAdapter GetDbDataAdapter(DbCommand command)
        {
            return new SQLiteDataAdapter(command as SQLiteCommand);
        }

        protected override DbParameter GetDbParameter(string key, object value)
        {
            return new SQLiteParameter(key, value);
        }

        public override IEnumerable<T> Get<T>(DataFilter filter, Pagination pagin)
        {
            DataConfigureAttribute custAttribute = DataConfigureAttribute.GetAttribute<T>();
            string alias = "T0";
            if (custAttribute != null)
            {
                filter = custAttribute.MetaData.DataAccess(filter);//数据权限    
                alias = custAttribute.MetaData.Alias;
            }
            string tableName = GetTableName<T>(custAttribute);
            List<KeyValuePair<string, string>> comMatch;
            string selectCol = GetSelectColumn<T>(custAttribute, out comMatch);
            string condition = filter.ToString();

            var primaryKey = GetPrimaryKeys(custAttribute);
            foreach (var item in primaryKey)
            {
                filter.OrderBy(string.Format("[{0}].[{1}]", alias, item.ColumnName), OrderType.Ascending);
            }
            string orderby = filter.GetOrderString();
            string orderByContrary = filter.GetContraryOrderString();

            var builderRela = new StringBuilder();
            if (custAttribute != null)
            {
                foreach (var item in custAttribute.MetaData.DataRelations)
                {
                    builderRela.Append(item);
                }
            }
            const string formatCount = "SELECT COUNT(*) FROM [{0}] {3} {2} {1}";
            DataTable recordCound = this.GetData(string.Format(formatCount,
                tableName,
                string.IsNullOrEmpty(condition) ? "" : "WHERE " + condition,
                builderRela,
                alias), filter.GetParameterValues());
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
            var builder = new StringBuilder();
            const string formatTable = "SELECT * FROM (SELECT * FROM (SELECT {0} FROM {3} {6} {5} {4} limit {2}) TEMP0 {7}) TEMP1 {8} limit {1}";
            builder.AppendFormat(formatTable,
                selectCol,
                pageSize,
                pagin.PageSize * (pagin.PageIndex + 1),
                string.Format("[{0}] {1}", tableName, custAttribute == null ? "T0" : custAttribute.MetaData.Alias),
                orderby,
                string.IsNullOrEmpty(condition) ? "" : "WHERE " + condition,
                builderRela,
                orderByContrary.Replace("[{0}].".FormatWith(alias), ""),
                orderby.Replace("[{0}].".FormatWith(alias), ""));


            DataTable table = this.GetData(builder.ToString(), filter.GetParameterValues());
            if (table == null) return new List<T>(); ;
            var list = new List<T>();
            Dictionary<string, PropertyInfo> properties = GetProperties<T>(custAttribute);
            for (int i = 0; i < table.Rows.Count; i++)
            {
                list.Add(Reflection.ClassAction.GetModel<T>(table, i, comMatch, properties));
            }
            return list;
        }

        public override int Delete(string tableName, string condition, List<KeyValuePair<string, object>> keyValue, string alias = "T0")
        {
            DbCommand comm = GetDbCommand();
            comm.CommandText = !string.IsNullOrEmpty(condition) ?
                string.Format("DELETE FROM [{0}] WHERE {1}", tableName, condition.Replace("[{0}].".FormatWith(alias), "")) :
            string.Format("DELETE FROM [{0}]", tableName);

            foreach (var item in keyValue)
            {
                SetParameter(comm, item.Key, item.Value);
            }
            return ExecCommand(comm);
        }
        public override object Insert(string tableName, Dictionary<string, object> values)
        {
            if (values.Count == 0) return null;
            DbCommand command = GetDbCommand();
            command.Connection = GetDbConnection();
            StringBuilder valueBuilder = new StringBuilder();
            StringBuilder fieldBuilder = new StringBuilder();

            foreach (var item in values)
            {
                valueBuilder.AppendFormat(",{0}", item.Value == null ? "NULL" : "@" + item.Key);
                fieldBuilder.AppendFormat(",[{0}]", item.Key);
            }
            const string comInsertFormat = "INSERT INTO [{0}] ({1}) VALUES ({2})";
            command.CommandText = comInsertFormat.FormatWith(tableName, fieldBuilder.ToString().Trim(','), valueBuilder.ToString().Trim(','));
            foreach (var itemV in values)
            {
                SetParameter(command, itemV.Key, itemV.Value);
            }
            try
            {
                if (command.Connection.State != ConnectionState.Open)
                    command.Connection.Open();
                command.ExecuteNonQuery();
                command.CommandText = "select last_insert_rowid()";
                object cou = command.ExecuteScalar();
                return cou;
            }
            catch (Exception ex)
            {
                Logger.Info(command.CommandText);
                Logger.Error(ex);
                throw ex;
            }
            finally
            {
                if (command.Connection.State != ConnectionState.Closed)
                    command.Connection.Close();
                command.Connection.Dispose();
                command.Dispose();
            }
        }
    }
}
