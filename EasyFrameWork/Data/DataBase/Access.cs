using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using System.Data;

namespace Easy.Data.DataBase
{
    public class Access : DataBasic
    {
        const string ACEOleDB = "Provider=Microsoft.ACE.OLEDB.12.0;";
        const string JetOleDB = "Provider=Microsoft.Jet.OLEDB.4.0;";
        public enum DdTypes
        {
            /// <summary>
            /// 2007以前版本
            /// </summary>
            JET = 1,
            /// <summary>
            /// 2007以后版本
            /// </summary>
            Ace = 2
        }
        public Access()
        {
            DataPath = AppDomain.CurrentDomain.BaseDirectory + @"App_Data\DataBase.accdb";
            this.DbType = DdTypes.JET;
        }

        public Access(string filePath)
        {
            if (filePath.Contains(":"))
            {
                DataPath = filePath;
            }
            else
            {
                DataPath = AppDomain.CurrentDomain.BaseDirectory + filePath;
            }
            this.DbType = DdTypes.JET;
        }
        public DdTypes DbType { get; set; }
        private string DataPath { get; set; }
        private OleDbConnection GetConnection()
        {
            if (this.DbType == DdTypes.JET)
            {
                OleDbConnection conn = new OleDbConnection(string.Format("{1};Data Source={0};persist security info=false;Jet OLEDB:Database Password=wayne19881112", DataPath, JetOleDB));
                return conn;
            }
            else
            {
                OleDbConnection conn = new OleDbConnection(string.Format("{1};Data Source={0};persist security info=false;Jet OLEDB:Database Password=wayne19881112", DataPath, ACEOleDB));
                return conn;
            }
        }

        private void SetParameter(OleDbCommand comm, string key, object value)
        {
            object va = null;
            if (value != null && (value.GetType() == typeof(DateTime) || value.GetType() == typeof(DateTime?)))
            {
                OleDbParameter parameter = new OleDbParameter();
                parameter.OleDbType = OleDbType.DBTimeStamp;
                parameter.Value = Convert.ToDateTime(value).ToString("yyyy-MM-dd HH:mm:ss");
                comm.Parameters.Add(parameter);
            }
            else
            {
                va = value;
                comm.Parameters.AddWithValue(key, va);
            }
        }

        private bool CheckConditions(string Condition)
        {
            if (Condition != string.Empty)
            {
                string[] strArray = new string[] { "%24", "%27", "%3a", "%3b", "%3c", ";", "‘", "–", "*", @"\", "&", ";", "{", "}", "–", "UPDATE", "DELETE", "CREATE", "ALTER", "DROP", "EXEC", "INSERT", "!", "@", "#", "$", "^", "*" };
                for (int i = 0; i < strArray.Length; i++)
                {
                    if (Condition.Contains(strArray[i]))
                        return false;
                }
            }
            return true;
        }

        private DataTable GetTable(OleDbCommand Comm)
        {
            Comm.Connection = GetConnection();
            if (Comm.Connection.State != ConnectionState.Open)
                Comm.Connection.Open();
            OleDbDataAdapter adapter = new OleDbDataAdapter(Comm);
            try
            {
                DataSet ds = new DataSet();
                adapter.Fill(ds);
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
            OleDbCommand command = new OleDbCommand(selectCommand);
            return this.GetTable(command);
        }

        private int ExecCommand(OleDbCommand Comm)
        {
            Comm.Connection = GetConnection();
            try
            {
                if (Comm.Connection.State != ConnectionState.Open)
                    Comm.Connection.Open();
                int cou = Comm.ExecuteNonQuery();
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

        public override object Insert(string tableName, Dictionary<string, object> values)
        {
            OleDbCommand Comm = new OleDbCommand();
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
            Comm.CommandText = comstr;
            foreach (var itemV in values)
            {
                SetParameter(Comm, itemV.Key, itemV.Value);
            }
            try
            {
                if (Comm.Connection.State != ConnectionState.Open)
                    Comm.Connection.Open();
                Comm.ExecuteNonQuery();
                Comm.CommandText = "SELECT @@IDENTITY";
                object cou = Comm.ExecuteScalar();
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
        public override bool UpDateTable(DataTable table, string tableName)
        {
            OleDbDataAdapter adapter = new OleDbDataAdapter(string.Format("SELECT * FROM [{0}]", tableName), GetConnection());
            OleDbCommandBuilder builder = new OleDbCommandBuilder(adapter);
            adapter.InsertCommand = builder.GetInsertCommand();
            adapter.DeleteCommand = builder.GetDeleteCommand();
            adapter.UpdateCommand = builder.GetUpdateCommand();
            int cou = adapter.Update(table);
            return cou > 0 ? true : false;
        }

        public override DataTable GetTable(string selectCommand, List<KeyValuePair<string, object>> keyValue)
        {
            OleDbCommand comm = new OleDbCommand(selectCommand);
            foreach (var item in keyValue)
            {
                SetParameter(comm, item.Key, item.Value);
            }
            return GetTable(comm);
        }

        public override int Delete(string tableName, string condition, List<KeyValuePair<string, object>> keyValue)
        {
            OleDbCommand comm;
            if (!string.IsNullOrEmpty(condition))
            {
                comm = new OleDbCommand(string.Format("DELETE FROM [{0}] WHERE {1}", tableName, condition));
            }
            else
            {
                comm = new OleDbCommand(string.Format("DELETE FROM [{0}]", tableName));
            }
            foreach (var item in keyValue)
            {
                SetParameter(comm, item.Key, item.Value);
            }
            return ExecCommand(comm);
        }

        public override bool Update(string tableName, string parameterString, List<KeyValuePair<string, object>> keyValue)
        {
            OleDbCommand comm = new OleDbCommand();
            comm.CommandText = string.Format("UPDATE [{0}] SET {1}", tableName, parameterString);
            foreach (var item in keyValue)
            {
                SetParameter(comm, item.Key, item.Value);
            }
            return this.ExecCommand(comm) > 0 ? true : false;
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
            OleDbCommand comm = new OleDbCommand(command);
            comm.CommandType = ct;
            foreach (var item in parameter)
            {
                SetParameter(comm, item.Key, item.Value);
            }
            return this.ExecCommand(comm);
        }

        public override bool IsExistTable(string tableName)
        {
            var conn = GetConnection();
            if (conn.State != ConnectionState.Open)
                conn.Open();
            var table = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, tableName });
            if (conn.State != ConnectionState.Closed)
                conn.Close();
            return table.Rows.Count != 0;
        }

        public override bool IsExistColumn(string tableName, string columnName)
        {
            var conn = GetConnection();
            if (conn.State != ConnectionState.Open)
                conn.Open();
            var table = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Columns, new object[] { null, null, tableName, columnName });
            if (conn.State != ConnectionState.Closed)
                conn.Close();
            return table.Rows.Count != 0;
        }
    }
}
