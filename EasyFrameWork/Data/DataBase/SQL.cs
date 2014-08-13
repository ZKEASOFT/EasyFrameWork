using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace Easy.Data.DataBase
{
    public class SQL : DataBasic
    {

        public SQL()
        {
            ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["Easy"].ConnectionString;
        }

        public SQL(string _ConnectionString)
        {
            ConnectionString = _ConnectionString;
        }

        public string ConnectionString { get; set; }

        private SqlConnection GetConnection()
        {
            SqlConnection conn = new SqlConnection(ConnectionString);
            return conn;
        }


        private int ExecCommand(SqlCommand Comm)
        {
            Comm.Connection = GetConnection();
            try
            {
                if (Comm.Connection.State != ConnectionState.Open)
                    Comm.Connection.Open();
                int cou = Comm.ExecuteNonQuery();
                if (Comm.Connection.State != ConnectionState.Closed)
                    Comm.Connection.Close();
                return cou;
            }
            catch (Exception ex)
            {
                Logger.Info(Comm.CommandText);
                Logger.Error(ex);
                throw ex;
            }
            finally
            {
                if (Comm.Connection.State != ConnectionState.Closed)
                    Comm.Connection.Close();
                Comm.Connection.Dispose();
                Comm.Dispose();
            }
        }

        private DataTable GetTable(SqlCommand Comm)
        {
            Comm.Connection = GetConnection();
            if (Comm.Connection.State != ConnectionState.Open)
                Comm.Connection.Open();
            SqlDataAdapter adapter = new SqlDataAdapter(Comm);
            try
            {
                DataSet ds = new DataSet();
                adapter.Fill(ds);
                if (Comm.Connection.State != ConnectionState.Closed)
                    Comm.Connection.Close();
                return ds.Tables[0];
            }
            catch (Exception ex)
            {
                Logger.Info(Comm.CommandText);
                Logger.Error(ex);
                throw ex;
            }
            finally
            {
                if (Comm.Connection.State != ConnectionState.Closed)
                    Comm.Connection.Close();
                Comm.Connection.Dispose();
                Comm.Dispose();
            }
        }

        public override DataTable GetTalbe(string selectCommand)
        {
            SqlCommand command = new SqlCommand(selectCommand);
            return this.GetTable(command);
        }

        public override DataTable GetTable(string selectCommand, List<KeyValuePair<string, object>> keyValue)
        {
            SqlCommand comm = new SqlCommand(selectCommand);
            foreach (var item in keyValue)
            {
                comm.Parameters.Add(new SqlParameter(item.Key, item.Value));
            }
            return GetTable(comm);
        }

        public override object Insert(string tableName, Dictionary<string, object> values)
        {
            SqlCommand Comm = new SqlCommand();
            Comm.Connection = GetConnection();
            string valuestr = string.Empty;
            string fielstr = string.Empty;
            int i = 0;
            foreach (var item in values)
            {
                if (i == 0)
                {
                    valuestr += "@" + item.Key;
                    fielstr += "[" + item.Key + "]";
                }
                else
                {
                    valuestr += ",@" + item.Key;
                    fielstr += ",[" + item.Key + "]";
                }
                i++;
            }



            string comstr = "INSERT INTO [" + tableName + "] (" + fielstr + ") VALUES (" + valuestr + ")";
            Comm.CommandText = comstr + " SELECT @@IDENTITY";
            Dictionary<string, string> NewStr = new Dictionary<string, string>();
            foreach (var itemV in values)
            {
                Comm.Parameters.AddWithValue("@" + itemV.Key, itemV.Value);
            }
            try
            {
                if (Comm.Connection.State != ConnectionState.Open)
                    Comm.Connection.Open();
                object cou = Comm.ExecuteScalar();
                if (Comm.Connection.State != ConnectionState.Closed)
                    Comm.Connection.Close();
                return cou;
            }
            catch (Exception ex)
            {
                Logger.Info(Comm.CommandText);
                Logger.Error(ex);
                throw ex;
            }
            finally
            {
                if (Comm.Connection.State != ConnectionState.Closed)
                    Comm.Connection.Close();
                Comm.Connection.Dispose();
                Comm.Dispose();
            }
        }

        public override int Delete(string tableName, string condition, List<KeyValuePair<string, object>> keyValue)
        {
            SqlCommand com;
            if (!string.IsNullOrEmpty(condition))
            {
                com = new SqlCommand(string.Format("DELETE TABLE [{0}] WHERE {1}", tableName, condition));
            }
            else
            {
                com = new SqlCommand(string.Format("DELETE TABLE [{0}]", tableName));
            }
            foreach (var item in keyValue)
            {
                com.Parameters.Add(new SqlParameter(item.Key, item.Value));
            }
            return ExecCommand(com);
        }

        public override bool Update(string tableName, string parameterString, List<KeyValuePair<string, object>> keyValue)
        {
            SqlCommand com = new SqlCommand();
            com.CommandText = string.Format("UPDATE [{0}] SET {1}", tableName, parameterString);
            foreach (var item in keyValue)
            {
                com.Parameters.Add(new SqlParameter(item.Key, item.Value));
            }
            return this.ExecCommand(com) > 0 ? true : false;
        }

        public override bool UpDateTable(DataTable table, string tableName)
        {
            SqlDataAdapter adapter = new SqlDataAdapter(string.Format("SELECT * FROM [{0}]", tableName), GetConnection());
            SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
            adapter.InsertCommand = builder.GetInsertCommand();
            adapter.DeleteCommand = builder.GetDeleteCommand();
            adapter.UpdateCommand = builder.GetUpdateCommand();
            int cou = adapter.Update(table);
            adapter.Dispose();
            return cou > 0 ? true : false;
        }

        public override List<T> Get<T>(DataFilter filter, Pagination pagin)
        {
            MetaData.DataConfigureAttribute custAttribute = Easy.MetaData.DataConfigureAttribute.GetAttribute<T>();
            string tableName = GetTableName<T>(custAttribute);
            List<KeyValuePair<string, string>> comMatch;
            string selectCol = GetSelectColumn<T>(custAttribute, out comMatch);
            string condition = filter.ToString();
            string orderby = filter.GetOrderString();
            if (string.IsNullOrEmpty(orderby))
            {
                Dictionary<int, string> primaryKey = new Dictionary<int, string>();
                primaryKey.Add(0, "ID");
                if (custAttribute != null)
                {
                    primaryKey = custAttribute.MetaData.Primarykey == null ? primaryKey : custAttribute.MetaData.Primarykey;
                }
                orderby = "Order by ";
                foreach (int item in primaryKey.Keys)
                {
                    orderby += string.Format("[{0}] Asc,", primaryKey[item]);
                }
                orderby = orderby.Trim(',');
            }
            StringBuilder builder = new StringBuilder();
            builder.Append("WITH T AS( ");
            builder.AppendFormat("SELECT TOP {0} ", pagin.PageSize * (pagin.PageIndex + 1));
            builder.Append(selectCol);
            builder.AppendFormat(",ROW_NUMBER() OVER ({0}) AS RowIndex ", orderby);
            builder.AppendFormat(" FROM [{0}] ", tableName);
            builder.Append(custAttribute == null ? "T0" : custAttribute.MetaData.Alias);
            StringBuilder builderRela = new StringBuilder();
            if (custAttribute != null)
            {
                foreach (var item in custAttribute.MetaData.DataRelations)
                {
                    builder.Append(item.ToString());
                    builderRela.Append(item.ToString());
                }
            }
            builder.Append(string.IsNullOrEmpty(condition) ? "" : " WHERE " + condition);
            builder.AppendFormat(") SELECT {0} FROM T WHERE RowIndex>{1} AND RowIndex<={2}", selectCol, pagin.PageIndex * pagin.PageSize, pagin.PageSize * (pagin.PageIndex + 1));
            DataTable table = this.GetTable(builder.ToString(), filter.GetParameterValues());
            if (table == null) return new List<T>();
            List<T> list = new List<T>();
            for (int i = 0; i < table.Rows.Count; i++)
            {
                list.Add(Reflection.ClassAction.GetModel<T>(table, i, comMatch));
            }
            DataTable recordCound = this.GetTable(string.Format("SELECT COUNT(*) FROM [{0}] {3} {2} {1}",
                tableName,
                string.IsNullOrEmpty(condition) ? "" : "WHERE " + condition,
                builderRela.ToString(),
                custAttribute == null ? "T0" : custAttribute.MetaData.Alias), filter.GetParameterValues());
            pagin.RecordCount = Convert.ToInt64(recordCound.Rows[0][0]);
            return list;

        }

        public override CustomerSqlHelper CustomerSql(string command)
        {
            CustomerSqlHelper customerSql = new CustomerSqlHelper(command, CommandType.Text, this);
            return customerSql;
        }
        public override CustomerSqlHelper StoredProcedures(string storedProcedures)
        {
            CustomerSqlHelper customerSql = new CustomerSqlHelper(storedProcedures, CommandType.StoredProcedure, this);
            return customerSql;
        }
        public override int ExecuteNonQuery(string command, CommandType ct, List<KeyValuePair<string, object>> parameter)
        {
            SqlCommand sqlcommand = new SqlCommand(command);
            foreach (var item in parameter)
            {
                sqlcommand.Parameters.AddWithValue(item.Key, item.Value);
            }
            return this.ExecCommand(sqlcommand);
        }
    }

}
