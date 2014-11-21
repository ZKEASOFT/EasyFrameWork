using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Easy.Data.DataBase;
using Easy.MetaData;

namespace Easy.Data
{
    public class TableBuilderEngine
    {
        public TableBuilderEngine()
        {
            string dataBase = System.Configuration.ConfigurationManager.AppSettings[DataBasic.DataBaseAppSetingKey];
            var con = System.Configuration.ConfigurationManager.ConnectionStrings[DataBasic.ConnectionKey];
            string connString = string.Empty;
            if (con != null)
            {
                connString = con.ConnectionString;
            }
            else
            {
                connString = System.Configuration.ConfigurationManager.ConnectionStrings[0].ConnectionString;
            }
            if (dataBase == DataBasic.Ace)
            {
                DB = new Access(connString);
                (DB as Access).DbType = Access.DdTypes.Ace;
            }
            else if (dataBase == DataBasic.Jet)
            {
                DB = new Access(connString);
                (DB as Access).DbType = Access.DdTypes.JET;
            }
            else if (dataBase == DataBasic.SQL)
            {
                DB = new SQL(connString);
            }
        }
        public DataBasic DB
        {
            get;
            private set;
        }
        public Type TargeType { get; set; }
        public void Buid<T>() where T : class
        {
            DataConfigureAttribute custAttribute = DataConfigureAttribute.GetAttribute<T>();
            TargeType = Loader.GetType<T>();
            System.Reflection.PropertyInfo[] propertys = TargeType.GetProperties();
            if (custAttribute != null)
            {
                if (!DB.IsExistTable(custAttribute.MetaData.Table))
                {
                    DB.CreateTable<T>();
                }
                else
                {
                    foreach (var item in propertys)
                    {
                        TypeCode code;
                        if (item.PropertyType.Name == "Nullable`1")
                        {
                            code = Type.GetTypeCode(item.PropertyType.GetGenericArguments()[0]);
                        }
                        else
                        {
                            code = Type.GetTypeCode(item.PropertyType);
                        }

                        if (custAttribute.MetaData.PropertyDataConfig.ContainsKey(item.Name))
                        {
                            PropertyDataInfo config = custAttribute.MetaData.PropertyDataConfig[item.Name];

                            if (!config.Ignore && !config.IsRelation)
                            {
                                string columnName = string.IsNullOrEmpty(config.ColumnName)
                                    ? config.PropertyName
                                    : config.ColumnName;
                                if (!DB.IsExistColumn(custAttribute.MetaData.Table, columnName))
                                {
                                    DB.AddColumn(custAttribute.MetaData.Table, columnName, Common.ConvertToDbType(code),
                                        config.StringLength);
                                }
                                else
                                {
                                    DB.AlterColumn(custAttribute.MetaData.Table, columnName,
                                        Common.ConvertToDbType(code),
                                        config.StringLength);
                                }
                            }
                        }
                        else
                        {
                            if (!DB.IsExistColumn(custAttribute.MetaData.Table, item.Name))
                            {
                                DB.AddColumn(custAttribute.MetaData.Table, item.Name, Common.ConvertToDbType(code));
                            }
                            else
                            {
                                DB.AlterColumn(custAttribute.MetaData.Table, item.Name, Common.ConvertToDbType(code));
                            }
                        }

                    }
                }
            }
            else
            {
                if (!DB.IsExistTable(TargeType.Name))
                {
                    DB.CreateTable<T>();
                }
                else
                {
                    foreach (var item in propertys)
                    {
                        TypeCode code;
                        if (item.PropertyType.Name == "Nullable`1")
                        {
                            code = Type.GetTypeCode(item.PropertyType.GetGenericArguments()[0]);
                        }
                        else
                        {
                            code = Type.GetTypeCode(item.PropertyType);
                        }
                        if (!DB.IsExistColumn(TargeType.Name, item.Name))
                        {
                            DB.AddColumn(TargeType.Name, item.Name, Common.ConvertToDbType(code));
                        }
                        else
                        {
                            DB.AlterColumn(TargeType.Name, item.Name, Common.ConvertToDbType(code));
                        }
                    }
                }
            }
        }
    }
}
