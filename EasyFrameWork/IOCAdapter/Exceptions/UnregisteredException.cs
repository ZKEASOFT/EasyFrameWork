using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Easy.IOCAdapter.Exceptions
{
    public class UnregisteredException : Exception
    {
        public UnregisteredException(Type type)
            : base(type.FullName + "未注册")
        {

        }
    }
}
