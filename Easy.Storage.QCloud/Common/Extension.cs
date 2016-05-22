using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Easy.Storage.QCloud.Common
{
    public static class Extension
    {
        public static long ToUnixTime(this DateTime nowTime)
        {
            DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1, 0, 0, 0, 0));
            return (long)Math.Round((nowTime - startTime).TotalMilliseconds, MidpointRounding.AwayFromZero);
        }
    }
}
