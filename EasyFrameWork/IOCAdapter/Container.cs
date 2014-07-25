using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Easy.IOCAdapter
{
    public static class Container
    {
        private static readonly Dictionary<Type, List<Type>> Tank = new Dictionary<Type, List<Type>>();
        public static Dictionary<Type, List<Type>> All
        {
            get { return Tank; }
        }
        public static void Register(Type _interface, Type impl)
        {
            if (!Tank.ContainsKey(_interface))
            {
                Tank.Add(_interface, new List<Type> { impl });
            }
            else if (!Tank[_interface].Contains(impl))
            {
                Tank[_interface].Add(impl);
            }
        }
    }
}
