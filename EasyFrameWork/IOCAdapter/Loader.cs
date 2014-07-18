using Easy.IOCAdapter.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Easy.IOCAdapter
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
