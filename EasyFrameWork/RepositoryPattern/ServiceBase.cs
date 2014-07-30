using Easy.Models;
using Easy.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Easy.RepositoryPattern
{
    public abstract class ServiceBase<T> : IService<T> where T : class
    {
        RepositoryBase<T> repBase;
        IApplicationContext applicationContext;
        public ServiceBase()
        {
            repBase = new RepositoryBase<T>();
            applicationContext = Easy.Loader.CreateInstance<IApplicationContext>();
        }
        public virtual T Get(params object[] primaryKeys)
        {
            return repBase.Get(primaryKeys);
        }
        public virtual List<T> Get()
        {
            return repBase.Get(new DataFilter());
        }
        public virtual List<T> Get(DataFilter filter)
        {
            return repBase.Get(filter);
        }
        public virtual List<T> Get(DataFilter filter, Pagination pagin)
        {
            return repBase.Get(filter, pagin);
        }
        public virtual void Add(T item)
        {
            if (item is EditorEntity)
            {
                EditorEntity entity = item as EditorEntity;
                if (applicationContext != null && applicationContext.CurrentUser != null)
                {
                    if (string.IsNullOrEmpty(entity.CreateBy))
                        entity.CreateBy = applicationContext.CurrentUser.UserID;
                    if (string.IsNullOrEmpty(entity.CreatebyName))
                        entity.CreatebyName = applicationContext.CurrentUser.NickName;
                    if (string.IsNullOrEmpty(entity.LastUpdateBy))
                        entity.LastUpdateBy = applicationContext.CurrentUser.UserID;
                    if (string.IsNullOrEmpty(entity.LastUpdateByName))
                        entity.LastUpdateByName = applicationContext.CurrentUser.NickName;
                }
                entity.CreateDate = DateTime.Now;
                entity.LastUpdateDate = DateTime.Now;
            }
            repBase.Add(item);
        }
        public virtual int Delete(params object[] primaryKeys)
        {
            return repBase.Delete(primaryKeys);
        }
        public virtual int Delete(DataFilter filter)
        {
            return repBase.Delete(filter);
        }
        public virtual bool Update(T item, DataFilter filter)
        {
            if (item is EditorEntity)
            {
                EditorEntity entity = item as EditorEntity;
                if (applicationContext != null && applicationContext.CurrentUser != null)
                {
                    if (string.IsNullOrEmpty(entity.LastUpdateBy))
                        entity.LastUpdateBy = applicationContext.CurrentUser.UserID;
                    if (string.IsNullOrEmpty(entity.LastUpdateByName))
                        entity.LastUpdateByName = applicationContext.CurrentUser.NickName;
                }
                entity.LastUpdateDate = DateTime.Now;
            }
            return repBase.Update(item, filter);
        }
        public virtual bool Update(T item, params object[] primaryKeys)
        {
            if (item is EditorEntity)
            {
                EditorEntity entity = item as EditorEntity;
                if (applicationContext != null && applicationContext.CurrentUser != null)
                {
                    if (string.IsNullOrEmpty(entity.LastUpdateBy))
                        entity.LastUpdateBy = applicationContext.CurrentUser.UserID;
                    if (string.IsNullOrEmpty(entity.LastUpdateByName))
                        entity.LastUpdateByName = applicationContext.CurrentUser.NickName;
                }
                entity.LastUpdateDate = DateTime.Now;
            }
            return repBase.Update(item, primaryKeys);
        }
        public virtual long Count(DataFilter filter)
        {
            return repBase.Count(filter);
        }
    }
}
