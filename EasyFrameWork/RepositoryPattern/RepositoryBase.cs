using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using Easy.Data.DataBase;
using Easy.Data;
using Easy.Extend;
using Easy.MetaData;
using Easy.Models;
using Microsoft.Practices.ServiceLocation;

namespace Easy.RepositoryPattern
{
    public class RepositoryBase<T> : IRepository<T> where T : class
    {
        private readonly DataConfigureAttribute _dataConfigure;
        private readonly Type _iEnumerableType;
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
            _dataConfigure = DataConfigureAttribute.GetAttribute<T>();
            _iEnumerableType = typeof(IEnumerable);
        }

        public virtual T Get(params object[] primaryKeys)
        {
            return GetReference(DataBase.Get<T>(primaryKeys));
        }
        public virtual IEnumerable<T> Get(DataFilter filter)
        {
            var result = DataBase.Get<T>(filter);
            result.Each(m => GetReference(m));
            return result;
        }
        public virtual IEnumerable<T> Get(DataFilter filter, Pagination pagin)
        {
            var result = DataBase.Get<T>(filter, pagin);
            result.Each(m => GetReference(m));
            return result;
        }
        public virtual void Add(T item)
        {
            if (item is EditorEntity)
            {
                EditorEntity entity = item as EditorEntity;
                if (ApplicationContext != null && ApplicationContext.CurrentUser != null)
                {
                    if (string.IsNullOrEmpty(entity.CreateBy))
                        entity.CreateBy = ApplicationContext.CurrentUser.UserID;
                    if (string.IsNullOrEmpty(entity.CreatebyName))
                        entity.CreatebyName = ApplicationContext.CurrentUser.UserName;
                    if (string.IsNullOrEmpty(entity.LastUpdateBy))
                        entity.LastUpdateBy = ApplicationContext.CurrentUser.UserID;
                    if (string.IsNullOrEmpty(entity.LastUpdateByName))
                        entity.LastUpdateByName = ApplicationContext.CurrentUser.UserName;
                }
                entity.CreateDate = DateTime.Now;
                entity.LastUpdateDate = DateTime.Now;
            }
            DataBase.Add(item);
            AddReference(item);
        }
        public virtual int Delete(params object[] primaryKeys)
        {
            var item = Get(primaryKeys);
            if (item != null)
            {
                DeleteReference(item);
            }
            return DataBase.Delete<T>(primaryKeys);
        }
        public virtual int Delete(T item)
        {
            var primaryKey = DataBase.GetPrimaryKeys(_dataConfigure);
            var filter = new DataFilter();
            foreach (PrimaryKey key in primaryKey)
            {
                filter.Where(key.ColumnName, OperatorType.Equal, Reflection.ClassAction.GetPropertyValue(item, key.PropertyName));
            }
            return Delete(filter);
        }
        public virtual int Delete(DataFilter filter)
        {
            Get(filter).Each(DeleteReference);
            return DataBase.Delete<T>(filter);
        }
        public virtual bool Update(T item, DataFilter filter)
        {
            if (item is EditorEntity)
            {
                EditorEntity entity = item as EditorEntity;
                if (ApplicationContext != null && ApplicationContext.CurrentUser != null)
                {
                    if (string.IsNullOrEmpty(entity.LastUpdateBy))
                        entity.LastUpdateBy = ApplicationContext.CurrentUser.UserID;
                    if (string.IsNullOrEmpty(entity.LastUpdateByName))
                        entity.LastUpdateByName = ApplicationContext.CurrentUser.UserName;
                }
                entity.LastUpdateDate = DateTime.Now;
            }
            UpdateReference(item);
            return DataBase.Update(item, filter);
        }
        public virtual bool Update(T item, params object[] primaryKeys)
        {
            if (item is EditorEntity)
            {
                EditorEntity entity = item as EditorEntity;
                if (ApplicationContext != null && ApplicationContext.CurrentUser != null)
                {
                    if (string.IsNullOrEmpty(entity.LastUpdateBy))
                        entity.LastUpdateBy = ApplicationContext.CurrentUser.UserID;
                    if (string.IsNullOrEmpty(entity.LastUpdateByName))
                        entity.LastUpdateByName = ApplicationContext.CurrentUser.UserName;
                }
                entity.LastUpdateDate = DateTime.Now;
            }
            UpdateReference(item);
            return DataBase.Update(item, primaryKeys);
        }
        public virtual long Count(DataFilter filter)
        {
            return DataBase.Count<T>(filter);
        }

        private void AddReference(T item)
        {
            _dataConfigure.MetaData.Properties.Each(m =>
            {
                if (_dataConfigure.MetaData.PropertyDataConfig.ContainsKey(m.Key))
                {
                    var dataInfo = _dataConfigure.MetaData.PropertyDataConfig[m.Key];
                    if (dataInfo.AddReference != null)
                    {
                        var value = m.Value.GetValue(item, null);
                        if (value is IEnumerable)
                        {
                            foreach (var valueItem in value as IEnumerable)
                            {
                                dataInfo.AddReference(item, valueItem);
                            }
                        }
                        else
                        {
                            dataInfo.AddReference(item, value);
                        }
                    }
                }
            });
        }
        private void DeleteReference(T item)
        {
            _dataConfigure.MetaData.Properties.Each(m =>
            {
                if (_dataConfigure.MetaData.PropertyDataConfig.ContainsKey(m.Key))
                {
                    var dataInfo = _dataConfigure.MetaData.PropertyDataConfig[m.Key];
                    if (dataInfo.DeleteReference != null)
                    {
                        var value = m.Value.GetValue(item, null);
                        if (value is IEnumerable)
                        {
                            foreach (var valueItem in value as IEnumerable)
                            {
                                dataInfo.DeleteReference(valueItem);
                            }
                        }
                        else
                        {
                            dataInfo.DeleteReference(value);
                        }
                    }
                }
            });
        }
        private T GetReference(T item)
        {
            _dataConfigure.MetaData.Properties.Each(m =>
            {
                if (_dataConfigure.MetaData.PropertyDataConfig.ContainsKey(m.Key))
                {
                    var dataInfo = _dataConfigure.MetaData.PropertyDataConfig[m.Key];
                    if (dataInfo.GetReference != null)
                    {
                        var value = dataInfo.GetReference(item);
                        if (_iEnumerableType.IsAssignableFrom(m.Value.PropertyType))
                        {
                            m.Value.SetValue(item, value, null);
                        }
                        else
                        {
                            foreach (var valueItem in value)
                            {
                                m.Value.SetValue(item, valueItem, null);
                                break;
                            }
                        }
                    }
                }
            });
            return item;
        }
        private void UpdateReference(T item)
        {
            _dataConfigure.MetaData.Properties.Each(m =>
            {
                if (_dataConfigure.MetaData.PropertyDataConfig.ContainsKey(m.Key))
                {
                    var dataInfo = _dataConfigure.MetaData.PropertyDataConfig[m.Key];

                    if (dataInfo.UpdateReference != null)
                    {
                        //add and delete
                        var value = m.Value.GetValue(item, null);
                        if (value is IEnumerable)
                        {
                            foreach (var valueItem in value as IEnumerable)
                            {
                                dataInfo.UpdateReference(valueItem);
                            }
                        }
                        else
                        {
                            dataInfo.UpdateReference(value);
                        }
                    }
                }
            });
        }
    }
}
