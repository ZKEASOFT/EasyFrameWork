using Easy.IOCAdapter;
using Easy.IOCAdapter.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Easy
{
    public static class Loader
    {
        public static T CreateInstance<T>()
        {
            Type ty = typeof(T);
            if ((ty.IsInterface || ty.IsAbstract))
            {
                if (Container.All.ContainsKey(ty))
                {
                    return (T)System.Activator.CreateInstance(Container.All[ty]);
                }
                else
                {
                    return default(T);
                    //throw new UnregisteredException(ty);
                }
            }
            else if (!ty.IsInterface)
            {
                return System.Activator.CreateInstance<T>();
            }
            return default(T);
        }
        public static T CreateInstance<T>(string assemblyName, string fullClassName) where T : class
        {
            return System.Activator.CreateInstance(assemblyName, fullClassName).Unwrap() as T;
        }
        public static object CreateInstance(Type type)
        {
            if (Container.All.ContainsKey(type))
            {
                type = Container.All[type];
            }
            return Activator.CreateInstance(type);
        }
        public static Type GetType<T>()
        {
            Type ty = typeof(T);
            if (Container.All.ContainsKey(ty))
            {
                return Container.All[ty];
            }
            else
            {
                return ty;
            }
        }
        public static Type GetType(Type t)
        {
            if (Container.All.ContainsKey(t))
            {
                return Container.All[t];
            }
            else
            {
                return t;
            }

        }
    }
}
