using Easy.Models;
using Easy.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Easy.RepositoryPattern
{
    public abstract class ServiceBase<Entity> : IService where Entity : class
    {
        RepositoryBase<Entity> repBase;
        IApplicationContext applicationContext;
        public ServiceBase()
        {
            repBase = new RepositoryBase<Entity>();
            applicationContext = Easy.Loader.CreateInstance<IApplicationContext>();
        }
        public virtual Entity Get(params object[] primaryKeys)
        {
            return repBase.Get(primaryKeys);
        }
        public virtual List<Entity> Get()
        {
            return repBase.Get(new DataFilter());
        }
        public virtual List<Entity> Get(DataFilter filter)
        {
            return repBase.Get(filter);
        }
        public virtual List<Entity> Get(DataFilter filter, Pagination pagin)
        {
            return repBase.Get(filter, pagin);
        }
        public virtual void Add(Entity item)
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
        public virtual bool Update(Entity item, DataFilter filter)
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
        public virtual bool Update(Entity item, params object[] primaryKeys)
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

        public virtual void Add<T>(T item) where T : class
        {
            RepositoryBase<T> rep = new RepositoryBase<T>();
            this.Add(item);
        }

        public virtual List<T> Get<T>() where T : class
        {
            RepositoryBase<T> rep = new RepositoryBase<T>();
            return rep.Get(new DataFilter());
        }

        public virtual List<T> Get<T>(DataFilter filter) where T : class
        {
            RepositoryBase<T> rep = new RepositoryBase<T>();
            return rep.Get(filter);
        }

        public virtual List<T> Get<T>(DataFilter filter, Pagination pagin) where T : class
        {
            RepositoryBase<T> rep = new RepositoryBase<T>();
            return rep.Get(filter, pagin);
        }

        public virtual T Get<T>(params object[] primaryKeys) where T : class
        {
            RepositoryBase<T> rep = new RepositoryBase<T>();
            return rep.Get(primaryKeys);
        }

        public virtual bool Update<T>(T item, DataFilter filter) where T : class
        {
            RepositoryBase<T> rep = new RepositoryBase<T>();
            return rep.Update(item, filter);
        }

        public virtual bool Update<T>(T item, params object[] primaryKeys) where T : class
        {
            RepositoryBase<T> rep = new RepositoryBase<T>();
            return rep.Update(item, primaryKeys);
        }
    }
}
