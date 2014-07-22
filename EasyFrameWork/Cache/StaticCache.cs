using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Easy.Extend;

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

        internal static Dictionary<string, CacheObject> Cache;

        static StaticCache()
        {
            Cache = new Dictionary<string, CacheObject>();
            System.Threading.Thread thr = new System.Threading.Thread(new System.Threading.ThreadStart(() =>
            {
                while (true)
                {
                    System.Threading.Thread.Sleep(new TimeSpan(0, 20, 1));
                    lock (Cache)
                    {
                        List<string> needRemove = new List<string>();
                        foreach (var item in Cache)
                        {
                            if (item.Value.AutoRemove && (DateTime.Now - item.Value.LastVisit).TotalMinutes > 20)
                            {
                                needRemove.Add(item.Key);
                            }
                        }
                        foreach (var item in needRemove)
                        {
                            Cache.Remove(item);
                        }
                    }
                }
            }));
            thr.IsBackground = true;
            thr.Start();
        }


        public T Get<T>(string key, Func<Signal, T> source)
        {
            lock (Cache)
            {
                if (Cache.ContainsKey(key))
                {
                    return (T)Cache[key].Get();
                }
                else
                {
                    var signal = new Signal(key);
                    T result = source(signal);
                    Cache.Add(key, new CacheObject(result, signal.AutoRemove));
                    return result;
                }
            }
        }


        public static int Count
        {
            get { return Cache.Keys.Count; }
        }
        public void Clear()
        {
            lock (Cache)
            {
                Cache.Clear();
            }
        }
    }

    public class Signal
    {
        private static readonly Dictionary<string, List<string>> SignalRela;
        public string CacheKey { get; private set; }

        public Signal()
        {

        }

        public Signal(string cacheKey)
        {
            this.CacheKey = cacheKey;
        }

        static Signal()
        {
            SignalRela = new Dictionary<string, List<string>>();
        }
        public bool AutoRemove { get; set; }
        public void When(string signal)
        {
            lock (SignalRela)
            {
                if (SignalRela.ContainsKey(signal))
                {
                    List<string> cacheKeys = SignalRela[signal];
                    if (!cacheKeys.Contains(CacheKey))
                    {
                        cacheKeys.Add(CacheKey);
                    }
                }
            }
        }

        public void Do(string signal)
        {
            lock (SignalRela)
            {
                if (SignalRela.ContainsKey(signal))
                {
                    lock (StaticCache.Cache)
                    {
                        List<string> cacheKeys = SignalRela[signal];
                        cacheKeys.Each(m =>
                        {
                            if (StaticCache.Cache.ContainsKey(m))
                            {
                                StaticCache.Cache.Remove(m);
                            }

                        });
                    }
                }
            }
        }
    }
}
