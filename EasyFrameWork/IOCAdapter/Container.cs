using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Easy.IOCAdapter
{
    public static class Container
    {
        static Dictionary<Type, Type> container = new Dictionary<Type, Type>();
        public static Dictionary<Type, Type> All
        {
            get { return container; }
        }
        public static void Register(Type _interface, Type Impl)
        {
            if (!container.ContainsKey(_interface))
            {
                container.Add(_interface, Impl);
            }
            else
            {
                container[_interface] = Impl;
            }
        }
    }
}
