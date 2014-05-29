using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Easy.Cache
{
    /// <summary>
    /// 静态缓存，自动移除20分钟没有访问的对象
    /// </summary>
    public class StaticCache
    {
        public class CacheObject
        {
            public bool AutoRemove { get; set; }
            public DateTime LastVisit { get; set; }
            object obj;
            public CacheObject(object obj, bool autoRemove)
            {
                this.obj = obj;
                LastVisit = DateTime.Now;
                this.AutoRemove = autoRemove;
            }
            public object Get()
            {
                LastVisit = DateTime.Now;
                return this.obj;
            }
        }

        static Dictionary<string, CacheObject> cache;

        static StaticCache()
        {
            cache = new Dictionary<string, CacheObject>();
            System.Threading.Thread thr = new System.Threading.Thread(new System.Threading.ThreadStart(() =>
            {
                while (true)
                {
                    System.Threading.Thread.Sleep(new TimeSpan(0, 20, 1));
                    lock (cache)
                    {
                        List<string> needRemove = new List<string>();
                        foreach (var item in cache)
                        {
                            if (item.Value.AutoRemove && (DateTime.Now - item.Value.LastVisit).TotalMinutes > 20)
                            {
                                needRemove.Add(item.Key);
                            }
                        }
                        foreach (var item in needRemove)
                        {
                            cache.Remove(item);
                        }
                    }
                }
            }));
            thr.IsBackground = true;
            thr.Start();
        }

        public void Add(string key, object obj)
        {
            lock (cache)
            {
                if (cache.ContainsKey(key))
                {
                    cache[key] = new CacheObject(obj, true);
                }
                else
                {
                    cache.Add(key, new CacheObject(obj, true));
                }
            }
        }
        public void Add(string key, object obj, bool autoRemove)
        {
            lock (cache)
            {
                if (cache.ContainsKey(key))
                {
                    cache[key] = new CacheObject(obj, autoRemove);
                }
                else
                {
                    cache.Add(key, new CacheObject(obj, autoRemove));
                }
            }
        }
        public object Get(string key)
        {
            lock (cache)
            {
                if (cache.ContainsKey(key))
                {
                    return cache[key].Get();
                }
                else
                {
                    return null;
                }
            }
        }

        public void Remove(string key)
        {
            lock (cache)
            {
                if (cache.ContainsKey(key))
                {
                    cache.Remove(key);
                }
            }
        }
        public List<string> GetKeys()
        {
            List<string> keys = new List<string>();
            foreach (string item in cache.Keys)
            {
                keys.Add(item);
            }
            return keys;
        }
        public static int Count
        {
            get { return cache.Keys.Count; }
        }
        public void Clear()
        {
            lock (cache)
            {
                cache.Clear();
            }
        }
    }
    /// <summary>
    /// 静态缓存，自动移除20分钟没有访问的对象
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class StaticCache<T> : StaticCache where T : class
    {
        string _key = string.Empty;
        public StaticCache()
        {
        }
        public StaticCache(string key)
        {
            this._key = key;
        }
        public void Add(T obj)
        {
            base.Add(typeof(T).FullName + _key, obj);
        }
        public void Add(T obj, bool autoRemove)
        {
            base.Add(typeof(T).FullName, obj, autoRemove);
        }
        public void Add(string key, T obj)
        {
            base.Add(typeof(T).FullName + "_" + key, obj);
        }
        public void Add(string key, T obj, bool autoRemove)
        {
            base.Add(typeof(T).FullName + "_" + key, obj, autoRemove);
        }
        public T Get()
        {
            return base.Get(typeof(T).FullName + _key) as T;
        }
        public new T Get(string key)
        {
            return base.Get(typeof(T).FullName + "_" + key) as T;
        }
        public void Remove()
        {
            base.Remove(typeof(T).FullName + _key);
        }
    }
}
