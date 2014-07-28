using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Easy.IOCAdapter
{
    public static class Container
    {
        private static readonly Dictionary<Type, ImplTypeCollection> Tank = new Dictionary<Type, ImplTypeCollection>();
        public static Dictionary<Type, ImplTypeCollection> All
        {
            get { return Tank; }
        }
        public static void Register(Type _interface, Type impl)
        {
            var implType = new ImplType(impl);
            if (!Tank.ContainsKey(_interface))
            {
                Tank.Add(_interface, new ImplTypeCollection { implType });
            }
            else if (!Tank[_interface].Contains(implType))
            {
                Tank[_interface].Add(implType);
            }
        }
        public static void Register(Type _interface, Type impl, bool oneInstance)
        {
            var implType = new ImplType(impl, oneInstance);
            if (!Tank.ContainsKey(_interface))
            {
                Tank.Add(_interface, new ImplTypeCollection { implType });
            }
            else if (!Tank[_interface].Contains(implType))
            {
                Tank[_interface].Add(implType);
            }
        }
    }
}
