using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Easy.Extend
{
    [DebuggerStepThrough]
    public static class ExtIEnumerable
    {
        public static void Each<T>(this IEnumerable<T> source, Action<T> fun)
        {
            foreach (T item in source)
            {
                fun(item);
            }
        }
    }
}
