using Easy.Models;
using Easy.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.ServiceLocation;

namespace Easy.RepositoryPattern
{
    public abstract class ServiceBase<Entity> : IService, IServiceBase<Entity> where Entity : class
    {
        public RepositoryBase<Entity> Repository { get; private set; }
        public IApplicationContext ApplicationContext { get; private set; }
        public ServiceBase()
        {
            Repository = new RepositoryBase<Entity>();
            ApplicationContext = ServiceLocator.Current.GetInstance<IApplicationContext>();
        }
        public virtual Entity Get(params object[] primaryKeys)
        {
            return Repository.Get(primaryKeys);
        }
        public virtual IEnumerable<Entity> Get()
        {
            return Repository.Get(new DataFilter());
        }
        public virtual IEnumerable<Entity> Get(DataFilter filter)
        {
            return Repository.Get(filter);
        }
        public virtual IEnumerable<Entity> Get(DataFilter filter, Pagination pagin)
        {
            return Repository.Get(filter, pagin);
        }
        public virtual IEnumerable<Entity> Get(string property, OperatorType operatorType, object value)
        {
            return Repository.Get(new DataFilter().Where(property, operatorType, value));
        }
        public virtual void Add(Entity item)
        {
            if (item is EditorEntity)
            {
                EditorEntity entity = item as EditorEntity;
                if (ApplicationContext != null && ApplicationContext.CurrentUser != null)
                {
                    if (string.IsNullOrEmpty(entity.CreateBy))
                        entity.CreateBy = ApplicationContext.CurrentUser.UserID;
                    if (string.IsNullOrEmpty(entity.CreatebyName))
                        entity.CreatebyName = ApplicationContext.CurrentUser.NickName;
                    if (string.IsNullOrEmpty(entity.LastUpdateBy))
                        entity.LastUpdateBy = ApplicationContext.CurrentUser.UserID;
                    if (string.IsNullOrEmpty(entity.LastUpdateByName))
                        entity.LastUpdateByName = ApplicationContext.CurrentUser.NickName;
                }
                entity.CreateDate = DateTime.Now;
                entity.LastUpdateDate = DateTime.Now;
            }
            Repository.Add(item);
        }
        public virtual int Delete(params object[] primaryKeys)
        {
            return Repository.Delete(primaryKeys);
        }
        public virtual int Delete(DataFilter filter)
        {
            return Repository.Delete(filter);
        }
        public virtual bool Update(Entity item, DataFilter filter)
        {
            if (item is EditorEntity)
            {
                EditorEntity entity = item as EditorEntity;
                if (ApplicationContext != null && ApplicationContext.CurrentUser != null)
                {
                    if (string.IsNullOrEmpty(entity.LastUpdateBy))
                        entity.LastUpdateBy = ApplicationContext.CurrentUser.UserID;
                    if (string.IsNullOrEmpty(entity.LastUpdateByName))
                        entity.LastUpdateByName = ApplicationContext.CurrentUser.NickName;
                }
                entity.LastUpdateDate = DateTime.Now;
            }
            return Repository.Update(item, filter);
        }
        public virtual bool Update(Entity item, params object[] primaryKeys)
        {
            if (item is EditorEntity)
            {
                EditorEntity entity = item as EditorEntity;
                if (ApplicationContext != null && ApplicationContext.CurrentUser != null)
                {
                    if (string.IsNullOrEmpty(entity.LastUpdateBy))
                        entity.LastUpdateBy = ApplicationContext.CurrentUser.UserID;
                    if (string.IsNullOrEmpty(entity.LastUpdateByName))
                        entity.LastUpdateByName = ApplicationContext.CurrentUser.NickName;
                }
                entity.LastUpdateDate = DateTime.Now;
            }
            return Repository.Update(item, primaryKeys);
        }
        public virtual long Count(DataFilter filter)
        {
            return Repository.Count(filter);
        }

        public virtual void AddGeneric<T>(T item) where T : class
        {
            RepositoryBase<T> rep = new RepositoryBase<T>();
            rep.Add(item);
        }

        public virtual IEnumerable<T> GetGeneric<T>() where T : class
        {
            RepositoryBase<T> rep = new RepositoryBase<T>();
            return rep.Get(new DataFilter());
        }

        public virtual IEnumerable<T> GetGeneric<T>(DataFilter filter) where T : class
        {
            RepositoryBase<T> rep = new RepositoryBase<T>();
            return rep.Get(filter);
        }

        public virtual IEnumerable<T> GetGeneric<T>(DataFilter filter, Pagination pagin) where T : class
        {
            RepositoryBase<T> rep = new RepositoryBase<T>();
            return rep.Get(filter, pagin);
        }

        public virtual T GetGeneric<T>(params object[] primaryKeys) where T : class
        {
            RepositoryBase<T> rep = new RepositoryBase<T>();
            return rep.Get(primaryKeys);
        }

        public virtual bool UpdateGeneric<T>(T item, DataFilter filter) where T : class
        {
            RepositoryBase<T> rep = new RepositoryBase<T>();
            return rep.Update(item, filter);
        }

        public virtual bool UpdateGeneric<T>(T item, params object[] primaryKeys) where T : class
        {
            RepositoryBase<T> rep = new RepositoryBase<T>();
            return rep.Update(item, primaryKeys);
        }



    }
}
